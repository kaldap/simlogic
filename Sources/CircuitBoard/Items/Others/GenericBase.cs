using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

namespace CircuitBoard.Items.Others
{
    public abstract class GenericBase : IItem
    {
        private List<Pin> mInputs = new List<Pin>();
        private List<Pin> mOutputs = new List<Pin>();
        
        protected PointF mLocation = new PointF();
        protected string mName = "[GenericBase]";
        protected SizeF mSize = new SizeF(200, 30);
        protected string mCName = "CIRCUIT";

        private float mLastIn = 60;
        private float mLastOut = 60;
        protected void AddInput(string name)
        {
            mInputs.Add(new Pin(true, this, name, new PointF(Rect.Left, mLastIn)));
            mLastIn += 30;
            mSize.Height = Math.Max(mLastIn, mLastOut);
        }
        protected void AddOutput(string name)
        {
            mOutputs.Add(new Pin(false, this, name, new PointF(Rect.Right, mLastOut)));
            mLastOut += 30;
            mSize.Height = Math.Max(mLastIn, mLastOut);
        }

        protected bool GetInput(int index)
        {
            return mInputs[index].State;
        }
        protected void SetOutput(int index, bool state)
        {
            mOutputs[index].EnqueueState(state);
        }

        #region Properties

        [Browsable(false)]
        public Pin[] Inputs
        {
            get { return mInputs.ToArray(); }
        }

        [Browsable(false)]
        public Pin[] Outputs
        {
            get { return mOutputs.ToArray(); }
        }

        [Browsable(false)]
        public bool IsGenerator
        {
            get { return false; }
        }

        [Browsable(false)]
        public bool IsDrain
        {
            get { return false; }
        }

        [Browsable(false)]
        public Pin this[bool input, int index]
        {
            get
            {
                try
                {
                    return input ? mInputs[index] : mOutputs[index];
                }
                catch (Exception)
                {
                    throw new PinDoesNotExistException(Name + " " + (input ? "I" : "O") + index);
                }
            }
        }

        [Browsable(false)]
        public RectangleF Rect
        {
            get { return new RectangleF(mLocation, mSize); }
        }

        [Browsable(false)]
        public PointF Center
        {
            get
            {
                return new PointF(mLocation.X + mSize.Width * 0.5f, mLocation.Y + mSize.Height * 0.5f);
            }
            set
            {
                mLocation = new PointF(value.X - mSize.Width * 0.5f, value.Y - mSize.Height * 0.5f);
            }
        }

        [Browsable(true)]
        [DisplayName("Pozice X")]
        public int X
        {
            get
            {
                return (int)Center.X;
            }
            set
            {
                Center = new PointF(value, Center.Y);
            }
        }

        [Browsable(true)]
        [DisplayName("Pozice Y")]
        public int Y
        {
            get
            {
                return (int)Center.Y;
            }
            set
            {
                Center = new PointF(Center.X, value);
            }
        }

        [Browsable(true)]
        [DisplayName("Jméno")]
        public virtual string Name
        {
            get
            {
                return mName;
            }
            set
            {
                if (value != null)
                    mName = value;
            }
        }

        #endregion

        public virtual void Restart() { }
        public abstract void _Update();
        public void Update()
        {
            foreach(Pin p in mOutputs)
                p.NextState();
            _Update();
        }       
        public void Disconnect()
        {
            foreach (Pin p in mInputs)
                p.DisconnectMe();
            foreach (Pin p in mOutputs)
                p.DisconnectMe();
        }
        public void CalculateTruthTable(List<IItem> justUpdated, List<Pin> recursion, Pin sourcePin)
        {
            Update();

            foreach(Pin p in mOutputs)
                p.NextState();

            if (!justUpdated.Contains(this))
            {
                justUpdated.Add(this);
            }
            else
            {
                if (recursion.Contains(sourcePin))
                    return; // Second Level of Recursion - stop!

                recursion.Add(sourcePin);
            }

            foreach (Pin p in mOutputs)
                foreach (Pin j in p.Joints)
                    j.Parent.CalculateTruthTable(justUpdated, recursion, p);
        }

        public PointF GetPinPosition(Pin p)
        {
            RectangleF r = Rect;

            if (mOutputs.Contains(p) || mInputs.Contains(p))
                return new PointF(r.Left + p.RelativePosition.X, r.Top + p.RelativePosition.Y);
            else
                throw new InvalidProgramException("Pin není součástí tohoto integrovaného obvodu!");
        }
        public Pin GetNearestPin(PointF point, float radius)
        {
            RectangleF r = Rect;

            if (Math.Abs(r.Left - point.X) >= radius && Math.Abs(r.Right - point.X) >= radius)
                return null;

            Pin nearest = null;
            double minDist = double.MaxValue;

            foreach (Pin p in mInputs)
            {
                double d = Math.Sqrt(Math.Pow(p.Position.X - point.X, 2) + Math.Pow(p.Position.Y - point.Y, 2));
                if (d >= radius)
                    continue;

                if (d > minDist)
                    continue;

                minDist = d;
                nearest = p;
            }

            if (nearest != null)
                return nearest;

            minDist = double.MaxValue;
            foreach (Pin p in mOutputs)
            {
                double d = Math.Sqrt(Math.Pow(p.Position.X - point.X, 2) + Math.Pow(p.Position.Y - point.Y, 2));
                if (d >= radius)
                    continue;

                if (d > minDist)
                    continue;

                minDist = d;
                nearest = p;
            }

            return nearest;
        }
        public bool OnClick(PointF point)
        {
            return false;
        }
        public virtual void Save(BinaryWriter writer)
        {
        }
        public virtual void Load(BinaryReader reader)
        {
        }

        public virtual void Paint(Graphics g)
        {
            RectangleF r = Rect;
            RectangleF small = Rect;
            SizeF ts;

            float y;
            float fh = 20;
            float fhh = fh * 0.5f;

            small.Inflate(-20, 0);
            g.FillRectangle(Brushes.White, small.X, small.Y, small.Width, small.Height);

            using (Font f = new Font(FontFamily.GenericMonospace, fh, FontStyle.Regular, GraphicsUnit.Pixel))
            {
                using (Pen pen = new Pen(Color.Black, 3))
                {
                    foreach (Pin p in mInputs)
                    {
                        y = p.Position.Y;
                        g.DrawLine(pen, p.Position.X, y, r.Left + 20, y);

                        y -= fhh;
                        if (p.Name.StartsWith("!"))
                        {
                            string sn = p.Name.Substring(1);
                            ts = g.MeasureString(sn, f);
                            g.DrawString(sn, f, Brushes.Black, r.Left + 25, y);
                            y -= 1;
                            g.DrawLine(Pens.Black, r.Left + 25, y, r.Left + 25 + ts.Width, y);
                        }
                        else
                            g.DrawString(p.Name, f, Brushes.Black, r.Left + 25, y);
                    }

                    foreach (Pin p in mOutputs)
                    {
                        
                        y = p.Position.Y;
                        g.DrawLine(pen, p.Position.X, y, r.Right - 20, y);
                        
                        y -= fhh;
                        if (p.Name.StartsWith("!"))
                        {
                            string sn = p.Name.Substring(1);
                            ts = g.MeasureString(sn, f);
                            g.DrawString(sn, f, Brushes.Black, r.Right - 25 - ts.Width, y);
                            y -= 1;
                            g.DrawLine(Pens.Black, r.Right - 25 - ts.Width, y, r.Right - 25, y);
                        }
                        else
                        {
                            ts = g.MeasureString(p.Name, f);
                            g.DrawString(p.Name, f, Brushes.Black, r.Right - 25 - ts.Width, y);
                        }
                    }
                }

                ts = g.MeasureString(mCName, f);

                g.DrawString(mCName, f, Brushes.Black, small.Left + ((small.Width - ts.Width) * 0.5f), small.Top + 5);
                g.DrawRectangle(Pens.Black, small.X, small.Y, small.Width, small.Height);
            }
        }

        protected GenericBase()
        { }
    }
}

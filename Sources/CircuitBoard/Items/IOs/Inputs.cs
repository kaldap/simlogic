using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.IO;

namespace CircuitBoard.Items.IOs
{
    public abstract class SignalSource : IItem
    {
        protected SizeF mSize = new SizeF(60, 30);
        protected Pin mPin = null;
        protected PointF mLocation = new PointF();
        protected string mName = "[Signal Source]";

        [Browsable(false)]
        public Pin[] Inputs
        {
            get { return new Pin[0]; }
        }
        
        [Browsable(false)]
        public Pin[] Outputs
        {
            get { return new Pin[] { mPin }; }
        }

        [Browsable(false)]
        public bool IsGenerator
        {
            get { return true; }
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
                if (!input && index == 0)
                    return mPin;
                throw new PinDoesNotExistException((input ? "I" : "O") + index);
            }
        }

        public void Disconnect()
        {
            mPin.DisconnectMe();
        }
        public abstract void Update();
        public void CalculateTruthTable(List<IItem> justUpdated, List<Pin> recursion, Pin sourcePin)
        {
            Update();
            foreach (Pin j in mPin.Joints)
                j.Parent.CalculateTruthTable(justUpdated, recursion, mPin);
        }


        [Browsable(false)]
        public abstract RectangleF Rect { get; }

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

        public abstract void Paint(Graphics g);

        public PointF GetPinPosition(Pin p)
        {
            RectangleF r = Rect;

            if (p == mPin)
                return new PointF(r.Left + p.RelativePosition.X, r.Top + p.RelativePosition.Y);
            else
                throw new InvalidProgramException("Pin není součástí tohoto zdroje signálu!");
        }
        public Pin GetNearestPin(PointF point, float radius)
        {
            RectangleF r = Rect;
            if (Math.Abs(mPin.Position.X - point.X) < radius && Math.Abs(mPin.Position.Y - point.Y) < radius)
                return mPin;
            return null;
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
        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }
        public abstract bool OnClick(PointF point);

        public void Save(BinaryWriter writer)
        {
        }
        public void Load(BinaryReader reader)
        {
        }
        public void Restart()
        { }
    }

    public class SwitchSource : SignalSource
    {
        private bool mState = false;

        [Browsable(false)]
        public bool State
        {
            set
            {
                mState = value;
            }
        }

        public SwitchSource()
        {
            mSize.Width += 20;
            mPin = new Pin(false, this, "Signál", new PointF(mSize.Width, mSize.Height * 0.5f));
            mName = "Spínač";
        }

        public override void Update()
        {
            mPin.State = mState;
        }

        public override RectangleF Rect
        {
            get { return new RectangleF(mLocation, mSize); }
        }

        public override void Paint(Graphics g)
        {
            RectangleF r = Rect;
            RectangleF l = new RectangleF(r.Location, new SizeF(2.0f * r.Width / 3.0f, r.Height));
            RectangleF s = new RectangleF(r.Location, new SizeF(r.Width / 3.0f, r.Height));
            RectangleF n = new RectangleF(new PointF(r.Location.X + s.Width, r.Location.Y), new SizeF(r.Width / 3.0f, r.Height));

            g.FillRectangle(mState ? Brushes.Yellow : Brushes.Aquamarine, l);
            g.FillRectangle(Brushes.Black, l.Right - 1, (l.Top + l.Height * 0.5f) - 1.5f, n.Width, 3);

            using (Pen p = new Pen(Color.Black, 2))
            {
                g.DrawRectangle(p, l.Left, l.Top, l.Width, l.Height);

                using (Font f = new Font(FontFamily.GenericSansSerif, s.Height - 8, FontStyle.Bold, GraphicsUnit.Pixel))
                {
                    string text = mState ? "1" : "0";
                    SizeF fs = g.MeasureString(text, f);
                    RectangleF target = new RectangleF(n.Left + 0.5f * (s.Width - fs.Width), n.Top + 0.5f * (n.Height - fs.Height), n.Width, n.Height);
                    g.DrawString(text, f, Brushes.Black, target);
                }

                s.Inflate(-4, -4);
                if (mState)
                    g.FillEllipse(Brushes.Black, s);
                g.DrawEllipse(p, s.Left, s.Top, s.Width, s.Height);
            }
        }
        public override bool OnClick(PointF point)
        {
            RectangleF r = Rect;
            r = new RectangleF(r.Left + 8, r.Top + 8, r.Width * 0.5f - 16.0f, r.Height - 16);
            if (!r.Contains(point))
                return false;

            mState = !mState;
            return true;
        }
    }
    public class PulseSource : SignalSource
    {
        private int mCount = 2;
        private bool mState = false;

        public PulseSource()
        {
            mSize.Width += 20;
            mPin = new Pin(false, this, "Signál", new PointF(mSize.Width, mSize.Height * 0.5f));
            mName = "Tlačítko";
        }

        public override void Update()
        {
            if (mCount >= 0)
            {
                mPin.State = true;
                mState = true;
                mCount--;
            }
            else
            {
                mPin.State = false;
                mState = false;
                mPin.EnqueueState(false);
            }
        }

        public override RectangleF Rect
        {
            get { return new RectangleF(mLocation, mSize); }
        }

        public override void Paint(Graphics g)
        {
            RectangleF r = Rect;
            RectangleF l = new RectangleF(r.Location, new SizeF(2.0f * r.Width / 3.0f, r.Height));
            RectangleF s = new RectangleF(r.Location, new SizeF(r.Width / 3.0f, r.Height));
            RectangleF n = new RectangleF(new PointF(r.Location.X + s.Width, r.Location.Y), new SizeF(r.Width / 3.0f, r.Height));

            g.FillRectangle(mState ? Brushes.Goldenrod : Brushes.LightGray, l);
            g.FillRectangle(Brushes.Black, l.Right - 1, (l.Top + l.Height * 0.5f) - 1.5f, n.Width, 3);

            using (Pen p = new Pen(Color.Black, 2))
            {
                g.DrawRectangle(p, l.Left, l.Top, l.Width, l.Height);

                using (Font f = new Font(FontFamily.GenericSansSerif, s.Height - 8, FontStyle.Bold, GraphicsUnit.Pixel))
                {
                    string text = mState ? "1" : "0";
                    SizeF fs = g.MeasureString(text, f);
                    RectangleF target = new RectangleF(n.Left + 0.5f * (s.Width - fs.Width), n.Top + 0.5f * (n.Height - fs.Height), n.Width, n.Height);
                    g.DrawString(text, f, Brushes.Black, target);
                }

                s.Inflate(-4, -4);
                if (mState)
                    g.FillEllipse(Brushes.Black, s);
                g.DrawEllipse(p, s.Left, s.Top, s.Width, s.Height);
            }
        }
        public override bool OnClick(PointF point)
        {
            RectangleF r = Rect;
            r = new RectangleF(r.Left + 8, r.Top + 8, r.Width * 0.5f - 16.0f, r.Height - 16);
            if (!r.Contains(point))
                return false;

            mCount = 2;

            return true;
        }
    }
    public class GeneratorSource : SignalSource
    {
        private Queue<bool> mStates = new Queue<bool>();
        private int mHighLength = 2;
        private int mLowLength = 2;
        private bool mState = false;


        [Browsable(true)]
        [DisplayName("Doba úrovně HIGH")]
        public int HighLength
        {
            get
            {
                return mHighLength;
            }
            set
            {
                mHighLength = value;
                RegenerateStates();
            }
        }


        [Browsable(true)]
        [DisplayName("Doba úrovně LOW")]
        public int LowLength
        {
            get
            {
                return mLowLength;
            }
            set
            {
                mLowLength = value;
                RegenerateStates();
            }
        }

        public GeneratorSource()
        {
            mPin = new Pin(false, this, "Signál", new PointF(mSize.Width, mSize.Height * 0.5f));
            mName = "Generátor";
            RegenerateStates();
        }

        public override void Update()
        {
            mState = mStates.Dequeue();
            mStates.Enqueue(mState);
            mPin.State = mState;
        }

        public override RectangleF Rect
        {
            get { return new RectangleF(mLocation, mSize); }
        }

        public override void Paint(Graphics g)
        {
            RectangleF r = Rect;
            RectangleF s = new RectangleF(r.Location, new SizeF(r.Width / 2.0f, r.Height));

            g.FillEllipse(mState ? Brushes.Yellow : Brushes.Aquamarine, s);
            g.FillRectangle(Brushes.Black, s.Right - 1, (s.Top + s.Height * 0.5f) - 1.5f, s.Width, 3);

            using (Pen p = new Pen(Color.Black, 2))
                g.DrawEllipse(p, s.Left, s.Top, s.Width, s.Height);

            using (Font f = new Font(FontFamily.GenericSansSerif, s.Height - 8, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                string text = mState ? "1" : "0";
                SizeF fs = g.MeasureString(text, f);
                RectangleF target = new RectangleF(s.Left + 0.5f * (s.Width - fs.Width), s.Top + 0.5f * (s.Height - fs.Height), s.Width, s.Height);
                g.DrawString(text, f, Brushes.Black, target);
            }
        }
        public override bool OnClick(PointF point)
        {
            return false;
        }

        private void RegenerateStates()
        {
            mStates.Clear();

            for (int i = 0; i < mLowLength; i++)
                mStates.Enqueue(false);
            for (int i = 0; i < mHighLength; i++)
                mStates.Enqueue(true);
            
            mStates.TrimExcess();
        }
    }

    public class HighSource : SignalSource
    {
        public HighSource()
        {
            mPin = new Pin(false, this, "Signál", new PointF(mSize.Width, mSize.Height * 0.5f));
            mName = "Spínač";
        }
        public override void Update()
        {
            mPin.State = true;
        }

        public override RectangleF Rect
        {
            get { return new RectangleF(mLocation, mSize); }
        }

        public override void Paint(Graphics g)
        {
            RectangleF r = Rect;
            RectangleF s = new RectangleF(r.Location, new SizeF(r.Width / 2.0f, r.Height));

            g.FillEllipse(Brushes.Yellow, s);
            g.FillRectangle(Brushes.Black, s.Right - 1, (s.Top + s.Height * 0.5f) - 1.5f, s.Width, 3);

            using (Pen p = new Pen(Color.Black, 2))
                g.DrawEllipse(p, s.Left, s.Top, s.Width, s.Height);

            using (Font f = new Font(FontFamily.GenericSansSerif, s.Height - 8, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                string text = "+";
                SizeF fs = g.MeasureString(text, f);
                RectangleF target = new RectangleF(s.Left + 0.5f * (s.Width - fs.Width), s.Top + 0.5f * (s.Height - fs.Height), s.Width, s.Height);
                g.DrawString(text, f, Brushes.Black, target);
            }
        }
        public override bool OnClick(PointF point)
        {
            return false;
        }
    }
    public class LowSource : SignalSource
    {
        public LowSource()
        {
            mPin = new Pin(false, this, "Signál", new PointF(mSize.Width, mSize.Height * 0.5f));
            mName = "Spínač";
        }
        public override void Update()
        {
            mPin.State = false;
        }

        public override RectangleF Rect
        {
            get { return new RectangleF(mLocation, mSize); }
        }

        public override void Paint(Graphics g)
        {
            RectangleF r = Rect;
            RectangleF s = new RectangleF(r.Location, new SizeF(r.Width / 2.0f, r.Height));

            g.FillEllipse(Brushes.Aquamarine, s);
            g.FillRectangle(Brushes.Black, s.Right - 1, (s.Top + s.Height * 0.5f) - 1.5f, s.Width, 3);

            using (Pen p = new Pen(Color.Black, 2))
                g.DrawEllipse(p, s.Left, s.Top, s.Width, s.Height);

            using (Font f = new Font(FontFamily.GenericSansSerif, s.Height - 8, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                string text = "-";
                SizeF fs = g.MeasureString(text, f);
                RectangleF target = new RectangleF(s.Left + 0.5f * (s.Width - fs.Width), s.Top + 0.5f * (s.Height - fs.Height), s.Width, s.Height);
                g.DrawString(text, f, Brushes.Black, target);
            }
        }
        public override bool OnClick(PointF point)
        {
            return false;
        }
    }

}

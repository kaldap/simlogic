using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.IO;

namespace CircuitBoard.Items.IOs
{
    public class SignalDrain : IItem
    {
        protected SizeF mSize = new SizeF(60, 30);
        protected Pin mPin = null;
        protected PointF mLocation = new PointF();
        protected string mName = "[Signal Drain]";

        [Browsable(false)]
        public Pin[] Inputs
        {
            get { return new Pin[] { mPin }; }
        }
        
        [Browsable(false)]
        public Pin[] Outputs
        {
            get { return new Pin[0]; }
        }

        [Browsable(false)]
        public bool IsGenerator
        {
            get { return false; }
        }

        [Browsable(false)]
        public bool IsDrain
        {
            get { return true; }
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
        public void Update()
        {
        }
        public void CalculateTruthTable(List<IItem> justUpdated, List<Pin> recursion, Pin sourcePin)
        {
            // Target reached
            justUpdated.Add(this);
        }

        [Browsable(false)]
        public RectangleF Rect
        {
            get
            {
                return new RectangleF(mLocation, mSize);
            }
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

        public void Paint(Graphics g)
        {
            RectangleF r = Rect;
            RectangleF s = new RectangleF(new PointF(mLocation.X + r.Width / 2.0f, mLocation.Y), new SizeF(r.Width / 2.0f, r.Height));

            g.FillRectangle(Brushes.Black, r.Left, (s.Top + s.Height * 0.5f) - 1.5f, s.Width, 3);
            g.FillEllipse(mPin.State ? Brushes.Yellow : Brushes.Aquamarine, s);

            using (Pen p = new Pen(Color.Black, 2))
                g.DrawEllipse(p, s.Left, s.Top, s.Width, s.Height);

            using (Font f = new Font(FontFamily.GenericSansSerif, s.Height - 8, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                string text = mPin.State ? "1" : "0";
                SizeF fs = g.MeasureString(text, f);
                RectangleF target = new RectangleF(s.Left + 0.5f * (s.Width - fs.Width), s.Top + 0.5f * (s.Height - fs.Height), s.Width, s.Height);
                g.DrawString(text, f, Brushes.Black, target);
            }
        }

        public PointF GetPinPosition(Pin p)
        {
            RectangleF r = Rect;

            if (p == mPin)
                return new PointF(r.Left + p.RelativePosition.X, r.Top + p.RelativePosition.Y);
            else
                throw new InvalidProgramException("Pin není součástí této sondy!");
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
        public bool OnClick(PointF point)
        {
            return false;
        }

        public SignalDrain()
        {
            mPin = new Pin(true, this, "Signál", new PointF(0, mSize.Height * 0.5f));
            mName = "Sonda";
        }

        public void Save(BinaryWriter writer)
        {
        }
        public void Load(BinaryReader reader)
        {
        }
        public void Restart()
        { }
    }
}


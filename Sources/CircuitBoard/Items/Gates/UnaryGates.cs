using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.IO;

namespace CircuitBoard.Items.Gates
{
    public abstract class UnaryGate : IItem
    {
        protected Pin mInput = null;
        protected Pin mOutput = null;
        protected PointF mLocation = new PointF();
        protected string mName = "[UnaryGate]";
        protected SizeF mSize = GateRenderer.GetSize(1);

        public abstract void _Update();
        public void Update()
        {
            mOutput.NextState();
            _Update();
        }
        public void CalculateTruthTable(List<IItem> justUpdated, List<Pin> recursion, Pin sourcePin)
        {
            bool outState = mOutput.State;
            Update();
            mOutput.NextState();

            if (!justUpdated.Contains(this))
            {
                justUpdated.Add(this);
            }
            else
            {
                if (outState == mOutput.State)
                    return; // Flip Flop - It is recursive but holds it output

                if (recursion.Contains(sourcePin))
                    return; // Second Level of Recursion - stop!

                recursion.Add(sourcePin);
            }

            foreach (Pin j in mOutput.Joints)
                j.Parent.CalculateTruthTable(justUpdated, recursion, mOutput);
        }

        [Browsable(false)]
        public RectangleF Rect
        {
            get
            {
                return new RectangleF(mLocation, GateRenderer.GetSize(1));
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
                if (mName != null)
                    mName = value;
            }
        }

        [Browsable(false)]
        protected abstract bool IsPositive { get; }

        public void Paint(Graphics g)
        {
            GateRenderer.Draw(g, Rect, mOutput, Inputs, BaseGateShape.Not, !IsPositive);
        }
        public PointF GetPinPosition(Pin p)
        {
            if(p != mInput && p != mOutput)
                throw new InvalidProgramException("Pin není součástí tohoto hradla!");

            RectangleF r = Rect;
            return new PointF(r.Left + p.RelativePosition.X, r.Top + p.RelativePosition.Y);
        }
        public Pin GetNearestPin(PointF point, float radius)
        {
            RectangleF r = Rect;
            float halfY = r.Top + r.Height * 0.5f;

            if (Math.Abs(halfY - point.Y) >= radius)
                return null;

            if (Math.Abs(r.Left - point.X) < radius)
                return mInput;

            if (Math.Abs(r.Right - point.X) < radius)
                return mOutput;

            return null;
        }

        protected UnaryGate()
        {
            mInput = new Pin(true, this, "Vstup", GateRenderer.GetPinPosition(1, 1));
            mOutput = new Pin(false, this, "Výstup", GateRenderer.GetPinPosition(1, -1));
        }

        [Browsable(false)]
        public Pin[] Inputs
        {
            get { return new Pin[] { mInput }; }
        }

        [Browsable(false)]
        public Pin[] Outputs
        {
            get { return new Pin[] { mOutput }; }
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
                    Pin[] a = input ? Inputs : Outputs; return a[index];
                }
                catch (Exception)
                {
                    throw new PinDoesNotExistException(Name + " " + (input ? "I" : "O") + index);
                }
            }
        }

        public void Disconnect()
        {
            mInput.DisconnectMe();
            mOutput.DisconnectMe();
        }

        public bool OnClick(PointF point)
        {
            return false;
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

    public class NotGate : UnaryGate
    {
        protected override bool IsPositive
        {
            get { return false; }
        }
        public override void _Update()
        {
            mOutput.EnqueueState(!mInput.State);
        }

        public NotGate()
        {
            mName = "Invertor";
        }
    }
    public class BufferGate : UnaryGate
    {
        protected override bool IsPositive
        {
            get { return true; }
        }
        public override void _Update()
        {
            mOutput.EnqueueState(mInput.State);
        }

        public BufferGate()
        {
            mName = "Buffer";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.IO;

namespace CircuitBoard.Items.Gates
{
    public abstract class BinaryGate : IItem
    {
        protected List<Pin> mInputs = new List<Pin>(2);
        protected Pin mOutput = null;
        protected PointF mLocation = new PointF();
        protected string mName = "[BinaryGate]";
        protected SizeF mSize;

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

        [Browsable(true)]
        [DisplayName("Počet vstupů")]
        public uint InputCount
        {
            get
            {
                return (uint)mInputs.Count;
            }
            set
            {
                if (value < 2)
                    throw new ArgumentOutOfRangeException("InputCount", value, "Počet pinů musí být větší nebo roven 2!");

                if (value > 25)
                    throw new ArgumentOutOfRangeException("InputCount", value, "Počet pinů nesmí přesahovat 25!");

                if (mInputs.Count > value)
                {
                    foreach(Pin p in mInputs)
                        p.DisconnectMe();

                    mInputs.Clear();
                }

                mSize = GateRenderer.GetSize((int)value);

                mOutput.RelativePosition = GateRenderer.GetPinPosition((int)value, -1);

                for (int i = 0; i < mInputs.Count; i++)
                    mInputs[i].RelativePosition = GateRenderer.GetPinPosition((int)value, i);

                for (int i = mInputs.Count; i < value; i++)
                    mInputs.Add(new Pin(true, this, "Vstup " + ((char)(65 + i)), GateRenderer.GetPinPosition((int)value, i)));
            }
        }

        public abstract void Paint(Graphics g);

        public PointF GetPinPosition(Pin p)
        {
            RectangleF r = Rect;

            if (p == mOutput || mInputs.Contains(p))
                return new PointF(r.Left + p.RelativePosition.X, r.Top + p.RelativePosition.Y);      
            else
                throw new InvalidProgramException("Pin není součástí tohoto hradla!");
        }
        public Pin GetNearestPin(PointF point, float radius)
        {
            RectangleF r = Rect;
            float pinOut = r.Top + mOutput.RelativePosition.Y;

            if (Math.Abs(r.Right - point.X) < radius && Math.Abs(pinOut - point.Y) < radius)
                return mOutput;

            if (Math.Abs(r.Left - point.X) >= radius)
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

            return nearest;
        }

        protected BinaryGate()
        {
            mOutput = new Pin(false, this, "Výstup", new PointF());
            InputCount = 2;
        }

        [Browsable(false)]
        public Pin[] Inputs
        {
            get { return mInputs.ToArray(); }
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
           mOutput.DisconnectMe();
           foreach (Pin p in mInputs)
               p.DisconnectMe();
        }

        public bool OnClick(PointF point)
        {
            return false;
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(mInputs.Count);
        }
        public void Load(BinaryReader reader)
        {
            InputCount = (uint)reader.ReadInt32();
        }
        public void Restart()
        { }
    }

    public class AndGate : BinaryGate
    {
        public override void _Update()
        {
            foreach (Pin p in mInputs)
            {
                if (p.State)
                    continue;

                mOutput.EnqueueState(false);
                return;
            }

            mOutput.EnqueueState(true);
        }
        public override void Paint(Graphics g)
        {
            GateRenderer.Draw(g, Rect, mOutput, Inputs, BaseGateShape.And, false);
        }

        public AndGate()
        {
            mName = "AND Hradlo";
        }
    }
    public class OrGate : BinaryGate
    {
        public override void _Update()
        {
            foreach (Pin p in mInputs)
            {
                if (!p.State)
                    continue;

                mOutput.EnqueueState(true);
                return;
            }

            mOutput.EnqueueState(false);
        }
        public override void Paint(Graphics g)
        {
            GateRenderer.Draw(g, Rect, mOutput, Inputs, BaseGateShape.Or, false);
        }

        public OrGate()
        {
            mName = "OR Hradlo";
        }
    }

    public class NAndGate : BinaryGate
    {
        public override void _Update()
        {
            foreach (Pin p in mInputs)
            {
                if (p.State)
                    continue;

                mOutput.EnqueueState(true);
                return;
            }

            mOutput.EnqueueState(false);
        }
        public override void Paint(Graphics g)
        {
            GateRenderer.Draw(g, Rect, mOutput, Inputs, BaseGateShape.And, true);
        }

        public NAndGate()
        {
            mName = "NAND Hradlo";
        }
    }
    public class NOrGate : BinaryGate
    {
        public override void _Update()
        {
            foreach (Pin p in mInputs)
            {
                if (!p.State)
                    continue;

                mOutput.EnqueueState(false);
                return;
            }

            mOutput.EnqueueState(true);
        }
        public override void Paint(Graphics g)
        {
            GateRenderer.Draw(g, Rect, mOutput, Inputs, BaseGateShape.Or, true);
        }

        public NOrGate()
        {
            mName = "NOR Hradlo";
        }
    }

    public class XOrGate : BinaryGate
    {
        public override void _Update()
        {
            int onStates = 0;
            foreach (Pin p in mInputs)
                if (p.State)
                    onStates++;

            mOutput.EnqueueState((onStates % 2) != 0);
        }
        public override void Paint(Graphics g)
        {
            GateRenderer.Draw(g, Rect, mOutput, Inputs, BaseGateShape.Xor, false);
        }

        public XOrGate()
        {
            mName = "XOR Hradlo";
        }
    }
    public class XNOrGate : BinaryGate
    {
        public override void _Update()
        {
            int onStates = 0;
            foreach (Pin p in mInputs)
                if (p.State)
                    onStates++;

            mOutput.EnqueueState((onStates % 2) == 0);
        }
        public override void Paint(Graphics g)
        {
            GateRenderer.Draw(g, Rect, mOutput, Inputs, BaseGateShape.Xor, true);
        }

        public XNOrGate()
        {
            mName = "XNOR Hradlo";
        }
    }
}

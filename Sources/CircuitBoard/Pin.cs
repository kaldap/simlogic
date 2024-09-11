using System;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CircuitBoard
{
    public delegate void PinChanged(Pin pin, bool state);

    public class Pin
    {
        private static Random sRandom = new Random();

        private Queue<bool> mStates = new Queue<bool>();
        private bool mIsInput = true;
        private bool mState  = false;
        private List<Pin> mJoints = new List<Pin>();
        private IItem mParent = null;
        private string mName = "Pin";
        private float mWireBreak = 0.5f;
        private PointF mPosition = new PointF();

        public PointF RelativePosition
        {
            get
            {
                return mPosition;
            }
            internal set
            {
                mPosition = value;
            }
        }
        public float WireBreak
        {
            get
            {
                return mWireBreak;
            }
        }
        public bool State
        {
            get { return mState; } 
            set
            {
                _VoltageChanged(value);
            } 
        }
        public bool IsInput
        {
            get
            {
                return mIsInput;
            }
        }
        public PointF Position
        {
            get
            {
                return mParent.GetPinPosition(this);
            }
        }
        public IItem Parent
        {
            get
            {
                return mParent;
            }
        }
        public Pin[] Joints
        {
            get
            {
                return mJoints.ToArray();
            }
        }
        public string Name
        {
            get
            {
                return mName;
            }
        }

        public Pin(bool inputPin, IItem parent, string name, PointF position)
        {
            mWireBreak = (float)sRandom.NextDouble() * 0.6f + 0.2f;
            mPosition = position;
            mIsInput = inputPin;
            mParent = parent;
            mName = name;
        }
        public void Join(Pin other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            if (!mIsInput && !other.mIsInput)
                throw new InvalidOperationException("Nelze spojit dva výstupní piny!\nMohlo by dojít ke zkratu!");

            if(mIsInput && other.mIsInput)
                throw new InvalidOperationException("Nelze spojovat vstupní piny!!!");

            if (mIsInput)
            {
                other.Join(this);
                return;
            }

            if (other.mJoints.Count > 0)
                throw new InvalidOperationException("Na vstupní pin smí být připojen maxmálně jeden vodič!");

            if (!mJoints.Contains(other))
            {
                mJoints.Add(other);               
                other.mJoints.Add(this);
            }
        }
        public void Split(Pin other)
        {
            mJoints.Remove(other);
            other.mJoints.Remove(this);
        }
        public void DisconnectMe()
        {
            Pin[] pins = Joints;
            foreach (Pin j in pins)
                Split(j);
        }

        public void Reset()
        {
            State = false;
            mStates.Clear();
        }
        public void EnqueueState(bool state)
        {
            mStates.Enqueue(state);
        }
        public void NextState()
        {
            if (mStates.Count > 0)
                State = mStates.Dequeue();
        }
        public bool ContainJoint(Pin p)
        {
            return mJoints.Contains(p);
        }

        private void _VoltageChanged(bool newValue)
        {
            mState = newValue;

            if (mIsInput)
                return;

            foreach (Pin p in mJoints)
                p._VoltageChanged(newValue);
        }
        public override string ToString()
        {
            return Parent.Name + "-" + mName;
        }
    }
}

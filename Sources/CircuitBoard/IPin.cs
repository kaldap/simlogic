using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitBoard
{
    public delegate void PinChanged(Pin pin, bool state);

    public class Pin
    {
        public event PinChanged Changed;

        private bool mState  = false;
        public bool State
        {
            get { return mState; } 
            set
            {
                mState = value;
                if (Changed != null)
                    Changed(this, mState);
            } 
        }

        public Pin()
        { }
    }
}

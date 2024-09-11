using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitBoard.Items.Others
{
    #region RS

    public class RSLatch : GenericBase
    {
        private bool mQn = false;
        private bool mQi = false;

        public RSLatch()
        {
            mName = mCName = "RS";

            AddOutput("Q");
            AddOutput("!Q");

            AddInput("S");
            AddInput("R");
        }
        public override void _Update()
        {
            bool s = GetInput(0);
            bool r = GetInput(1);

            if (s && r)
            {
                mQn = true;
                mQi = true;
            }
            else if (s)
            {
                mQn = true;
                mQi = false;

            }
            else if (r)
            {
                mQn = false;
                mQi = true;
            }
            else
            { } // Keep state

            SetOutput(0, mQn);
            SetOutput(1, mQi);
        }
        public override void Restart()
        {
            mQn = false;
            mQi = false;
        }
    }
    public class RSClockLatch : GenericBase
    {
        private bool mQn = false;
        private bool mQi = false;

        public RSClockLatch()
        {
            mName = mCName = "RS";

            AddOutput("Q");
            AddOutput("!Q");

            AddInput("S");
            AddInput("R");
            AddInput("CLK");
        }
        public override void _Update()
        {
            bool s = GetInput(0);
            bool r = GetInput(1);

            if (GetInput(2))
            {
                if (s && r)
                {
                    mQn = true;
                    mQi = true;
                }
                else if (s)
                {
                    mQn = true;
                    mQi = false;

                }
                else if (r)
                {
                    mQn = false;
                    mQi = true;
                }
                else
                { } // Keep state
            }

            SetOutput(0, mQn);
            SetOutput(1, mQi);
        }
        public override void Restart()
        {
            mQn = false;
            mQi = false;
        }
    }
    public class RSFlipFlop : GenericBase
    {
        private bool mQn = false;
        private bool mQi = false;
        private bool mClk = false;

        public RSFlipFlop()
        {
            mName = mCName = "RS";

            AddOutput("Q");
            AddOutput("!Q");

            AddInput("S");
            AddInput("R");
            AddInput("CLK");
        }
        public override void _Update()
        {
            bool s = GetInput(0);
            bool r = GetInput(1);
            bool clk = GetInput(2);

            if (!clk && mClk) // Falling edge
            {
                if (s == r) // Keep state
                {
                }
                else if (s)
                {
                    mQn = true;
                    mQi = false;

                }
                else if (r)
                {
                    mQn = false;
                    mQi = true;
                }
            }
            
            mClk = clk;

            SetOutput(0, mQn);
            SetOutput(1, mQi);
        }
        public override void Restart()
        {
            mQn = false;
            mQi = false;
            mClk = false;
        }
    }

    #endregion

    #region JK

    public class JKFlipFlop : GenericBase
    {
        private bool mQn = false;
        private bool mQi = false;
        private bool mClk = false;

        public JKFlipFlop()
        {
            mName = mCName = "JK";

            AddOutput("Q");
            AddOutput("!Q");

            AddInput("J");
            AddInput("K");
            AddInput("CLK");
        }
        public override void _Update()
        {
            bool s = GetInput(0);
            bool r = GetInput(1);
            bool clk = GetInput(2);

            if (!clk && mClk) // Falling edge
            {
                if (s && r) // Keep state
                {
                    mQi = !mQi;
                    mQn = !mQn;
                }
                else if (s)
                {
                    mQn = true;
                    mQi = false;

                }
                else if (r)
                {
                    mQn = false;
                    mQi = true;
                }
            }

            mClk = clk;

            SetOutput(0, mQn);
            SetOutput(1, mQi);
        }
        public override void Restart()
        {
            mQn = false;
            mQi = false;
            mClk = false;
        }
    }
    public class JKSCFlipFlop : GenericBase
    {
        private bool mQn = false;
        private bool mQi = false;
        private bool mClk = false;

        public JKSCFlipFlop()
        {
            mName = mCName = "JK";

            AddOutput("Q");
            AddOutput("!Q");

            AddInput("J");
            AddInput("K");
            AddInput("CLK");

            AddInput("!SET");
            AddInput("!CLR");
        }
        public override void _Update()
        {
            bool s = GetInput(0);
            bool r = GetInput(1);
            bool clk = GetInput(2);

            bool set = GetInput(3);
            bool clr = GetInput(4);

            if (set && clr)
            {
                if (!clk && mClk) // Falling edge
                {
                    if (s && r) // Keep state
                    {
                        mQi = !mQi;
                        mQn = !mQn;
                    }
                    else if (s)
                    {
                        mQn = true;
                        mQi = false;

                    }
                    else if (r)
                    {
                        mQn = false;
                        mQi = true;
                    }
                }
            }
            else
            {
                if (!set && !clr)
                {
                    mQn = mQi = true;
                }
                else if (!set)
                {
                    mQn = true;
                    mQi = false;
                }
                else
                {
                    mQn = false;
                    mQi = true;
                }
            }

            mClk = clk;

            SetOutput(0, mQn);
            SetOutput(1, mQi);
        }
        public override void Restart()
        {
            mQn = false;
            mQi = false;
            mClk = false;
        }
    }

    #endregion

    #region D

    public class DLatch : GenericBase
    {
        private bool mQ = false;
        private bool mQn = false;

        public DLatch()
        {
            mName = mCName = "D";

            AddOutput("Q");
            AddOutput("!Q");

            AddInput("D");
            AddInput("CLK");
            AddInput("R");
            AddInput("S");
        }
        public override void _Update()
        {
            bool r = GetInput(2);
            bool s = GetInput(3);

            if (GetInput(1)) // Rising edge
                mQ = GetInput(0);

            mQn = !mQ;

            if (r && s)
                mQ = mQn = true;
            else if (s)
            {
                mQ = true;
                mQn = false;
            }
            else if (r)
            {
                mQn = true;
                mQ = false;
            }

            SetOutput(0, mQ);
            SetOutput(1, mQn);
        }
        public override void Restart()
        {
            mQ = false;
        }
    }
    public class DFlipFlop : GenericBase
    {
        private bool mQ = false;
        private bool mQn = false;
        private bool mClk = false;

        public DFlipFlop()
        {
            mName = mCName = "D";

            AddOutput("Q");
            AddOutput("!Q");

            AddInput("D");
            AddInput("CLK");
            AddInput("R");
            AddInput("S");
        }
        public override void _Update()
        {
            bool d = GetInput(0);
            bool clk = GetInput(1);
            bool r = GetInput(2);
            bool s = GetInput(3);

            if (!clk && mClk) // Falling edge
                mQ = d;

            mQn = !mQ;

            if (r && s)
                mQ = mQn = true;
            else if (s)
            {
                mQ = true;
                mQn = false;
            }
            else if (r)
            {
                mQn = true;
                mQ = false;
            }

            mClk = clk;

            SetOutput(0, mQ);
            SetOutput(1, mQn);
        }
        public override void Restart()
        {
            mQ = false;         
            mClk = false;
        }
    }

    #endregion

    #region T

    public class TFlipFlop : GenericBase
    {
        private bool mQ = false;
        private bool mClk = false;

        public TFlipFlop()
        {
            mName = mCName = "T";

            AddOutput("Q");
            AddOutput("!Q");

            AddInput("CLK");
        }
        public override void _Update()
        {
            bool clk = GetInput(0);

            if (!clk && mClk) // Falling edge
                mQ = !mQ;

            mClk = clk;

            SetOutput(0, mQ);
            SetOutput(1, !mQ);
        }
        public override void Restart()
        {
            mQ = false;
            mClk = false;
        }
    }

    #endregion
}

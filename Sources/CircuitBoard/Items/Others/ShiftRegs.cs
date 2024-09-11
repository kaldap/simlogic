using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitBoard.Items.Others
{
    public class SPShiftRegister : GenericBase
    {
        private byte mData = 0;
        private bool mClk = false;

        public SPShiftRegister()
        {
            mName = mCName = "SP SHIFT";

            AddInput("CLK");
            AddInput("DATA");

            AddOutput("S1");
            AddOutput("S2");
            AddOutput("S3");
            AddOutput("S4");
            AddOutput("S5");
            AddOutput("S6");
            AddOutput("S7");
            AddOutput("S8");
        }
        public override void _Update()
        {
            bool clk = GetInput(0);
            byte data = (byte)(GetInput(1) ? 1 : 0);

            if (!clk && mClk)
                mData = (byte)((mData << 1) | data);

            mClk = clk;

            for (int i = 0; i < 8; i++)
                SetOutput(i, (mData & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mData = 0;
            mClk = false;
        }
    }
    public class PSShiftRegister : GenericBase
    {
        private byte mData = 0;
        private bool mClk = false;
        private bool mOut = false;

        public PSShiftRegister()
        {
            mName = mCName = "PS SHIFT";

            AddInput("D1");
            AddInput("D2");
            AddInput("D3");
            AddInput("D4");
            AddInput("D5");
            AddInput("D6");
            AddInput("D7");
            AddInput("D8");

            AddInput("MODE");
            AddInput("DATA");
            AddInput("CLK");

            AddOutput("S1");
            AddOutput("S2");
            AddOutput("S3");
            AddOutput("S4");
            AddOutput("S5");
            AddOutput("S6");
            AddOutput("S7");
            AddOutput("S8");

            AddOutput("DATA");
        }
        public override void _Update()
        {
            bool clk = GetInput(10);
            bool data = GetInput(9);
            bool mode = GetInput(8);

            byte value = 0;
            for (int i = 0; i < 8; i++)
                value |= (byte)(GetInput(i) ? (1 << i) : 0);

            if (!clk && mClk)
            {
                if (mode) // Parallel
                    mData = value;
                else
                {
                    mOut = (mData & 1) != 0;
                    mData >>= 1;
                    mData |= (byte)(data ? 0x80 : 0);
                }
            }

            mClk = clk;

            SetOutput(8, mOut);
            for (int i = 0; i < 8; i++)
                SetOutput(i, (mData & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mData = 0;
            mClk = false;
            mOut = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitBoard.Items.Others
{
    public class BCD2OneFromTen : GenericBase
    {
        public BCD2OneFromTen()
        {
            mName = mCName = "BCD -> 1/10";

            AddInput("D1");
            AddInput("D2");
            AddInput("D3");
            AddInput("D4");

            AddOutput("S0");
            AddOutput("S1");
            AddOutput("S2");
            AddOutput("S3");
            AddOutput("S4");
            AddOutput("S5");
            AddOutput("S6");
            AddOutput("S7");
            AddOutput("S8");
            AddOutput("S9");
        }
        public override void _Update()
        {
            byte value = 0;
            for (int i = 0; i < 4; i++)
                value |= (byte)(GetInput(i) ? (1 << i) : 0);
           
            for (int i = 0; i < 10; i++)
                SetOutput(i, value == i);
        }
    }
    public class OneFromTen2BCD : GenericBase
    {
        public OneFromTen2BCD()
        {
            mName = mCName = "1/10 -> BCD";

            AddOutput("D1");
            AddOutput("D2");
            AddOutput("D3");
            AddOutput("D4");
            AddOutput("ERR");

            AddInput("S0");
            AddInput("S1");
            AddInput("S2");
            AddInput("S3");
            AddInput("S4");
            AddInput("S5");
            AddInput("S6");
            AddInput("S7");
            AddInput("S8");
            AddInput("S9");
        }
        public override void _Update()
        {
            byte cnt = 0;
            byte value = 0;

            for (int i = 0; i < 10; i++)
            {
                if (!GetInput(i))
                    continue;

                value = (byte)i;
                cnt++;
            }

            if (cnt != 1)
            {
                SetOutput(4, true);
                for (int i = 0; i < 4; i++)
                    SetOutput(i, false);
            }
            else
            {
                SetOutput(4, false);
                for (int i = 0; i < 4; i++)
                    SetOutput(i, (value & (1 << i)) != 0);
            }
        }
    }
    public class BCD2Display : GenericBase
    {
        private static readonly byte[] cTable = new byte[] { 0x3F, 0x06, 0x5B, 0x4F, 0x66, 0x6D, 0x7D, 0x07, 0x7F, 0x6F, 0x77, 0x7C, 0x39, 0x5E, 0x79, 0x71, 0x40 };

        public BCD2Display()
        {
            mName = mCName = "BCD -> DISP";

            AddInput("D1");
            AddInput("D2");
            AddInput("D3");
            AddInput("D4");
            AddInput("ERR");

            AddOutput("A");
            AddOutput("B");
            AddOutput("C");
            AddOutput("D");
            AddOutput("E");
            AddOutput("F");
            AddOutput("G");
        }
        public override void _Update()
        {
            byte value = 0;
            if (GetInput(4))
                value = 16;
            else
            {
                for (int i = 0; i < 4; i++)
                    value |= (byte)(GetInput(i) ? (1 << i) : 0);
            }

            for(int i = 0;i<7;i++)
                SetOutput(i, (cTable[value] & (1 << i)) !=0);
        }
    }
}

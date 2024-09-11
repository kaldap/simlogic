using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitBoard.Items.Others
{
    public class Bit16Demux : GenericBase
    {
        public Bit16Demux()
        {
            mName = mCName = "DEMUX";

            AddOutput("O1");
            AddOutput("O2");
            AddOutput("O3");
            AddOutput("O4");
            AddOutput("O5");
            AddOutput("O6");
            AddOutput("O7");
            AddOutput("O8");
            AddOutput("O9");
            AddOutput("O10");
            AddOutput("O11");
            AddOutput("O12");
            AddOutput("O13");
            AddOutput("O14");
            AddOutput("O15");
            AddOutput("O16");

            AddInput("A1");
            AddInput("A2");
            AddInput("A3");
            AddInput("A4");

            AddInput("I");
        }
        public override void _Update()
        {
            byte a = 0;

            if (GetInput(0))
                a |= 1;

            if (GetInput(1))
                a |= 2;

            if (GetInput(2))
                a |= 4;

            if (GetInput(3))
                a |= 8;

            for(int i = 0;i<16;i++)
                SetOutput(i, (i == a) ? GetInput(4) : false);
        }
    }
    public class Bit8Demux : GenericBase
    {
        public Bit8Demux()
        {
            mName = mCName = "DEMUX";

            AddOutput("O1");
            AddOutput("O2");
            AddOutput("O3");
            AddOutput("O4");
            AddOutput("O5");
            AddOutput("O6");
            AddOutput("O7");
            AddOutput("O8");

            AddInput("A1");
            AddInput("A2");
            AddInput("A3");

            AddInput("I");
        }
        public override void _Update()
        {
            byte a = 0;

            if (GetInput(0))
                a |= 1;

            if (GetInput(1))
                a |= 2;

            if (GetInput(2))
                a |= 4;


            for (int i = 0; i < 8; i++)
                SetOutput(i, (i == a) ? GetInput(3) : false);
        }
    }
    public class Bit4Demux : GenericBase
    {
        public Bit4Demux()
        {
            mName = mCName = "DEMUX";

            AddOutput("O1");
            AddOutput("O2");
            AddOutput("O3");
            AddOutput("O4");

            AddInput("A1");
            AddInput("A2");

            AddInput("I");
        }
        public override void _Update()
        {
            byte a = 0;

            if (GetInput(0))
                a |= 1;

            if (GetInput(1))
                a |= 2;


            for (int i = 0; i < 4; i++)
                SetOutput(i, (i == a) ? GetInput(2) : false);
        }
    }
    public class Bit2Demux : GenericBase
    {
        public Bit2Demux()
        {
            mName = mCName = "DEMUX";

            AddOutput("O1");
            AddOutput("O2");

            AddInput("A");
            AddInput("I");
        }
        public override void _Update()
        {
            bool a = GetInput(0);
            bool i = GetInput(1);

            SetOutput(0, a ? false : i);
            SetOutput(1, a ? i : false);
        }
    }
}

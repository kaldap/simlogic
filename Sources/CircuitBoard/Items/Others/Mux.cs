using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitBoard.Items.Others
{
    public class Bit16Mux : GenericBase
    {
        public Bit16Mux()
        {
            mName = mCName = "MUX";

            AddInput("I1");
            AddInput("I2");
            AddInput("I3");
            AddInput("I4");
            AddInput("I5");
            AddInput("I6");
            AddInput("I7");
            AddInput("I8");
            AddInput("I9");
            AddInput("I10");
            AddInput("I11");
            AddInput("I12");
            AddInput("I13");
            AddInput("I14");
            AddInput("I15");
            AddInput("I16");

            AddInput("A1");
            AddInput("A2");
            AddInput("A3");
            AddInput("A4");

            AddOutput("X");
        }
        public override void _Update()
        {
            byte a = 0;

            if (GetInput(16))
                a |= 1;

            if (GetInput(17))
                a |= 2;

            if (GetInput(18))
                a |= 4;

            if (GetInput(19))
                a |= 8;
                     

            SetOutput(0, GetInput(a));
        }
    }
    public class Bit8Mux : GenericBase
    {
        public Bit8Mux()
        {
            mName = mCName = "MUX";

            AddInput("I1");
            AddInput("I2");
            AddInput("I3");
            AddInput("I4");
            AddInput("I5");
            AddInput("I6");
            AddInput("I7");
            AddInput("I8");        

            AddInput("A1");
            AddInput("A2");
            AddInput("A3");

            AddOutput("X");
        }
        public override void _Update()
        {
            byte a = 0;

            if (GetInput(8))
                a |= 1;

            if (GetInput(9))
                a |= 2;

            if (GetInput(10))
                a |= 4;  

            SetOutput(0, GetInput(a));
        }
    }
    public class Bit4Mux : GenericBase
    {
        public Bit4Mux()
        {
            mName = mCName = "MUX";

            AddInput("I1");
            AddInput("I2");
            AddInput("I3");
            AddInput("I4");          

            AddInput("A1");
            AddInput("A2");

            AddOutput("X");
        }
        public override void _Update()
        {
            byte a = 0;

            if (GetInput(4))
                a |= 1;

            if (GetInput(5))
                a |= 2;


            SetOutput(0, GetInput(a));
        }
    }
    public class Bit2Mux : GenericBase
    {
        public Bit2Mux()
        {
            mName = mCName = "MUX";

            AddInput("I1");
            AddInput("I2");

            AddInput("A");

            AddOutput("X");
        }
        public override void _Update()
        {
            SetOutput(0, GetInput(GetInput(2) ? 1 : 0));
        }
    }
}

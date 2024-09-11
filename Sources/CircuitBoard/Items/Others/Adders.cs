using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitBoard.Items.Others
{
    public class SimpleAdder : GenericBase
    {
        public SimpleAdder()
        {
            mName = mCName = "ADD";

            AddInput("A");
            AddInput("B");

            AddOutput("SUM");
            AddOutput("CARRY");
        }
        public override void _Update()
        {
            bool a = GetInput(0);
            bool b = GetInput(1);

            SetOutput(0, a != b);
            SetOutput(1, a && b);
        }
    }
    public class FullAdder : GenericBase
    {
        public FullAdder()
        {
            mName = mCName = "ADD";

            AddInput("A");
            AddInput("B");
            AddInput("Cin");

            AddOutput("SUM");
            AddOutput("Cout");
        }
        public override void _Update()
        {
            bool a = GetInput(0);
            bool b = GetInput(1);
            bool c = GetInput(2);

            SetOutput(0, c != (a != b));
            SetOutput(1, ((a != b) && c) || (a && b));
        }
    }
    public class Bit4Adder : GenericBase
    {
        public Bit4Adder()
        {
            mName = mCName = "ADD";

            AddInput("A1");
            AddInput("A2");
            AddInput("A3");
            AddInput("A4");

            AddInput("B1");
            AddInput("B2");
            AddInput("B3");
            AddInput("B4");

            AddInput("Cin");

            AddOutput("S1");
            AddOutput("S2");
            AddOutput("S3");
            AddOutput("S4");
            AddOutput("Cout");
        }
        public override void _Update()
        {
            sbyte a = 0, b = 0;

            if (GetInput(0))
                a |= 1;

            if (GetInput(1))
                a |= 2;

            if (GetInput(2))
                a |= 4;

            if (GetInput(3))
                a |= 8;

            if (GetInput(4))
                b |= 1;

            if (GetInput(5))
                b |= 2;

            if (GetInput(6))
                b |= 4;

            if (GetInput(7))
                b |= 8;

            if (GetInput(8))
                b -= a;
            else
                b += a;

            SetOutput(0, (b & 1) != 0);
            SetOutput(1, (b & 2) != 0);
            SetOutput(2, (b & 4) != 0);
            SetOutput(3, (b & 8) != 0);
            SetOutput(4, (b & 16) != 0);
        }
    }
}

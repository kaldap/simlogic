using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitBoard.Items.Others
{
    #region Digital

    public class Bit16Counter : GenericBase
    {
        private uint mValue = 0;
        private bool mClk = false;

        public Bit16Counter()
        {
            mName = mCName = "COUNTER";

            AddInput("CLK");
            AddInput("R0");
            AddInput("UD");

            AddOutput("S1");
            AddOutput("S2");
            AddOutput("S3");
            AddOutput("S4");
            AddOutput("S5");
            AddOutput("S6");
            AddOutput("S7");
            AddOutput("S8");
            AddOutput("S9");
            AddOutput("S10");
            AddOutput("S11");
            AddOutput("S12");
            AddOutput("S13");
            AddOutput("S14");
            AddOutput("S15");
            AddOutput("S16");
        }
        public override void _Update()
        {
            bool clk = GetInput(0);

            if (GetInput(1))
                mValue = 0;
            else if (!clk && mClk)
            {
                if (!GetInput(2))
                    mValue++;
                else
                {
                    if (mValue == 0)
                        mValue = 0xFFFF;
                    else
                        mValue--;
                }
            }

            mValue &= 0xFFFF;
            mClk = clk;

            for (int i = 0; i < 16; i++)
                SetOutput(i, (mValue & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }
    }
    public class Bit8Counter : GenericBase
    {
        private ushort mValue = 0;
        private bool mClk = false;

        public Bit8Counter()
        {
            mName = mCName = "COUNTER";          

            AddInput("CLK");
            AddInput("R0");
            AddInput("UD");

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

            if (GetInput(1))
                mValue = 0;
            else if (!clk && mClk)
            {
                if (!GetInput(2))
                    mValue++;
                else
                {
                    if (mValue == 0)
                        mValue = 0xFF;
                    else
                        mValue--;
                }
            }

            mValue &= 0xFF;
            mClk = clk;

            for (int i = 0; i < 8; i++)
                SetOutput(i, (mValue & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }
    }
    public class Bit4Counter : GenericBase
    {
        private ushort mValue = 0;
        private bool mClk = false;

        public Bit4Counter()
        {
            mName = mCName = "COUNTER";

            AddInput("CLK");
            AddInput("R0");
            AddInput("UD");

            AddOutput("S1");
            AddOutput("S2");
            AddOutput("S3");
            AddOutput("S4");
        }
        public override void _Update()
        {
            bool clk = GetInput(0);

            if (GetInput(1))
                mValue = 0;
            else if (!clk && mClk)
            {
                if (!GetInput(2))
                    mValue++;
                else
                {
                    if (mValue == 0)
                        mValue = 0x0F;
                    else
                        mValue--;
                }
            }

            mValue &= 0x0F;
            mClk = clk;

            for (int i = 0; i < 4; i++)
                SetOutput(i, (mValue & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }
    }

    public class Bit16RevCounter : GenericBase
    {
        private uint mValue = 0;
        private bool mClk = false;

        public Bit16RevCounter()
        {
            mName = mCName = "-COUNTER";

            AddInput("CLK");
            AddInput("R0");

            AddOutput("S1");
            AddOutput("S2");
            AddOutput("S3");
            AddOutput("S4");
            AddOutput("S5");
            AddOutput("S6");
            AddOutput("S7");
            AddOutput("S8");
            AddOutput("S9");
            AddOutput("S10");
            AddOutput("S11");
            AddOutput("S12");
            AddOutput("S13");
            AddOutput("S14");
            AddOutput("S15");
            AddOutput("S16");
        }
        public override void _Update()
        {
            bool clk = GetInput(0);

                        if (GetInput(1))
                mValue = 0;
            else if (!clk && mClk)
                unchecked { mValue--; }

            mValue &= 0xFFFF;
            mClk = clk;

            for (int i = 0; i < 16; i++)
                SetOutput(i, (mValue & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }
    }
    public class Bit8RevCounter : GenericBase
    {
        private ushort mValue = 0;
        private bool mClk = false;

        public Bit8RevCounter()
        {
            mName = mCName = "-COUNTER";

            AddInput("CLK");
            AddInput("R0");

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

                        if (GetInput(1))
                mValue = 0;
            else if (!clk && mClk)
                unchecked { mValue--; }

            mValue &= 0xFF;
            mClk = clk;

            for (int i = 0; i < 8; i++)
                SetOutput(i, (mValue & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }
    }
    public class Bit4RevCounter : GenericBase
    {
        private ushort mValue = 0;
        private bool mClk = false;

        public Bit4RevCounter()
        {
            mName = mCName = "-COUNTER";

            AddInput("CLK");
            AddInput("R0");

            AddOutput("S1");
            AddOutput("S2");
            AddOutput("S3");
            AddOutput("S4");
        }
        public override void _Update()
        {
            bool clk = GetInput(0);

                        if (GetInput(1))
                mValue = 0;
            else if (!clk && mClk)
                unchecked { mValue--; }

            mValue &= 0x0F;
            mClk = clk;

            for (int i = 0; i < 4; i++)
                SetOutput(i, (mValue & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }
    }

    #endregion

    #region Decimal

    public class DecimalCounter : GenericBase
    {
        private ushort mValue = 0;
        private bool mClk = false;

        public DecimalCounter()
        {
            mName = mCName = "COUNTER 10";

            AddInput("CLK");
            AddInput("R0");

            AddOutput("S1");
            AddOutput("S2");
            AddOutput("S3");
            AddOutput("S4");
        }
        public override void _Update()
        {
            bool clk = GetInput(0);

            if (GetInput(1))
                mValue = 0;
            else if (!clk && mClk)
                mValue++;

            if (mValue == 10)
                mValue = 0;
            
            mClk = clk;

            for (int i = 0; i < 4; i++)
                SetOutput(i, (mValue & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }
    }
    public class DecimalRevCounter : GenericBase
    {
        private ushort mValue = 0;
        private bool mClk = false;

        public DecimalRevCounter()
        {
            mName = mCName = "-COUNTER 10";

            AddInput("CLK");
            AddInput("R0");

            AddOutput("S1");
            AddOutput("S2");
            AddOutput("S3");
            AddOutput("S4");
        }
        public override void _Update()
        {
            bool clk = GetInput(0);

            if (GetInput(1))
                mValue = 0;
            else if (!clk && mClk)
            {
                if (mValue == 0)
                    mValue = 9;
                else
                    mValue--;
            }

            mClk = clk;

            for (int i = 0; i < 4; i++)
                SetOutput(i, (mValue & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }
    }

    #endregion

    #region Custom

    public class CustomCounter : GenericBase
    {
        private ushort mValue = 0;
        private bool mClk = false;
        private byte mMax = 10;

        public byte Soustava
        {
            get
            {
                return mMax;
            }
            set
            {
                if (value < 2 || value > 255)
                    throw new ArgumentOutOfRangeException("Parametr musí být v rozmezí od 2 do 255!");
                mMax = value;
                mCName = "COUNTER " + mMax;
            }
        }

        public CustomCounter()
        {
            mName = mCName = "COUNTER 10";

            AddInput("CLK");
            AddInput("R0");

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

                        if (GetInput(1))
                mValue = 0;
            else if (!clk && mClk)
                mValue++;

            if (mValue >= mMax)
                mValue = 0;

            mClk = clk;

            for (int i = 0; i < 8; i++)
                SetOutput(i, (mValue & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }

        public override void Save(System.IO.BinaryWriter writer)
        {
            base.Save(writer);
            writer.Write(mMax);
        }

        public override void Load(System.IO.BinaryReader reader)
        {
            base.Load(reader);
            mMax = reader.ReadByte();
        }

    }
    public class CustomRevCounter : GenericBase
    {
        private ushort mValue = 0;
        private bool mClk = false;
        private byte mMax = 10;

        public byte Soustava
        {
            get
            {
                return mMax;
            }
            set
            {
                if (value < 2 || value > 255)
                    throw new ArgumentOutOfRangeException("Parametr musí být v rozmezí od 2 do 255!");
                mMax = value;
                mCName = "-COUNTER " + mMax;
            }
        }

        public CustomRevCounter()
        {
            mName = mCName = "-COUNTER 10";

            AddInput("CLK");
            AddInput("R0");

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

            if (GetInput(1))
                mValue = 0;
            else if (!clk && mClk)
            {
                if (mValue == 0)
                    mValue = (ushort)(mMax - 1);
                else
                    mValue--;
            }

            mClk = clk;

            for (int i = 0; i < 8; i++)
                SetOutput(i, (mValue & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }

        public override void Save(System.IO.BinaryWriter writer)
        {
            base.Save(writer);
            writer.Write(mMax);
        }

        public override void Load(System.IO.BinaryReader reader)
        {
            base.Load(reader);
            mMax = reader.ReadByte();
        }
    }

    #endregion

    #region Johnson

    public class JohnsonBitCounter : GenericBase
    {
        private ushort mValue = 0;
        private bool mClk = false;

        private static readonly byte[] cTable = new byte[] { 0, 1, 3, 7, 15, 31, 30, 28, 24, 16 };

        public JohnsonBitCounter()
        {
            mName = mCName = "JOHNSON";

            AddInput("CLK");
            AddInput("RST");

            AddOutput("S1");
            AddOutput("S2");
            AddOutput("S3");
            AddOutput("S4");
            AddOutput("S5");
        }
        public override void _Update()
        {
            bool clk = GetInput(0);

            if (!clk && mClk)
                mValue++;

            if (mValue == 10)
                mValue = 0;

            mClk = clk;

            for (int i = 0; i < 4; i++)
                SetOutput(i, (cTable[mValue] & (1 << i)) != 0);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }
    }
    public class JohnsonCounter : GenericBase
    {
        private ushort mValue = 0;
        private bool mClk = false;

        public JohnsonCounter()
        {
            mName = mCName = "JOHNSON";

            AddInput("CLK");
            AddInput("RST");

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
            AddOutput("CARRY");
        }
        public override void _Update()
        {
            bool clk = GetInput(0);

            if (GetInput(1))
                mValue = 0;
            else if (!clk && mClk)
                mValue++;

            if (mValue == 10)
                mValue = 0;

            mClk = clk;

            for (int i = 0; i < 10; i++)
                SetOutput(i, i == mValue);
            SetOutput(10, mValue < 5);
        }
        public override void Restart()
        {
            mValue = 0;
            mClk = false;
        }
    }

    #endregion
}

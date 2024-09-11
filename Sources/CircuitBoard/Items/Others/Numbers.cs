using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitBoard.Items.Others
{
    public class NumberSourceU8 : GenericBase
    {
        private byte mNumber = 0;
        public byte Hodnota
        {
            get
            {
                return mNumber;
            }
            set
            {
                mNumber = value;
                mCName = "N " + value;
            }
        }

        public NumberSourceU8()
        {
            mName = mCName = "N " + mNumber;

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
            for (int i = 0; i < 8; i++)
                SetOutput(i, (mNumber & (1 << i)) != 0);
        }

        public override void Save(System.IO.BinaryWriter writer)
        {
            base.Save(writer);
            writer.Write(mNumber);
        }

        public override void Load(System.IO.BinaryReader reader)
        {
            base.Load(reader);
            Hodnota = reader.ReadByte();
        }
    }
    public class NumberSourceS8 : GenericBase
    {
        private sbyte mNumber = 0;
        public sbyte Hodnota
        {
            get
            {
                return mNumber;
            }
            set
            {
                mNumber = value;
                mCName = "N " + value;
            }
        }

        public NumberSourceS8()
        {
            mName = mCName = "N " + mNumber;

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
            for (int i = 0; i < 8; i++)
                SetOutput(i, (mNumber & (1 << i)) != 0);
        }

        public override void Save(System.IO.BinaryWriter writer)
        {
            base.Save(writer);
            writer.Write(mNumber);
        }

        public override void Load(System.IO.BinaryReader reader)
        {
            base.Load(reader);
            Hodnota = reader.ReadSByte();
        }
    }

    public class NumberSourceU16 : GenericBase
    {
        private ushort mNumber = 0;
        public ushort Hodnota
        {
            get
            {
                return mNumber;
            }
            set
            {
                mNumber = value;
                mCName = "N " + value;
            }
        }

        public NumberSourceU16()
        {
            mName = mCName = "N " + mNumber;

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
            for (int i = 0; i < 16; i++)
                SetOutput(i, (mNumber & (1 << i)) != 0);
        }

        public override void Save(System.IO.BinaryWriter writer)
        {
            base.Save(writer);
            writer.Write(mNumber);
        }

        public override void Load(System.IO.BinaryReader reader)
        {
            base.Load(reader);
            Hodnota = reader.ReadUInt16();
        }
    }
    public class NumberSourceS16 : GenericBase
    {
        private short mNumber = 0;
        public short Hodnota
        {
            get
            {
                return mNumber;
            }
            set
            {
                mNumber = value;
                mCName = "N " + value;
            }
        }

        public NumberSourceS16()
        {
            mName = mCName = "N " + mNumber;

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
            for (int i = 0; i < 16; i++)
                SetOutput(i, (mNumber & (1 << i)) != 0);
        }

        public override void Save(System.IO.BinaryWriter writer)
        {
            base.Save(writer);
            writer.Write(mNumber);
        }

        public override void Load(System.IO.BinaryReader reader)
        {
            base.Load(reader);
            Hodnota = reader.ReadInt16();
        }
    }

    public class NumberViewU8 : GenericBase
    {
        public NumberViewU8()
        {
            mName = "NumberView";
            mCName = "-> 0";

            AddInput("S1");
            AddInput("S2");
            AddInput("S3");
            AddInput("S4");
            AddInput("S5");
            AddInput("S6");
            AddInput("S7");
            AddInput("S8");
        }
        public override void _Update()
        {
            byte value = 0;
            for (int i = 0; i < 8; i++)
                value |= (byte)(GetInput(i) ? (1 << i) : 0);
            
            mCName = "-> " + value.ToString();
        }
    }
    public class NumberViewS8 : GenericBase
    {
        public NumberViewS8()
        {
            mName = "NumberView";
            mCName = "-> 0";

            AddInput("S1");
            AddInput("S2");
            AddInput("S3");
            AddInput("S4");
            AddInput("S5");
            AddInput("S6");
            AddInput("S7");
            AddInput("S8");
        }
        public override void _Update()
        {
            sbyte value = 0;
            for (int i = 0; i < 8; i++)
                value |= (sbyte)(GetInput(i) ? (1 << i) : 0);

            mCName = "-> " + value.ToString();
        }
    }

    public class NumberViewU16 : GenericBase
    {
        public NumberViewU16()
        {
            mName = "NumberView";
            mCName = "-> 0";

            AddInput("S1");
            AddInput("S2");
            AddInput("S3");
            AddInput("S4");
            AddInput("S5");
            AddInput("S6");
            AddInput("S7");
            AddInput("S8");
            AddInput("S9");
            AddInput("S10");
            AddInput("S11");
            AddInput("S12");
            AddInput("S13");
            AddInput("S14");
            AddInput("S15");
            AddInput("S16");
        }
        public override void _Update()
        {
            ushort value = 0;
            for (int i = 0; i < 16; i++)
                value |= (ushort)(GetInput(i) ? (1 << i) : 0);

            mCName = "-> " + value.ToString();
        }
    }
    public class NumberViewS16 : GenericBase
    {
        public NumberViewS16()
        {
            mName = "NumberView";
            mCName = "-> 0";

            AddInput("S1");
            AddInput("S2");
            AddInput("S3");
            AddInput("S4");
            AddInput("S5");
            AddInput("S6");
            AddInput("S7");
            AddInput("S8");
            AddInput("S9");
            AddInput("S10");
            AddInput("S11");
            AddInput("S12");
            AddInput("S13");
            AddInput("S14");
            AddInput("S15");
            AddInput("S16");
        }
        public override void _Update()
        {
            short value = 0;
            for (int i = 0; i < 16; i++)
                value |= (short)(GetInput(i) ? (1 << i) : 0);

            mCName = "-> " + value.ToString();
        }
    }
}

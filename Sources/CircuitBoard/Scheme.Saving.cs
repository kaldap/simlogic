using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using CircuitBoard.Items.Gates;

namespace CircuitBoard
{
    public partial class Scheme
    {
        public void Save(string file)
        {
            if (Busy)
                return;

            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write((byte)'S');
                    bw.Write((byte)'L');
                    bw.Write((byte)'S');
                    bw.Write((byte)'F');
                    bw.Write(mZoom);
                    bw.Write(mTopLeft.X);
                    bw.Write(mTopLeft.Y);
                    bw.Write(mGrid);
                    bw.Write(mItems.Count);

                    for(int i = 0;i<mItems.Count;i++)
                    {
                        IItem it = mItems[i];
                        bw.Write(it.GetType().FullName);
                        bw.Write(it.Name);
                        bw.Write(it.Center.X);
                        bw.Write(it.Center.Y);
                        it.Save(bw);
                    }

                    for (int i = 0; i < mItems.Count; i++)
                    {
                        IItem it = mItems[i];
                        foreach (Pin p in it.Outputs)
                        {
                            bw.Write(p.State);
                            bw.Write(p.Joints.Length);
                            foreach (Pin ip in p.Joints)
                            {
                                bw.Write(mItems.IndexOf(ip.Parent));

                                List<Pin> ins = new List<Pin>();
                                ins.AddRange(ip.Parent.Inputs);
                                bw.Write(ins.IndexOf(ip));
                            }
                        }
                    }
                }
            }
        }
        public new void Load(string file)
        {
            if (Busy)
                return;

            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    if (br.ReadByte() != (byte)'S') return;
                    if (br.ReadByte() != (byte)'L') return;
                    if (br.ReadByte() != (byte)'S') return;
                    if (br.ReadByte() != (byte)'F') return;

                    mZoom = br.ReadSingle();
                    float tlx = br.ReadSingle();
                    float tly = br.ReadSingle();
                    mGrid = br.ReadInt32();
                    mTopLeft = new PointF(tlx, tly);

                    mItems.Clear();
                    int numIt = br.ReadInt32();
                    int i;

                    for (i = 0; i < numIt; i++)
                    {
                        string type = br.ReadString();
                        IItem it = (IItem)Type.GetType(type, false, true).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
                        it.Name = br.ReadString();
                        float x = br.ReadSingle();
                        float y = br.ReadSingle();
                        it.Load(br);
                        it.Center = new PointF(x, y);
                        mItems.Add(it);
                    }

                    for (i = 0; i < numIt; i++)
                    {
                        IItem it = mItems[i];
                        foreach (Pin p in it.Outputs)
                        {
                            p.State = br.ReadBoolean();
                            int jlen = br.ReadInt32();
                            for(int j =0;j<jlen;j++)
                            {
                                IItem item = mItems[br.ReadInt32()];
                                Pin tp = item.Inputs[br.ReadInt32()];
                                p.Join(tp);
                            }
                        }
                    }
                }
            }

            Sim_Reset();
            Invalidate();
        }
    }
}

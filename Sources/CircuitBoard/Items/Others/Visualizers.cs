using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace CircuitBoard.Items.Others
{
    public class Display : GenericBase
    {
        private byte mValue = 0;

        private Point[][] segPoints;
        private int gridHeight = 270;
        private int gridWidth = 200;
        private int elementWidth = 33;

        public Display()
        {
            mName = "7Seg";
            mCName = "";

            AddInput("A");
            AddInput("B");
            AddInput("C");
            AddInput("D");
            AddInput("E");
            AddInput("F");
            AddInput("G");
            AddInput("DOT");

            mSize.Width = 300;
            UpdatePoints();
        }
        public override void _Update()
        {
            mValue = 0;
            for (int i = 0; i < 8; i++)
                mValue |= (byte)(GetInput(i) ? (1 << i) : 0);
        }
        public override void Paint(Graphics g)
        {
            base.Paint(g);

            RectangleF srcRect = new RectangleF(0, 0, mSize.Width, mSize.Height);
            RectangleF destRect = new RectangleF(Rect.Right - gridWidth, Rect.Top + 20 + (mSize.Height - gridHeight) * 0.5f, gridWidth, gridHeight);

            // Begin graphics container that remaps coordinates for our convenience
            GraphicsContainer containerState = g.BeginContainer(destRect, srcRect, GraphicsUnit.Pixel);

            g.FillPolygon((mValue & 0x1) == 0x1 ? Brushes.Red : Brushes.LightGray, segPoints[0]);
            g.FillPolygon((mValue & 0x2) == 0x2 ? Brushes.Red : Brushes.LightGray, segPoints[2]);
            g.FillPolygon((mValue & 0x4) == 0x4 ? Brushes.Red : Brushes.LightGray, segPoints[5]);
            g.FillPolygon((mValue & 0x8) == 0x8 ? Brushes.Red : Brushes.LightGray, segPoints[6]);
            g.FillPolygon((mValue & 0x10) == 0x10 ? Brushes.Red : Brushes.LightGray, segPoints[4]);
            g.FillPolygon((mValue & 0x20) == 0x20 ? Brushes.Red : Brushes.LightGray, segPoints[1]);
            g.FillPolygon((mValue & 0x40) == 0x40 ? Brushes.Red : Brushes.LightGray, segPoints[3]);

            g.FillEllipse((mValue & 0x80) == 0x80 ? Brushes.Red : Brushes.LightGray, gridWidth - 1, gridHeight - elementWidth + 1, elementWidth, elementWidth);

            g.EndContainer(containerState);
        }

        private void UpdatePoints()
        {
            int halfHeight = gridHeight / 2, halfWidth = elementWidth / 2;

            segPoints = new Point[7][];
            for (int i = 0; i < 7; i++) segPoints[i] = new Point[6];

            int p = 0;
            segPoints[p][0].X = elementWidth + 1; segPoints[p][0].Y = 0;
            segPoints[p][1].X = gridWidth - elementWidth - 1; segPoints[p][1].Y = 0;
            segPoints[p][2].X = gridWidth - halfWidth - 1; segPoints[p][2].Y = halfWidth;
            segPoints[p][3].X = gridWidth - elementWidth - 1; segPoints[p][3].Y = elementWidth;
            segPoints[p][4].X = elementWidth + 1; segPoints[p][4].Y = elementWidth;
            segPoints[p][5].X = halfWidth + 1; segPoints[p][5].Y = halfWidth;

            p++;
            segPoints[p][0].X = 0; segPoints[p][0].Y = elementWidth + 1;
            segPoints[p][1].X = halfWidth; segPoints[p][1].Y = halfWidth + 1;
            segPoints[p][2].X = elementWidth; segPoints[p][2].Y = elementWidth + 1;
            segPoints[p][3].X = elementWidth; segPoints[p][3].Y = halfHeight - halfWidth - 1;
            segPoints[p][4].X = 4; segPoints[p][4].Y = halfHeight - 1;
            segPoints[p][5].X = 0; segPoints[p][5].Y = halfHeight - 1;

            p++;
            segPoints[p][0].X = gridWidth - elementWidth; segPoints[p][0].Y = elementWidth + 1;
            segPoints[p][1].X = gridWidth - halfWidth; segPoints[p][1].Y = halfWidth + 1;
            segPoints[p][2].X = gridWidth; segPoints[p][2].Y = elementWidth + 1;
            segPoints[p][3].X = gridWidth; segPoints[p][3].Y = halfHeight - 1;
            segPoints[p][4].X = gridWidth - 4; segPoints[p][4].Y = halfHeight - 1;
            segPoints[p][5].X = gridWidth - elementWidth; segPoints[p][5].Y = halfHeight - halfWidth - 1;

            p++;
            segPoints[p][0].X = elementWidth + 1; segPoints[p][0].Y = halfHeight - halfWidth;
            segPoints[p][1].X = gridWidth - elementWidth - 1; segPoints[p][1].Y = halfHeight - halfWidth;
            segPoints[p][2].X = gridWidth - 5; segPoints[p][2].Y = halfHeight;
            segPoints[p][3].X = gridWidth - elementWidth - 1; segPoints[p][3].Y = halfHeight + halfWidth;
            segPoints[p][4].X = elementWidth + 1; segPoints[p][4].Y = halfHeight + halfWidth;
            segPoints[p][5].X = 5; segPoints[p][5].Y = halfHeight;

            p++;
            segPoints[p][0].X = 0; segPoints[p][0].Y = halfHeight + 1;
            segPoints[p][1].X = 4; segPoints[p][1].Y = halfHeight + 1;
            segPoints[p][2].X = elementWidth; segPoints[p][2].Y = halfHeight + halfWidth + 1;
            segPoints[p][3].X = elementWidth; segPoints[p][3].Y = gridHeight - elementWidth - 1;
            segPoints[p][4].X = halfWidth; segPoints[p][4].Y = gridHeight - halfWidth - 1;
            segPoints[p][5].X = 0; segPoints[p][5].Y = gridHeight - elementWidth - 1;

            p++;
            segPoints[p][0].X = gridWidth - elementWidth; segPoints[p][0].Y = halfHeight + halfWidth + 1;
            segPoints[p][1].X = gridWidth - 4; segPoints[p][1].Y = halfHeight + 1;
            segPoints[p][2].X = gridWidth; segPoints[p][2].Y = halfHeight + 1;
            segPoints[p][3].X = gridWidth; segPoints[p][3].Y = gridHeight - elementWidth - 1;
            segPoints[p][4].X = gridWidth - halfWidth; segPoints[p][4].Y = gridHeight - halfWidth - 1;
            segPoints[p][5].X = gridWidth - elementWidth; segPoints[p][5].Y = gridHeight - elementWidth - 1;

            p++;
            segPoints[p][0].X = elementWidth + 1; segPoints[p][0].Y = gridHeight - elementWidth;
            segPoints[p][1].X = gridWidth - elementWidth - 1; segPoints[p][1].Y = gridHeight - elementWidth;
            segPoints[p][2].X = gridWidth - halfWidth - 1; segPoints[p][2].Y = gridHeight - halfWidth;
            segPoints[p][3].X = gridWidth - elementWidth - 1; segPoints[p][3].Y = gridHeight;
            segPoints[p][4].X = elementWidth + 1; segPoints[p][4].Y = gridHeight;
            segPoints[p][5].X = halfWidth + 1; segPoints[p][5].Y = gridHeight - halfWidth;
        }
    }

    public class Label : GenericBase
    {
        public Label()
        {
            mName = mCName = "Popisek";
        }
        public override void _Update()
        {
        }

        public override void Paint(Graphics g)
        {
            using (Font f = new Font(FontFamily.GenericMonospace, 20, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                mSize = g.MeasureString(mName, f);
                g.DrawString(mName, f, Brushes.Black, Rect.Left, Rect.Top);
            }
        }

        public override void Save(System.IO.BinaryWriter writer)
        {
            base.Save(writer);
            writer.Write(mSize.Width);
            writer.Write(mSize.Height);
        }

        public override void Load(System.IO.BinaryReader reader)
        {
            base.Load(reader);
            float w = reader.ReadSingle();
            float h = reader.ReadSingle();
            mSize = new SizeF(w, h);
        }

    }
}

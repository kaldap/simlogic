using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace CircuitBoard.Items.Gates
{
    public enum BaseGateShape
    {
        And,
        Or,
        Xor,
        Not
    }
    public static class GateRenderer
    {
        public const float cMinGateWidth = 90.0f;
        public const float cMinGateHeight = 45.0f;

        public static SizeF GetSize(int inputCount)
        {
            inputCount = Math.Max(inputCount, 2);
            float h = 15.0f * (inputCount + 1);
            return new SizeF(2.0f * h, h);
        }

        public static PointF GetPinPosition(int inputCount, int inputIndex)
        {
            SizeF size = GetSize(inputCount);
            if (inputIndex < 0) // Output
                return new PointF(size.Width, size.Height / 2.0f);
            
            if(inputCount == 1) // Unary Gate
                return new PointF(0, size.Height / 2.0f);

            return new PointF(0.0f, 15.0f * (inputIndex + 1));
        }

        public static void Draw(Graphics g, RectangleF r, Pin output, Pin[] inputs, BaseGateShape shape, bool negative)
        {
            RectangleF bounds = new RectangleF(r.Left + (2.0f / 9.0f) * r.Width, r.Top, r.Width / 2.0f, r.Height);
            float oneFifth = bounds.Left + bounds.Width / 5.0f;
            float fourNinth = bounds.Left + 4.0f * bounds.Width / 9.0f;
            float twoNinth = bounds.Left + 2.0f * bounds.Width / 9.0f;
            float oneThird = bounds.Height / 3.0f;

            // Wires
            g.FillRectangle(Brushes.Black, r.Left + r.Width / 2.0f, r.Top + output.RelativePosition.Y - 1.5f, r.Width / 2, 3);
            foreach(Pin p in inputs)
                g.FillRectangle(Brushes.Black, r.Left, r.Top + p.RelativePosition.Y - 1.5f, fourNinth - r.Left, 3);

            // Shape
            using (GraphicsPath path = new GraphicsPath())
            {               
                switch (shape)
                {
                    case BaseGateShape.And:
                        {
                            float hrw = bounds.Width - fourNinth + bounds.Left;
                            path.AddLine(bounds.Left, bounds.Top, fourNinth, bounds.Top);
                            path.AddArc(fourNinth - hrw, bounds.Top, 2.0f * hrw, bounds.Height, -90.0f, 180.0f);
                            path.AddLine(fourNinth, bounds.Bottom, bounds.Left, bounds.Bottom);
                            path.AddLine(bounds.Left, bounds.Bottom, bounds.Left, bounds.Top);
                            path.CloseFigure();
                        }
                        break;
                    case BaseGateShape.Or:
                    case BaseGateShape.Xor:
                        {
                            float hstep = (bounds.Width - fourNinth + bounds.Left) / 3.0f;
                            float vstep = bounds.Height / 6.0f;
                            float threeNinth = bounds.Left + 3.0f * bounds.Width / 9.0f;
                            path.AddLine(bounds.Left, bounds.Top, threeNinth, bounds.Top);
                            path.AddBezier(
                                new PointF(threeNinth, bounds.Top),
                                new PointF(threeNinth + 2.0f * hstep, bounds.Top + vstep * 0.5f),
                                new PointF(bounds.Right - 0.5f * hstep, bounds.Top + vstep * 2.0f),
                                new PointF(bounds.Right, bounds.Top + 0.5f * bounds.Height)
                                );
                            path.AddBezier(
                                new PointF(bounds.Right, bounds.Top + 0.5f * bounds.Height),
                                new PointF(bounds.Right - 0.5f * hstep, bounds.Bottom - vstep * 2.0f),
                                new PointF(threeNinth + 2.0f * hstep, bounds.Bottom - vstep * 0.5f),
                                new PointF(threeNinth, bounds.Bottom)
                                );

                            path.AddLine(threeNinth, bounds.Bottom, bounds.Left, bounds.Bottom);
                            path.AddBezier(bounds.Left, bounds.Bottom, twoNinth, bounds.Bottom - oneThird, twoNinth, bounds.Top + oneThird, bounds.Left, bounds.Top);
                            path.CloseFigure();
                        }
                        break;
                    case BaseGateShape.Not:
                        {
                            path.AddLines(
                                new PointF[]
                                    {
                                        new PointF(bounds.Left, bounds.Top),
                                        new PointF(bounds.Left,bounds.Bottom),
                                        new PointF(bounds.Right, bounds.Top + bounds.Height / 2.0f),
                                    }
                                );
                            path.CloseFigure();
                        }
                        break;
                }

                using (Pen shapePen = new Pen(Color.Black, 3))
                {
                    g.FillPath(Brushes.White, path);
                    g.DrawPath(shapePen, path);

                    if (shape == BaseGateShape.Xor)
                    {
                        const float off = 6;
                        g.DrawBezier(shapePen, bounds.Left - off, bounds.Bottom, twoNinth - off, bounds.Bottom - oneThird, twoNinth - off, bounds.Top + oneThird, bounds.Left - off, bounds.Top);
                    }
                }
            }               

            // NegOut
            if (negative)
            {
                using (Pen shapePen = new Pen(Color.Black, 3))
                {
                    float cs = 6.0f * r.Width / 90.0f;
                    RectangleF circle = new RectangleF(r.Left + (7.0f / 9.0f) * r.Width - cs + 1.5f, r.Top + (r.Height / 2.0f) - cs, 2.0f * cs, 2.0f * cs);
                    g.FillEllipse(Brushes.White, circle);
                    g.DrawEllipse(shapePen, circle);
                }
            }
        }
    }
}

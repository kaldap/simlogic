using System;
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
        private Pin mStartPin = null;

        private void StartDrawing(Pin p)
        {
            mStartPin = p;            
        }
        private void UpdateDrawing()
        {
            try
            {
                if (mStartPin != null)
                {
                    if (mSelectedPin != null)
                    {
                        if (mStartPin.IsInput == mSelectedPin.IsInput)
                            throw new InvalidOperationException("Lze spojit pouze vstupní pin s výstupním!");

                        if (mStartPin.IsInput)
                            mSelectedPin.Join(mStartPin);
                        else
                            mStartPin.Join(mSelectedPin);

                        mSelectedPin = null;
                        mStartPin = null;
                    }
                }
            }
            catch (Exception e)
            {
                CancelDrawing();
                MessageBox.Show(e.Message, "Nelze vytvořit spoj!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void CancelDrawing()
        {
            mSelectedPin = null;
            mStartPin = null;
        }

        private const float cDrawPinRadius = 6.0f;
        private const float cDrawDoublePinRadius = 2.0f * cDrawPinRadius;
        private const float cDrawCorner = 30.0f;

        public void DrawPin(Graphics g, Brush b, PointF pinPos)
        {
            RectangleF rect = new RectangleF(pinPos.X - cDrawPinRadius, pinPos.Y - cDrawPinRadius, cDrawDoublePinRadius, cDrawDoublePinRadius);
            g.FillEllipse(b, rect);
            g.DrawEllipse(mPinPen, rect);
        }
        public void DrawWire(Graphics g, Pen p, PointF a, PointF b, bool aIsInput)
        {
            if(aIsInput)
            {
                // B must be Input
                (a, b) = (b, a);
            }

            a = new PointF(a.X - 5, a.Y);
            PointF ctrlA = new PointF(a.X + 75, a.Y);
            PointF ctrlB = new PointF(b.X - 75, b.Y);
            b = new PointF(b.X + 5, b.Y);

            g.DrawBezier(mShadowPen, a, ctrlA, ctrlB, b);
            g.DrawBezier(p, a, ctrlA, ctrlB, b);
        }

        public void DrawCorners(Graphics g, Pen p, RectangleF rect)
        {
            PointF[] lt = new PointF[] { new PointF(rect.Left, rect.Top + cDrawCorner), new PointF(rect.Left, rect.Top), new PointF(rect.Left + cDrawCorner, rect.Top) };
            PointF[] rt = new PointF[] { new PointF(rect.Right - cDrawCorner, rect.Top), new PointF(rect.Right, rect.Top), new PointF(rect.Right, rect.Top + cDrawCorner) };
            PointF[] lb = new PointF[] { new PointF(rect.Left, rect.Bottom - cDrawCorner), new PointF(rect.Left, rect.Bottom), new PointF(rect.Left + cDrawCorner, rect.Bottom) };
            PointF[] rb = new PointF[] { new PointF(rect.Right - cDrawCorner, rect.Bottom), new PointF(rect.Right, rect.Bottom), new PointF(rect.Right, rect.Bottom - cDrawCorner) };

            g.DrawLines(mShadowPen, lt);
            g.DrawLines(mShadowPen, rt);
            g.DrawLines(mShadowPen, lb);
            g.DrawLines(mShadowPen, rb);

            g.DrawLines(p, lt);
            g.DrawLines(p, rt);
            g.DrawLines(p, lb);
            g.DrawLines(p, rb);
        }
    }
}

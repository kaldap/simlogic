using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CircuitBoard
{
    public partial class Scheme : UserControl
    {
        private List<IItem> mItems = new List<IItem>();
        
        private float mZoom = 1;
        private PointF mTopLeft = new PointF(0, 0);
        private int mGrid = 20;

        private Font mCalcFont = null;
        private StringFormat mCalcFormat = null;
        private string mCalcString = "";

        private Pen mOnPen;
        private Pen mOffPen;
        private Pen mSelectedPen;
        private Pen mShadowPen;
        private Pen mPinPen;

        public Scheme()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            mCalcFont = new Font(Font.FontFamily, 26.0f, FontStyle.Bold, GraphicsUnit.Point);
            mCalcFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            mOnPen = new Pen(Color.Gold, 3)
            {
                EndCap = LineCap.Round,
                StartCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };

            mOffPen = new Pen(Color.Cyan, 3)
            {
                EndCap = LineCap.Round,
                StartCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };

            mSelectedPen = new Pen(Color.LimeGreen, 3)
            {
                EndCap = LineCap.Round,
                StartCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };

            mShadowPen = new Pen(Color.Black, 6)
            {
                EndCap = LineCap.Round,
                StartCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };


            mPinPen = new Pen(Color.Black, 3)
            {
                EndCap = LineCap.Round,
                StartCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };

            this.Disposed += new EventHandler(Scheme_Disposed);
        }

        void Scheme_Disposed(object sender, EventArgs e)
        {
            mOnPen.Dispose();
            mOffPen.Dispose();
            mSelectedPen.Dispose();
            mShadowPen.Dispose();
            mPinPen.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            RectangleF windowRect = ClientRectangle;

            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            // Draw BG
            e.Graphics.Clear(BackColor);

            if (mCalculations >= 0)
            {
                float percent = ((float)mCalculations) / ((float)mCalculationsTotal);
                string s = string.Format(mCalcString, mCalculations, mCalculationsTotal, (int)Math.Round(percent * 100));
                e.Graphics.DrawString(s, mCalcFont, Brushes.Black, windowRect, mCalcFormat);
                return;
            }

            // Zoom and Draw Controls
            e.Graphics.TranslateTransform(-mTopLeft.X, -mTopLeft.Y);
            e.Graphics.ScaleTransform(mZoom, mZoom);

            // Draw Wires
            foreach (IItem item in mItems)
            {
                try
                {
                    foreach (Pin p in item.Outputs)
                    {
                        if (mSelectedWire == null)
                            foreach (Pin j in p.Joints)
                                DrawWire(e.Graphics, p.State ? mOnPen : mOffPen, p.Position, j.Position, false);
                        else
                        {
                            Pen pen;
                            foreach (Pin j in p.Joints)
                            {
                                if (mSelectedWire == j)
                                    pen = mSelectedPen;
                                else
                                {
                                    if (j.ContainJoint(mSelectedWire))
                                        pen = mSelectedPen;
                                    else
                                        pen = p.State ? mOnPen : mOffPen;
                                }

                                DrawWire(e.Graphics, pen, p.Position, j.Position, false);
                            }
                        }

                        /*if (p.Joints.Length > 1)
                            DrawJunction(e.Graphics, p.State ? Brushes.OrangeRed : Brushes.DodgerBlue, p.Position);*/
                    }
                }
                catch (PinDoesNotExistException)
                { }
            }
            
            // Draw Items
            foreach (IItem item in mItems)
                item.Paint(e.Graphics);

            if (mSelectedItem != null)
            {
                RectangleF r = mSelectedItem.Rect;
                r.Inflate(cDoublePinRadius, cDoublePinRadius);
                DrawCorners(e.Graphics, mSelectedPen, r);
            }

            // Draw Pins
            foreach (IItem item in mItems)
            {
                try
                {
                    foreach (Pin p in item.Inputs)
                        DrawPin(e.Graphics, p == mSelectedWire ? Brushes.LimeGreen : Brushes.LightGray, p.Position);
                    foreach (Pin p in item.Outputs)
                        DrawPin(e.Graphics, p == mSelectedWire ? Brushes.LimeGreen : Brushes.LightGray, p.Position);
                }
                catch (PinDoesNotExistException)
                { }
            }

            // Draw Acutal Wire
            if (mStartPin != null)
            {
                PointF schemePt = GetSchemePoint(PointToClient(Control.MousePosition));
                using (Pen p = new Pen(Color.Orchid, 4))
                    DrawWire(e.Graphics, p, mStartPin.Position, schemePt, mStartPin.IsInput);
            }

            e.Graphics.ResetTransform();
        }

        public void Clear()
        {
            if (Busy)
                return;


            mZoom = 1;
            mTopLeft = new PointF(0, 0);
            mGrid = 20;

            mItems.Clear();
            Invalidate();
        }
    }
}

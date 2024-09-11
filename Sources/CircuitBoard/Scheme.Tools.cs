using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using CircuitBoard.Items.Gates;
using CircuitBoard.Items.IOs;
using CircuitBoard.Items.Others;

namespace CircuitBoard
{
    public delegate void SelectionChangedDelegate(object selection);
    public partial class Scheme
    {
        public event SelectionChangedDelegate SelectionChanged;

        private bool mIgnoreRightButton = false;
        private const float cPinRadius = 20.0f;
        private const float cDoublePinRadius = 2.0f * cPinRadius;

        private IItem mSelectedItem = null;
        private IItem mMoveableItem = null;
        private IItem SelectedItem
        {
            set
            {
                mSelectedItem = value;
                Invalidate();
            }
        }
        private Pin mSelectedPin = null;
        private Pin mSelectedWire = null;

        private IItem FindItem(Point position)
        {
            return FindItemInScheme(GetSchemePoint(position));
        }
        private IItem FindItemInScheme(PointF position)
        {
            IItem sitem = null;
            foreach (IItem item in mItems)
            {
                RectangleF rect = item.Rect;
                rect.Inflate(cDoublePinRadius, 0);
                if (rect.Contains(position))
                    sitem = item;
            }

            return sitem;
        }

        #region Create

        private bool mWasDouble = false;
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (Busy)
                return;
            
            if (e.Button == MouseButtons.Left)
            {
                if (mSelectedItem == null && mSelectedPin == null)
                    _Create(GetSchemePoint(e.Location), e.Location);

                if (mSelectedItem != null)
                {
                    PointF pt = mSelectedItem.Center;
                    pt.X += 20;
                    pt.Y += 20;
                    IItem clone = CreateItem(mSelectedItem.GetType(), pt);
                    mMoveableItem = null;
                    mSelectedItem = null;
                    mSelectedPin = null;
                    mWasDouble = true;
                }
            }

            base.OnMouseDoubleClick(e);
        }
        private void _Create(PointF where, Point point)
        {
            if (Busy)
                return;

            createContextMenu.Tag = where;
            createContextMenu.Show(this, point, ToolStripDropDownDirection.BelowRight);
        }
        private void createContextMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            mLast = Point.Empty;
        }
        private IItem CreateItem(Type type, PointF point)
        {
            if (Busy)
                return null;

            IItem item = (IItem)type.GetConstructor(System.Type.EmptyTypes).Invoke(new object[0]);
            item.Center = point;
            mItems.Add(item);
            Invalidate();
            
            return item;
        }
        private void CreateItem(Type type)
        {
            CreateItem(type, (PointF)createContextMenu.Tag);
        }

        private void invertorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(NotGate));
        }
        private void bufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(BufferGate));
        }
        private void aNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(AndGate));
        }
        private void nANDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(NAndGate));
        }
        private void oRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(OrGate));
        }
        private void nORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(NOrGate));
        }
        private void xORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(XOrGate));
        }
        private void xNORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(XNOrGate));
        }
        private void spínačToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(SwitchSource));
        }
        private void generátorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(GeneratorSource));
        }
        private void trvaleHIGH1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(HighSource));
        }
        private void trvaleLOW0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(LowSource));
        }
        private void sondaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(SignalDrain));
        }
        private void signed8bitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(NumberSourceS8));
        }
        private void signed16bitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(NumberSourceS16));
        }
        private void unsigned8bitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(NumberSourceU8));
        }
        private void unsigned16bitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(NumberSourceU16));
        }
        private void signed8bitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(NumberViewS8));
        }
        private void signed16bitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(NumberViewS16));
        }
        private void unsigned8bitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(NumberViewU8));
        }
        private void unsigned16bitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(NumberViewU16));
        }
        private void bitovýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit16Counter));
        }
        private void biToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit8Counter));
        }
        private void bitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit4Counter));
        }
        private void bitReverzníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit16RevCounter));
        }
        private void bitReverzníToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit8RevCounter));
        }
        private void bitReverzníToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit4RevCounter));
        }
        private void decimálníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(DecimalCounter));
        }
        private void decimálníReverzníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(DecimalRevCounter));
        }
        private void uživatelskýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(CustomCounter));
        }
        private void uživatelskýReverzníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(CustomRevCounter));
        }
        private void johnsonůvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(JohnsonCounter));
        }
        private void johnsonůvBitovýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(JohnsonBitCounter));
        }
        private void jednoducháToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(SimpleAdder));
        }
        private void plnáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(FullAdder));
        }
        private void bitováToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit4Adder));
        }
        private void bitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit16Mux));
        }
        private void bitToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit8Mux));
        }
        private void bitToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit4Mux));
        }
        private void bitToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit2Mux));
        }
        private void bitToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit16Demux));
        }
        private void bitToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit8Demux));
        }
        private void bitToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit4Demux));
        }
        private void bitToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Bit2Demux));
        }
        private void rSLatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(RSLatch));
        }
        private void rSClockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(RSClockLatch));
        }
        private void rSFlipFlopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(RSFlipFlop));
        }
        private void jKFlipFlopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(JKFlipFlop));
        }
        private void jKRSFlipFlopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(JKSCFlipFlop));
        }
        private void dLatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(DLatch));
        }
        private void dFlipFlopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(DFlipFlop));
        }
        private void tFlipFlopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(TFlipFlop));
        }
        private void serialParalelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(SPShiftRegister));
        }
        private void parallelSerialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(PSShiftRegister));
        }
        private void bCD1Z10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(BCD2OneFromTen));
        }
        private void z10BCDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(OneFromTen2BCD));
        }
        private void bCD7segDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(BCD2Display));
        }
        private void displejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(Display));
        }
        private void tlačítkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(PulseSource));
        }
        private void popisekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateItem(typeof(CircuitBoard.Items.Others.Label));
        }

        #endregion

        #region Removing

        private void _Disconnect(IItem item)
        {
            item.Disconnect();
        }
        private void _Disconnect(Pin pin)
        {
            pin.DisconnectMe();
        }
        private void _Delete(IItem item)
        {
            _Disconnect(item);
            mItems.Remove(item);
            mSelectedItem = null;
            Invalidate();
        }

        #endregion

        #region Keyboard

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (mCalculations >= 0)
            {
                if (e.KeyCode == Keys.Escape)
                    mCalculations = -1;
                return;
            }

            Point pt = PointToClient(Control.MousePosition);
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, pt.X, pt.Y, 0));
                    break;
                case Keys.Delete:
                    if (mSelectedItem != null)
                        _Delete(mSelectedItem);
                    
                    if (mSelectedWire != null)
                        _Disconnect(mSelectedWire);

                    Invalidate();
                    break;
                case Keys.Escape:
                    CancelDrawing();
                    break;
                case Keys.Right:
                    Sim_NextStep();
                    break;
                case Keys.Left:
                    Sim_Reset();
                    break;
            }

        }

        #endregion

        #region View

        private Point mLast = Point.Empty;
        private Point mMove = Point.Empty;
        private bool mIsMoving = false;
        private PointF GetSchemePoint(Point clientPt)
        {
            return new PointF((clientPt.X + mTopLeft.X) / mZoom, (clientPt.Y + mTopLeft.Y) / mZoom);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Busy)
                return;

            if (e.Button == MouseButtons.Left)
                mMoveableItem = FindItem(e.Location);

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Busy)
                return;

            base.OnMouseUp(e);

            if (e.Clicks > 1)
                return;

            if (mWasDouble)
            {
                mWasDouble = false;
                return;
            }

            bool stop = mMove != Point.Empty;

            mMove = Point.Empty;
            mMoveableItem = null;
            mIsMoving = false;
            SelectedItem = null;
            
            if ((mIgnoreRightButton && e.Button == MouseButtons.Right) || stop)
                return;

            PointF pt = GetSchemePoint(e.Location);

            IItem sitem = FindItem(e.Location);
            if (sitem != null)
            {
                mSelectedPin = sitem.GetNearestPin(pt, cPinRadius);
                if (mSelectedPin == null)
                {
                    if (sitem.OnClick(pt) && !mIsRunning)
                    {
                        string[] recs;
                        Sim_UpdateRecursively(out recs);

                        Invalidate();
                        SelectedItem = null;
                        return;
                    }

                    SelectedItem = sitem;
                }
                else
                {
                    SelectedItem = null;

                    if (e.Button == MouseButtons.Left)
                    {
                        if (mStartPin == null)
                            StartDrawing(mSelectedPin);
                        else
                            UpdateDrawing();
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        mSelectedWire = mSelectedPin;
                    }
                }
            }
            else
            {
                SelectedItem = null;
                mSelectedPin = null;
                mSelectedWire = null;
            }

            if (SelectionChanged != null)
                SelectionChanged(mSelectedItem);

            // Nothing selected
            if (mSelectedItem == null && mSelectedPin == null)
            {
                if (e.Button == MouseButtons.Right)
                    _Create(pt, e.Location);
                else if (e.Button == MouseButtons.Left)
                    CancelDrawing();
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Busy)
                return;

            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Right)
            {
                if (mLast == Point.Empty)
                {
                    mLast = e.Location;
                    return;
                }

                mTopLeft.X -= ((float)(e.X - mLast.X));
                mTopLeft.Y -= ((float)(e.Y - mLast.Y));

                mTopLeft.X = Math.Min(Math.Max(-16000, mTopLeft.X), 16000);
                mTopLeft.Y = Math.Min(Math.Max(-16000, mTopLeft.Y), 16000);

                mLast = e.Location;
                mIgnoreRightButton = true;

                Invalidate();
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if (mLast == Point.Empty)
                {
                    mLast = e.Location;
                    return;
                }

                mZoom += ((float)(e.Y - mLast.Y)) * 0.01f;
                mZoom = Math.Max(0.1f, mZoom);
                mLast = e.Location;
                Invalidate();
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (mMoveableItem != null)
                {
                    if (Math.Abs(e.X - mMove.X) > 2 || Math.Abs(e.Y - mMove.Y) > 2)
                    {
                        if(mIsMoving && mMove != Point.Empty)
                            mMoveableItem.Center = GetSchemePoint(e.Location);
                        mIsMoving = true;
                        mMove = e.Location;
                    }
                }

                Invalidate();
            }
            else
            {
                mIgnoreRightButton = false;
                mLast = Point.Empty;
                mMove = Point.Empty;

                if (mStartPin != null)
                    Invalidate();
            }
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (Busy)
                return;

            base.OnMouseWheel(e);
            mZoom += 0.05f * Math.Sign(e.Delta);
            mZoom = Math.Max(0.1f, mZoom);
            Invalidate();
        }

        #endregion
    }
}

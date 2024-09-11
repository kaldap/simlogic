using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitBoard.Items.Gates
{
    public class TestItem : IItem
    {
        #region IItem Members

        public Pin[] InputPins
        {
            get { return new Pin[0]; }
        }

        public Pin[] OutputPins
        {
            get { return new Pin[0]; }
        }

        public Pin GetPinAt(int x, int y, bool inputPins, bool outputPins)
        {
            return null;
        }

        public void Simulate()
        {
            
        }

        public System.Drawing.RectangleF Rect
        {
            get
            {
                return new System.Drawing.RectangleF(0, 0, 10, 10);
            }
            set
            {
                
            }
        }

        public object Settings
        {
            get { return null; }
        }

        public void Paint(System.Drawing.Graphics g)
        {
            g.FillRectangle(System.Drawing.Brushes.Green, Rect);
        }

        #endregion
    }
}

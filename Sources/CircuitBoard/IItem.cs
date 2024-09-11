using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

namespace CircuitBoard
{
    public enum ItemType
    {
        Gate,
        FlipFlop,
        Wire,
        Input,
        Output
    }

    public interface IItem
    {
        #region Pins

        Pin[] Inputs { get; }
        Pin[] Outputs { get; }
        bool IsGenerator { get; }
        bool IsDrain { get; }

        Pin this[bool input, int index] { get; }

        void Disconnect();

        #endregion

        #region Simulation

        void Update();
        void CalculateTruthTable(List<IItem> justUpdated, List<Pin> recursion, Pin sourcePin);
        void Restart();
        
        #endregion

        #region Appearance

        RectangleF Rect { get; }
        PointF Center { get; set; }
        void Paint(Graphics g);
        PointF GetPinPosition(Pin p);
        Pin GetNearestPin(PointF point, float radius);
        bool OnClick(PointF point);        

        #endregion

        #region Info

        string Name { get; set; }
        
        #endregion

        #region Save

        void Save(BinaryWriter writer);
        void Load(BinaryReader reader);

        #endregion
    }

    public class PinDoesNotExistException : Exception
    {
        public PinDoesNotExistException(string pin)
            : base(pin)
        {
        }
    }
}

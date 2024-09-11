using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitBoard
{
    public interface IStorage
    {
        void AddCol(object value);
        void NewRow();
    }
}

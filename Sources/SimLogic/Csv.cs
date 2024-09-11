using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimLogic
{
    public class Csv : IDisposable, CircuitBoard.IStorage
    {
        private TextWriter mWriter = null;
        private List<string> mRow = new List<string>();
        private const string split = "\t";

        public Csv(string file)
        {
            mWriter = new StreamWriter(file);
        }
        public void WriteLine(params object[] data)
        {
            string line = "";
            foreach (object o in data)
                line += o.ToString() + split;
            mWriter.WriteLine(line);
        }
        public void WriteLine(string[] data)
        {
            string line = "";
            foreach (string s in data)
                line += s + split;
            mWriter.WriteLine(line);
        }
        public void WriteLine()
        {
            mWriter.WriteLine();
        }

        public void AddCol(object value)
        {
            if (value == null)
                mRow.Add("");
            else
                mRow.Add(value.ToString());
        }
        public void NewRow()
        {
            mWriter.WriteLine(string.Join(split, mRow.ToArray()));
            mWriter.Flush();
            mRow.Clear();
        }

        #region IDisposable Members

        public void Dispose()
        {
            mWriter.Close();
            mWriter.Dispose();
            mWriter = null;
        }

        #endregion
    }
}

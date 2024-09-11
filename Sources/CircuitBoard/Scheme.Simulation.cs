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
        private bool mIsRunning = false;
        private int mCalculations = -1;
        private int mCalculationsTotal = 0;
        private uint mStep = 0;

        public uint Step
        {
            get
            {
                return mStep;
            }
        }
        public bool Simulation
        {
            get
            {
                return mIsRunning;
            }
        }

        public bool Busy
        {
            get
            {
                return mCalculations >= 0;
            }
        }
        public void CancelCalc()
        {
            mCalculations = -1;
        }

        public void Sim_TruthTable(ref TT_Row row, Pin[] outputs)
        {
            TriState[] states = new TriState[outputs.Length];
            TriState state;
            bool warn = false;

            Sim_Steps(900);
            for (int j = 0; j < outputs.Length; j++)
                states[j] = outputs[j].State ? TriState.True : TriState.False;

            for (int i = 0; i < 100; i++)
            {
                Sim_NextStep();
                for (int j = 0; j < outputs.Length; j++)
                {
                    state = outputs[j].State ? TriState.True : TriState.False;
                    if (state != states[j])
                    {
                        states[j] = TriState.Unknown;
                        warn = true;
                    }
                }
            }

            row.OutputStates.AddRange(states);
            row.Warning = warn;
        }
        public void Sim_Reset()
        {
            mIsRunning = false;
            mStep = 0;

            foreach (IItem item in mItems)
            {
                foreach (Pin p in item.Outputs)
                    p.Reset();
                item.Restart();
            }

            foreach (IItem item in mItems)
            {
                if (!item.IsGenerator)
                {
                    item.Update();
                    item.Update();
                }
            }

            Invalidate();
        }
        public void Sim_Steps(uint steps)
        {
            mStep += steps;
            mIsRunning = true;
            for (uint i = 0; i < steps; i++)
            {
                foreach (IItem item in mItems)
                {
                    if (item.IsGenerator)
                        item.Update();
                }

                foreach (IItem item in mItems)
                {
                    if (!item.IsGenerator)
                        item.Update();
                }
            }

            Invalidate();
        }
        public void Sim_NextStep()
        {
            Sim_Steps(1);
        }

        public void TruthTable_Get(out TT_Info info, out TT_Row[] rows)
        {
            try
            {
                mCalcString = "Generuji tabulku pravdivostních hodnot... {2} %\n [ {0} / {1} ]\n\nOperaci lze přerušit stiskem ESC.";

                info = new TT_Info();
                List<Items.IOs.SwitchSource> ip = new List<Items.IOs.SwitchSource>();
                List<Pin> op = new List<Pin>();

                foreach (IItem ii in mItems)
                {
                    if (ii.IsDrain)
                    {
                        info.OutputNames.Add(ii.Name);
                        foreach (Pin p in ii.Inputs)
                            op.Add(p);
                    }

                    if (ii.IsGenerator)
                    {
                        if (ii.GetType() != typeof(Items.IOs.SwitchSource))
                        {
                            MessageBox.Show(this, "Obvod nesmí obsahovat jiné vstupy než spínače!\nProsím nahraďte jej spínačem.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            info = null;
                            rows = null;
                            return;
                        }
                        info.InputNames.Add(ii.Name);
                        ip.Add((Items.IOs.SwitchSource)ii);
                    }
                }

                if (ip.Count > 16)
                {
                    MessageBox.Show(this, "Obvod obsahuje příliš mnoho vstupů pro generování pravdivostní tabulky!\nMaximální počet vstupů je 16.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    info = null;
                    rows = null;
                    return;
                }

                // Set Inputs
                int l = (int)Math.Pow(2, ip.Count);
                List<TT_Row> _rows = new List<TT_Row>(l);

                mCalculations = 0;
                mCalculationsTotal = l;

                for (int i = 0; i < l; i++)
                {
                    if (mCalculations < 0)
                        throw new InvalidOperationException("USERCANC");

                    mCalculations = i;

                    if (i % 100 == 0)
                    {
                        Invalidate();
                        Application.DoEvents();
                    }

                    Sim_Reset();

                    TT_Row row = new TT_Row();

                    for (int j = 0; j < ip.Count; j++)
                    {
                        bool b = (i & (1 << j)) != 0;
                        ip[j].State = b;
                        row.InputStates.Add(b);
                    }

                    Sim_TruthTable(ref row, op.ToArray());

                    _rows.Add(row);
                }

                rows = _rows.ToArray();
                mCalculations = -1;
                Invalidate();
            }
            catch
            {
                mCalculations = -1;
                Invalidate();
                throw;
            }
        }
        public void Analysis_Get(IStorage target, uint firstStep, uint stepCount)
        {
            try
            {
                List<string> iid = new List<string>();
                List<string> oid = new List<string>();
                List<Pin> ip = new List<Pin>();
                List<Pin> op = new List<Pin>();

                foreach (IItem ii in mItems)
                {
                    if (ii.IsDrain)
                    {
                        oid.Add(ii.Name);
                        op.AddRange(ii.Inputs);
                    }
                    else if (ii.IsGenerator)
                    {
                        iid.Add(ii.Name);
                        ip.AddRange(ii.Outputs);
                    }
                }

                foreach (string s in iid)
                    target.AddCol(s);

                target.AddCol(null);

                foreach (string s in oid)
                    target.AddCol(s);

                target.NewRow();

                Sim_Reset();

                mCalculations = 0;
                mCalculationsTotal = (int)firstStep;
                mCalcString = "Přeskakuji na počáteční krok... {2} %\n [ {0} / {1} ]\n\nOperaci lze přerušit stiskem ESC.";
                for (uint i = 0; i < firstStep; i++)
                {
                    if (mCalculations < 0)
                        throw new InvalidOperationException("USERCANC");

                    mCalculations = (int)i;

                    if (i % 100 == 0)
                    {
                        Invalidate();
                        Application.DoEvents();
                    }

                    Sim_NextStep();
                }

                mCalculationsTotal = (int)stepCount;
                mCalcString = "Generuji tabulku průběhů... {2} %\n [ {0} / {1} ]\n\nOperaci lze přerušit stiskem ESC.";
                for (uint i = 0; i < stepCount; i++)
                {
                    if (mCalculations < 0)
                        throw new InvalidOperationException("USERCANC");

                    mCalculations = (int)i;

                    if (i % 100 == 0)
                    {
                        Invalidate();
                        Application.DoEvents();
                    }

                    foreach (Pin p in ip)
                        target.AddCol(p.State ? "1" : "0");
                    target.AddCol(null);
                    foreach (Pin p in op)
                        target.AddCol(p.State ? "1" : "0");

                    target.NewRow();

                    Sim_NextStep();
                }

                mCalculations = -1;
                Invalidate();
            }
            catch
            {
                mCalculations = -1;
                Invalidate();
                throw;
            }
        }

        public void Sim_UpdateRecursively(out string[] recursions)
        {
            List<string> recs = new List<string>();
            foreach (IItem item in mItems)
            {
                if (!item.IsGenerator)
                    continue;

                List<IItem> its = new List<IItem>();
                List<Pin> pins = new List<Pin>();

                item.CalculateTruthTable(its, pins, null);

                foreach (Pin p in pins)
                    recs.Add(p.ToString());
            }

            recursions = recs.ToArray();
        }
    }

    public enum TriState
    {
        Unknown = 0,
        True = 1,
        False = 2,
    }
    public class TT_Row
    {
        public List<bool> InputStates = new List<bool>();
        public List<TriState> OutputStates = new List<TriState>();
        public bool Warning = false;
    }
    public class TT_Info
    {
        public List<string> InputNames = new List<string>();
        public List<string> OutputNames = new List<string>();
    }
}

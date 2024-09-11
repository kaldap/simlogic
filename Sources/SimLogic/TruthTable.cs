using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CircuitBoard;

namespace SimLogic
{
    public partial class TruthTable : Form
    {
        public TruthTable(TT_Row[] rows, TT_Info info)
        {
            InitializeComponent();

            int i = 1;
            int cw = table.Columns.Add(new DataGridViewImageColumn(false));
            table.Columns[cw].HeaderText = "Stav";

            foreach (string s in info.InputNames)
            {
                i++;
                cw = table.Columns.Add(new DataGridViewImageColumn(false));
                table.Columns[cw].HeaderText = s;
            }
            int sp = table.Columns.Add("colsplit", "   ");
            table.Columns[sp].MinimumWidth = 20;
            table.Columns[sp].Resizable = DataGridViewTriState.False;
            foreach (string s in info.OutputNames)
            {
                i++;
                cw = table.Columns.Add(new DataGridViewImageColumn(false));
                table.Columns[cw].HeaderText = s;
            }

            int ri = 0;
            foreach (TT_Row r in rows)
            {
                i = 1;
                ri = table.Rows.Add();
                table[0, ri].Value = r.Warning ? Properties.Resources.warn : Properties.Resources.ok;
                table[0, ri].Tag = r.Warning ? "W!" : "OK";
                table[0, ri].ToolTipText = r.Warning ? "varování" : "OK";
                table[0, ri].Style.BackColor = r.Warning ? Color.Goldenrod : Color.LightGreen;

                foreach (bool b in r.InputStates)
                {
                    table[i, ri].Value = b ? Properties.Resources.hi : Properties.Resources.lo;
                    table[i, ri].Tag = b ? "1" : "0";
                    table[i, ri].ToolTipText = b ? "Úroveň HIGH (1)" : "Úroveň LOW (0)";
                    table[i, ri].Style.BackColor = b ? Color.Yellow : Color.LightBlue;
                    i++;
                }

                table[i, ri].Style.BackColor = Color.Gray;
                table[i, ri].Tag = null;

                i++;
                foreach (TriState s in r.OutputStates)
                {
                    switch (s)
                    {
                        case TriState.True:
                            table[i, ri].Value = Properties.Resources.hi;
                            table[i, ri].Tag = "1";
                            table[i, ri].ToolTipText = "Úroveň HIGH (1)";
                            table[i, ri].Style.BackColor = Color.Yellow;
                            break;
                        case TriState.False:
                            table[i, ri].Value = Properties.Resources.lo;
                            table[i, ri].Tag = "0";
                            table[i, ri].ToolTipText = "Úroveň LOW (0)";
                            table[i, ri].Style.BackColor = Color.LightBlue;
                            break;
                        case TriState.Unknown:
                            table[i, ri].Value = Properties.Resources.unknown;
                            table[i, ri].Tag = "?";
                            table[i, ri].ToolTipText = "Nestabilní (?)";
                            table[i, ri].Style.BackColor = Color.Goldenrod;
                            break;
                    }
                    i++;
                }
            }
        }

        private void uložitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (csvDialog.ShowDialog() != DialogResult.OK)
                return;

            using (Csv csv = new Csv(csvDialog.FileName))
            {
                foreach (DataGridViewColumn col in table.Columns)
                    csv.AddCol(col.HeaderCell.Tag);
                csv.NewRow();

                foreach (DataGridViewRow row in table.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                        csv.AddCol(cell.Tag);
                    csv.NewRow();
                }
            }
        }
        private void zavřítToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

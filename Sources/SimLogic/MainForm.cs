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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            splitContainer1.Panel2Collapsed = true;
            timeTimer_Tick(this, null);
            ukoncitSimulaciToolStripMenuItem_Click(this, null);   
        }

        private void scheme1_SelectionChanged(object selection)
        {
            if(selection != null)
                propertyGrid.SelectedObject = selection;

            splitContainer1.Panel2Collapsed = selection == null;
        }
        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            scheme1.Invalidate();
        }

        private void oAplikaciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }
        private void konecToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            konecToolStripMenuItem_Click(sender, e);
        }

        private void generovatPravdivostníTabulkuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scheme1.Busy)
                return;

            try
            {
                TT_Row[] rows;
                TT_Info info;

                scheme1.TruthTable_Get(out info, out rows);

                if (info == null || rows == null)
                    return;

                TruthTable tt = new TruthTable(rows, info);
                tt.ShowDialog();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(InvalidOperationException) && ex.Message == "USERCANC")
                    MessageBox.Show("Operace byla přerušena uživatelem!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Operace selhala chybou: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void generovatGrafLogickýchHodnotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scheme1.Busy)
                return;

            try
            {
                using (AnalysisSettings ansett = new AnalysisSettings())
                {
                    if (ansett.ShowDialog() != DialogResult.OK)
                        return;

                    if (csvDialog.ShowDialog() != DialogResult.OK)
                        return;

                    using (Csv csv = new Csv(csvDialog.FileName))
                    {
                        scheme1.Analysis_Get(csv, ansett.start, ansett.count);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(InvalidOperationException) && ex.Message == "USERCANC")
                    MessageBox.Show("Operace byla přerušena uživatelem!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Operace selhala chybou: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ukoncitSimulaciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scheme1.Busy)
                return;

            animTimer.Enabled = false;
            scheme1.Sim_Reset();
        }
        private void dalšíKrokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scheme1.Busy)
                return;

            scheme1.Sim_NextStep();
        }
        private void velmiPomaluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            animTimer.Interval = 1000;
            animTimer.Enabled = true;
        }
        private void pomaluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            animTimer.Interval = 750;
            animTimer.Enabled = true;
        }
        private void středněToolStripMenuItem_Click(object sender, EventArgs e)
        {
            animTimer.Interval = 500;
            animTimer.Enabled = true;
        }
        private void rychleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            animTimer.Interval = 250;
            animTimer.Enabled = true;
        }
        private void velmiRychleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            animTimer.Interval = 100;
            animTimer.Enabled = true;
        }
        private void nejrychlejiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            animTimer.Interval = 10;
            animTimer.Enabled = true;
        }
        private void načístToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scheme1.Busy)
                return;

            try
            {
                if(openDlg.ShowDialog() != DialogResult.OK)
                    return;

                scheme1.Load(openDlg.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Načítání selhalo chybou " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void uložitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scheme1.Busy)
                return;

            try
            {
                if (saveDlg.ShowDialog() != DialogResult.OK)
                    return;

                scheme1.Save(saveDlg.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ukládádní selhalo chybou " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scheme1.CancelCalc();
            Close();
        }

        private void animTimer_Tick(object sender, EventArgs e)
        {
            dalšíKrokToolStripMenuItem_Click(sender, e);
        }
        private void zastavitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            animTimer.Enabled = false;
        }

        private void novýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scheme1.Busy)
                return;

            if (MessageBox.Show("Opravdu chcete schema vymazat?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            scheme1.Clear();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = MessageBox.Show("Opravdu chcete aplikaci ukončit?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
        }

        private void timeTimer_Tick(object sender, EventArgs e)
        {
            dateTime.Text = DateTime.Now.ToString();
            
            if (scheme1.Simulation)
                simStatus.Text = "Simulace (krok " + scheme1.Step + ") - režim " + (animTimer.Enabled ? "animace" : "krokování");
            else
                simStatus.Text = "Okamžitý mód";
        }

        private void homepageLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://simlogic.cz.cc");
        }
    }
}

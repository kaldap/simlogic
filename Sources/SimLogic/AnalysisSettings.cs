using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimLogic
{
    public partial class AnalysisSettings : Form
    {
        public uint start = 0;
        public uint count = 1000;

        public AnalysisSettings()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void analyse_Click(object sender, EventArgs e)
        {
            start = (uint)stepNUD.Value;
            count = (uint)countNUD.Value;

            start--;

            DialogResult = DialogResult.OK;
        }
    }
}

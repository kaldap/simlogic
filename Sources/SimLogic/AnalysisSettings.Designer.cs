namespace SimLogic
{
    partial class AnalysisSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.stepNUD = new System.Windows.Forms.NumericUpDown();
            this.countNUD = new System.Windows.Forms.NumericUpDown();
            this.analyse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.stepNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Počáteční krok:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Počet kroků:";
            // 
            // stepNUD
            // 
            this.stepNUD.Location = new System.Drawing.Point(136, 5);
            this.stepNUD.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.stepNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.stepNUD.Name = "stepNUD";
            this.stepNUD.Size = new System.Drawing.Size(120, 20);
            this.stepNUD.TabIndex = 2;
            this.stepNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // countNUD
            // 
            this.countNUD.Location = new System.Drawing.Point(136, 31);
            this.countNUD.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.countNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countNUD.Name = "countNUD";
            this.countNUD.Size = new System.Drawing.Size(120, 20);
            this.countNUD.TabIndex = 3;
            this.countNUD.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // analyse
            // 
            this.analyse.Location = new System.Drawing.Point(136, 57);
            this.analyse.Name = "analyse";
            this.analyse.Size = new System.Drawing.Size(120, 23);
            this.analyse.TabIndex = 4;
            this.analyse.Text = "Analyzuj";
            this.analyse.UseVisualStyleBackColor = true;
            this.analyse.Click += new System.EventHandler(this.analyse_Click);
            // 
            // AnalysisSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 93);
            this.Controls.Add(this.analyse);
            this.Controls.Add(this.countNUD);
            this.Controls.Add(this.stepNUD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AnalysisSettings";
            this.Text = "Nastavení analýzy";
            ((System.ComponentModel.ISupportInitialize)(this.stepNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown stepNUD;
        private System.Windows.Forms.NumericUpDown countNUD;
        private System.Windows.Forms.Button analyse;
    }
}
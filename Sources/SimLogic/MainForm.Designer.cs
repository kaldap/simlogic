namespace SimLogic
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.scheme1 = new CircuitBoard.Scheme();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.souborToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.načístToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uložitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.konecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartovatSimulaciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dalšíKrokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.velmiPomaluToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomaluToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.středněToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rychleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.velmiRychleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nejrychlejiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.zastavitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analýzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generovatPravdivostníTabulkuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generovatGrafLogickýchHodnotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konecToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.oAplikaciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animTimer = new System.Windows.Forms.Timer(this.components);
            this.openDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveDlg = new System.Windows.Forms.SaveFileDialog();
            this.csvDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.simStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timeTimer = new System.Windows.Forms.Timer(this.components);
            this.homepageLink = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.scheme1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(576, 489);
            this.splitContainer1.SplitterDistance = 318;
            this.splitContainer1.TabIndex = 1;
            // 
            // scheme1
            // 
            this.scheme1.BackColor = System.Drawing.Color.White;
            this.scheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheme1.Location = new System.Drawing.Point(0, 0);
            this.scheme1.Name = "scheme1";
            this.scheme1.Size = new System.Drawing.Size(318, 489);
            this.scheme1.TabIndex = 0;
            this.scheme1.SelectionChanged += new CircuitBoard.SelectionChangedDelegate(this.scheme1_SelectionChanged);
            // 
            // propertyGrid
            // 
            this.propertyGrid.CommandsVisibleIfAvailable = false;
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.HelpVisible = false;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid.Size = new System.Drawing.Size(254, 489);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.souborToolStripMenuItem,
            this.simulaceToolStripMenuItem,
            this.analýzaToolStripMenuItem,
            this.konecToolStripMenuItem1,
            this.oAplikaciToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(576, 40);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // souborToolStripMenuItem
            // 
            this.souborToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novýToolStripMenuItem,
            this.načístToolStripMenuItem,
            this.uložitToolStripMenuItem,
            this.toolStripSeparator1,
            this.konecToolStripMenuItem});
            this.souborToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_39;
            this.souborToolStripMenuItem.Name = "souborToolStripMenuItem";
            this.souborToolStripMenuItem.Size = new System.Drawing.Size(85, 36);
            this.souborToolStripMenuItem.Text = "&Soubor";
            // 
            // novýToolStripMenuItem
            // 
            this.novýToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_46;
            this.novýToolStripMenuItem.Name = "novýToolStripMenuItem";
            this.novýToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.novýToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.novýToolStripMenuItem.Text = "N&ový";
            this.novýToolStripMenuItem.Click += new System.EventHandler(this.novýToolStripMenuItem_Click);
            // 
            // načístToolStripMenuItem
            // 
            this.načístToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_11;
            this.načístToolStripMenuItem.Name = "načístToolStripMenuItem";
            this.načístToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.načístToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.načístToolStripMenuItem.Text = "&Načíst";
            this.načístToolStripMenuItem.Click += new System.EventHandler(this.načístToolStripMenuItem_Click);
            // 
            // uložitToolStripMenuItem
            // 
            this.uložitToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_12;
            this.uložitToolStripMenuItem.Name = "uložitToolStripMenuItem";
            this.uložitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.uložitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.uložitToolStripMenuItem.Text = "&Uložit";
            this.uložitToolStripMenuItem.Click += new System.EventHandler(this.uložitToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // konecToolStripMenuItem
            // 
            this.konecToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_35;
            this.konecToolStripMenuItem.Name = "konecToolStripMenuItem";
            this.konecToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.konecToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.konecToolStripMenuItem.Text = "&Konec";
            this.konecToolStripMenuItem.Click += new System.EventHandler(this.konecToolStripMenuItem_Click);
            // 
            // simulaceToolStripMenuItem
            // 
            this.simulaceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartovatSimulaciToolStripMenuItem,
            this.dalšíKrokToolStripMenuItem,
            this.animaceToolStripMenuItem});
            this.simulaceToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_02;
            this.simulaceToolStripMenuItem.Name = "simulaceToolStripMenuItem";
            this.simulaceToolStripMenuItem.Size = new System.Drawing.Size(92, 36);
            this.simulaceToolStripMenuItem.Text = "S&imulace";
            // 
            // restartovatSimulaciToolStripMenuItem
            // 
            this.restartovatSimulaciToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_29;
            this.restartovatSimulaciToolStripMenuItem.Name = "restartovatSimulaciToolStripMenuItem";
            this.restartovatSimulaciToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.restartovatSimulaciToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.restartovatSimulaciToolStripMenuItem.Text = "&Restartovat simulaci";
            this.restartovatSimulaciToolStripMenuItem.Click += new System.EventHandler(this.ukoncitSimulaciToolStripMenuItem_Click);
            // 
            // dalšíKrokToolStripMenuItem
            // 
            this.dalšíKrokToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_27;
            this.dalšíKrokToolStripMenuItem.Name = "dalšíKrokToolStripMenuItem";
            this.dalšíKrokToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.dalšíKrokToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.dalšíKrokToolStripMenuItem.Text = "&Další krok";
            this.dalšíKrokToolStripMenuItem.Click += new System.EventHandler(this.dalšíKrokToolStripMenuItem_Click);
            // 
            // animaceToolStripMenuItem
            // 
            this.animaceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.velmiPomaluToolStripMenuItem,
            this.pomaluToolStripMenuItem,
            this.středněToolStripMenuItem,
            this.rychleToolStripMenuItem,
            this.velmiRychleToolStripMenuItem,
            this.nejrychlejiToolStripMenuItem,
            this.toolStripSeparator2,
            this.zastavitToolStripMenuItem});
            this.animaceToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_15;
            this.animaceToolStripMenuItem.Name = "animaceToolStripMenuItem";
            this.animaceToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.animaceToolStripMenuItem.Text = "&Animace";
            // 
            // velmiPomaluToolStripMenuItem
            // 
            this.velmiPomaluToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_05;
            this.velmiPomaluToolStripMenuItem.Name = "velmiPomaluToolStripMenuItem";
            this.velmiPomaluToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.velmiPomaluToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.velmiPomaluToolStripMenuItem.Text = "&Velmi pomalu";
            this.velmiPomaluToolStripMenuItem.Click += new System.EventHandler(this.velmiPomaluToolStripMenuItem_Click);
            // 
            // pomaluToolStripMenuItem
            // 
            this.pomaluToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_06;
            this.pomaluToolStripMenuItem.Name = "pomaluToolStripMenuItem";
            this.pomaluToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.pomaluToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.pomaluToolStripMenuItem.Text = "&Pomalu";
            this.pomaluToolStripMenuItem.Click += new System.EventHandler(this.pomaluToolStripMenuItem_Click);
            // 
            // středněToolStripMenuItem
            // 
            this.středněToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_08;
            this.středněToolStripMenuItem.Name = "středněToolStripMenuItem";
            this.středněToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.středněToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.středněToolStripMenuItem.Text = "&Středně";
            this.středněToolStripMenuItem.Click += new System.EventHandler(this.středněToolStripMenuItem_Click);
            // 
            // rychleToolStripMenuItem
            // 
            this.rychleToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_10;
            this.rychleToolStripMenuItem.Name = "rychleToolStripMenuItem";
            this.rychleToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.rychleToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.rychleToolStripMenuItem.Text = "&Rychle";
            this.rychleToolStripMenuItem.Click += new System.EventHandler(this.rychleToolStripMenuItem_Click);
            // 
            // velmiRychleToolStripMenuItem
            // 
            this.velmiRychleToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_07;
            this.velmiRychleToolStripMenuItem.Name = "velmiRychleToolStripMenuItem";
            this.velmiRychleToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.velmiRychleToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.velmiRychleToolStripMenuItem.Text = "V&elmi rychle";
            this.velmiRychleToolStripMenuItem.Click += new System.EventHandler(this.velmiRychleToolStripMenuItem_Click);
            // 
            // nejrychlejiToolStripMenuItem
            // 
            this.nejrychlejiToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_09;
            this.nejrychlejiToolStripMenuItem.Name = "nejrychlejiToolStripMenuItem";
            this.nejrychlejiToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.nejrychlejiToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.nejrychlejiToolStripMenuItem.Text = "&Nejrychleji";
            this.nejrychlejiToolStripMenuItem.Click += new System.EventHandler(this.nejrychlejiToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(151, 6);
            // 
            // zastavitToolStripMenuItem
            // 
            this.zastavitToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_49;
            this.zastavitToolStripMenuItem.Name = "zastavitToolStripMenuItem";
            this.zastavitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.zastavitToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.zastavitToolStripMenuItem.Text = "&Zastavit";
            this.zastavitToolStripMenuItem.Click += new System.EventHandler(this.zastavitToolStripMenuItem_Click);
            // 
            // analýzaToolStripMenuItem
            // 
            this.analýzaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generovatPravdivostníTabulkuToolStripMenuItem,
            this.generovatGrafLogickýchHodnotToolStripMenuItem});
            this.analýzaToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_16;
            this.analýzaToolStripMenuItem.Name = "analýzaToolStripMenuItem";
            this.analýzaToolStripMenuItem.Size = new System.Drawing.Size(89, 36);
            this.analýzaToolStripMenuItem.Text = "&Analýza";
            // 
            // generovatPravdivostníTabulkuToolStripMenuItem
            // 
            this.generovatPravdivostníTabulkuToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_20;
            this.generovatPravdivostníTabulkuToolStripMenuItem.Name = "generovatPravdivostníTabulkuToolStripMenuItem";
            this.generovatPravdivostníTabulkuToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.generovatPravdivostníTabulkuToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.generovatPravdivostníTabulkuToolStripMenuItem.Text = "&Generovat pravdivostní tabulku";
            this.generovatPravdivostníTabulkuToolStripMenuItem.Click += new System.EventHandler(this.generovatPravdivostníTabulkuToolStripMenuItem_Click);
            // 
            // generovatGrafLogickýchHodnotToolStripMenuItem
            // 
            this.generovatGrafLogickýchHodnotToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_19;
            this.generovatGrafLogickýchHodnotToolStripMenuItem.Name = "generovatGrafLogickýchHodnotToolStripMenuItem";
            this.generovatGrafLogickýchHodnotToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.generovatGrafLogickýchHodnotToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.generovatGrafLogickýchHodnotToolStripMenuItem.Text = "G&enerovat tabulku průběhu";
            this.generovatGrafLogickýchHodnotToolStripMenuItem.Click += new System.EventHandler(this.generovatGrafLogickýchHodnotToolStripMenuItem_Click);
            // 
            // konecToolStripMenuItem1
            // 
            this.konecToolStripMenuItem1.Image = global::SimLogic.Properties.Resources.onebit_35;
            this.konecToolStripMenuItem1.Name = "konecToolStripMenuItem1";
            this.konecToolStripMenuItem1.Size = new System.Drawing.Size(80, 36);
            this.konecToolStripMenuItem1.Text = "&Konec";
            this.konecToolStripMenuItem1.Click += new System.EventHandler(this.konecToolStripMenuItem1_Click);
            // 
            // oAplikaciToolStripMenuItem
            // 
            this.oAplikaciToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.oAplikaciToolStripMenuItem.Image = global::SimLogic.Properties.Resources.onebit_38;
            this.oAplikaciToolStripMenuItem.Name = "oAplikaciToolStripMenuItem";
            this.oAplikaciToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.oAplikaciToolStripMenuItem.ShowShortcutKeys = false;
            this.oAplikaciToolStripMenuItem.Size = new System.Drawing.Size(97, 36);
            this.oAplikaciToolStripMenuItem.Text = "&O Aplikaci";
            this.oAplikaciToolStripMenuItem.Click += new System.EventHandler(this.oAplikaciToolStripMenuItem_Click);
            // 
            // animTimer
            // 
            this.animTimer.Interval = 1000;
            this.animTimer.Tick += new System.EventHandler(this.animTimer_Tick);
            // 
            // openDlg
            // 
            this.openDlg.DefaultExt = "sls";
            this.openDlg.Filter = "SLS Soubory (*.sls)|*.sls";
            // 
            // saveDlg
            // 
            this.saveDlg.DefaultExt = "sls";
            this.saveDlg.Filter = "SLS Soubory (*.sls)|*.sls";
            // 
            // csvDialog
            // 
            this.csvDialog.DefaultExt = "csv";
            this.csvDialog.Filter = "Tabulka CSV|*.csv|Všechny soubory|*.*";
            this.csvDialog.Title = "Exportovat do CSV";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simStatus,
            this.dateTime,
            this.homepageLink});
            this.statusStrip1.Location = new System.Drawing.Point(0, 529);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(576, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // simStatus
            // 
            this.simStatus.Name = "simStatus";
            this.simStatus.Size = new System.Drawing.Size(22, 17);
            this.simStatus.Text = "???";
            // 
            // dateTime
            // 
            this.dateTime.Name = "dateTime";
            this.dateTime.Size = new System.Drawing.Size(400, 17);
            this.dateTime.Spring = true;
            this.dateTime.Text = "???";
            this.dateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timeTimer
            // 
            this.timeTimer.Enabled = true;
            this.timeTimer.Interval = 500;
            this.timeTimer.Tick += new System.EventHandler(this.timeTimer_Tick);
            // 
            // homepageLink
            // 
            this.homepageLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.homepageLink.IsLink = true;
            this.homepageLink.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.homepageLink.Name = "homepageLink";
            this.homepageLink.Size = new System.Drawing.Size(103, 17);
            this.homepageLink.Text = "http://simlogic.cz.cc";
            this.homepageLink.VisitedLinkColor = System.Drawing.Color.Blue;
            this.homepageLink.Click += new System.EventHandler(this.homepageLink_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 551);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "SimLogic";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CircuitBoard.Scheme scheme1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem souborToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem načístToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uložitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartovatSimulaciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dalšíKrokToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem velmiPomaluToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomaluToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem středněToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rychleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem velmiRychleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analýzaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generovatPravdivostníTabulkuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generovatGrafLogickýchHodnotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konecToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem oAplikaciToolStripMenuItem;
        private System.Windows.Forms.Timer animTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem zastavitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openDlg;
        private System.Windows.Forms.SaveFileDialog saveDlg;
        private System.Windows.Forms.ToolStripMenuItem novýToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog csvDialog;
        private System.Windows.Forms.ToolStripMenuItem nejrychlejiToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel simStatus;
        private System.Windows.Forms.ToolStripStatusLabel dateTime;
        private System.Windows.Forms.Timer timeTimer;
        private System.Windows.Forms.ToolStripStatusLabel homepageLink;
    }
}


namespace FOnlineScalex
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStripMain = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            infoToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            howToUseToolStripMenuItem = new ToolStripMenuItem();
            btnStop = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnSetOutDir = new Button();
            btnSetInDir = new Button();
            tboxOutDir = new TextBox();
            tboxInputDir = new TextBox();
            lblInDir = new Label();
            lblOutDir = new Label();
            cboxRecursive = new CheckBox();
            gboxMain = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            lblCurrProc = new Label();
            tboxCurrProc = new TextBox();
            pboxCurrentFrame = new PictureBox();
            pictureBox1 = new PictureBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblPrecision = new Label();
            btnGo = new Button();
            cboxAlgo = new ComboBox();
            lblAlgo = new Label();
            numericPrecision = new NumericUpDown();
            progBar = new ProgressBar();
            menuStripMain.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            gboxMain.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pboxCurrentFrame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericPrecision).BeginInit();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, infoToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(500, 24);
            menuStripMain.TabIndex = 11;
            menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(93, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // infoToolStripMenuItem
            // 
            infoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem, howToUseToolStripMenuItem });
            infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            infoToolStripMenuItem.Size = new Size(40, 20);
            infoToolStripMenuItem.Text = "Info";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(136, 22);
            aboutToolStripMenuItem.Text = "About";
            // 
            // howToUseToolStripMenuItem
            // 
            howToUseToolStripMenuItem.Name = "howToUseToolStripMenuItem";
            howToUseToolStripMenuItem.Size = new Size(136, 22);
            howToUseToolStripMenuItem.Text = "How To Use";
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Image = Properties.Resources.stop;
            btnStop.ImageAlign = ContentAlignment.MiddleLeft;
            btnStop.Location = new Point(355, 32);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(136, 23);
            btnStop.TabIndex = 9;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.81884F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.18116F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 141F));
            tableLayoutPanel1.Controls.Add(btnSetOutDir, 2, 0);
            tableLayoutPanel1.Controls.Add(btnSetInDir, 2, 1);
            tableLayoutPanel1.Controls.Add(tboxOutDir, 1, 0);
            tableLayoutPanel1.Controls.Add(tboxInputDir, 1, 1);
            tableLayoutPanel1.Controls.Add(lblInDir, 0, 0);
            tableLayoutPanel1.Controls.Add(lblOutDir, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(494, 78);
            tableLayoutPanel1.TabIndex = 13;
            // 
            // btnSetOutDir
            // 
            btnSetOutDir.Dock = DockStyle.Fill;
            btnSetOutDir.Image = Properties.Resources.dir_icon_opened;
            btnSetOutDir.ImageAlign = ContentAlignment.MiddleLeft;
            btnSetOutDir.Location = new Point(355, 3);
            btnSetOutDir.Name = "btnSetOutDir";
            btnSetOutDir.Size = new Size(136, 33);
            btnSetOutDir.TabIndex = 3;
            btnSetOutDir.Text = "Open...";
            btnSetOutDir.UseVisualStyleBackColor = true;
            btnSetOutDir.Click += btnSetOutDir_Click;
            // 
            // btnSetInDir
            // 
            btnSetInDir.Dock = DockStyle.Fill;
            btnSetInDir.Image = Properties.Resources.dir_icon_opened;
            btnSetInDir.ImageAlign = ContentAlignment.MiddleLeft;
            btnSetInDir.Location = new Point(355, 42);
            btnSetInDir.Name = "btnSetInDir";
            btnSetInDir.Size = new Size(136, 33);
            btnSetInDir.TabIndex = 1;
            btnSetInDir.Text = "Open...";
            btnSetInDir.UseVisualStyleBackColor = true;
            btnSetInDir.Click += btnSetInDir_Click;
            // 
            // tboxOutDir
            // 
            tboxOutDir.Dock = DockStyle.Fill;
            tboxOutDir.Location = new Point(90, 3);
            tboxOutDir.Name = "tboxOutDir";
            tboxOutDir.ReadOnly = true;
            tboxOutDir.Size = new Size(259, 23);
            tboxOutDir.TabIndex = 2;
            tboxOutDir.TextChanged += tboxOutDir_TextChanged;
            // 
            // tboxInputDir
            // 
            tboxInputDir.Dock = DockStyle.Fill;
            tboxInputDir.Location = new Point(90, 42);
            tboxInputDir.Name = "tboxInputDir";
            tboxInputDir.ReadOnly = true;
            tboxInputDir.Size = new Size(259, 23);
            tboxInputDir.TabIndex = 0;
            tboxInputDir.TextChanged += tboxInputDir_TextChanged;
            // 
            // lblInDir
            // 
            lblInDir.AutoSize = true;
            lblInDir.Dock = DockStyle.Fill;
            lblInDir.Location = new Point(3, 0);
            lblInDir.Name = "lblInDir";
            lblInDir.Size = new Size(81, 39);
            lblInDir.TabIndex = 4;
            lblInDir.Text = "Input Directory:";
            // 
            // lblOutDir
            // 
            lblOutDir.AutoSize = true;
            lblOutDir.Dock = DockStyle.Fill;
            lblOutDir.Location = new Point(3, 39);
            lblOutDir.Name = "lblOutDir";
            lblOutDir.Size = new Size(81, 39);
            lblOutDir.TabIndex = 5;
            lblOutDir.Text = "Output Directory:";
            // 
            // cboxRecursive
            // 
            cboxRecursive.AutoSize = true;
            cboxRecursive.Dock = DockStyle.Fill;
            cboxRecursive.Location = new Point(139, 61);
            cboxRecursive.Name = "cboxRecursive";
            cboxRecursive.Size = new Size(210, 22);
            cboxRecursive.TabIndex = 4;
            cboxRecursive.Text = "Recurse through child directories";
            cboxRecursive.UseVisualStyleBackColor = true;
            // 
            // gboxMain
            // 
            gboxMain.Controls.Add(tableLayoutPanel3);
            gboxMain.Controls.Add(tableLayoutPanel2);
            gboxMain.Controls.Add(progBar);
            gboxMain.Controls.Add(tableLayoutPanel1);
            gboxMain.Dock = DockStyle.Fill;
            gboxMain.Location = new Point(0, 24);
            gboxMain.Name = "gboxMain";
            gboxMain.Size = new Size(500, 333);
            gboxMain.TabIndex = 13;
            gboxMain.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.6375542F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.36244F));
            tableLayoutPanel3.Controls.Add(lblCurrProc, 0, 0);
            tableLayoutPanel3.Controls.Add(tboxCurrProc, 1, 0);
            tableLayoutPanel3.Controls.Add(pboxCurrentFrame, 0, 1);
            tableLayoutPanel3.Controls.Add(pictureBox1, 1, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 183);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 25.6880741F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 74.31193F));
            tableLayoutPanel3.Size = new Size(494, 117);
            tableLayoutPanel3.TabIndex = 15;
            // 
            // lblCurrProc
            // 
            lblCurrProc.AutoSize = true;
            lblCurrProc.Dock = DockStyle.Fill;
            lblCurrProc.Location = new Point(3, 0);
            lblCurrProc.Name = "lblCurrProc";
            lblCurrProc.Size = new Size(125, 30);
            lblCurrProc.TabIndex = 0;
            lblCurrProc.Text = "Processing:";
            lblCurrProc.TextAlign = ContentAlignment.MiddleRight;
            lblCurrProc.Click += lblCurrProc_Click;
            // 
            // tboxCurrProc
            // 
            tboxCurrProc.Dock = DockStyle.Fill;
            tboxCurrProc.Location = new Point(134, 3);
            tboxCurrProc.Name = "tboxCurrProc";
            tboxCurrProc.Size = new Size(357, 23);
            tboxCurrProc.TabIndex = 1;
            tboxCurrProc.TextChanged += tboxCurrProc_TextChanged;
            // 
            // pboxCurrentFrame
            // 
            pboxCurrentFrame.Dock = DockStyle.Fill;
            pboxCurrentFrame.Location = new Point(3, 33);
            pboxCurrentFrame.Name = "pboxCurrentFrame";
            pboxCurrentFrame.Size = new Size(125, 81);
            pboxCurrentFrame.TabIndex = 3;
            pboxCurrentFrame.TabStop = false;
            pboxCurrentFrame.Click += pboxCurrentFrame_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = Properties.Resources.gplv3_logo;
            pictureBox1.Location = new Point(134, 33);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(357, 81);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.65546F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.34454F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 141F));
            tableLayoutPanel2.Controls.Add(lblPrecision, 0, 1);
            tableLayoutPanel2.Controls.Add(btnGo, 2, 0);
            tableLayoutPanel2.Controls.Add(cboxAlgo, 1, 0);
            tableLayoutPanel2.Controls.Add(btnStop, 2, 1);
            tableLayoutPanel2.Controls.Add(lblAlgo, 0, 0);
            tableLayoutPanel2.Controls.Add(cboxRecursive, 1, 2);
            tableLayoutPanel2.Controls.Add(numericPrecision, 1, 1);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(3, 97);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tableLayoutPanel2.Size = new Size(494, 86);
            tableLayoutPanel2.TabIndex = 14;
            // 
            // lblPrecision
            // 
            lblPrecision.AutoSize = true;
            lblPrecision.Dock = DockStyle.Fill;
            lblPrecision.Location = new Point(3, 29);
            lblPrecision.Name = "lblPrecision";
            lblPrecision.Size = new Size(130, 29);
            lblPrecision.TabIndex = 14;
            lblPrecision.Text = "Precision:";
            lblPrecision.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnGo
            // 
            btnGo.Image = Properties.Resources.gatling;
            btnGo.ImageAlign = ContentAlignment.MiddleLeft;
            btnGo.Location = new Point(355, 3);
            btnGo.Name = "btnGo";
            btnGo.Size = new Size(136, 23);
            btnGo.TabIndex = 10;
            btnGo.Text = "GO";
            btnGo.UseVisualStyleBackColor = true;
            btnGo.Click += btnGo_Click;
            // 
            // cboxAlgo
            // 
            cboxAlgo.Dock = DockStyle.Fill;
            cboxAlgo.FormattingEnabled = true;
            cboxAlgo.Items.AddRange(new object[] { "Scalex2x", "Scalex3x", "Scalex4x" });
            cboxAlgo.Location = new Point(139, 3);
            cboxAlgo.Name = "cboxAlgo";
            cboxAlgo.Size = new Size(210, 23);
            cboxAlgo.TabIndex = 11;
            // 
            // lblAlgo
            // 
            lblAlgo.AutoSize = true;
            lblAlgo.Dock = DockStyle.Fill;
            lblAlgo.Location = new Point(3, 0);
            lblAlgo.Name = "lblAlgo";
            lblAlgo.Size = new Size(130, 29);
            lblAlgo.TabIndex = 12;
            lblAlgo.Text = "Algorithm:";
            lblAlgo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numericPrecision
            // 
            numericPrecision.DecimalPlaces = 2;
            numericPrecision.Dock = DockStyle.Fill;
            numericPrecision.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericPrecision.Location = new Point(139, 32);
            numericPrecision.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericPrecision.Name = "numericPrecision";
            numericPrecision.Size = new Size(210, 23);
            numericPrecision.TabIndex = 13;
            // 
            // progBar
            // 
            progBar.Dock = DockStyle.Bottom;
            progBar.Location = new Point(3, 300);
            progBar.Name = "progBar";
            progBar.Size = new Size(494, 30);
            progBar.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(500, 357);
            Controls.Add(gboxMain);
            Controls.Add(menuStripMain);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripMain;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FOnlineScalex";
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            gboxMain.ResumeLayout(false);
            gboxMain.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pboxCurrentFrame).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericPrecision).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStripMain;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem howToUseToolStripMenuItem;
        private Button btnStop;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnSetInDir;
        private TextBox tboxInputDir;
        private Button btnSetOutDir;
        private TextBox tboxOutDir;
        private CheckBox cboxRecursive;
        private GroupBox gboxMain;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btnGo;
        private ComboBox cboxAlgo;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lblCurrProc;
        private TextBox tboxCurrProc;
        private ProgressBar progBar;
        private PictureBox pboxCurrentFrame;
        private Label lblInDir;
        private Label lblOutDir;
        private Label lblAlgo;
        private PictureBox pictureBox1;
        private Label lblPrecision;
        private NumericUpDown numericPrecision;
    }
}
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
            tboxInputDir = new TextBox();
            tboxOutputDir = new TextBox();
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
            lblNeqDiff = new Label();
            lblEqDiff = new Label();
            cboxAlgo = new ComboBox();
            lblAlgo = new Label();
            numericEqualDifference = new NumericUpDown();
            numericNequalDifference = new NumericUpDown();
            btnGo = new Button();
            cboxAlpha = new CheckBox();
            progBar = new ProgressBar();
            menuStripMain.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            gboxMain.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pboxCurrentFrame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericEqualDifference).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericNequalDifference).BeginInit();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, infoToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(580, 24);
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
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // howToUseToolStripMenuItem
            // 
            howToUseToolStripMenuItem.Name = "howToUseToolStripMenuItem";
            howToUseToolStripMenuItem.Size = new Size(136, 22);
            howToUseToolStripMenuItem.Text = "How To Use";
            howToUseToolStripMenuItem.Click += howToUseToolStripMenuItem_Click;
            // 
            // btnStop
            // 
            btnStop.Dock = DockStyle.Fill;
            btnStop.Enabled = false;
            btnStop.Image = Properties.Resources.stop;
            btnStop.ImageAlign = ContentAlignment.MiddleLeft;
            btnStop.Location = new Point(276, 61);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(295, 23);
            btnStop.TabIndex = 9;
            btnStop.Text = "Stop";
            btnStop.TextAlign = ContentAlignment.MiddleRight;
            btnStop.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(btnSetOutDir, 2, 1);
            tableLayoutPanel1.Controls.Add(btnSetInDir, 2, 0);
            tableLayoutPanel1.Controls.Add(tboxInputDir, 1, 0);
            tableLayoutPanel1.Controls.Add(tboxOutputDir, 1, 1);
            tableLayoutPanel1.Controls.Add(lblInDir, 0, 0);
            tableLayoutPanel1.Controls.Add(lblOutDir, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(574, 78);
            tableLayoutPanel1.TabIndex = 13;
            // 
            // btnSetOutDir
            // 
            btnSetOutDir.Dock = DockStyle.Fill;
            btnSetOutDir.Image = Properties.Resources.dir_icon_opened2;
            btnSetOutDir.ImageAlign = ContentAlignment.MiddleLeft;
            btnSetOutDir.Location = new Point(373, 42);
            btnSetOutDir.Name = "btnSetOutDir";
            btnSetOutDir.Size = new Size(198, 33);
            btnSetOutDir.TabIndex = 3;
            btnSetOutDir.Text = "Open...";
            btnSetOutDir.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnSetOutDir.UseVisualStyleBackColor = true;
            btnSetOutDir.Click += btnSetOutDir_Click;
            // 
            // btnSetInDir
            // 
            btnSetInDir.Dock = DockStyle.Fill;
            btnSetInDir.Image = Properties.Resources.dir_icon_opened;
            btnSetInDir.ImageAlign = ContentAlignment.MiddleLeft;
            btnSetInDir.Location = new Point(373, 3);
            btnSetInDir.Name = "btnSetInDir";
            btnSetInDir.Size = new Size(198, 33);
            btnSetInDir.TabIndex = 1;
            btnSetInDir.Text = "Open...";
            btnSetInDir.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnSetInDir.UseVisualStyleBackColor = true;
            btnSetInDir.Click += btnSetInDir_Click;
            // 
            // tboxInputDir
            // 
            tboxInputDir.Dock = DockStyle.Fill;
            tboxInputDir.Location = new Point(108, 3);
            tboxInputDir.Name = "tboxInputDir";
            tboxInputDir.ReadOnly = true;
            tboxInputDir.Size = new Size(259, 23);
            tboxInputDir.TabIndex = 2;
            tboxInputDir.TextChanged += tboxInputDir_TextChanged;
            // 
            // tboxOutputDir
            // 
            tboxOutputDir.Dock = DockStyle.Fill;
            tboxOutputDir.Location = new Point(108, 42);
            tboxOutputDir.Name = "tboxOutputDir";
            tboxOutputDir.ReadOnly = true;
            tboxOutputDir.Size = new Size(259, 23);
            tboxOutputDir.TabIndex = 0;
            tboxOutputDir.TextChanged += tboxOutputDir_TextChanged;
            // 
            // lblInDir
            // 
            lblInDir.AutoSize = true;
            lblInDir.Dock = DockStyle.Fill;
            lblInDir.Location = new Point(3, 0);
            lblInDir.Name = "lblInDir";
            lblInDir.Size = new Size(99, 39);
            lblInDir.TabIndex = 4;
            lblInDir.Text = "Input Directory:";
            // 
            // lblOutDir
            // 
            lblOutDir.AutoSize = true;
            lblOutDir.Dock = DockStyle.Fill;
            lblOutDir.Location = new Point(3, 39);
            lblOutDir.Name = "lblOutDir";
            lblOutDir.Size = new Size(99, 39);
            lblOutDir.TabIndex = 5;
            lblOutDir.Text = "Output Directory:";
            // 
            // cboxRecursive
            // 
            cboxRecursive.AutoSize = true;
            cboxRecursive.Dock = DockStyle.Fill;
            cboxRecursive.Location = new Point(276, 90);
            cboxRecursive.Name = "cboxRecursive";
            cboxRecursive.Size = new Size(295, 19);
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
            gboxMain.Size = new Size(580, 413);
            gboxMain.TabIndex = 13;
            gboxMain.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(lblCurrProc, 0, 0);
            tableLayoutPanel3.Controls.Add(tboxCurrProc, 1, 0);
            tableLayoutPanel3.Controls.Add(pboxCurrentFrame, 0, 1);
            tableLayoutPanel3.Controls.Add(pictureBox1, 1, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 209);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(574, 171);
            tableLayoutPanel3.TabIndex = 15;
            // 
            // lblCurrProc
            // 
            lblCurrProc.AutoSize = true;
            lblCurrProc.Dock = DockStyle.Fill;
            lblCurrProc.Location = new Point(3, 0);
            lblCurrProc.Name = "lblCurrProc";
            lblCurrProc.Size = new Size(112, 29);
            lblCurrProc.TabIndex = 0;
            lblCurrProc.Text = "Processing:";
            lblCurrProc.TextAlign = ContentAlignment.MiddleRight;
            lblCurrProc.Click += lblCurrProc_Click;
            // 
            // tboxCurrProc
            // 
            tboxCurrProc.Dock = DockStyle.Fill;
            tboxCurrProc.Location = new Point(121, 3);
            tboxCurrProc.Name = "tboxCurrProc";
            tboxCurrProc.Size = new Size(450, 23);
            tboxCurrProc.TabIndex = 1;
            tboxCurrProc.TextChanged += tboxCurrProc_TextChanged;
            // 
            // pboxCurrentFrame
            // 
            pboxCurrentFrame.Dock = DockStyle.Fill;
            pboxCurrentFrame.Location = new Point(3, 32);
            pboxCurrentFrame.Name = "pboxCurrentFrame";
            pboxCurrentFrame.Size = new Size(112, 136);
            pboxCurrentFrame.TabIndex = 3;
            pboxCurrentFrame.TabStop = false;
            pboxCurrentFrame.Click += pboxCurrentFrame_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = Properties.Resources.gplv3_logo;
            pictureBox1.Location = new Point(121, 32);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(450, 136);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(lblNeqDiff, 0, 2);
            tableLayoutPanel2.Controls.Add(lblEqDiff, 0, 1);
            tableLayoutPanel2.Controls.Add(cboxAlgo, 1, 0);
            tableLayoutPanel2.Controls.Add(lblAlgo, 0, 0);
            tableLayoutPanel2.Controls.Add(numericEqualDifference, 1, 1);
            tableLayoutPanel2.Controls.Add(numericNequalDifference, 1, 2);
            tableLayoutPanel2.Controls.Add(cboxRecursive, 2, 3);
            tableLayoutPanel2.Controls.Add(btnStop, 2, 2);
            tableLayoutPanel2.Controls.Add(btnGo, 2, 0);
            tableLayoutPanel2.Controls.Add(cboxAlpha, 1, 3);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(3, 97);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(574, 112);
            tableLayoutPanel2.TabIndex = 14;
            // 
            // lblNeqDiff
            // 
            lblNeqDiff.AutoSize = true;
            lblNeqDiff.Dock = DockStyle.Fill;
            lblNeqDiff.Location = new Point(3, 58);
            lblNeqDiff.Name = "lblNeqDiff";
            lblNeqDiff.Size = new Size(105, 29);
            lblNeqDiff.TabIndex = 16;
            lblNeqDiff.Text = "Nequal Difference:";
            lblNeqDiff.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEqDiff
            // 
            lblEqDiff.AutoSize = true;
            lblEqDiff.Dock = DockStyle.Fill;
            lblEqDiff.Location = new Point(3, 29);
            lblEqDiff.Name = "lblEqDiff";
            lblEqDiff.Size = new Size(105, 29);
            lblEqDiff.TabIndex = 14;
            lblEqDiff.Text = "Equal Difference:";
            lblEqDiff.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboxAlgo
            // 
            cboxAlgo.Dock = DockStyle.Fill;
            cboxAlgo.FormattingEnabled = true;
            cboxAlgo.Items.AddRange(new object[] { "Scalex2x", "Scalex3x", "Scalex4x" });
            cboxAlgo.Location = new Point(114, 3);
            cboxAlgo.Name = "cboxAlgo";
            cboxAlgo.Size = new Size(156, 23);
            cboxAlgo.TabIndex = 11;
            // 
            // lblAlgo
            // 
            lblAlgo.AutoSize = true;
            lblAlgo.Dock = DockStyle.Fill;
            lblAlgo.Location = new Point(3, 0);
            lblAlgo.Name = "lblAlgo";
            lblAlgo.Size = new Size(105, 29);
            lblAlgo.TabIndex = 12;
            lblAlgo.Text = "Algorithm:";
            lblAlgo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numericEqualDifference
            // 
            numericEqualDifference.DecimalPlaces = 2;
            numericEqualDifference.Dock = DockStyle.Fill;
            numericEqualDifference.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericEqualDifference.Location = new Point(114, 32);
            numericEqualDifference.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericEqualDifference.Name = "numericEqualDifference";
            numericEqualDifference.Size = new Size(156, 23);
            numericEqualDifference.TabIndex = 13;
            numericEqualDifference.ValueChanged += numericEqualDifference_ValueChanged;
            // 
            // numericNequalDifference
            // 
            numericNequalDifference.DecimalPlaces = 2;
            numericNequalDifference.Dock = DockStyle.Fill;
            numericNequalDifference.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericNequalDifference.Location = new Point(114, 61);
            numericNequalDifference.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericNequalDifference.Name = "numericNequalDifference";
            numericNequalDifference.Size = new Size(156, 23);
            numericNequalDifference.TabIndex = 15;
            numericNequalDifference.ValueChanged += numericNequalDifference_ValueChanged;
            // 
            // btnGo
            // 
            btnGo.Dock = DockStyle.Fill;
            btnGo.Image = Properties.Resources.gatling;
            btnGo.Location = new Point(276, 3);
            btnGo.Name = "btnGo";
            tableLayoutPanel2.SetRowSpan(btnGo, 2);
            btnGo.Size = new Size(295, 52);
            btnGo.TabIndex = 10;
            btnGo.Text = "GO";
            btnGo.TextImageRelation = TextImageRelation.ImageAboveText;
            btnGo.UseVisualStyleBackColor = true;
            btnGo.Click += btnGo_Click;
            // 
            // cboxAlpha
            // 
            cboxAlpha.AutoSize = true;
            cboxAlpha.Dock = DockStyle.Fill;
            cboxAlpha.Location = new Point(114, 90);
            cboxAlpha.Name = "cboxAlpha";
            cboxAlpha.Size = new Size(156, 19);
            cboxAlpha.TabIndex = 17;
            cboxAlpha.Text = "Include Alpha Difference";
            cboxAlpha.UseVisualStyleBackColor = true;
            // 
            // progBar
            // 
            progBar.Dock = DockStyle.Bottom;
            progBar.Location = new Point(3, 380);
            progBar.Name = "progBar";
            progBar.Size = new Size(574, 30);
            progBar.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(580, 437);
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
            ((System.ComponentModel.ISupportInitialize)numericEqualDifference).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericNequalDifference).EndInit();
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
        private TextBox tboxOutputDir;
        private Button btnSetOutDir;
        private TextBox tboxInputDir;
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
        private Label lblEqDiff;
        private NumericUpDown numericEqualDifference;
        private Label lblNeqDiff;
        private NumericUpDown numericNequalDifference;
        private CheckBox cboxAlpha;
    }
}
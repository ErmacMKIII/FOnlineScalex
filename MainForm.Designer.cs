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
            label1 = new Label();
            label2 = new Label();
            cboxRecursive = new CheckBox();
            gboxMain = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            lblCurrProc = new Label();
            tboxCurrProc = new TextBox();
            progBar = new ProgressBar();
            pboxCurrentFrame = new PictureBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnGo = new Button();
            cboxAlgo = new ComboBox();
            label3 = new Label();
            menuStripMain.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            gboxMain.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pboxCurrentFrame).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, infoToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(624, 24);
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
            btnStop.Image = Properties.Resources.stop;
            btnStop.ImageAlign = ContentAlignment.MiddleLeft;
            btnStop.Location = new Point(479, 32);
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
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(618, 78);
            tableLayoutPanel1.TabIndex = 13;
            // 
            // btnSetOutDir
            // 
            btnSetOutDir.Dock = DockStyle.Fill;
            btnSetOutDir.Image = Properties.Resources.dir_icon_opened;
            btnSetOutDir.ImageAlign = ContentAlignment.MiddleLeft;
            btnSetOutDir.Location = new Point(479, 3);
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
            btnSetInDir.Location = new Point(479, 42);
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
            tboxOutDir.Location = new Point(121, 3);
            tboxOutDir.Name = "tboxOutDir";
            tboxOutDir.ReadOnly = true;
            tboxOutDir.Size = new Size(352, 23);
            tboxOutDir.TabIndex = 2;
            tboxOutDir.TextChanged += tboxOutDir_TextChanged;
            // 
            // tboxInputDir
            // 
            tboxInputDir.Dock = DockStyle.Fill;
            tboxInputDir.Location = new Point(121, 42);
            tboxInputDir.Name = "tboxInputDir";
            tboxInputDir.ReadOnly = true;
            tboxInputDir.Size = new Size(352, 23);
            tboxInputDir.TabIndex = 0;
            tboxInputDir.TextChanged += tboxInputDir_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(112, 39);
            label1.TabIndex = 4;
            label1.Text = "Input Directory:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 39);
            label2.Name = "label2";
            label2.Size = new Size(112, 39);
            label2.TabIndex = 5;
            label2.Text = "Output Directory:";
            // 
            // cboxRecursive
            // 
            cboxRecursive.AutoSize = true;
            cboxRecursive.Location = new Point(3, 32);
            cboxRecursive.Name = "cboxRecursive";
            cboxRecursive.Size = new Size(158, 19);
            cboxRecursive.TabIndex = 4;
            cboxRecursive.Text = "Recurse through child directories";
            cboxRecursive.UseVisualStyleBackColor = true;
            // 
            // gboxMain
            // 
            gboxMain.Controls.Add(tableLayoutPanel3);
            gboxMain.Controls.Add(tableLayoutPanel2);
            gboxMain.Controls.Add(tableLayoutPanel1);
            gboxMain.Dock = DockStyle.Fill;
            gboxMain.Location = new Point(0, 24);
            gboxMain.Name = "gboxMain";
            gboxMain.Size = new Size(624, 448);
            gboxMain.TabIndex = 13;
            gboxMain.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.73139F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.26861F));
            tableLayoutPanel3.Controls.Add(lblCurrProc, 0, 0);
            tableLayoutPanel3.Controls.Add(tboxCurrProc, 1, 0);
            tableLayoutPanel3.Controls.Add(progBar, 0, 1);
            tableLayoutPanel3.Controls.Add(pboxCurrentFrame, 1, 1);
            tableLayoutPanel3.Dock = DockStyle.Bottom;
            tableLayoutPanel3.Location = new Point(3, 156);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 10.03861F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 89.96139F));
            tableLayoutPanel3.Size = new Size(618, 289);
            tableLayoutPanel3.TabIndex = 15;
            // 
            // lblCurrProc
            // 
            lblCurrProc.AutoSize = true;
            lblCurrProc.Dock = DockStyle.Fill;
            lblCurrProc.Location = new Point(3, 0);
            lblCurrProc.Name = "lblCurrProc";
            lblCurrProc.Size = new Size(221, 29);
            lblCurrProc.TabIndex = 0;
            lblCurrProc.Text = "Currently Processing:";
            // 
            // tboxCurrProc
            // 
            tboxCurrProc.Dock = DockStyle.Fill;
            tboxCurrProc.Location = new Point(230, 3);
            tboxCurrProc.Name = "tboxCurrProc";
            tboxCurrProc.Size = new Size(385, 23);
            tboxCurrProc.TabIndex = 1;
            // 
            // progBar
            // 
            progBar.Dock = DockStyle.Top;
            progBar.Location = new Point(3, 32);
            progBar.Name = "progBar";
            progBar.Size = new Size(221, 30);
            progBar.TabIndex = 2;
            // 
            // pboxCurrentFrame
            // 
            pboxCurrentFrame.Dock = DockStyle.Fill;
            pboxCurrentFrame.Location = new Point(230, 32);
            pboxCurrentFrame.Name = "pboxCurrentFrame";
            pboxCurrentFrame.Size = new Size(385, 254);
            pboxCurrentFrame.TabIndex = 3;
            pboxCurrentFrame.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.5058632F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65.49414F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 141F));
            tableLayoutPanel2.Controls.Add(btnGo, 2, 0);
            tableLayoutPanel2.Controls.Add(cboxAlgo, 1, 0);
            tableLayoutPanel2.Controls.Add(btnStop, 2, 1);
            tableLayoutPanel2.Controls.Add(label3, 0, 0);
            tableLayoutPanel2.Controls.Add(cboxRecursive, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(3, 97);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(618, 58);
            tableLayoutPanel2.TabIndex = 14;
            // 
            // btnGo
            // 
            btnGo.Image = Properties.Resources.gatling;
            btnGo.ImageAlign = ContentAlignment.MiddleLeft;
            btnGo.Location = new Point(479, 3);
            btnGo.Name = "btnGo";
            btnGo.Size = new Size(136, 23);
            btnGo.TabIndex = 10;
            btnGo.Text = "GO";
            btnGo.UseVisualStyleBackColor = true;
            btnGo.Click += btnGo_Click;
            // 
            // cboxAlgo
            // 
            cboxAlgo.FormattingEnabled = true;
            cboxAlgo.Items.AddRange(new object[] { "Scalex2x", "Scalex3x", "Scalex4x" });
            cboxAlgo.Location = new Point(167, 3);
            cboxAlgo.Name = "cboxAlgo";
            cboxAlgo.Size = new Size(306, 23);
            cboxAlgo.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 12;
            label3.Text = "Algorithm:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 472);
            Controls.Add(gboxMain);
            Controls.Add(menuStripMain);
            MainMenuStrip = menuStripMain;
            Name = "MainForm";
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
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
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
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
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
            tboxInputDir = new TextBox();
            btnSetInDir = new Button();
            btnSetOutDir = new Button();
            tboxOutDir = new TextBox();
            cboxRecursive = new CheckBox();
            label2 = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            btnGo = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // tboxInputDir
            // 
            tboxInputDir.Dock = DockStyle.Fill;
            tboxInputDir.Location = new Point(3, 19);
            tboxInputDir.Name = "tboxInputDir";
            tboxInputDir.ReadOnly = true;
            tboxInputDir.Size = new Size(478, 23);
            tboxInputDir.TabIndex = 0;
            tboxInputDir.TextChanged += tboxInputDir_TextChanged;
            // 
            // btnSetInDir
            // 
            btnSetInDir.Dock = DockStyle.Right;
            btnSetInDir.Location = new Point(481, 19);
            btnSetInDir.Name = "btnSetInDir";
            btnSetInDir.Size = new Size(142, 32);
            btnSetInDir.TabIndex = 1;
            btnSetInDir.Text = "Open...";
            btnSetInDir.UseVisualStyleBackColor = true;
            btnSetInDir.Click += btnSetInDir_Click;
            // 
            // btnSetOutDir
            // 
            btnSetOutDir.Dock = DockStyle.Right;
            btnSetOutDir.Location = new Point(482, 19);
            btnSetOutDir.Name = "btnSetOutDir";
            btnSetOutDir.Size = new Size(142, 33);
            btnSetOutDir.TabIndex = 3;
            btnSetOutDir.Text = "Open...";
            btnSetOutDir.UseVisualStyleBackColor = true;
            btnSetOutDir.Click += btnSetOutDir_Click;
            // 
            // tboxOutDir
            // 
            tboxOutDir.Dock = DockStyle.Fill;
            tboxOutDir.Location = new Point(3, 19);
            tboxOutDir.Name = "tboxOutDir";
            tboxOutDir.Size = new Size(479, 23);
            tboxOutDir.TabIndex = 2;
            tboxOutDir.TextChanged += tboxOutDir_TextChanged;
            // 
            // cboxRecursive
            // 
            cboxRecursive.AutoSize = true;
            cboxRecursive.Location = new Point(307, 146);
            cboxRecursive.Name = "cboxRecursive";
            cboxRecursive.Size = new Size(200, 19);
            cboxRecursive.TabIndex = 4;
            cboxRecursive.Text = "Recurse through child directories";
            cboxRecursive.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 18);
            label2.Name = "label2";
            label2.Size = new Size(96, 15);
            label2.TabIndex = 6;
            label2.Text = "Output Directory";
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.Controls.Add(tboxInputDir);
            groupBox1.Controls.Add(btnSetInDir);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(626, 54);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Input Directory";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tboxOutDir);
            groupBox2.Controls.Add(btnSetOutDir);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(11, 72);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(627, 55);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Output Directory";
            // 
            // btnGo
            // 
            btnGo.Location = new Point(529, 143);
            btnGo.Name = "btnGo";
            btnGo.Size = new Size(75, 23);
            btnGo.TabIndex = 9;
            btnGo.Text = "GO";
            btnGo.UseVisualStyleBackColor = true;
            btnGo.Click += btnGo_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnGo);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(cboxRecursive);
            Name = "MainForm";
            Text = "FOnlineScalex";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tboxInputDir;
        private Button btnSetInDir;
        private Button btnSetOutDir;
        private TextBox tboxOutDir;
        private CheckBox cboxRecursive;
        private Label label2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btnGo;
    }
}
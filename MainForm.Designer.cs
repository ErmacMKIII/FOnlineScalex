﻿/* Copyright (C) 2023 Aleksandar Stojanovic <coas91@rocketmail.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/> */
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
            pboxPreview = new PictureBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblMulThres = new Label();
            numericAlphaMulThres = new NumericUpDown();
            lblDropThres = new Label();
            numericAlphaDropThres = new NumericUpDown();
            cboxPostProc = new CheckBox();
            lblEqDiff = new Label();
            cboxAlgo = new ComboBox();
            lblAlgo = new Label();
            numericAccuracy = new NumericUpDown();
            btnGo = new Button();
            cboxScale = new CheckBox();
            menuStripMain.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            gboxMain.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pboxCurrentFrame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pboxPreview).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericAlphaMulThres).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericAlphaDropThres).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericAccuracy).BeginInit();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, infoToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(1004, 24);
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
            btnStop.Location = new Point(323, 61);
            btnStop.Name = "btnStop";
            tableLayoutPanel2.SetRowSpan(btnStop, 2);
            btnStop.Size = new Size(682, 44);
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
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.268734F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.09819F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.5038757F));
            tableLayoutPanel1.Controls.Add(btnSetOutDir, 2, 1);
            tableLayoutPanel1.Controls.Add(btnSetInDir, 2, 0);
            tableLayoutPanel1.Controls.Add(tboxInputDir, 1, 0);
            tableLayoutPanel1.Controls.Add(tboxOutputDir, 1, 1);
            tableLayoutPanel1.Controls.Add(lblInDir, 0, 0);
            tableLayoutPanel1.Controls.Add(lblOutDir, 0, 1);
            tableLayoutPanel1.Controls.Add(cboxRecursive, 2, 2);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(998, 103);
            tableLayoutPanel1.TabIndex = 13;
            // 
            // btnSetOutDir
            // 
            btnSetOutDir.Dock = DockStyle.Fill;
            btnSetOutDir.Image = Properties.Resources.dir_icon_opened2;
            btnSetOutDir.ImageAlign = ContentAlignment.MiddleLeft;
            btnSetOutDir.Location = new Point(845, 42);
            btnSetOutDir.Name = "btnSetOutDir";
            btnSetOutDir.Size = new Size(150, 33);
            btnSetOutDir.TabIndex = 3;
            btnSetOutDir.Text = "Open...";
            btnSetOutDir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSetOutDir.UseVisualStyleBackColor = true;
            btnSetOutDir.Click += btnSetOutDir_Click;
            // 
            // btnSetInDir
            // 
            btnSetInDir.Dock = DockStyle.Fill;
            btnSetInDir.Image = Properties.Resources.dir_icon_opened;
            btnSetInDir.ImageAlign = ContentAlignment.MiddleLeft;
            btnSetInDir.Location = new Point(845, 3);
            btnSetInDir.Name = "btnSetInDir";
            btnSetInDir.Size = new Size(150, 33);
            btnSetInDir.TabIndex = 1;
            btnSetInDir.Text = "Open...";
            btnSetInDir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSetInDir.UseVisualStyleBackColor = true;
            btnSetInDir.Click += btnSetInDir_Click;
            // 
            // tboxInputDir
            // 
            tboxInputDir.Dock = DockStyle.Fill;
            tboxInputDir.Location = new Point(85, 3);
            tboxInputDir.Name = "tboxInputDir";
            tboxInputDir.ReadOnly = true;
            tboxInputDir.Size = new Size(754, 23);
            tboxInputDir.TabIndex = 2;
            tboxInputDir.TextChanged += tboxInputDir_TextChanged;
            // 
            // tboxOutputDir
            // 
            tboxOutputDir.Dock = DockStyle.Fill;
            tboxOutputDir.Location = new Point(85, 42);
            tboxOutputDir.Name = "tboxOutputDir";
            tboxOutputDir.ReadOnly = true;
            tboxOutputDir.Size = new Size(754, 23);
            tboxOutputDir.TabIndex = 0;
            tboxOutputDir.TextChanged += tboxOutputDir_TextChanged;
            // 
            // lblInDir
            // 
            lblInDir.AutoSize = true;
            lblInDir.Dock = DockStyle.Fill;
            lblInDir.Location = new Point(3, 0);
            lblInDir.Name = "lblInDir";
            lblInDir.Size = new Size(76, 39);
            lblInDir.TabIndex = 4;
            lblInDir.Text = "Input Directory:";
            // 
            // lblOutDir
            // 
            lblOutDir.AutoSize = true;
            lblOutDir.Dock = DockStyle.Fill;
            lblOutDir.Location = new Point(3, 39);
            lblOutDir.Name = "lblOutDir";
            lblOutDir.Size = new Size(76, 39);
            lblOutDir.TabIndex = 5;
            lblOutDir.Text = "Output Directory:";
            // 
            // cboxRecursive
            // 
            cboxRecursive.AutoSize = true;
            cboxRecursive.Dock = DockStyle.Fill;
            cboxRecursive.Location = new Point(845, 81);
            cboxRecursive.Name = "cboxRecursive";
            cboxRecursive.Size = new Size(150, 19);
            cboxRecursive.TabIndex = 4;
            cboxRecursive.Text = "Recurse Through";
            cboxRecursive.UseVisualStyleBackColor = true;
            // 
            // gboxMain
            // 
            gboxMain.AutoSize = true;
            gboxMain.Controls.Add(tableLayoutPanel3);
            gboxMain.Controls.Add(tableLayoutPanel2);
            gboxMain.Controls.Add(tableLayoutPanel1);
            gboxMain.Dock = DockStyle.Fill;
            gboxMain.Location = new Point(0, 24);
            gboxMain.Name = "gboxMain";
            gboxMain.Size = new Size(1004, 701);
            gboxMain.TabIndex = 13;
            gboxMain.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoScroll = true;
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.Controls.Add(lblCurrProc, 0, 0);
            tableLayoutPanel3.Controls.Add(tboxCurrProc, 1, 0);
            tableLayoutPanel3.Controls.Add(pboxCurrentFrame, 1, 1);
            tableLayoutPanel3.Controls.Add(pboxPreview, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 288);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(998, 410);
            tableLayoutPanel3.TabIndex = 15;
            // 
            // lblCurrProc
            // 
            lblCurrProc.AutoSize = true;
            lblCurrProc.Dock = DockStyle.Fill;
            lblCurrProc.Location = new Point(3, 0);
            lblCurrProc.Name = "lblCurrProc";
            lblCurrProc.Size = new Size(67, 29);
            lblCurrProc.TabIndex = 0;
            lblCurrProc.Text = "Processing:";
            lblCurrProc.TextAlign = ContentAlignment.MiddleRight;
            lblCurrProc.Click += lblCurrProc_Click;
            // 
            // tboxCurrProc
            // 
            tboxCurrProc.Dock = DockStyle.Fill;
            tboxCurrProc.Location = new Point(76, 3);
            tboxCurrProc.Name = "tboxCurrProc";
            tboxCurrProc.ReadOnly = true;
            tboxCurrProc.Size = new Size(919, 23);
            tboxCurrProc.TabIndex = 1;
            tboxCurrProc.TextChanged += tboxCurrProc_TextChanged;
            // 
            // pboxCurrentFrame
            // 
            pboxCurrentFrame.BorderStyle = BorderStyle.Fixed3D;
            pboxCurrentFrame.Dock = DockStyle.Fill;
            pboxCurrentFrame.Location = new Point(76, 32);
            pboxCurrentFrame.Name = "pboxCurrentFrame";
            pboxCurrentFrame.Size = new Size(919, 404);
            pboxCurrentFrame.SizeMode = PictureBoxSizeMode.AutoSize;
            pboxCurrentFrame.TabIndex = 3;
            pboxCurrentFrame.TabStop = false;
            pboxCurrentFrame.Click += pboxCurrentFrame_Click;
            // 
            // pboxPreview
            // 
            pboxPreview.BorderStyle = BorderStyle.Fixed3D;
            pboxPreview.Dock = DockStyle.Fill;
            pboxPreview.Image = Properties.Resources.FOnlineScalex;
            pboxPreview.Location = new Point(3, 32);
            pboxPreview.Name = "pboxPreview";
            pboxPreview.Size = new Size(67, 404);
            pboxPreview.SizeMode = PictureBoxSizeMode.AutoSize;
            pboxPreview.TabIndex = 4;
            pboxPreview.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(lblMulThres, 0, 5);
            tableLayoutPanel2.Controls.Add(numericAlphaMulThres, 0, 5);
            tableLayoutPanel2.Controls.Add(lblDropThres, 0, 4);
            tableLayoutPanel2.Controls.Add(numericAlphaDropThres, 0, 4);
            tableLayoutPanel2.Controls.Add(cboxPostProc, 0, 3);
            tableLayoutPanel2.Controls.Add(lblEqDiff, 0, 1);
            tableLayoutPanel2.Controls.Add(cboxAlgo, 1, 0);
            tableLayoutPanel2.Controls.Add(lblAlgo, 0, 0);
            tableLayoutPanel2.Controls.Add(numericAccuracy, 1, 1);
            tableLayoutPanel2.Controls.Add(btnStop, 2, 2);
            tableLayoutPanel2.Controls.Add(btnGo, 2, 0);
            tableLayoutPanel2.Controls.Add(cboxScale, 0, 2);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(3, 122);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(998, 166);
            tableLayoutPanel2.TabIndex = 14;
            // 
            // lblMulThres
            // 
            lblMulThres.AutoSize = true;
            lblMulThres.Dock = DockStyle.Fill;
            lblMulThres.Enabled = false;
            lblMulThres.Location = new Point(3, 137);
            lblMulThres.Name = "lblMulThres";
            lblMulThres.Size = new Size(108, 29);
            lblMulThres.TabIndex = 23;
            lblMulThres.Text = "Multiply-Threshold";
            lblMulThres.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numericAlphaMulThres
            // 
            numericAlphaMulThres.Dock = DockStyle.Fill;
            numericAlphaMulThres.Enabled = false;
            numericAlphaMulThres.Location = new Point(117, 140);
            numericAlphaMulThres.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericAlphaMulThres.Name = "numericAlphaMulThres";
            numericAlphaMulThres.Size = new Size(200, 23);
            numericAlphaMulThres.TabIndex = 22;
            numericAlphaMulThres.Value = new decimal(new int[] { 128, 0, 0, 0 });
            numericAlphaMulThres.ValueChanged += numericAlpaMulThres_ValueChanged;
            // 
            // lblDropThres
            // 
            lblDropThres.AutoSize = true;
            lblDropThres.Dock = DockStyle.Fill;
            lblDropThres.Enabled = false;
            lblDropThres.Location = new Point(3, 108);
            lblDropThres.Name = "lblDropThres";
            lblDropThres.Size = new Size(108, 29);
            lblDropThres.TabIndex = 21;
            lblDropThres.Text = "Drop-Threshold:";
            lblDropThres.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numericAlphaDropThres
            // 
            numericAlphaDropThres.Dock = DockStyle.Fill;
            numericAlphaDropThres.Enabled = false;
            numericAlphaDropThres.Location = new Point(117, 111);
            numericAlphaDropThres.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericAlphaDropThres.Name = "numericAlphaDropThres";
            numericAlphaDropThres.Size = new Size(200, 23);
            numericAlphaDropThres.TabIndex = 20;
            numericAlphaDropThres.Value = new decimal(new int[] { 64, 0, 0, 0 });
            numericAlphaDropThres.ValueChanged += numericAlphaDropThres_ValueChanged;
            // 
            // cboxPostProc
            // 
            cboxPostProc.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(cboxPostProc, 2);
            cboxPostProc.Dock = DockStyle.Fill;
            cboxPostProc.Location = new Point(3, 86);
            cboxPostProc.Name = "cboxPostProc";
            cboxPostProc.Size = new Size(314, 19);
            cboxPostProc.TabIndex = 18;
            cboxPostProc.Text = "Post Processing (.PNG, .BMP)";
            cboxPostProc.UseVisualStyleBackColor = true;
            cboxPostProc.CheckedChanged += cboxPostProc_CheckedChanged;
            // 
            // lblEqDiff
            // 
            lblEqDiff.AutoSize = true;
            lblEqDiff.Dock = DockStyle.Fill;
            lblEqDiff.Location = new Point(3, 29);
            lblEqDiff.Name = "lblEqDiff";
            lblEqDiff.Size = new Size(108, 29);
            lblEqDiff.TabIndex = 14;
            lblEqDiff.Text = "Accuracy:";
            lblEqDiff.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboxAlgo
            // 
            cboxAlgo.Dock = DockStyle.Fill;
            cboxAlgo.FormattingEnabled = true;
            cboxAlgo.Items.AddRange(new object[] { "Scalex2x", "Scalex3x", "Scalex4x", "Hqx2x", "Hqx3x", "Hqx4x" });
            cboxAlgo.Location = new Point(117, 3);
            cboxAlgo.Name = "cboxAlgo";
            cboxAlgo.Size = new Size(200, 23);
            cboxAlgo.TabIndex = 11;
            cboxAlgo.SelectedIndexChanged += cboxAlgo_SelectedIndexChanged;
            // 
            // lblAlgo
            // 
            lblAlgo.AutoSize = true;
            lblAlgo.Dock = DockStyle.Fill;
            lblAlgo.Location = new Point(3, 0);
            lblAlgo.Name = "lblAlgo";
            lblAlgo.Size = new Size(108, 29);
            lblAlgo.TabIndex = 12;
            lblAlgo.Text = "Algorithm:";
            lblAlgo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numericAccuracy
            // 
            numericAccuracy.DecimalPlaces = 2;
            numericAccuracy.Dock = DockStyle.Fill;
            numericAccuracy.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numericAccuracy.Location = new Point(117, 32);
            numericAccuracy.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericAccuracy.Name = "numericAccuracy";
            numericAccuracy.Size = new Size(200, 23);
            numericAccuracy.TabIndex = 13;
            numericAccuracy.Value = new decimal(new int[] { 95, 0, 0, 131072 });
            numericAccuracy.ValueChanged += numericAccuracy_ValueChanged;
            // 
            // btnGo
            // 
            btnGo.Dock = DockStyle.Fill;
            btnGo.Image = Properties.Resources.gatling;
            btnGo.Location = new Point(323, 3);
            btnGo.Name = "btnGo";
            tableLayoutPanel2.SetRowSpan(btnGo, 2);
            btnGo.Size = new Size(682, 52);
            btnGo.TabIndex = 10;
            btnGo.Text = "GO";
            btnGo.TextImageRelation = TextImageRelation.ImageAboveText;
            btnGo.UseVisualStyleBackColor = true;
            btnGo.Click += btnGo_Click;
            // 
            // cboxScale
            // 
            cboxScale.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(cboxScale, 2);
            cboxScale.Dock = DockStyle.Fill;
            cboxScale.Location = new Point(3, 61);
            cboxScale.Name = "cboxScale";
            cboxScale.Size = new Size(314, 19);
            cboxScale.TabIndex = 17;
            cboxScale.Text = "Scale Image";
            cboxScale.UseVisualStyleBackColor = true;
            cboxScale.CheckedChanged += cboxScale_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1004, 725);
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
            ((System.ComponentModel.ISupportInitialize)pboxPreview).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericAlphaMulThres).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericAlphaDropThres).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericAccuracy).EndInit();
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
        private PictureBox pboxCurrentFrame;
        private Label lblInDir;
        private Label lblOutDir;
        private Label lblAlgo;
        private PictureBox pboxPreview;
        private Label lblEqDiff;
        private NumericUpDown numericAccuracy;
        private CheckBox cboxScale;
        private CheckBox cboxPostProc;
        private Label lblDropThres;
        private NumericUpDown numericAlphaDropThres;
        private Label lblMulThres;
        private NumericUpDown numericAlphaMulThres;
    }
}
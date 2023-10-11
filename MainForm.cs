/* Copyright (C) 2023 Aleksandar Stojanovic <coas91@rocketmail.com>

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
using FOnlineScalex.Logger;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using FOnlineScalex.FRMFile;
using FOnlineScalex.Properties;
using System.Text;
using FOnlineScalex.ScalexFamily;
using FOnlineScalex.Algorithm;
using FOnlineScalex.Algorithm.HqxFamily;
using FOnlineScalex.PostProcessing;

namespace FOnlineScalex
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Defines the DarkBackground.
        /// </summary>
        public static readonly Color DarkBackground = Color.FromArgb(0x1E, 0x1E, 0x1E);

        /// <summary>
        /// Defines the DarkForeground.
        /// </summary>
        public static readonly Color DarkForeground = Color.FromArgb(0xFF, 0x50, 0x7F);

        protected readonly BackgroundWorker backgroundWorker = new BackgroundWorker();

        protected readonly FOnlineScalex fOnlineScalex = new FOnlineScalex();

        protected string inDirPath = string.Empty;
        protected string outDirPath = string.Empty;

        protected double eqAccuracy = 0.95;
        protected AlphaRange alphaRange = new AlphaRange() { DropThreshold = 64, MultiplyThreshold = 128 };

        protected DarkRenderer darkRenderer = new DarkRenderer();
        protected readonly CustomProgressBar progBar;

        public MainForm()
        {
            InitializeComponent();

            this.progBar = new CustomProgressBar()
            {
                VisualMode = CustomProgressBar.ProgressBarDisplayMode.Percentage,
                TextFont = MainForm.DefaultFont,
                TextColor = DarkForeground
            };
            this.progBar.Dock = DockStyle.Bottom;
            this.Controls.Add(progBar);

            InitToolTip();
            InitDarkTheme(this);

            this.MainMenuStrip.Renderer = darkRenderer;

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;

            fOnlineScalex.OnProgressUpdate += FOnlineScalex_OnProgressUpdate;
            fOnlineScalex.OnCancel += FOnlineScalex_OnCancel;
        }

        private void FOnlineScalex_OnCancel()
        {
            backgroundWorker.CancelAsync();
            base.Invoke(() =>
            {
                btnGo.Enabled = true;
                btnStop.Enabled = true;
                this.pboxCurrentFrame.Update();
                this.fOnlineScalex.CancelWork();
            });

        }

        private void FOnlineScalex_OnProgressUpdate(int progress, string? file, Bitmap? frame)
        {
            base.Invoke(() =>
            {
                BackgroundWorker_ProgressChanged(this, new ProgressChangedEventArgs((int)Math.Round(fOnlineScalex.Progress), null));
                this.tboxCurrProc.Text = fOnlineScalex.ProcessingFile;
                this.pboxCurrentFrame.Image = fOnlineScalex.Frame;
            });
        }

        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            this.progBar.Value = e.ProgressPercentage;
            this.progBar.Update();
        }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            // Display Error Message
            if (fOnlineScalex.Cancelled)
            {
                MessageBox.Show("App work cancelled by user!", "FOnlineScalex", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (fOnlineScalex.Erroneous)
            {
                MessageBox.Show($"App work resulted in error {fOnlineScalex.ErrorMessage}!", "FOnlineScalex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("App work finished!", "FOnlineScalex", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            btnGo.Enabled = true;
            btnStop.Enabled = false;
            progBar.Value = 0;
        }

        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            if (args != null && args.Length == 9)
            {
                fOnlineScalex.DoWork((string)args[0], (string)args[1], (bool)args[2],
                    (double)args[3], (IAlgorithm.AlgorithmId)args[4], (bool)args[5], (bool)args[6], (AlphaRange)args[7], (IFOSLogger)args[8]);
            }
        }

        protected IFOSLogger fOSLogger = new FOSLogger("FOnlineScalex.log", "./");

        public static void InitDarkTheme(Control root)
        {
            root.BackColor = DarkBackground;
            root.ForeColor = DarkForeground;
            foreach (Control ctrl in root.Controls)
            {
                InitDarkTheme(ctrl);
            }
        }

        private void InitToolTip()
        {
            ToolTip toolTip = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip.SetToolTip(this.tboxInputDir, "Input directory path(s)");
            toolTip.SetToolTip(this.tboxOutputDir, "Output directory path(s)");
            toolTip.SetToolTip(this.btnSetInDir, "Set input directory path(s)");
            toolTip.SetToolTip(this.btnSetOutDir, "Set output directory path(s)");
            toolTip.SetToolTip(this.cboxRecursive, "Recurse through input dir & make same dirs on output");

            toolTip.SetToolTip(this.cboxAlgo, "Choose algorithm for pixel manipulation");
            toolTip.SetToolTip(this.cboxPostProc, "Post process alpha images (.BMP, .PNG). Remove artifacts etc.");
            toolTip.SetToolTip(this.cboxScale, "Checked - scale image, Unchecked - original size.");

            toolTip.SetToolTip(this.btnGo, "Commence processing");
            toolTip.SetToolTip(this.btnStop, "Cancel processing");

            toolTip.SetToolTip(this.tboxCurrProc, "Currently processing file");
            toolTip.SetToolTip(this.progBar, "Progress of the processing");
            toolTip.SetToolTip(this.numericAlphaDropThres, "Threshold of alpha drop lequal. No effect if set to zero.");
            toolTip.SetToolTip(this.numericAlphaMulThres, "Threshold of alpha pre-multiply lequal. No effect if set to zero.");
        }

        private void SetInDirPath()
        {
            using (FolderBrowserDialog openInDirDialog = new FolderBrowserDialog())
            {
                if (openInDirDialog.ShowDialog() == DialogResult.OK)
                {
                    inDirPath = openInDirDialog.SelectedPath;
                    tboxInputDir.Text = inDirPath;
                }
            }
        }

        private void SetOutDirPath()
        {
            using (FolderBrowserDialog openOutDirDialog = new FolderBrowserDialog())
            {
                if (openOutDirDialog.ShowDialog() == DialogResult.OK)
                {
                    outDirPath = openOutDirDialog.SelectedPath;
                    tboxOutputDir.Text = outDirPath;
                }
            }
        }



        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            fOnlineScalex.CancelWork();
        }

        private void btnSetInDir_Click(object sender, EventArgs e)
        {
            SetInDirPath();
        }

        private void btnSetOutDir_Click(object sender, EventArgs e)
        {
            SetOutDirPath();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.inDirPath) || string.IsNullOrEmpty(this.outDirPath))
            {
                MessageBox.Show("Directory paths cannot be empty!", "FOnlineScalex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (this.inDirPath.Equals(this.outDirPath))
                {
                    DialogResult dialogResult = MessageBox.Show("Input & output directories are same, it will result in overwriting files.\nAre you sure you want to continue?", "FOnlineScalex", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        btnGo.Enabled = false;
                        btnStop.Enabled = true;
                        IAlgorithm.AlgorithmId algorithm;
                        Enum.TryParse<IAlgorithm.AlgorithmId>((string?)this.cboxAlgo.SelectedItem, false, out algorithm);
                        object[] args = { inDirPath, outDirPath, cboxRecursive.Checked,
                            this.eqAccuracy, algorithm, cboxScale.Checked,
                            cboxPostProc.Checked, this.alphaRange, fOSLogger };
                        backgroundWorker.RunWorkerAsync(args);
                    }
                }
                else
                {
                    btnGo.Enabled = false;
                    btnStop.Enabled = true;
                    IAlgorithm.AlgorithmId algorithm;
                    Enum.TryParse<IAlgorithm.AlgorithmId>((string?)this.cboxAlgo.SelectedItem, false, out algorithm);
                    object[] args = { inDirPath, outDirPath, cboxRecursive.Checked,
                        this.eqAccuracy, algorithm, cboxScale.Checked,
                        cboxPostProc.Checked, this.alphaRange, this.fOSLogger };
                    backgroundWorker.RunWorkerAsync(args);
                }
            }
        }

        private void lblCurrProc_Click(object sender, EventArgs e)
        {

        }

        private void tboxCurrProc_TextChanged(object sender, EventArgs e)
        {
        }

        private void pboxCurrentFrame_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void numericAccuracy_ValueChanged(object sender, EventArgs e)
        {
            this.eqAccuracy = (double)this.numericAccuracy.Value;
            GeneratePreview();
        }

        private void tboxInputDir_TextChanged(object sender, EventArgs e)
        {
            inDirPath = tboxInputDir.Text;
        }

        private void tboxOutputDir_TextChanged(object sender, EventArgs e)
        {
            outDirPath = this.tboxOutputDir.Text;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("VERSION v1.2 - OXYGEN - STABLE\n");
            sb.Append("\n");
            sb.Append("PUBLIC BUILD reviewed on 2023-10-11 at 14:10).\n");
            sb.Append("This software is free software.\n");
            sb.Append("Licensed under GNU General Public License (GPL).\n");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("Purpose:\n");
            sb.Append("FOnline Pixel Art Scalex adapted to FRM formats.\n");
            sb.Append("\n");
            sb.Append("\n");
            sb.Append("Copyright © 2023\n");
            sb.Append("Alexander \"Ermac\" Stojanovich\n");
            sb.Append("\n");

            MessageBox.Show(sb.ToString(), "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("- FOR THE PURPOSE ABOUT THIS PROGRAM, \n");
            sb.Append("check About. Make sure that you checked it first.\n");
            sb.Append("\n");
            sb.Append("To build your own result perform these steps:");
            sb.Append("\n");
            sb.Append("1) Set input filepath - directory with files.\n");
            sb.Append("\n");
            sb.Append("2) Set output filepath - directory with files to write to.\n");
            sb.Append("\n");
            sb.Append("3) Recursive detects directories of input recursively (optional).\n");
            sb.Append("\n");
            sb.Append("4) Choosing algorithm from preset. Scalex 2x default.");
            sb.Append("\n");
            sb.Append("5) Choosing accuracy for algorithm. Values in range [0,1].\n");
            sb.Append("\n");
            sb.Append("6) Scale Image (if unchecked output is equal to original).\n");
            sb.Append("\n");
            sb.Append("7) Post processing for .BMP & .PNG formats with alpha drop threshold & alpha premultiply threshold (optional).\n");
            sb.Append("\n");
            sb.Append("8) GO Let the app go.\n");
            sb.Append("\n");
            sb.Append("9) STOPS stops the app.\n");
            sb.Append("(App can be cancelled anytime during it's processing)\n");
            MessageBox.Show(sb.ToString(), "How To Use", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GeneratePreview()
        {
            IAlgorithm.AlgorithmId algoId;
            Enum.TryParse<IAlgorithm.AlgorithmId>((string?)this.cboxAlgo.SelectedItem, false, out algoId);
            IAlgorithm? algorithm = null;
            Bitmap srcBitmap = Resources.FOnlineScalex;
            Bitmap? dstBitmap = null;
            switch (algoId)
            {
                default:
                    algorithm = null;
                    break;
                case IAlgorithm.AlgorithmId.Scalex2x:
                    algorithm = new Scalex2x();
                    break;
                case IAlgorithm.AlgorithmId.Scalex3x:
                    algorithm = new Scalex3x();
                    break;
                case IAlgorithm.AlgorithmId.Scalex4x:
                    algorithm = new Scalex4x();
                    break;
                case IAlgorithm.AlgorithmId.Hqx2x:
                    algorithm = new Hqx2x();
                    break;
                case IAlgorithm.AlgorithmId.Hqx3x:
                    algorithm = new Hqx3x();
                    break;
                case IAlgorithm.AlgorithmId.Hqx4x:
                    algorithm = new Hqx4x();
                    break;
            }

            algorithm?.Process(srcBitmap, out dstBitmap, this.eqAccuracy, cboxScale.Checked);

            if (this.cboxPostProc.Checked)
            {
                if (dstBitmap != null)
                {
                    Bitmap postDstBitmap;
                    PostProcessing.PostProcessor.Process(dstBitmap, out postDstBitmap, alphaRange);
                    pboxPreview.Image = postDstBitmap;
                }
                else
                {
                    pboxPreview.Image = dstBitmap;
                }
            }
            else
            {
                pboxPreview.Image = dstBitmap;
            }

        }

        private void cboxAlgo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GeneratePreview();
        }

        private void cboxScale_CheckedChanged(object sender, EventArgs e)
        {
            GeneratePreview();
        }

        private void cboxPostProc_CheckedChanged(object sender, EventArgs e)
        {
            this.lblDropThres.Enabled = this.cboxPostProc.Checked;
            this.lblMulThres.Enabled = this.cboxPostProc.Checked;
            this.numericAlphaDropThres.Enabled = this.cboxPostProc.Checked;
            this.numericAlphaMulThres.Enabled = this.cboxPostProc.Checked;
            GeneratePreview();
        }

        private void numericAlphaDropThres_ValueChanged(object sender, EventArgs e)
        {
            this.alphaRange.DropThreshold = (int)this.numericAlphaDropThres.Value;
            GeneratePreview();
        }

        private void numericAlpaMulThres_ValueChanged(object sender, EventArgs e)
        {
            this.alphaRange.MultiplyThreshold = (int)this.numericAlphaMulThres.Value;
            GeneratePreview();
        }
    }
}
using FOnlineScalex.Logger;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using FOnlineScalex.FRMFile;
using FOnlineScalex.Properties;
using System.Text;

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

        protected double eqDifference = 0.0;
        protected double neqDifference = 0.0;

        protected DarkRenderer darkRenderer = new DarkRenderer();

        public MainForm()
        {
            InitializeComponent();
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
            if (e.Cancelled)
            {
                MessageBox.Show("App work cancelled by user!", "FOnlineScalex", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (args != null && args.Length == 8)
            {
                fOnlineScalex.DoWork((string)args[0], (string)args[1], (bool)args[2],
                    (double)args[3], (double)args[4], (FOnlineScalex.Algorithm)args[5], (bool)args[6], (IFOSLogger)args[7]);
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
            btnGo.Enabled = false;
            btnStop.Enabled = true;
            object[] args = { inDirPath, outDirPath, cboxRecursive.Checked, this.eqDifference, this.neqDifference,
                Enum.Parse<FOnlineScalex.Algorithm>(this.cboxAlgo.SelectedText), cboxAlpha.Checked, fOSLogger };
            backgroundWorker.RunWorkerAsync(args);
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

        private void numericEqualDifference_ValueChanged(object sender, EventArgs e)
        {
            this.eqDifference = (double)this.numericEqualDifference.Value;
        }

        private void numericNequalDifference_ValueChanged(object sender, EventArgs e)
        {
            this.neqDifference = (double)this.numericNequalDifference.Value;
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
            sb.Append("VERSION v0.1 - ARCTIC - ALPHA\n");
            sb.Append("\n");
            sb.Append("PUBLIC BUILD reviewed on 2023-05-16 at 12:30).\n");
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
            sb.Append("- Set input filepath\n");
            sb.Append("\n");
            sb.Append("- Set output filepath\n");
            sb.Append("\n");
            sb.Append("- Choosing algorithm");
            sb.Append("\n");
            sb.Append("- Choosing difference (tolerance) for Equal and Not Equal in range 0 to 1\n");
            sb.Append("\n");
            sb.Append("- Include alpha in difference\n");
            sb.Append("\n");
            sb.Append("- GO Let the app go\n");
            sb.Append("- STOPS stops the App\n");
            MessageBox.Show(sb.ToString(), "How To Use", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
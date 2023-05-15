using FOnlineScalex.Logger;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using FOnlineScalex.FRMFile;
using FOnlineScalex.Properties;

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

        protected string inDirPath = @"C:\Users\coas9\Desktop\INPUT";
        protected string outDirPath = @"C:\Users\coas9\Desktop\OUTPUT";

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

        private void FOnlineScalex_OnProgressUpdate(int progress, string? file, Frame? frame)
        {
            base.Invoke(() =>
            {
                BackgroundWorker_ProgressChanged(this, new ProgressChangedEventArgs((int)Math.Round(fOnlineScalex.Progress), null));
                this.tboxCurrProc.Text = fOnlineScalex.ProcessingFile;
                this.pboxCurrentFrame.Image = fOnlineScalex.Frame?.ToBitmap();
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
            if (args != null && args.Length == 4)
            {
                fOnlineScalex.DoWork((string)args[0], (string)args[1], (bool)args[2], (IFOSLogger)args[3]);
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
                    tboxOutDir.Text = outDirPath;
                }
            }
        }

        private void tboxInputDir_TextChanged(object sender, EventArgs e)
        {
            inDirPath = tboxInputDir.Text;
        }

        private void tboxOutDir_TextChanged(object sender, EventArgs e)
        {
            outDirPath = tboxOutDir.Text;
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
            object[] args = { inDirPath, outDirPath, cboxRecursive.Checked, fOSLogger };
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
    }
}
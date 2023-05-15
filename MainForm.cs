using FOnlineScalex.Logger;
using System.Windows.Forms;

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

        public MainForm()
        {
            InitializeComponent();
            InitDarkTheme(this);
        }

        protected string inDirPath = @"C:\Users\coas9\Desktop\INPUT";
        protected string outDirPath = @"C:\Users\coas9\Desktop\OUTPUT";

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

        private void btnGo_Click(object sender, EventArgs e)
        {
            FOnlineScalex.Work(inDirPath, outDirPath, cboxRecursive.Checked, fOSLogger);
        }

        private void btnSetInDir_Click(object sender, EventArgs e)
        {
            SetInDirPath();
        }

        private void btnSetOutDir_Click(object sender, EventArgs e)
        {
            SetOutDirPath();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
    }
}
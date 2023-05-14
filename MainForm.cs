using FOnlineScalex.Logger;
using System.Windows.Forms;

namespace FOnlineScalex
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();            
        }

        protected string inDirPath = @"C:\Users\coas9\Desktop\INPUT";
        protected string outDirPath = @"C:\Users\coas9\Desktop\OUTPUT";

        protected IFOSLogger fOSLogger = new FOSLogger("FOnlineScalex.log", "./");

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
            Performance.Work(inDirPath, outDirPath, cboxRecursive.Checked, fOSLogger);
        }

        private void btnSetInDir_Click(object sender, EventArgs e)
        {
            SetInDirPath();
        }

        private void btnSetOutDir_Click(object sender, EventArgs e)
        {
            SetOutDirPath();
        }
    }
}
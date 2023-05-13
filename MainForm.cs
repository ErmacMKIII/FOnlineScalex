using System.Windows.Forms;

namespace FOnlineScalex
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected string inDirPath = string.Empty;
        protected string outDirPath = string.Empty;

        private void SetInDirPath()
        {
            using (FolderBrowserDialog openDirDialog = new FolderBrowserDialog())
            {
                if (openDirDialog.ShowDialog() == DialogResult.OK)
                {
                    inDirPath = openDirDialog.SelectedPath;
                }
            }
        }

        private void SetOutDirPath()
        {
            using (FolderBrowserDialog openDirDialog = new FolderBrowserDialog())
            {
                if (openDirDialog.ShowDialog() == DialogResult.OK)
                {
                    outDirPath = openDirDialog.SelectedPath;
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
    }
}
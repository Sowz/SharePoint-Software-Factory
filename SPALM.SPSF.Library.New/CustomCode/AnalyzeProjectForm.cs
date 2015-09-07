namespace SPALM.SPSF.Library
{
    using System.Windows.Forms;

    public partial class AnalyzeProjectForm : Form
    {
        public AnalyzeProjectForm(string hiddenFile)
        {
            InitializeComponent();

            this.label1.Text = "Warning: Hidden file " + hiddenFile + " found in project.";
        }
    }
}

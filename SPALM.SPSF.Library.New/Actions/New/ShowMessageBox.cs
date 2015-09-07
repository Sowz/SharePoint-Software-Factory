#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System.IO;
    using System.Windows.Forms;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;

    /// <summary>
    /// Displays the help 
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class ShowMessageBox : ConfigurableAction
    {
        private string _Message;

        protected string GetBasePath()
        {
            return base.GetService<IConfigurationService>(true).BasePath;
        }

        [Input(Required = true)]
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public override void Execute()
        {
            DTE dte = GetService<DTE>(true);

            if ((dte.Solution != null) && (dte.Solution.IsOpen) && (dte.Solution.Projects != null) && (dte.Solution.Projects.Count > 0))
            {
                MessageBox.Show(_Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string _HelpPage = "SPSF_OVERVIEW_510_HOWTOUPGRADEPROJECTS.html";
                string basePath = GetBasePath();
                string uriString = basePath + "\\Help\\OutputHTML\\SPSF_ROOT.html";
                if (!string.IsNullOrEmpty(_HelpPage))
                {
                    uriString = basePath + "\\Help\\OutputHTML\\" + _HelpPage;
                }

                if (File.Exists(uriString))
                {
                    Helpers.LogMessage(dte, this, "Opening " + uriString);
                    Helpers.OpenWebPage(dte, uriString);
                }
            }
        }

        /// <summary>
        /// Removes the previously added reference, if it was created
        /// </summary>
        public override void Undo()
        {
        }
    }
}
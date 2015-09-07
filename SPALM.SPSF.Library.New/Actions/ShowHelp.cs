#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.IO;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;

    /// <summary>
    /// Displays the help 
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class ShowHelp : ConfigurableAction
    {
        private string _HelpPage;

        protected string GetBasePath()
        {
            return base.GetService<IConfigurationService>(true).BasePath;
        }

        [Input(Required = false)]
        public string HelpPage
        {
            get { return _HelpPage; }
            set { _HelpPage = value; }
        }

        public override void Execute()
        {
            DTE dte = GetService<DTE>(true);

            try
            {
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
                    return;
                }
                else
                {
                    Helpers.LogMessage(dte, this, "Help not found at " + uriString);
                }
            }
            catch (Exception ex)
            {
                Helpers.LogMessage(dte, this, ex.ToString());
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
#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;

    [ServiceDependency(typeof(DTE))]
    public class GoToDebuggingWebApp : ConfigurableAction
    {
        protected string GetBasePath()
        {
            return base.GetService<IConfigurationService>(true).BasePath;
        }

        public override void Execute()
        {
          DTE dte = GetService<DTE>(true);

          try
          {
            string url = Helpers.GetApplicationConfigValue(dte, "DebuggingWebApp", "").ToString();

            if (url != "")
            {
                Helpers.OpenWebPage(dte, url);
            }
            else
            {
                Helpers.LogMessage(dte, this, "There is no web application for debugging given for the project.");
            }
          }
          catch (Exception ex)
          {
            Helpers.LogMessage(dte, this, ex.Message);
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
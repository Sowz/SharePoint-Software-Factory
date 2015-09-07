#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    //Recreates the SharePointConfiguration.xml with the local information on the computer (features, contenttypes etc.)
    [ServiceDependency(typeof(DTE))]
    public class LoadSharePointConfiguration : ConfigurableAction
    {        
        public override void Execute()
        {
            DTE dte = GetService<DTE>();
            Helpers.LogMessage(dte, this, "Reading local features, site templates, content types etc. into file SharePointConfiguration.xml");
            try
            {
                SharePointConfigurationHelper.CreateSharePointConfigurationFile(dte);
                Helpers.LogMessage(dte, this, "Finished");
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
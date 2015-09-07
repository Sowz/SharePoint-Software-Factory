#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class GoToSolutions : ConfigurableAction
    {
      public override void Execute()
      {
        DTE dte = GetService<DTE>(true);

        try
        {
            Helpers.LogMessage(dte, this, "Retrieving Central Administration");
            string centralAdminUrl = new SharePointBrigdeHelper(dte).GetCentralAdministrationUrl();

            centralAdminUrl += "_admin/Solutions.aspx";

            Helpers.OpenWebPage(dte, centralAdminUrl);
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
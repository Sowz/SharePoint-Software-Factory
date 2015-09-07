#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class RecycleAppPools : ConfigurableAction
    {
      private string _AppPoolName = "";
      
      [Input(Required = true)]
      public string AppPoolName
      {
        get { return _AppPoolName; }
        set { _AppPoolName = value; }
      }
      
      public override void Execute()
      {
        DTE dte = GetService<DTE>(true);
        Helpers.RecycleAppPool(dte, _AppPoolName);
        Helpers.LogMessage(dte, dte, "*** Recycling finished ***");
        Helpers.LogMessage(dte, dte, "");
      }

        /// <summary>
        /// Removes the previously added reference, if it was created
        /// </summary>
        public override void Undo()
        {
        }
    }    
}
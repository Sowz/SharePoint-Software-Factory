#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class DeleteFailedDeploymentJobs : ConfigurableAction
    {     

      public override void Execute()
      {
        DTE dte = GetService<DTE>(true);
        DeploymentHelpers.DeleteFailedDeploymentJobs(dte);
      }

      public override void Undo()
      {

      }
    }    
}
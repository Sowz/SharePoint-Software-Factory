#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class RedeploySolution : ConfigurableAction
    {
        public override void Execute()
        {
            DTE dte = GetService<DTE>(true);
            if (DeploymentHelpers.CheckRebuildForSelectedProjects(dte))
            {
                DeploymentHelpers.RedeploySolutions(dte);
            }
        }

        public override void Undo()
        {
        }
    }
}
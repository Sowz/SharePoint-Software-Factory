#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class UpgradeSolution : ConfigurableAction
    {
        public override void Execute()
        {
            DTE dte = GetService<DTE>(true);
            
            if (DeploymentHelpers.CheckRebuildForSelectedProjects(dte))
            {
                DeploymentHelpers.UpgradeSolutions(dte);
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
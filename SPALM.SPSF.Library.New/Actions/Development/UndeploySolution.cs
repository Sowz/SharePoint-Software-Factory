#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class UndeploySolution : ConfigurableAction
    {
        public override void Execute()
        {
            DTE dte = GetService<DTE>(true);
            DeploymentHelpers.UndeploySolutions(dte);
        }

        public override void Undo()
        {

        }
    }
}
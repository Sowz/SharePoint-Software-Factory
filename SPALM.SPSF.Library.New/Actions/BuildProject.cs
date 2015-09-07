#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class BuildProject : ConfigurableAction
    {
        private string _TargetName = "";

        [Input(Required = true)]
        public string TargetName
        {
          get { return _TargetName; }
          set { _TargetName = value; }
        }

        public override void Execute()
        {
            
            DTE service = (DTE)this.GetService(typeof(DTE));
            Project project = Helpers.GetSelectedProject(service);
            service.Solution.SolutionBuild.BuildProject(TargetName, project.UniqueName, true);         
        }

        /// <summary>
        /// Removes the previously added reference, if it was created
        /// </summary>
        public override void Undo()
        {
        }
    }    
}
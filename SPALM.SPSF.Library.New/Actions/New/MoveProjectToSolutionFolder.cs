#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
    /// Adds a new Elements definition, e.g. a contenttype and registers the element in the parent feature
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class MoveProjectToSolutionFolder : ConfigurableAction
    {
        public MoveProjectToSolutionFolder()
        {

        }

        /// <summary>
        /// Contains the name of the solution folder
        /// </summary>
        [Input(Required = true)]
        public string SolutionFolder { get; set; }

        /// <summary>
        /// Contains the name of the project
        /// </summary>
        [Input(Required = true)]
        public string ProjectName { get; set; }

        public override void Execute()
        {
            DTE dte = this.GetService<DTE>(true);

            //Helpers2.MoveProjectToSolutionFolder(dte, ProjectName, SolutionFolder);
            
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Undo()
        {
            //TODO: Delete the created projectitems
        }
    }
}
#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using VSLangProj;

    /// <summary>
    /// Adds a reference to a project
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class AddReferenceToProject : ConfigurableAction
    {
        [Input(Required = true)]
        public string ReferenceName { get; set; }

        [Input(Required = false)]
        public string ProjectName { get; set; }

        [Input(Required = false)]
        public Project Project { get; set; }

        public override void Execute()
        {
            DTE dte = (DTE)this.GetService(typeof(DTE));

            try
            {
                Project destinationProject = null;
                string projectName = "";
                if (Project != null)
                {
                    destinationProject = Project;
                    projectName = destinationProject.Name;
                }
                else
                {
                    destinationProject = Helpers.GetProjectByName(dte, ProjectName);
                    projectName = ProjectName;
                }
                
                if (destinationProject != null)
                {
                    VSProject project = (VSProject)destinationProject.Object;
                    project.References.Add(this.ReferenceName);
                    Helpers.LogMessage(dte, this, "Adding reference '" + ReferenceName + "' to project '" + projectName + "'");
                }
                else
                {
                    Helpers.LogMessage(dte, this, "Error: Could not find project to add reference '" + ReferenceName + "'");
                }
            }
            catch
            {
                Helpers.LogMessage(dte, this, "Error: Could not add reference '" + ReferenceName + "' to project '" + ProjectName + "'");
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Undo()
        {            
        }
    }
}
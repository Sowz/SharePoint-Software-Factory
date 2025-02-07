#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.Windows.Forms;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
    /// Renames a project
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class RenameSolutionFolder : ConfigurableAction
    {
        [Input(Required = true)]
        public string SourceName { get; set; }

        [Input(Required = true)]
        public string TargetName { get; set; }

        public override void Execute()
        {
            try
            {
                DTE service = (DTE)this.GetService(typeof(DTE));

                Solution2 soln = (Solution2)service.Solution;

                Project projectsourcefolder = Helpers.GetSolutionFolder(soln, SourceName);
                if (projectsourcefolder == null)
                {
                    //source folder not found, cannot rename
                    //Helpers.LogMessage(service, this, "Warning: SolutionFolder '" + TargetName + "' already exists");
                    return;
                }

                Project projecttargetfolder = Helpers.GetSolutionFolder(soln, TargetName);
                if (projecttargetfolder != null)
                {
                    Helpers.LogMessage(service, this, "Warning: SolutionFolder '" + TargetName + "' already exists");
                    return;
                }

               
                foreach (Project project in service.Solution.Projects)
                {
                    if (project.Object is SolutionFolder)
                    {
                        SolutionFolder sfolder = project.Object as SolutionFolder;
                        if (project.Name.ToUpper().Equals(SourceName.ToUpper()))
                        {
                            project.Name = TargetName;                        
                            Helpers.LogMessage(service, this, "SolutionFolder '" + SourceName + "' renamed to '" + TargetName + "'");
                            break;
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
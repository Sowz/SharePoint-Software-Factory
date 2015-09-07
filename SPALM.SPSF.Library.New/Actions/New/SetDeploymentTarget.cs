#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.ComponentModel.Design;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.VisualStudio.SharePoint;

    [ServiceDependency(typeof(DTE)), ServiceDependency(typeof(ITypeResolutionService))]
    public class SetDeploymentTarget : ConfigurableAction
    {
        [Input(Required = false)]
        public string DeploymentTarget { get; set; }

        [Input(Required = false)]
        public Project CurrentProject { get; set; }

        [Input(Required = false)]
        public string CurrentProjectName { get; set; }

        public override void Execute()
        {
            if (string.IsNullOrEmpty(DeploymentTarget))
            {
                return;
            }

            var service = this.GetService<DTE>(true);
            if (!string.IsNullOrEmpty(CurrentProjectName))
            {
                CurrentProject = Helpers.GetProjectByName(service, CurrentProjectName);
            }


            try
            {
                ISharePointProjectService projectService = Helpers2.GetSharePointProjectService(service);

                if (CurrentProject != null)
                {
                    try
                    {
                        //set url for current project
                        ISharePointProject sharePointProject = projectService.Convert<Project, ISharePointProject>(CurrentProject);
                        ChangeDeploymentTarget(sharePointProject, DeploymentTarget);
                    }
                    catch { }
                }
                else
                {
                    foreach (ISharePointProject sharePointProject in projectService.Projects)
                    {
                        try
                        {
                            ChangeDeploymentTarget(sharePointProject, DeploymentTarget);
                        }
                        catch { }
                    }
                }
            }
            catch { }

        }

        private void ChangeDeploymentTarget(ISharePointProject sharePointProject, string deploymentTarget)
        {
          if (deploymentTarget.Equals("GAC", StringComparison.InvariantCultureIgnoreCase) || deploymentTarget.Equals("GlobalAssemblyCache", StringComparison.InvariantCultureIgnoreCase))
            {
                sharePointProject.AssemblyDeploymentTarget = AssemblyDeploymentTarget.GlobalAssemblyCache;
            }
            else
            {
                sharePointProject.AssemblyDeploymentTarget = AssemblyDeploymentTarget.WebApplication;
            }
        }

        public override void Undo()
        {
            // nothing to do.
        }
    }
}

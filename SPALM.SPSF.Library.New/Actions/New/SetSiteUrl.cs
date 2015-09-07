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
    public class SetSiteUrl : ConfigurableAction
    {
        [Input(Required = false)]
        public string SiteUrl { get; set; }

        [Input(Required = false)]
        public Project CurrentProject { get; set; }

        [Input(Required = false)]
        public string CurrentProjectName { get; set; }

        public override void Execute()
        {
            if (string.IsNullOrEmpty(SiteUrl))
            {
                return;
            }

            DTE service = this.GetService<DTE>(true);

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
                        sharePointProject.SiteUrl = new Uri(SiteUrl);
                    }
                    catch { }
                }
                else
                {
                    //set url for all projects
                    foreach (ISharePointProject sharePointProject in projectService.Projects)
                    {
                        try
                        {
                            if (sharePointProject.SiteUrl == null)
                            {
                                sharePointProject.SiteUrl = new Uri(SiteUrl);
                                Helpers.LogMessage(service, this, "Settings site url of project  " + sharePointProject.Name + " to " + new Uri(SiteUrl).ToString());
                            }
                        }
                        catch (Exception)
                        {
                            Helpers.LogMessage(service, this, "Could not set site url of project " + sharePointProject.Name + " to " + new Uri(SiteUrl).ToString());
                        }
                    }
                }
            }
            catch { }
        }

        public override void Undo()
        {
            // nothing to do.
        }
    }
}

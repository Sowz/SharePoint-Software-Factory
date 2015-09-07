#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;
    using EnvDTE;
    using Microsoft.VisualStudio.SharePoint;

    /// <summary>
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class ShowProperties : ConfigurableAction
    {
        private ProjectItem projectItem;
        private Project project;

        [Input(Required = false)]
        public ProjectItem SelectedItem
        {
            get { return projectItem; }
            set { projectItem = value; }
        }

        [Input(Required = false)]
        public Project SelectedProject
        {
            get { return project; }
            set { project = value; }
        }

        protected string GetBasePath()
        {
            return base.GetService<IConfigurationService>(true).BasePath;
        }

        public object GetService(object serviceProvider, Type type)
        {
            return GetService(serviceProvider, type.GUID);
        }

        public object GetService(object serviceProviderObject, Guid guid)
        {

            object service = null;
            Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider = null;
            IntPtr serviceIntPtr;
            int hr = 0;
            Guid SIDGuid;
            Guid IIDGuid;

            SIDGuid = guid;
            IIDGuid = SIDGuid;
            serviceProvider = (Microsoft.VisualStudio.OLE.Interop.IServiceProvider)serviceProviderObject;
            hr = serviceProvider.QueryService(ref SIDGuid, ref IIDGuid, out serviceIntPtr);

            if (hr != 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }
            else if (!serviceIntPtr.Equals(IntPtr.Zero))
            {
                service = Marshal.GetObjectForIUnknown(serviceIntPtr);
                Marshal.Release(serviceIntPtr);
            }

            return service;
        }

        public override void Execute()
        {
            DTE service = (DTE)this.GetService(typeof(DTE)); 

            try
            {
                if (projectItem != null)
                {
                    Helpers.LogMessage(service, this, "*********** SharePointService Properties ********************");
                    try
                    {
                        ISharePointProjectService projectService = Helpers2.GetSharePointProjectService(service);
                        ISharePointProject sharePointProject = projectService.Convert<Project, ISharePointProject>(project);

                        try
                        {   //is it a feature
                            ISharePointProjectFeature sharePointFeature = projectService.Convert<ProjectItem, ISharePointProjectFeature>(projectItem);

                        }
                        catch { }

                        try
                        {   //is it a feature
                            ISharePointProjectItem sharePointItem = projectService.Convert<ProjectItem, ISharePointProjectItem>(projectItem);
                            
                        }
                        catch { }

                        try
                        {   //is it a feature
                            ISharePointProjectItemFile sharePointItemFile = projectService.Convert<ProjectItem, ISharePointProjectItemFile>(projectItem);
                            
                        }
                        catch { }

                        try
                        {   //is it a mapped folder
                            IMappedFolder sharePointMappedFolder = projectService.Convert<ProjectItem, IMappedFolder>(projectItem);

                        }
                        catch { }

                        try
                        {   //is it a mapped folder
                            ISharePointProjectPackage sharePointProjectPackage = projectService.Convert<ProjectItem, ISharePointProjectPackage>(projectItem);

                        }
                        catch { }
                    }
                    catch{}

                    //codelements
                    Helpers.LogMessage(service, this, "*********** EnvDTE CodeElements ********************");
                    
                    foreach (CodeElement codeElement in projectItem.FileCodeModel.CodeElements)
                    {
                        try
                        {
                            Helpers.LogMessage(service, this, "codeElement.FullName: " + codeElement.FullName);
                            Helpers.LogMessage(service, this, "codeElement.Type: " + codeElement.GetType().ToString());
                            Helpers.LogMessage(service, this, "codeElement.Kind: " + codeElement.Kind.ToString());
                        }
                        catch {}
                    }

                    Helpers.LogMessage(service, this, "*********** EnvDTE Properties ********************");
                    for (int i = 0; i < projectItem.Properties.Count; i++)
                    {
                        try
                        {
                            string name = projectItem.Properties.Item(i).Name;
                            string value = "";
                            try
                            {
                                value = projectItem.Properties.Item(i).Value.ToString();
                            }
                            catch (Exception)
                            {
                            }
                            Helpers.LogMessage(service, this, name + "=" + value);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else if (project != null)
                {
                    for (int i = 0; i < project.Properties.Count; i++)
                    {
                        try
                        {
                            string name = project.Properties.Item(i).Name;
                            string value = "";
                            try
                            {
                                value = project.Properties.Item(i).Value.ToString();
                            }
                            catch (Exception)
                            {
                            }
                            Helpers.LogMessage(service, this, name + "=" + value);

                            if (project.Properties.Item(i).Collection.Count > 0)
                            {
                            }
                        }
                        catch (Exception)
                        {
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Helpers.LogMessage(service, this, ex.ToString());
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
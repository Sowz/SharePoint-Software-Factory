#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Process = System.Diagnostics.Process;

    /// <summary>
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class OpenItemInHive : ConfigurableAction
    {
        private ProjectItem projectItem;

        [Input(Required = true)]
        public ProjectItem SelectedItem
        {
            get { return projectItem; }
            set { projectItem = value; }
        }

        public override void Execute()
        {
            DTE service = (DTE)this.GetService(typeof(DTE));

            try
            {
                if (projectItem != null)
                {
                    string itemPath = Helpers.GetSharePointHive() + DeploymentHelpers.GetDeploymentPathOfItem(service, projectItem); //path starting with \\template

                    bool completePathAvailable = true;
                    char[] sep = { '\\' };
                    string[] folders = itemPath.Split(sep);

                    string existingFolder = folders[0];
                    for (int i = 1; i < folders.Length; i++)
                    {
                        if (Directory.Exists(existingFolder + "\\" + folders[i]))
                        {
                            existingFolder = existingFolder + "\\" + folders[i];
                        }
                        else
                        {
                            completePathAvailable = false;
                            break;
                        }
                    }

                    if (completePathAvailable)
                    {
                        Helpers.LogMessage(service, this, "Opening folder '" + itemPath + "'.");
                    }
                    else
                    {
                        Helpers.LogMessage(service, this, "Folder '" + itemPath + "' does not exists. Opening existing part of folder.");
                    }
                   

                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = existingFolder;
                    psi.Arguments = "";
                    psi.CreateNoWindow = true;
                    psi.UseShellExecute = true;
                    Process p = new Process();
                    p.StartInfo = psi;
                    p.Start();

                    return;
                }
            }
            catch (Exception)
            {
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
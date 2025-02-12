#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;
    using Process = System.Diagnostics.Process;

    [ServiceDependency(typeof(DTE))]
    public class AddKeyFileToSolution : ConfigurableAction
    {
        [Input(Required = true)]
        public string KeyFileName
        {
            get { return _KeyFileName; }
            set { _KeyFileName = value; }
        }
        private string _KeyFileName = "";

        protected string GetBasePath()
        {
            return base.GetService<IConfigurationService>(true).BasePath;
        }

        public override void Execute()
        {
            string snPath = GetBasePath() + "\\Tools\\sn.exe";

            DTE dte = this.GetService<DTE>(true);
            string solutionPath = (string)dte.Solution.Properties.Item("Path").Value;
            string solutionDir = Path.GetDirectoryName(solutionPath);
            string keyFilePath = Path.Combine(solutionDir, this.KeyFileName);

            // Launch the process and wait until it's done (with a 10 second timeout).
            ProcessStartInfo startInfo = new ProcessStartInfo(snPath, string.Format("-k \"{0}\"", keyFilePath));
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            Process snProcess = Process.Start(startInfo);
            snProcess.WaitForExit(20000);

            try
            {
                Solution2 soln = (Solution2)dte.Solution;
                Project projecttargetfolder = Helpers.GetSolutionFolder(soln, "ApplicationConfiguration");
                if (projecttargetfolder == null)
                {
                    projecttargetfolder = soln.AddSolutionFolder("ApplicationConfiguration");
                }

                if (projecttargetfolder != null)
                {
                    try
                    {
                        Helpers.AddFromFile(projecttargetfolder, keyFilePath);
                        dte.ActiveWindow.Close(vsSaveChanges.vsSaveChangesNo);
                    }
                    catch (Exception ex)
                    {
                        Helpers.LogMessage(dte, this, ex.ToString());
                    }
                }
                //dte.Solution.AddFromFile(keyFilePath, false);
                //dte.ActiveWindow.Close(EnvDTE.vsSaveChanges.vsSaveChangesNo);
                /*
                                DteHelper.SelectSolution(dte);
                                dte.ItemOperations.AddExistingItem(keyFilePath);
                                dte.ActiveWindow.Close(EnvDTE.vsSaveChanges.vsSaveChangesNo);
                 * */
            }
            catch (Exception)
            {


            }
        }

        public override void Undo()
        {
        }
    }
}
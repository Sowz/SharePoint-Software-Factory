#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System.IO;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Process = System.Diagnostics.Process;

    /// <summary>
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class RunBatch : ConfigurableAction
    {
			private ProjectItem _BatchFile = null;

        [Input(Required = true)]
        public ProjectItem BatchFile
        {
					get { return _BatchFile; }
					set { _BatchFile = value; }
        }

        public override void Execute()
        {
            string targetsFilePath = "";
						if (_BatchFile != null)
            {
							targetsFilePath = Helpers.GetFullPathOfProjectItem(_BatchFile);
            }

            Process snProcess = new Process();
						snProcess.StartInfo.FileName = targetsFilePath;
            snProcess.StartInfo.Arguments = "";
            snProcess.StartInfo.CreateNoWindow = false;
            snProcess.StartInfo.UseShellExecute = false;
						snProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(targetsFilePath);
            snProcess.Start();          
        }

        /// <summary>
        /// Removes the previously added reference, if it was created
        /// </summary>
        public override void Undo()
        {
        }
    }    
}
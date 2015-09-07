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

    [ServiceDependency(typeof(DTE))]
    public class GoToTraceLogs : ConfigurableAction
    {

      public override void Execute()
      {
        DTE dte = GetService<DTE>(true);

        try
        {
            string traceLogPath = new SharePointBrigdeHelper(dte).GetPathToTraceLogs();
            if (!Directory.Exists(traceLogPath))
            {
                throw new FileNotFoundException("Trace Log folder " + traceLogPath + " not found");
            }

            DateTime oldestDate = DateTime.Now.AddYears(-10);
            string newestFile = "";
            DirectoryInfo traceDir = new DirectoryInfo(traceLogPath);
            foreach(FileSystemInfo fileInfo in traceDir.GetFileSystemInfos("*.log"))
            {
                if (fileInfo.LastWriteTime > oldestDate)
                {
                    oldestDate = fileInfo.LastWriteTime;
                    newestFile = fileInfo.FullName;
                }
            }

            if (!string.IsNullOrEmpty(newestFile))
            {
                traceLogPath = newestFile;
            }

            //start process with ShellExecute
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = traceLogPath;
            psi.Arguments = "";
            psi.CreateNoWindow = true;
            psi.UseShellExecute = true;
            Process p = new Process();
            p.StartInfo = psi;
            p.Start();
        }
        catch (Exception ex)
        {
            Helpers.LogMessage(dte, this, ex.Message);
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
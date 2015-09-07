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
    public class GoToTraceLogsFolder : ConfigurableAction
    {

      public override void Execute()
      {
        DTE dte = GetService<DTE>(true);

        try
        {
            string traceLogPath = new SharePointBrigdeHelper(dte).GetPathToTraceLogs();
            Helpers.LogMessage(dte, this, "Trace log location: " + traceLogPath);

            if (!Directory.Exists(traceLogPath))
            {
                throw new FileNotFoundException("Trace Log folder " + traceLogPath + " not found");
            }

            //start process with ShellExecute
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "explorer.exe";
            psi.Arguments = "\"" + traceLogPath + "\"";
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
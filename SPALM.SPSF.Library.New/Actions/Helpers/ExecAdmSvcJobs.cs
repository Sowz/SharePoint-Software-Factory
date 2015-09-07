#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class ExecAdmSvcJobs : ConfigurableAction
    {
      DTE dte = null;

      public override void Execute()
      {
        dte = GetService<DTE>(true);
        string STSAdmPath = Helpers.GetSharePointHive() + "\\BIN\\stsadm.exe";
        ProcessExec exec = new ProcessExec(dte, STSAdmPath, "-o execadmsvcjobs", "Running stsadm -o execadmsvcjobs");
        exec.RunProcess();
      }

      public override void Undo()
      {

      }
  }    
}
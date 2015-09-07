#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class IISReset : ConfigurableAction
    {
      DTE dte = null;

      public override void Execute()
      {
        dte = GetService<DTE>(true);
				ProcessExec exec = new ProcessExec(dte, "iisreset", "", "Running iisreset");
				exec.RunProcess();

				
      }

        /// <summary>
        /// Removes the previously added reference, if it was created
        /// </summary>
        public override void Undo()
        {
        }      
    } 
}
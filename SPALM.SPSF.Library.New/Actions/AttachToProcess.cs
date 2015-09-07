#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.Windows.Forms;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class AttachToProcess : ConfigurableAction
    {
        private int _ProcessID = 0;

        [Input(Required = true)]
        public int ProcessID
        {
          get { return _ProcessID; }
          set { _ProcessID = value; }
        }

        public override void Execute()
        {
            DTE service = (DTE)this.GetService(typeof(DTE));

            try
            {
              Processes processes = service.Debugger.LocalProcesses;
              foreach (Process proc in processes)
              {
                if (proc.ProcessID == ProcessID)
                {
                  Helpers.LogMessage(service, this, "Attaching to process " + proc.Name + " (" + ProcessID.ToString() + ")");
                  proc.Attach();
                }
              }
            }
            catch (Exception ex)
            {
              MessageBox.Show("Process not found. Maybe the process is not started");
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
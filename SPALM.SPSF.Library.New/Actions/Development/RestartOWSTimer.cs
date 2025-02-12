#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.ServiceProcess;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class RestartOWSTimer : ConfigurableAction
    {
      DTE dte = null;

      public override void Execute()
      {
        dte = GetService<DTE>(true);
        RestartTimerService(dte);
        Helpers.LogMessage(dte, dte, "*** Restart OWSTimer finished ***");
        Helpers.LogMessage(dte, dte, "");
      }


      private bool RestartTimerService(DTE dte)
      {
          try
          {
              if (CheckAndStartService(dte, "SPTimerV3"))
              {
                  return true;
              }
              if (CheckAndStartService(dte, "SPTimerV4"))
              {
                  return true;
              }
              if (CheckAndStartService(dte, "WSSTimerV3"))
              {
                  return true;
              }
              if (CheckAndStartService(dte, "WSSTimerV4"))
              {
                  return true;
              }
          }
          catch { }
          return false;
      }

      private bool CheckAndStartService(DTE dte, string serviceName)
      {
          ServiceController sc = null;

          try
          {
              sc = new ServiceController(serviceName);
              if (!sc.CanStop)
              {
                  return false;
              }
          }
          catch (Exception)
          {
              //service is not installed, ignore
              return false;
          }

          if (sc == null)
          {
              return false;
          }

          try
          {
              if (sc.Status == ServiceControllerStatus.Running)
              {
                  sc.Stop();
                  sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 0, 10));
                  sc.Start();
                  sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 0, 10));
                  return true;
              }

              Helpers.LogMessage(dte, dte, "Service '" + serviceName + "' could not be restarted within 10 seconds");
          }
          catch(Exception ex)
          {
              Helpers.LogMessage(dte, dte, "Could not restart service '" + serviceName + "': " + ex.Message);
          }
          return false;
      }

        /// <summary>
        /// Removes the previously added reference, if it was created
        /// </summary>
        public override void Undo()
        {
        }      
    } 
}
namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class ProcessIdValueProvider : ValueProvider
    {
      //Returns the ID of a given Process

      private string _ProcessName = "";
        public string ProcessName
        {
          get { return _ProcessName; }
          set { _ProcessName = value; }
        }

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
          if (currentValue != null)
          {
            // Do not assign a new value, and return false to flag that 
            // we don't want the current value to be changed.
            newValue = null;
            return false;
          }

            newValue = "";

            DTE service = (DTE)this.GetService(typeof(DTE));
            
           foreach(Process proc in service.Debugger.LocalProcesses)
           {
             if(proc.Name.EndsWith("OWSTIMER.EXE", StringComparison.InvariantCultureIgnoreCase))
             {
               newValue = proc.ProcessID;
               return true;
             }
           }
          
           newValue = 0;
           return true;
        }       
    }
}


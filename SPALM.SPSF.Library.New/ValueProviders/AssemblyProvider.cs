namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;

    [ServiceDependency(typeof(DTE))]
    public class AssemblyProvider : ValueProvider
    {
        protected string GetBasePath()
        {
            return base.GetService<IConfigurationService>(true).BasePath;
        }

        public override bool OnBeforeActions(object currentValue, out object newValue)
        {
            return SetValue(currentValue, out newValue);
        }

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            return SetValue(currentValue, out newValue);
        }

        private bool SetValue(object currentValue, out object newValue)
        {
          if (currentValue != null)
          {
            // Do not assign a new value, and return false to flag that 
            // we don't want the current value to be changed.
            newValue = null;
            return false;
          }

            DTE service = (DTE)this.GetService(typeof(DTE));
            
            string snPath = GetBasePath() + "\\Tools\\sn.exe";

            try
            {
              string assembly = Helpers.GetAssemblyName(Helpers.GetSelectedProject(service), snPath);
              if (assembly != "")
              {
                newValue = assembly;
                return true;
              }
            }
            catch (Exception)
            {
            }

            newValue = "";
            return true;
        }
    }
}


namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class ApplicationPoolProvider : ValueProvider
    {
        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            if (currentValue != null)
            {
                // Do not assign a new value, and return false to flag that 
                // we don't want the current value to be changed.
                newValue = null;
                return false;
            }

            try
            {
                DTE dte = (DTE)this.GetService(typeof(DTE));
                Project currentProject = Helpers.GetSelectedProject(dte);
                if (currentProject != null)
                {
                    string applicationPool = Helpers.GetAppPoolOfWebApp(dte, Helpers.GetBasePath());
                    newValue = applicationPool;
                    return true;
                }
            }
            catch { }

            newValue = null;
            return true;           
        }
    }
}


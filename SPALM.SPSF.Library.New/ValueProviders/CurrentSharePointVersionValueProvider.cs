namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
  /// checks all projects and checks what sharepoint version is used in the projects
  /// </summary>
    [ServiceDependency(typeof(DTE))]
  public class CurrentSharePointVersionValueProvider : ValueProvider
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

            DTE dte = (DTE)this.GetService(typeof(DTE));
            
            newValue = Helpers.GetApplicationConfigValue(dte, "SharePointVersion", "15");
            return true;
        }
    }
}


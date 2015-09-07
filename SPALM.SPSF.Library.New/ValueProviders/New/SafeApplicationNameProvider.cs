namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class SafeApplicationNameProvider : ValueProvider
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

            
            DTE service = (DTE)this.GetService(typeof(DTE));
            
            newValue = Helpers.GetSaveApplicationName(service);
            return true;
        }
    }
}


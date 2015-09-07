namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
    /// returns the name of the application, taken from the solution name
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class ApplicationNameProvider : ValueProvider
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

            newValue = "";

            DTE service = (DTE)this.GetService(typeof(DTE));
            newValue = Helpers.GetApplicationName(service);
            return true;
        }
    }
}


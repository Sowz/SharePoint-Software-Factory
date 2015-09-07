namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class InstalledSilverlightVersionProvider : ValueProvider
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

            bool silverlightIsInstalled = false;
            if (!silverlightIsInstalled)
            {
                throw new Exception("Silverlight SDK is not installed. Please install runtime and SDK.");
            }

            DTE service = (DTE)this.GetService(typeof(DTE));
            string sharePointVersionOfApplication = Helpers.GetSharePointVersion(service);
            newValue = false;
            return true;
        }
    }
}


namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
    /// Provides the version of Visual Studio
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class VisualStudioVersionProvider : ValueProvider
    {
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
            DTE dte = this.GetService<DTE>(true);
            
            newValue = dte.Version;
            return true;
        }  
    }
}

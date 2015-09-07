namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class CurrentItemProvider : ValueProvider
    {
        // Methods
        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            DTE service = (DTE)this.GetService(typeof(DTE));
            newValue = service.SelectedItems.Item(1).ProjectItem;
            return true;
        }
    }


}

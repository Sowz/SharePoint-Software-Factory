namespace SPALM.SPSF.Library.ValueProviders
{
    using Microsoft.Practices.RecipeFramework;

    public class DefaultNameValueCollectionProvider : ValueProvider
    {
        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            if (currentValue != null)
            {                
                newValue = null;
                return false;
            }
            
            newValue = new NameValueItem[0];
            return true;
            
        }
    }
}

namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class SolutionSubFolderNameProvider : ValueProvider
    {
      public override bool OnBeforeActions(object currentValue, out object newValue)
      {
        if (currentValue != null)
        {
          // Do not assign a new value, and return false to flag that 
          // we don't want the current value to be changed.
          newValue = null;
          return false;
        }

        //is there a separate Project for resources?
        DTE dte = this.GetService<DTE>(true);
        
        newValue = Helpers.GetSaveApplicationName(dte);
        return true;
      }
    }
}

namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using System.ComponentModel.Design;
    using Microsoft.Practices.RecipeFramework;

    public class VisualStudioCreateProjectName : ValueProvider
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

      IDictionaryService dictionaryService = (IDictionaryService)this.GetService(typeof(IDictionaryService));
      try
      {
        string itemname = dictionaryService.GetValue("ProjectName").ToString();
        if (itemname != "")
        {
          newValue = itemname;
          return true;
        }
      }
      catch (Exception)
      {
      }

      newValue = null;
      return false;
    }
  }
}

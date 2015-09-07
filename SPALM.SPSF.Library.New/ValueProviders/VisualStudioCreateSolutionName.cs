namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using System.ComponentModel.Design;
    using System.Text.RegularExpressions;
    using Microsoft.Practices.RecipeFramework;

    public class VisualStudioCreateSolutionName : ValueProvider
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
        string solutionName = dictionaryService.GetValue("SolutionName").ToString();
        solutionName = Regex.Replace(solutionName, @"[^\w\.@-]", string.Empty);
        //solutionName.Replace(" ", "_");
        newValue = solutionName;
        return true;
      }
      catch (Exception)
      {
       
      }

      newValue = "Defaultname";
      return true;
    }
  }
}

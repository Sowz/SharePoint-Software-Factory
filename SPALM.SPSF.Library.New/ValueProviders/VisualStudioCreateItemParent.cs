namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using System.ComponentModel.Design;
    using EnvDTE;
    using Microsoft.Practices.RecipeFramework;

    public class VisualStudioCreateItemParent : ValueProvider
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
        //gets the current parent item when add new item is started in vs.net
        object pitems = dictionaryService.GetValue("ProjectItems");
        if (pitems is ProjectItems)
        {
          ProjectItems pi = (ProjectItems)pitems;
          if (pi.Parent is Project)
          {
            //parent is project
            //x = ((Project)pi.Parent).Name;
          }
          else if (pi.Parent is ProjectItem)
          {
            //parent is a folder
            ProjectItem pit = (ProjectItem)pi.Parent;
            newValue = pit;
            return true;
          }
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

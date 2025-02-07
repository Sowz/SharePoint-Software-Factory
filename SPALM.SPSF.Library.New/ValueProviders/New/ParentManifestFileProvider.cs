namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;

    /// <summary>
      /// for manifest recipes the parent elementmanifest folder is needed, this class returns the elements.xml ProjectItem
      /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class ParentManifestFileProvider : ValueProvider
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
        if (currentValue != null)
        {
          newValue = null;
          return false;
        }

        DTE dte = this.GetService<DTE>(true);

        //first we need the xpath, which is needed by the manifest-recipe
        string manifestXpath = "";
        string manifestXpathNamespace = "";
        IConfigurationService b = (IConfigurationService)GetService(typeof(IConfigurationService));
        Microsoft.Practices.RecipeFramework.Configuration.Recipe recipe = b.CurrentRecipe;
        if (recipe.HostData.Any.Attributes["XPath"] != null)
        {
            manifestXpath = recipe.HostData.Any.Attributes["XPath"].Value;
        }
        if (recipe.HostData.Any.Attributes["XPathNamespace"] != null)
        {
            manifestXpathNamespace = recipe.HostData.Any.Attributes["XPathNamespace"].Value;
        }

        //if file is selected we check, if the xpath can be found in this item
        //if folder is selected we search in this folder for the elements.xml which contains the xpath
        if (dte.SelectedItems.Count > 0)
        {
            SelectedItem item = dte.SelectedItems.Item(1);
            ProjectItem selectedItem = null;

            if (item is ProjectItem)
            {
                selectedItem = item as ProjectItem;
            }
            else if (item.ProjectItem is ProjectItem)
            {
                selectedItem = item.ProjectItem as ProjectItem;
            }

            if (selectedItem != null)
            {
                if (selectedItem.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
                {
                    //is the xml file clicked?
                    try
                    {
                        if (Helpers2.IsXPathInFile(selectedItem, manifestXpath, manifestXpathNamespace))
                        {
                            newValue = selectedItem;
                            return true;
                        }
                    }
                    catch { }
                }
                else
                {
                    //folder is clicked, we search for xml files below the folder which contains the needed xpath
                    foreach (ProjectItem childItem in selectedItem.ProjectItems)
                    {
                        if (Helpers2.IsXPathInFile(childItem, manifestXpath, manifestXpathNamespace))
                        {
                            newValue = childItem;
                            return true;
                        }
                    }
                }
            }
        }
        //string projectId = currentProject.

        newValue = null;
        return false;
      }      
    }
}

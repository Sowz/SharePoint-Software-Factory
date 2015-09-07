namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class DefaultNamespaceProvider : ValueProvider
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

            DTE service = (DTE)this.GetService(typeof(DTE));
            
            Project project = Helpers.GetSelectedProject(service);
            if (project == null)
            {
                throw new Exception("No Project selected: count " + service.SelectedItems.Count.ToString());
            }

            if (project != null)
            {
              newValue = project.Properties.Item("DefaultNamespace").Value;
              return true;
            }
            newValue = null;
            return false;
        }
    }
}

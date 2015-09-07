namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
  /// returns the id of the current selected project
  /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class ProjectIdProvider : ValueProvider
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

        Project currentProject = Helpers.GetSelectedProject(dte);
        //string projectId = currentProject.

        newValue = Helpers2.GetProjectGuid(currentProject).ToString();
        return true;
      }      
    }
}

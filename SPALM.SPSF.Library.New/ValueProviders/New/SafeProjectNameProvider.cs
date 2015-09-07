namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class SafeProjectNameProvider : ValueProvider
    {
        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            if (currentValue != null)
            {
                newValue = null;
                return false;
            }

            DTE dte = (DTE)this.GetService(typeof(DTE));
            string resourcefilename = "";
            Project project = Helpers.GetSelectedProject(dte);
            if (project != null)
            {
                resourcefilename = Helpers.GetSaveApplicationName(dte) + SPSFConstants.NameSeparator + Helpers.GetSaveProjectName(project);
                newValue = resourcefilename;
                return true;
            }

            newValue = "Resources.resx";
            return true;
        }
    }
}


namespace SPALM.SPSF.Library.ValueProviders
{
    using System.Collections.Specialized;
    using EnvDTE;
    using Microsoft.Practices.Common;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class ProjectIsMissingProvider : ValueProvider, IAttributesConfigurable
    {
        private string _ProjectName = "";
        public string ProjectName
        {
            get { return _ProjectName; }
            set { _ProjectName = value; }
        }

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

            Project project = Helpers.GetProjectByName(service, ProjectName);
            if (project != null)
            {
                newValue = false;
                return true;
            }

            newValue = true;
            return true;
        }

        #region IAttributesConfigurable Members

        void IAttributesConfigurable.Configure(StringDictionary attributes)
        {
            if (attributes["ConfigValue"] != null)
            {
                ProjectName = attributes["ProjectName"];
            }
        }

        #endregion
    }
}


namespace SPALM.SPSF.Library.ValueProviders
{
    using System.Collections.Specialized;
    using System.ComponentModel.Design;
    using EnvDTE;
    using Microsoft.Practices.Common;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    //returns the id of the feature with the given name
    [ServiceDependency(typeof(DTE))]
    public class FeatureScopeProvider : ValueProvider, IAttributesConfigurable
    {
        private string RecipeArgument = "";

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
          if (currentValue != null)
          {
            newValue = null;
            return false;
          }
          newValue = null;
          return this.Evaluate(out newValue);
        }

        public override bool OnBeforeActions(object currentValue, out object newValue)
        {
          if (currentValue != null)
          {
            newValue = null;
            return false;
          }
          newValue = null;
          return this.Evaluate(out newValue);
        }

        private bool Evaluate(out object newValue)
        {          
          DTE service = (DTE)this.GetService(typeof(DTE));

          IDictionaryService dictionaryService = (IDictionaryService)ServiceHelper.GetService(this, typeof(IDictionaryService));
          try
          {            
              string featureName = dictionaryService.GetValue(RecipeArgument).ToString();
              ProjectItem featureXMLFile = Helpers.GetFeatureXML(Helpers.GetSelectedProject(service), featureName);
              newValue = Helpers.GetFeatureScope(featureXMLFile);
              if (newValue == null)
              {
                  newValue = "Web";
              }
              return true;
          }
          catch
          {
              newValue = "Web";
              return true;
          }
        }

        #region IAttributesConfigurable Members

        public void Configure(StringDictionary attributes)
        {
            if (attributes["RecipeArgument"] != null)
            {
                RecipeArgument = attributes["RecipeArgument"];
            }
        }

        #endregion
    }
}

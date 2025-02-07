namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel.Design;
    using EnvDTE;
    using Microsoft.Practices.Common;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    //removes all special characters from a string so that the returned string could be used e.g. as a resource key
    [ServiceDependency(typeof(DTE))]
    public class ConcatStringProvider : ValueProvider, IAttributesConfigurable
    {
        private string RecipeArgument = "";
        private string Prefix = "";
        private string Sufix = "";

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
          newValue = null;
          return this.Evaluate(out newValue);
        }

        public override bool OnBeforeActions(object currentValue, out object newValue)
        {
          newValue = null;
          return this.Evaluate(out newValue);
        }

        private bool Evaluate(out object newValue)
        {          
          IDictionaryService dictionaryService = (IDictionaryService)ServiceHelper.GetService(this, typeof(IDictionaryService));
          try
          {
              string argumentvalue = dictionaryService.GetValue(RecipeArgument).ToString();
              newValue = Prefix + argumentvalue + Sufix;
              return true;
          }
          catch (Exception)
          {
          }
          newValue = null;
          return false;
        }

        #region IAttributesConfigurable Members

        public void Configure(StringDictionary attributes)
        {
            if (attributes["RecipeArgument"] != null)
            {
                RecipeArgument = attributes["RecipeArgument"];
            }
            if (attributes["Prefix"] != null)
            {
              Prefix = attributes["Prefix"];
            }
            if (attributes["Sufix"] != null)
            {
              Sufix = attributes["Sufix"];
            }
        }

        #endregion
    }
}

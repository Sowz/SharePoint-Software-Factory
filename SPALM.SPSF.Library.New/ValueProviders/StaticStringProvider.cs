namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using System.Collections.Specialized;
    using EnvDTE;
    using Microsoft.Practices.Common;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    //returns a fixed string
    [ServiceDependency(typeof(DTE))]
  public class StaticStringProvider : ValueProvider, IAttributesConfigurable
    {
        private string _StaticString = "";

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
          if (currentValue != null)
          {
            newValue = null;
            return false;
          }
          if (this.Argument.Type == typeof(Boolean))
          {
            newValue = Boolean.Parse(_StaticString);
            return true;
          }
          else if (this.Argument.Type == typeof(Int32))
          {
            newValue = Int32.Parse(_StaticString);
            return true;
          }

          newValue = _StaticString;
          return true;
        }

        public override bool OnBeforeActions(object currentValue, out object newValue)
        {
          if (currentValue != null)
          {
            newValue = null;
            return false;
          }
          if (this.Argument.Type == typeof(Boolean))
          {
            newValue = Boolean.Parse(_StaticString);
            return true;
          }
          if (this.Argument.Type == typeof(Int32))
          {
            newValue = Int32.Parse(_StaticString);
            return true;
          }
          newValue = _StaticString;
          return true;
        }

        #region IAttributesConfigurable Members

        public void Configure(StringDictionary attributes)
        {
          if (attributes["StaticString"] != null)
          {
            _StaticString = attributes["StaticString"];
          }          
        }

        #endregion
    }
}

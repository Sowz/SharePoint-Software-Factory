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
    public class ConvertedStringProvider : ValueProvider, IAttributesConfigurable
    {
        private string RecipeArgument = "";

        public override bool OnBeforeActions(object currentValue, out object newValue)
        {
          if (currentValue != null)
          {
            // Do not assign a new value, and return false to flag that 
            // we don't want the current value to be changed.
            newValue = null;
            return false;
          }

            try
            {
                IDictionaryService dictionaryService = (IDictionaryService)ServiceHelper.GetService(this, typeof(IDictionaryService));
                string argumentvalue = dictionaryService.GetValue(RecipeArgument).ToString();
                argumentvalue = argumentvalue.Replace(" ","");
                argumentvalue = argumentvalue.Replace("-","");
                argumentvalue = argumentvalue.Replace("(","");
                argumentvalue = argumentvalue.Replace(")","");
                argumentvalue = argumentvalue.Replace("&","");
                argumentvalue = argumentvalue.Replace("_", "");
                newValue = argumentvalue;
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
        }

        #endregion
    }
}

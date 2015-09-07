namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel.Design;
    using EnvDTE;
    using Microsoft.Practices.Common;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
    /// Provides the name of a nameValueItem as string
    /// </summary>
    public class NameValueItemNameProvider : ValueProvider, IAttributesConfigurable
    {
        private string _defaultValue = "";
        private string _recipeArgument = "";
        private bool _makeSafe = true;

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
            DTE service = (DTE)this.GetService(typeof(DTE));

            IDictionaryService dictionaryService = (IDictionaryService)this.GetService(typeof(IDictionaryService));
         
            if (_recipeArgument != "")
            {
                try
                {
                    NameValueItem arg = dictionaryService.GetValue(_recipeArgument) as NameValueItem;
                    if (arg != null)
                    {
                        string name = arg.Name;
                        if (_makeSafe)
                        {
                            name = Helpers.GetSaveName(name);
                        }

                        newValue = name;
                        return true;
                    }
                }
                catch (Exception)
                {
                }
            }

            if (_defaultValue != "")
            {
                newValue = _defaultValue;
                return true;
            }

            newValue = null;
            return false;
        }

        #region IAttributesConfigurable Members

        public void Configure(StringDictionary attributes)
        {
            if (attributes["DefaultValue"] != null)
            {
                _defaultValue = attributes["DefaultValue"];
            }
            if (attributes["RecipeArgument"] != null)
            {
                _recipeArgument = attributes["RecipeArgument"];
            }
            if (attributes["MakeSafe"] != null)
            {
                try
                {
                    _makeSafe = Boolean.Parse(attributes["MakeSafe"]);
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion
    }
}

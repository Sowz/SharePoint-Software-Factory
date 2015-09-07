namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel.Design;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using EnvDTE;
    using Microsoft.Practices.Common;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class SerializedVersionProvider : ValueProvider, IAttributesConfigurable
    {
        private string RecipeArgument = "";


        public override bool OnBeforeActions(object currentValue, out object newValue)
        {
            return SetValue(currentValue, out newValue);
        }

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            return SetValue(currentValue, out newValue);
        }

        #region IAttributesConfigurable Members

        void IAttributesConfigurable.Configure(StringDictionary attributes)
        {
            if (attributes["RecipeArgument"] != null)
            {
                RecipeArgument = attributes["RecipeArgument"];
            }
        }

        #endregion

        private bool SetValue(object currentValue, out object newValue)
        {
            DTE dte = this.GetService<DTE>(true);
            IDictionaryService dictionaryService = (IDictionaryService)ServiceHelper.GetService(this, typeof(IDictionaryService));

            try
            {
                if (!string.IsNullOrEmpty(RecipeArgument))
                {
                    
                    string argumentvalue = dictionaryService.GetValue(RecipeArgument).ToString();
                    Version v = new Version(argumentvalue);
                    if (v != null)
                    {
                        byte[] bytes;
                        long length = 0;
                        MemoryStream ws = new MemoryStream();
                        BinaryFormatter sf = new BinaryFormatter();
                        sf.Serialize(ws, v);
                        length = ws.Length;
                        bytes = ws.GetBuffer();
                        newValue = Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None);
                        return true;
                    }
                }
            }
            catch { }
            //string projectId = currentProject.

            newValue = "";
            return false;
        }
    }
}

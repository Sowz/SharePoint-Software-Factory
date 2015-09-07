namespace SPALM.SPSF.Library.ValueProviders
{
    using System.Collections.Specialized;
    using System.IO;
    using EnvDTE;
    using Microsoft.Practices.Common;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;

    [ServiceDependency(typeof(DTE))]
    public class BrandingImageProvider : ValueProvider, IAttributesConfigurable
    {
        private string Logo = "";

        protected string GetBasePath()
        {
          return this.GetService<IConfigurationService>(true).BasePath;
        }

        private string GetTemplateBasePath()
        {
          return new DirectoryInfo(this.GetBasePath() + @"\Templates").FullName;
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

          GuidancePackage package = (GuidancePackage)GetService(typeof(IExecutionService));
          string temp = package.BasePath;
          /*
          if (Directory.Exists(temp))
          {
            basepath = temp;
            temp = string.Format(CultureInfo.InvariantCulture, @"{0}\Templates\Branding", basepath);
            templatepath = temp;
          }
           * */

          DTE service = (DTE)this.GetService(typeof(DTE));
          newValue = Path.Combine(temp, Logo);
          return true;
        }

        #region IAttributesConfigurable Members

        public void Configure(StringDictionary attributes)
        {
          if (attributes["Logo"] != null)
          {
            Logo = attributes["Logo"];
          }
        }

        #endregion
    }
}


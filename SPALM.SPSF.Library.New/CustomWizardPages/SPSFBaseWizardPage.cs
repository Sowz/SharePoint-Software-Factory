namespace SPALM.SPSF.Library.CustomWizardPages
{
    using System.ComponentModel.Design;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework.Services;
    using Microsoft.Practices.WizardFramework;

    [ServiceDependency(typeof(IConfigurationService))]
    public class SPSFBaseWizardPage : CustomWizardPage
    {
        public SPSFBaseWizardPage()
            : base()
        {
        }

        public SPSFBaseWizardPage(WizardForm parent)
            : base(parent)
        {
        }

        internal BrandingPanel bPanel = null;
        internal bool HasBeenActivated;

        internal void AddBrandingPanel()
        {
            if (!HasBeenActivated)
            {
                string basePath = "";
                string recipeName = "";
                Microsoft.VisualStudio.VSHelp.Help helpService = null;

                try
                {
                    IConfigurationService s = this.GetService(typeof(IConfigurationService)) as IConfigurationService;
                    basePath = s.BasePath;

                    helpService = (Microsoft.VisualStudio.VSHelp.Help)base.GetService(typeof(Microsoft.VisualStudio.VSHelp.Help));

                    IDictionaryService dictionaryService = GetService(typeof(IDictionaryService)) as IDictionaryService;
                    recipeName = dictionaryService.GetValue("RecipeName").ToString();
                }
                catch
                {
                    //MessageBox.Show(ex.ToString());
                }

                bPanel = new BrandingPanel();
                this.Controls.Add(bPanel);
                bPanel.UpdateImage(GetBasePath());
                bPanel.UpdateHelpPage(basePath, recipeName, helpService);
            }
        }

        protected string GetBasePath()
        {
            IConfigurationService s = this.GetService(typeof(IConfigurationService)) as IConfigurationService;
            return s.BasePath;
        }
    }
}

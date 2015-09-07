namespace SPALM.SPSF.Library.CustomWizardPages
{
    using System;
    using System.IO;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework.Services;
    using Microsoft.Practices.WizardFramework;
    using Microsoft.Win32;

    /// <summary>
    /// Example of a class that is a custom wizard page
    /// </summary>
    /// 
    [ServiceDependency(typeof(IConfigurationService))]
    public partial class SilverlightCheckPage : SPSFBaseWizardPage
    {
        bool stepHasBeenSkipped = false;

        public SilverlightCheckPage(WizardForm parent)
            : base(parent)
        {
            InitializeComponent();
        }

        public override bool IsDataValid
        {
            get
            {
                return SilverightIsInstalled;
            }
        }

        protected bool SilverightIsInstalled
        {
            get
            {
                //path exists
                if (IsInstalled("Microsoft Silverlight"))
                {
                    //C:\Program Files (x86)\MSBuild\Microsoft\Silverlight\v4.0\Microsoft.Silverlight.CSharp.targets                    
                    string pathToBuildTargets = Path.Combine(Helpers.GetProgramsFolder32(), @"MSBuild\Microsoft\Silverlight\v4.0\Microsoft.Silverlight.CSharp.targets");
                    if (File.Exists(pathToBuildTargets))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private bool IsInstalled(string productName)
        {
            try
            {
                //The registry key:
                string SoftwareKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(SoftwareKey))
                {
                    //Let's go through the registry keys and get the info we need:
                    foreach (string skName in rk.GetSubKeyNames())
                    {
                        using (RegistryKey sk = rk.OpenSubKey(skName))
                        {
                            try
                            {
                                //If the key has value, continue, if not, skip it:
                                if (!(sk.GetValue("DisplayName") == null))
                                {
                                    if (sk.GetValue("DisplayName").ToString().StartsWith(productName))
                                    {
                                        return true;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (SilverightIsInstalled && !stepHasBeenSkipped)
            {
                //skip screen only once
                stepHasBeenSkipped = true;
                this.Wizard.GotoPage(this.Wizard.NextPage);
                this.SetVisibleCore(false);
            }
        }

        public override bool OnActivate()
        {
            base.OnActivate();
            AddBrandingPanel();
            HasBeenActivated = true;
            this.ShowInfoPanel = false;
            return true;
        }
    }   
}
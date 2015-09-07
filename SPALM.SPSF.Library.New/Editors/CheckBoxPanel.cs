namespace SPALM.SPSF.Library
{
    using System;
    using System.ComponentModel;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.WizardFramework;

    [ServiceDependency(typeof(IServiceProvider)), ServiceDependency(typeof(IContainer))]
	public class CheckBoxPanel : ArgumentPanelTypeEditor
	{
		public CheckBoxPanel() : base()
		{			
		}

		public override void EndInit()
		{
			base.EndInit();
			if (!base.IsInitializing)
			{
				
				//this.textbox.Text = base.FieldConfig.Label;
				/*base.toolTip.SetToolTip(this.listbox, base.FieldConfig.Tooltip);
				IDictionaryService service = this.GetService(typeof(IDictionaryService)) as IDictionaryService;
				if (base.FieldConfig.ReadOnly)
				{
					this.listbox.Enabled = false;
				}
				 * */
			}
		}

	}
}

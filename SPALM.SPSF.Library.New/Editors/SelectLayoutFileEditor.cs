namespace SPALM.SPSF.Library.Editors
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using EnvDTE;

    public class SelectLayoutFileEditor : UITypeEditor
  {
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context,
        IServiceProvider provider, object value)
    {
      object svc = provider.GetService(typeof(IWindowsFormsEditorService));
      if (svc == null)
      {
        return base.EditValue(context, provider, value);
      }

      DTE dte = (DTE)provider.GetService(typeof(DTE));
      SelectLayoutFileForm form = new SelectLayoutFileForm(dte, value);
      if (value != null)
      {
        
      }
      if (((IWindowsFormsEditorService)svc).ShowDialog(form) == DialogResult.OK)
      {
        return form.selectedPath;
      }
      else
      {
        return value;
      }
    }
  }
}

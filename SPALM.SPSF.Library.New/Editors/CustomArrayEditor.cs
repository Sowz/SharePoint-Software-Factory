namespace SPALM.SPSF.Library.Editors
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;

    [ServiceDependency(typeof(DTE))]
    public class CustomArrayEditor : CollectionEditor
    {
        public CustomArrayEditor() : base(typeof(Object))
        {
        }

        public CustomArrayEditor(Type t)
            : base(t)
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            UITypeEditor editor = (UITypeEditor)context.PropertyDescriptor.GetEditor(typeof(UITypeEditor));
            EditorRuntimeServiceProvider serviceProvider = new EditorRuntimeServiceProvider(context);
            return editor.EditValue(serviceProvider, serviceProvider, value);
       }
    }



    public class EditorRuntimeServiceProvider : IServiceProvider, ITypeDescriptorContext
    {
        ITypeDescriptorContext context = null;

        public EditorRuntimeServiceProvider(ITypeDescriptorContext _context)
        {
            context = _context;
        }

        #region IServiceProvider Members

        object IServiceProvider.GetService(Type serviceType)
        {
            if (serviceType == typeof(IWindowsFormsEditorService))
            {
                return new WindowsFormsEditorService();
            }
            else
            {
                return context.GetService(serviceType);
            }
        }

        class WindowsFormsEditorService : IWindowsFormsEditorService
        {
            #region IWindowsFormsEditorService Members

            public void DropDownControl(Control control)
            {
            }

            public void CloseDropDown()
            {
            }

            public DialogResult ShowDialog(Form dialog)
            {
                return dialog.ShowDialog();
            }

            #endregion
        }

        #endregion

        #region ITypeDescriptorContext Members

        public void OnComponentChanged()
        {
        }

        public IContainer Container
        {
            get { return null; }
        }

        public bool OnComponentChanging()
        {
            return true; // true to keep changes, otherwise false
        }

        public object Instance
        {
            get { return null; }
        }

        public PropertyDescriptor PropertyDescriptor
        {
            get { return null; }
        }

        #endregion
    }
}

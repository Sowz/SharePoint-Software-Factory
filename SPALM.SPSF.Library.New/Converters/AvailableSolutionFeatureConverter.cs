namespace SPALM.SPSF.Library.Converters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Forms;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;

    /// <summary>
  /// returns a list of all features in the current application
  /// </summary>
  [ServiceDependency(typeof(DTE))]
  public class AvailableSolutionFeatureConverter : StringConverter
  {
      private bool acceptFreeText = false;
      private List<NameValueItem> list = null;

      public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
      {
        Cursor.Current = Cursors.WaitCursor;

          /*
        if(list != null)
        {
          return new StandardValuesCollection(list.ToArray());
        }
          */

        list = new List<NameValueItem>();

        if (context != null)
        {
          _DTE service = (_DTE)context.GetService(typeof(_DTE));

          foreach (Project project in Helpers.GetAllProjects(service))
          {
            Helpers.GetAllFeatures(list, project);
          }
        }

        Cursor.Current = Cursors.Default;

        return new StandardValuesCollection(list.ToArray());
      }

      public override bool IsValid(ITypeDescriptorContext context, object value)
      {
        return true;
      }

      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
      {
        if (sourceType == typeof(NameValueItem))
        {
          return true;
        }
        return base.CanConvertFrom(context, sourceType);
      }

      public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
      {
        if (destinationType == typeof(String))
        {
          return true;
        }
        return base.CanConvertTo(context, destinationType);
      }

      public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
      {
        if (context.PropertyDescriptor.PropertyType == typeof(String))
        {
          return value.ToString();
        }
        return base.ConvertFrom(context, culture, value);
      }

      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
      {
        if (destinationType == typeof(String))
        {
          return value.ToString();
        }
        return base.ConvertTo(context, culture, value, destinationType);
      }

      public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
      {
        if (acceptFreeText)
        {
          return false;
        }
        return true;
      }

      public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
      {
        return true;
      }
    }  
}

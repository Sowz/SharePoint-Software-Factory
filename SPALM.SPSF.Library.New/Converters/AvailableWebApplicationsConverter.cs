namespace SPALM.SPSF.Library.Converters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Forms;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using SPALM.SPSF.SharePointBridge;

    /// <summary>
  /// returns a list of web applications in the local sharepoint farm
  /// </summary>
  [ServiceDependency(typeof(DTE))]
  public class AvailableWebApplicationsConverter : StringConverter
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
            DTE service = (DTE)context.GetService(typeof(DTE));

            try
            {
                

                List<SharePointWebApplication> webApplications = new SharePointBrigdeHelper(service).GetAllWebApplications();
                foreach (SharePointWebApplication webapp in webApplications)
                {
                    NameValueItem item = new NameValueItem();
                    item.Name = webapp.Url;
                    item.Value = webapp.Url;
                    item.ItemType = "WebApplication";
                    item.Description = webapp.Description;
                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                Helpers.LogMessage(service, service, ex.ToString());
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

namespace SPALM.SPSF.Library.Converters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
  /// returns a list of worker processes
  /// </summary>
  public class SharePointVersionConverter : StringConverter
  {
      private bool acceptFreeText = false;
      private List<NameValueItem> list = null;

      public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
      {        
        if(list != null)
        {
          return new StandardValuesCollection(list.ToArray());
        }

        list = new List<NameValueItem>();

        if (context != null)
        {
          NameValueItem item15 = new NameValueItem();
          item15.Name = "15";
          item15.Value = "15";
          item15.ItemType = "SharePointVersion";
          item15.Description = "SharePoint 2013";
          list.Add(item15);

          NameValueItem item14 = new NameValueItem();
          item14.Name = "14";
          item14.Value = "14";
          item14.ItemType = "SharePointVersion";
          item14.Description = "SharePoint 2010";
          list.Add(item14);

        }

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

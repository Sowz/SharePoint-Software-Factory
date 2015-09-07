namespace SPALM.SPSF.Library.Converters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;

    /// <summary>
  /// returns a list of worker processes
  /// </summary>
  [ServiceDependency(typeof(DTE))]
  public class NamespaceStringConverter : StringConverter
  {
    public NamespaceStringConverter() : base()
    {
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      return base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {      
      return base.CanConvertTo(context, destinationType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      if (!Helpers.IsValidNamespace(value.ToString()))
      {
        return null;
      }     
      return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      return base.ConvertTo(context, culture, value, destinationType);
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return base.GetStandardValuesExclusive(context);
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return base.GetStandardValuesSupported(context);
    }
      public override bool IsValid(ITypeDescriptorContext context, object value)
      {
        return true;
      }
    }  
}

namespace SPALM.SPSF.Library.Converters
{
    using System.ComponentModel;

    public class ModuleUrlConverter : TypeConverter
  {
    static string[] validValues = new string[] {
            "_catalogs/masterpage", "_catalogs/masterpage/Preview Images", "_catalogs/wp", "Style Library" };

    public override bool IsValid(ITypeDescriptorContext context, object value)
    {
      return true;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
			return false;
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(validValues);
    }
  }  
}

namespace SPALM.SPSF.Library.Converters
{
    using System.ComponentModel;

    /// <summary>
    /// returns available scopes for a feature
    /// </summary>
    public class SilverlightVersionConverter : TypeConverter
    {
        static string[] validValues = new string[] {
            "v2.0", "v3.0", "v4.0" };

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true; //no other values allowed
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

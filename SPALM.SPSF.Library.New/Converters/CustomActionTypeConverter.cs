namespace SPALM.SPSF.Library.Converters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    public class CustomActionTypeConverter : StringConverter
    {
        private bool acceptFreeText = false;
        private List<NameValueItem> list = null;

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {

            if (list != null)
            {
                return new StandardValuesCollection(list.ToArray());
            }

            list = new List<NameValueItem>();

            AddItem(list, "UrlActionTag", "UrlActionTag", "Adds UrlAction-Tag to specificy a fixed url");
            AddItem(list, "ClassFile", "ClassFile", "Adds a class file to specific a dynamic url");
            AddItem(list, "WebControl", "WebControl", "Adds a web control for the menu item");

            return new StandardValuesCollection(list.ToArray());
        }

        private void AddItem(List<NameValueItem> _list, string name, string value, string description)
        {
            NameValueItem item = new NameValueItem();
            item.Name = name;
            item.Value = value;
            item.ItemType = "";
            item.Description = description;
            _list.Add(item);
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

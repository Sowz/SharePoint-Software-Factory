namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using Microsoft.Practices.Common;

    [Serializable]
    public class CustomizationReferenceTemplate : SharePointVersionDependendReferenceTemplate, IAttributesConfigurable
    {       
        public CustomizationReferenceTemplate(string template)
            : base(template)
        {
        }

        #region ISerializable Members

        /// <summary>
        /// Required constructor for deserialization.
        /// </summary>
        protected CustomizationReferenceTemplate(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        #endregion ISerializable Members

        public override bool IsEnabledFor(object target)
        {
            Helpers.Log("CustomizationReferenceTemplate");

            if (!(target is Project))
            {
                return false;
            }
            if (!Helpers.IsTargetInCustomizationProject(target))
            {
                return false;
            }
            if (!base.IsEnabledFor(target))
            {
                return false;
            }
            return true;
        }

        public override string AppliesTo
        {
            get { return "Customization projects of SharePoint version " + this.SharePointVersions; }
        }

        #region IAttributesConfigurable Members

        //public void Configure(System.Collections.Specialized.StringDictionary attributes)
        //{
        //    base.Configure(attributes);
        //}

        #endregion
    }
}

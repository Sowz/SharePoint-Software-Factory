namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using Microsoft.Practices.Common;
    using Microsoft.Practices.ComponentModel;

    [Serializable]
    [ServiceDependency(typeof(DTE))]
    public class CustomizationReference : SharePointVersionDependendReference, IAttributesConfigurable
    {       
        public CustomizationReference(string template)
            : base(template)
        {
        }

        #region ISerializable Members

        /// <summary>
        /// Required constructor for deserialization.
        /// </summary>
        protected CustomizationReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        #endregion ISerializable Members

        public override bool IsEnabledFor(object target)
        {
            Helpers.Log("CustomizationReference");

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
            get { return "Solution projects for SharePoint " + (!string.IsNullOrEmpty(this.SharePointVersions)?"("+this.SharePointVersions+")":""); }
        }

        #region IAttributesConfigurable Members

        //public void Configure(System.Collections.Specialized.StringDictionary attributes)
        //{
        //    base.Configure(attributes);
        //}

        #endregion
    }
}

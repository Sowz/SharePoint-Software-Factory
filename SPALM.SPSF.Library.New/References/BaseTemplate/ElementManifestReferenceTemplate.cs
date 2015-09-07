namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using EnvDTE;
    using Microsoft.Practices.Common;

    [Serializable]
    public class ElementManifestReferenceTemplate : CustomizationReferenceTemplate, IAttributesConfigurable
    {
        public string FeatureScopes { get; set; }

        public ElementManifestReferenceTemplate(string template)
            : base(template)
        {
        }

        #region ISerializable Members

        protected ElementManifestReferenceTemplate(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            try
            {
                this.FeatureScopes = info.GetString("FeatureScopes");
            }
            catch { }
        }


        #endregion ISerializable Members

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FeatureScopes", this.FeatureScopes);
            base.GetObjectData(info, context);
        }

        public override bool IsEnabledFor(object target)
        {
            Helpers.Log("ElementManifestReferenceTemplate");

            //elments can only be added to a project or to a feature
            if (target is Project)
            {
                if (!base.IsEnabledFor(target))
                {
                    return false;
                }
                return true;
            }
            else if (target is ProjectItem)
            {

                //check if we are in a feature
                try
                {
                    if (FeatureScopes != "")
                    {
                        //recipe is for features, then we check if we are within a feature
                        if (target is ProjectItem)
                        {
                            return Helpers.IsFeatureScope((ProjectItem)target, FeatureScopes);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            return false;
        }

        public override string AppliesTo
        {
            get { return "Feature Elements of scope " + this.FeatureScopes; }
        }

        #region IAttributesConfigurable Members

        public new void Configure(StringDictionary attributes)
        {
            base.Configure(attributes);

            if (attributes.ContainsKey("FeatureScopes"))
            {
                FeatureScopes = attributes["FeatureScopes"];
            }            
        }

        #endregion
    }
}

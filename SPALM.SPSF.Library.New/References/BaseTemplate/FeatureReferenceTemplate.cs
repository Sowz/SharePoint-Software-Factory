namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;

    [Serializable]
    public class FeatureReferenceTemplate : CustomizationReferenceTemplate
    {
        public string FeatureScopes { get; set; }

        public FeatureReferenceTemplate(string template)
            : base(template)
        {
        }

        #region ISerializable Members

        protected FeatureReferenceTemplate(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion ISerializable Members

        public override bool IsEnabledFor(object target)
        {
            Helpers.Log("FeatureReferenceTemplate");

            //elments can only be added to a project or to a feature
            if(target is Project)
            {            
                if (!base.IsEnabledFor(target))
                {
                    return false;
                }
                return true;
            }
            else if(target is ProjectItem)
            {
                if((target as ProjectItem).Name.ToUpper() == "FEATURES")
                {
                    return true;
                }
            }            
            return false;
        }

        public override string AppliesTo
        {
            get { return "Features Folder"; }
        }        
    }
}

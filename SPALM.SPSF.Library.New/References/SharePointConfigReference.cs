namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using Microsoft.Practices.RecipeFramework;

    [Serializable]
    public class SharePointConfigReference : UnboundRecipeReference
    {
        public SharePointConfigReference( string recipe ) : base( recipe )
        {
        }

        protected SharePointConfigReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override bool IsEnabledFor( object target )
        {
					try
					{
                        if( target is ProjectItem)
                        {
                            if (((ProjectItem)target).Name.Equals("SharepointConfiguration.xml", StringComparison.InvariantCultureIgnoreCase))
                            {
                                return true;
                            }
                        }
					}
					catch (Exception)
					{
					}
            return false;
         }
        
        public override string AppliesTo
        {
             get
             {
               return "SharepointConfiguration.xml";
             }
        }
    }
}

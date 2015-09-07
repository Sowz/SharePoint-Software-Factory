namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using Microsoft.Practices.RecipeFramework;

    [Serializable]
    public class MSTargetsReference : UnboundRecipeReference
    {
        public MSTargetsReference( string recipe ) : base( recipe )
        {
        }

        protected MSTargetsReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override bool IsEnabledFor( object target )
        {
            if( target is ProjectItem)
            {
                if (((ProjectItem)target).Name.EndsWith(".msbuild"))
                {
                    return true;
                }
                if (((ProjectItem)target).Name.EndsWith(".targets"))
                {
                    return true;
                }
            }
            return false;
         }
        
        public override string AppliesTo
        {
             get
             {
                 return "*.msbuild or *.targets";
             }
        }
    }
}

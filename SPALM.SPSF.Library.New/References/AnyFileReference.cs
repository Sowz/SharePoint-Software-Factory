namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.Practices.RecipeFramework;

    [Serializable]
    public class AnyFileReference : UnboundRecipeReference
    {
        public AnyFileReference(string recipe) : base(recipe) { }

        protected AnyFileReference(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override bool IsEnabledFor(object target)
        {
            return true;
        }

        public override string AppliesTo
        {
            get { return "Any solution element"; }
        }
    }
}

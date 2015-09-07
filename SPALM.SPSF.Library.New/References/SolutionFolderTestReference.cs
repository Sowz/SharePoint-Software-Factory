namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using Microsoft.Practices.RecipeFramework;

    [Serializable]
    public class SolutionFolderTestReference : UnboundRecipeReference
    {
        public SolutionFolderTestReference(string template)
            : base(template)
        {
        }

        public override bool IsEnabledFor(object target)
        {

            return target is Project && ((Project)target).FullName.Contains(".Test.");
        }

        public override string AppliesTo
        {
            get { return "All Test Projects (project name contains '.Test.')"; }
        }

        #region ISerializable Members

        /// <summary>
        /// Required constructor for deserialization.
        /// </summary>
        protected SolutionFolderTestReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion ISerializable Members
    }
}

namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;

    [Serializable]
  public class AnyElementReferenceTemplate : UnboundTemplateReference
    {
        public AnyElementReferenceTemplate(string recipe) : base(recipe) { }

        protected AnyElementReferenceTemplate(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override bool IsEnabledFor(object target)
        {
            Helpers.Log("AnyElementReferenceTemplate");

					try
					{
						if (target is ProjectItem)
						{
							if ((target as ProjectItem).Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFile)
							{
								return false;
							}
						}
					}
					catch (Exception)
					{
					}
          return true;
        }

        public override string AppliesTo
        {
            get { return "Any solution element"; }
        }
    }
}

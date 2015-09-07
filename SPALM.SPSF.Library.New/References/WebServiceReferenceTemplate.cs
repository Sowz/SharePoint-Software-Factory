namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;

    [Serializable]
  public class WebServiceReferenceTemplate : UnboundTemplateReference
  {
    public WebServiceReferenceTemplate(string recipe)
      : base(recipe)
    {
    }

    protected WebServiceReferenceTemplate(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public override bool IsEnabledFor(object target)
    {
      if (target is ProjectItem)
      {
        if (((ProjectItem)target).Name.EndsWith(".asmx"))
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
        return "*.asmx files";
      }
    }
  }
}

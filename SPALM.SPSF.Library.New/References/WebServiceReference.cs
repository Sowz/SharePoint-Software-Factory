namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using Microsoft.Practices.RecipeFramework;

    [Serializable]
  public class WebServiceReference : UnboundRecipeReference
  {
    public WebServiceReference(string recipe)
      : base(recipe)
    {
    }

    protected WebServiceReference(SerializationInfo info, StreamingContext context)
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

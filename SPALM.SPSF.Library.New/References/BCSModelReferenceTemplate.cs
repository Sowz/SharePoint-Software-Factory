namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;

    [Serializable]
  public class BCSModelReferenceTemplate : UnboundTemplateReference
  {
    public BCSModelReferenceTemplate(string recipe)
      : base(recipe)
    {
    }

    protected BCSModelReferenceTemplate(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public override bool IsEnabledFor(object target)
    {
      if (target is ProjectItem)
      {
         if (((ProjectItem)target).Name.ToLower().EndsWith(".bdcm"))
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
        return "*.bdcm files";
      }
    }
  }
}

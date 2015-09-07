namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using Microsoft.Practices.RecipeFramework;

    [Serializable]
  public class BCSModelReference : UnboundRecipeReference
  {
    public BCSModelReference(string recipe)
      : base(recipe)
    {
    }

    protected BCSModelReference(SerializationInfo info, StreamingContext context)
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

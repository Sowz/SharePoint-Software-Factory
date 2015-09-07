namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;

    [Serializable]
  public class SolutionFolderWSPReferenceTemplate : UnboundTemplateReference
  {
    public SolutionFolderWSPReferenceTemplate(string template)
      : base(template)
    {
    }

    public override bool IsEnabledFor(object target)
    {
        Helpers.Log("SolutionFolderWSPReferenceTemplate");

      try
      {
        return Helpers.IsTargetInCustomizationProject(target);
      }
      catch (Exception)
      {
      }
      return false;
    }

    public override string AppliesTo
    {
      get { return "WSP Solutions"; }
    }

    #region ISerializable Members

    /// <summary>
    /// Required constructor for deserialization.
    /// </summary>
    protected SolutionFolderWSPReferenceTemplate(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion ISerializable Members
  }
}

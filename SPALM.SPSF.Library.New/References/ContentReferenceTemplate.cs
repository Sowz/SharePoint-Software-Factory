namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;

    [Serializable]
  public class ContentReferenceTemplate : UnboundTemplateReference
  {
    public ContentReferenceTemplate(string template)
      : base(template)
    {
    }

    public override bool IsEnabledFor(object target)
    {
      try
      {
        if (target is SolutionFolder || target is Solution)
        {
          return true;
        }
        if (((Project)target).Object is SolutionFolder)
        {
          //any solution folder with name deployment
          if (((SolutionFolder)((Project)target).Object).Parent.Name == "Contents")
          {
            return true;
          }
        }
        return false;
      }
      catch (Exception)
      {
      }
      return false;
    }

    public override string AppliesTo
    {
      get { return "Content Folders"; }
    }

    #region ISerializable Members

    /// <summary>
    /// Required constructor for deserialization.
    /// </summary>
    protected ContentReferenceTemplate(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion ISerializable Members
  }
}

namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;

    [Serializable]
  public class TestReferenceTemplate : UnboundTemplateReference
  {
    public TestReferenceTemplate(string template)
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
          if (((SolutionFolder)((Project)target).Object).Parent.Name == "Test")
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
      get { return "Test Folders"; }
    }

    #region ISerializable Members

    /// <summary>
    /// Required constructor for deserialization.
    /// </summary>
    protected TestReferenceTemplate(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion ISerializable Members
  }
}

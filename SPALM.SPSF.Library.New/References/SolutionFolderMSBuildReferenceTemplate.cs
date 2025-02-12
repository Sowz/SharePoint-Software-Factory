namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;

    [Serializable]
  public class SolutionFolderMSBuildReferenceTemplate : UnboundTemplateReference
    {
        public SolutionFolderMSBuildReferenceTemplate(string template)
            : base(template)
        {
        }

        public override bool IsEnabledFor(object target)
        {
          if(target is Project)
          {
            //output assembly contains .MSBuildTasks.
            return Helpers.GetOutputName(target as Project).Contains(".MSBuildTasks.");
          }

          return false;
        }

        public override string AppliesTo
        {
            get { return "All MSBuild Task Projects (project name contains '.MSBuildTasks.')"; }
        }

        #region ISerializable Members

        /// <summary>
        /// Required constructor for deserialization.
        /// </summary>
        protected SolutionFolderMSBuildReferenceTemplate(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion ISerializable Members
    }
}

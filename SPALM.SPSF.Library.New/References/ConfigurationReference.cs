namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.Practices.RecipeFramework;

    [Serializable]
    public class ConfigurationReference : UnboundRecipeReference
    {
        public ConfigurationReference(string template)
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
                if (((SolutionFolder)((Project)target).Object).Parent.Name == "Configurations")
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
          get { return "Content folders"; }
        }

        #region ISerializable Members

        /// <summary>
        /// Required constructor for deserialization.
        /// </summary>
        protected ConfigurationReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion ISerializable Members
    }
}

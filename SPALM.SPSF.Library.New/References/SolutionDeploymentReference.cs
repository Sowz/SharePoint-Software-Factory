namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.Practices.RecipeFramework;

    [Serializable]
    public class SolutionDeploymentReference : UnboundRecipeReference
    {
        public SolutionDeploymentReference(string template)
            : base(template)
        {
        }

        public override bool IsEnabledFor(object target)
        {
            Helpers.Log("SolutionDeploymentReference");

            //gilt nicht für projectItems
            if (target is ProjectItem)
            {
                return false;
            }

            try
            {             
                if (target is Project)
                {
                  if(Helpers.IsCustomizationProject((Project)target))
                  {
                    return true;
                  }
                  else if (((Project)target).Object is SolutionFolder)
                  {
                    //is there any Customization Project in this solutionfolder
                    SolutionFolder f = ((Project)target).Object as SolutionFolder;
                    return Helpers.ContainsCustomizationProject(f);
                  }                   
                }
                else if (target is Solution)
                {
                  return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public override string AppliesTo
        {
            get { return "All Customization projects or solution folders with customization projects"; }
        }

        #region ISerializable Members

        /// <summary>
        /// Required constructor for deserialization.
        /// </summary>
        protected SolutionDeploymentReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion ISerializable Members
    }
}

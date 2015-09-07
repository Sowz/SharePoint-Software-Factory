namespace SPALM.SPSF.Library.References
{
    using System;
    using System.Runtime.Serialization;
    using EnvDTE;
    using EnvDTE80;

    [Serializable]
    public class AddProjectReferenceTemplate : SharePointVersionDependendReferenceTemplate
	{
		public AddProjectReferenceTemplate(string recipe) : base(recipe) { }

		protected AddProjectReferenceTemplate(SerializationInfo info, StreamingContext context) : base(info, context) { }

		public override bool IsEnabledFor(object target)
		{
            Helpers.Log("AddProjectReferenceTemplate");

            //gilt nicht für projectItems
            if (target is ProjectItem)
            {
                return false;
            }

            try
            {
                if (target is SolutionFolder || target is Solution)
                {
                    if (base.IsEnabledFor(target))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (target is Project)
                {
                    if ((target as Project).Object is SolutionFolder)
                    {
                        if (base.IsEnabledFor(target))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
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
			get { return "Solution or solution folders"; }
		}
	}
}

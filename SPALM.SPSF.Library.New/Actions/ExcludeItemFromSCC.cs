#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;

    /// <summary>
	/// </summary>
	[ServiceDependency(typeof(DTE))]
    public class ExcludeItemFromSCC : ConfigurableAction
	{
		private ProjectItem projectItem;
		private Project project;

		[Input(Required = false)]
		public ProjectItem SelectedItem
		{
			get { return projectItem; }
			set { projectItem = value; }
		}

		[Input(Required = false)]
		public Project SelectedProject
		{
			get { return project; }
			set { project = value; }
		}

		protected string GetBasePath()
		{
			return base.GetService<IConfigurationService>(true).BasePath;
		}

		public override void Execute()
		{
			DTE service = (DTE)this.GetService(typeof(DTE));
			
			try
			{
				if(projectItem != null)
				{
                    Helpers.ExcludeItemFromSCC(service, project, projectItem);
				}
			}
			catch (Exception ex)
			{
				Helpers.LogMessage(service, this, ex.ToString());
			}
		}

		/// <summary>
		/// Removes the previously added reference, if it was created
		/// </summary>
		public override void Undo()
		{
		}
	}
}
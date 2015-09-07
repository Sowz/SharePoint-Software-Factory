#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
	/// </summary>
	[ServiceDependency(typeof(DTE))]
	public class OpenItemInDesigner : ConfigurableAction
	{
		private string _ItemPath = "";
		private Project project;

        [Input(Required = false)]
        public ProjectItem Item { get; set; }

		[Input(Required = false)]
		public string ItemPath
		{
			get { return _ItemPath; }
			set { _ItemPath = value; }
		}

		[Input(Required = false)]
		public Project Project
		{
			get
			{
				return this.project;
			}
			set
			{
				this.project = value;
			}
		}

		public override void Execute()
		{
			DTE service = (DTE)this.GetService(typeof(DTE));
			string fullItemPath = ItemPath;

			try
			{
                if (Item != null)
                {
                    Helpers.LogMessage(service, this, "Opening " + Helpers.GetFullPathOfProjectItem(Item) + " in designer");
                    Window window = Item.Open(Constants.vsViewKindDesigner);
                    window.Visible = true;
                    window.Activate();
                    return;
                }
                else
                {
                    /*
                    if (!Path.IsPathRooted(fullItemPath))
                    {
                        string projectDir = Path.GetDirectoryName(Helpers.GetFullPathOfProjectItem(Project));
                        fullItemPath = Path.Combine(projectDir, fullItemPath);
                    }
                    */
                    ProjectItem pitem = Helpers.FindProjectItemByPath(project, fullItemPath);

                    if (pitem != null)
                    {
                        Helpers.LogMessage(service, this, "Opening " + fullItemPath + " in designer");
                        Window window = pitem.Open(Constants.vsViewKindDesigner);
                        window.Visible = true;
                        window.Activate();

                        return;
                    }
                }
			}
			catch (Exception)
			{
			}

			Helpers.LogMessage(service, this, "Could not open item " + fullItemPath);

		}

		/// <summary>
		/// Removes the previously added reference, if it was created
		/// </summary>
		public override void Undo()
		{
		}
	}
}
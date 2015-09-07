#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
	public class CheckBrokenFields : ConfigurableAction
  {
			private string _SiteCollectionUrl;

			[Input(Required = true)]
			public string SiteCollectionUrl
			{
				get { return _SiteCollectionUrl; }
				set { _SiteCollectionUrl = value; }
			}

      public override void Execute()
      {
        DTE dte = GetService<DTE>(true);
				DeploymentHelpers.CheckBrokenFields(dte, _SiteCollectionUrl);
      }

      public override void Undo()
      {

      }
	}   
}
#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System.ComponentModel.Design;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE)), ServiceDependency(typeof(ITypeResolutionService))]
  public class DeleteDummyFile : ConfigurableAction
    {
      private string _Filename = "";
      private ProjectItem _ParentItem = null;

      [Input(Required = false)]
      public string Filename
      {
        get { return _Filename; }
        set { _Filename = value; }
      }

      [Input(Required = false)]
      public ProjectItem ParentItem
      {
        get { return _ParentItem; }
        set { _ParentItem = value; }
      }
      
      public override void Execute()
      {
        DTE service = this.GetService<DTE>(true);

        Helpers2.DeleteDummyFile(service, ParentItem, true);        
      }

      public override void Undo()
      {
          // nothing to do.
      }      
    }
}

#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;

    [ServiceDependency(typeof(DTE))]
    public class ImportWSP : ConfigurableAction
    {
      private Project _TargetProject = null;
      private string _WSPFilename = "";      

      [Input(Required = true)]
      public Project TargetProject
      {
        get { return _TargetProject; }
        set { _TargetProject = value; }
      }   

        [Input(Required = true)]
      public string WSPFilename
      {
        get { return _WSPFilename; }
        set { _WSPFilename = value; }
      }

        protected string GetBasePath()
        {
          return base.GetService<IConfigurationService>(true).BasePath;
        }

      public override void Execute()
      {
        DTE dte = GetService<DTE>(true);
        Project project = TargetProject;
        if(project == null)
        { project = Helpers.GetSelectedProject(dte);
        }

        if(Helpers2.IsSharePointVSTemplate(dte, project))
        {
          Helpers2.GetOrCreateMappedFolder(project, "RootFile", "SharePointRoot");
        }

        Helpers.ExtractWSPToProject(dte, _WSPFilename, _TargetProject, GetBasePath());
      }
    
      /// <summary>
      /// Removes the previously added reference, if it was created
      /// </summary>
      public override void Undo()
      {
      }
    }    
}
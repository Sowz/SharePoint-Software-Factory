#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;

    [ServiceDependency(typeof(DTE))]
    public class ImportWSPFromServer : ConfigurableAction
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
          string tempwspfolder = "";
          string tempwspfilename = "";
          try
          {
            //retrieve the file locally
            tempwspfolder = Path.Combine(Path.GetTempPath(), "SPSFImport" + Guid.NewGuid().ToString());
            if (!Directory.Exists(tempwspfolder))
            {
              Directory.CreateDirectory(tempwspfolder);
            }
            tempwspfilename = Path.Combine(tempwspfolder, WSPFilename);

						SharePointBrigdeHelper helper = new SharePointBrigdeHelper(dte);
						helper.ExportSolutionAsFile(WSPFilename, tempwspfilename);            
          }
          catch (Exception ex)
          {
            MessageBox.Show("Could not download and save solution " + WSPFilename + " to temp folder " + tempwspfolder + ". " + ex.ToString());
          }

          if (File.Exists(tempwspfilename))
          {
            Helpers.ExtractWSPToProject(dte, tempwspfilename, _TargetProject, GetBasePath());
          }

          //clean up
          try
          {
            Directory.Delete(tempwspfolder, true);
          }
          catch (Exception ex)
          {
            MessageBox.Show("Could not delete temp folder " + tempwspfolder + ". " + ex.ToString());
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
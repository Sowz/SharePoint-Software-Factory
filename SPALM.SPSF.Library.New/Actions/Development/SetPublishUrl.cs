#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.Windows.Forms;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class SetPublishUrl : ConfigurableAction
    {
      private Project _Project = null;
      private string _PublishUrl = "";
      private string _DeploymentTarget = "";

      [Input(Required = true)]
      public Project Project
      {
        get
        {
          return _Project;
        }
        set
        {
          _Project = value;
        }
      }

      [Input(Required = true)]
      public string PublishUrl
      {
        get
        {
          return _PublishUrl;
        }
        set
        {
          _PublishUrl = value;
        }
      }

      [Input(Required = true)]
      public string DeploymentTarget
      {
        get
        {
          return _DeploymentTarget;
        }
        set
        {
          _DeploymentTarget = value;
        }
      }
     
      public override void Execute()
      {
        DTE dte = GetService<DTE>(true);
        try
        {
          Properties publishProperties = (Properties)_Project.Properties.Item("Publish").Value;
          publishProperties.Item("PublishUrl").Value = _PublishUrl;
        }
        catch (Exception)
        {
          MessageBox.Show("PublishUrl could not be changed.");
        }

        try
        {
          _Project.Properties.Item("TargetZone").Value = _DeploymentTarget;
        }
        catch (Exception)
        {
          MessageBox.Show("Value for TargetZone could not be changed to " + _DeploymentTarget + ".");
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
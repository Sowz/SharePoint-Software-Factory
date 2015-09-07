#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.Windows.Forms;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
    /// Created a project dependency
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class MakeProjectDependentOnOther : ConfigurableAction
    {

        [Input(Required = true)]
        public string DependingProject { get; set; }

        [Input(Required = true)]
        public string SourceProject { get; set; }


        public override void Execute()
        {
            DTE dte = base.GetService<DTE>(true);

            Project _dependingProject = Helpers.GetProjectByName(dte, this.DependingProject);
            Project _sourceProject = Helpers.GetProjectByName(dte, this.SourceProject);

            if (_dependingProject == null || _sourceProject == null)
            {
                return;
            }

            try
            {
                Helpers2.AddBuildDependency(dte, _dependingProject, _sourceProject);
            }
            catch (Exception ex)
            {
                Helpers.LogMessage(dte, this, ex.ToString());

                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Undo()
        {

        }
    }
}
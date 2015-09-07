#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
    /// Adds a new Elements definition, e.g. a contenttype and registers the element in the parent feature
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class AddFileToFeatureAction : BaseAction
    {
        public AddFileToFeatureAction()
            : base()
        {

        }

        /// <summary>
        /// Contains the name of the element, used to create filenames and folders
        /// </summary>
        [Input(Required = true)]
        public ProjectItem File { get; set; }
        
        /// <summary>
        /// Name of the parent feature
        /// </summary>
        [Input(Required = true)]
        public string ParentFeatureName { get; set; }

        public override void Execute()
        {
            DTE dte = (DTE)this.GetService(typeof(DTE));
            Project project = this.GetTargetProject(dte);

            //1. get correct parameters ("$(FeatureName)" as "FeatureX")       
            string evaluatedParentFeatureName = EvaluateParameterAsString(dte, ParentFeatureName);

            Helpers2.AddVSElementToVSFeature(dte, project, File, evaluatedParentFeatureName);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Undo()
        {
            //TODO: Delete the created projectitems
        }
    }
}
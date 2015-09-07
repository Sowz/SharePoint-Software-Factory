#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.ComponentModel.Design;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Library;

    /// <summary>
    /// Adds a new Elements definition, e.g. a contenttype and registers the element in the parent feature
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class AddProjectOutputReference : ConfigurableAction
    {
        public AddProjectOutputReference()
        {

        }

        /// <summary>
        /// Contains the name of the source project
        /// </summary>
        [Input(Required = true)]
        public string SourceProjectName { get; set; }

        /// <summary>
        /// contains the folder where the element should be added
        /// </summary>
        [Input(Required = true)]
        public ProjectItem TargetFolder { get; set; }

        /// <summary>
        /// Type of file (e.g. RootFile, TemplateFile, Image, LayoutsFile etc.
        /// </summary>
        [Input(Required = true)]
        public SPFileType DeploymentType
        {
            get
            {
                return this.deploymentType;
            }
            set
            {
                this.deploymentType = value;
            }
        }
        private SPFileType deploymentType = SPFileType.RootFile;

        public override void Execute()
        {
            DTE dte = this.GetService<DTE>(true);

            string evaluatedSourceProjectName = EvaluateParameterAsString(dte, SourceProjectName);

            Project sourceProject = Helpers.GetProjectByName(dte, evaluatedSourceProjectName);
            Helpers2.AddProjectOutputReferenceToFolder(dte, sourceProject, TargetFolder, DeploymentType);
            
        }

        /// <summary>
        /// takes a parameter like "$(FeatureName)$(NameSeparator)$(ID)" and returns the current value "FeatureX_34"
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        internal string EvaluateParameterAsString(DTE dte, string parameterName)
        {
            try
            {
                IDictionaryService serviceToAdapt = (IDictionaryService)this.GetService(typeof(IDictionaryService));
                ServiceAdapterDictionary serviceAdaptor = new ServiceAdapterDictionary(serviceToAdapt);
                ExpressionEvaluationService2 expressionService2 = new ExpressionEvaluationService2();
                string evaluatedValue = expressionService2.Evaluate(parameterName, serviceAdaptor).ToString();
                if (string.IsNullOrEmpty(evaluatedValue))
                {
                    return "";
                }
                return evaluatedValue;
            }
            catch (NullReferenceException)
            {
            }
            return "";
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
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
    public class AddSPDataTemplateAction : BaseTemplateAction
    {
      public AddSPDataTemplateAction()
        : base()
        {

        }

        /// <summary>
        /// Contains the name of the element, used to create filenames and folders
        /// </summary>
        [Input(Required = true)]
        public string SPDataName { get; set; }

        /// <summary>
        /// Contains the type of element, e.g. "ContentTypes". This is used to summarize elements of the same type (e.g. in feature "ContentTypes.xml" or in a folder "ContentTypes")
        /// </summary>
        [Input(Required = true)]
        public string SPDataCategory { get; set; }

        /// <summary>
        /// Required for Visual Studio 2010 project template 
        /// </summary>
        [Input(Required = true)]
        public string SPDataTemplate { get; set; }

        //[Output]
        //public ProjectItem CreatedProjectFolder { get; set; }

        //[Output]
        //public ProjectItem CreatedProjectItem { get; set; }

        public override void Execute()
        {
            DTE dte = (DTE)this.GetService(typeof(DTE));
            Project project = this.GetTargetProject(dte);

            string evaluatedSPDataTemplate = EvaluateParameterAsString(dte, SPDataTemplate);
            string evaluatedTemplate = EvaluateParameterAsString(dte, TemplateFileName);
            string evaluatedSPDataCategory = EvaluateParameterAsString(dte, SPDataCategory);
            string evaluatedSPDataName = EvaluateParameterAsString(dte, SPDataName);
            string evaluatedTargetFileName = EvaluateParameterAsString(dte, TargetFileName);
            string evaluatedDeploymentPath = EvaluateParameterAsString(dte, DeploymentPath);

            //2. create the Element
            if (Helpers2.IsSharePointVSTemplate(dte, project))
            {
              AddSPDataDefinitionToVSTemplate(dte, project, evaluatedSPDataTemplate, evaluatedTemplate, evaluatedSPDataCategory, evaluatedSPDataName, evaluatedTargetFileName, evaluatedDeploymentPath);
            }
            else
            {
              base.Execute();
            }
        }

        private void AddSPDataDefinitionToVSTemplate(DTE dte, Project project, string evaluatedSPDataTemplate, string evaluatedTemplate, string evaluatedSPDataCategory, string evaluatedSPDataName, string evaluatedTargetFileName, string evaluatedDeploymentPath)
        {
          string elementsXmlContent = GenerateContent(evaluatedTemplate, "Elements.xml");
            string spDataContent = GenerateContent(evaluatedSPDataTemplate, "SharePointProjectItem.spdata");

            //1. create the folder for the content type, which means adding the spdata stuff to a folder in the project

            string targetFolder = evaluatedSPDataCategory + @"\" + evaluatedSPDataName;
            ProjectItems contentTypeFolder = null;
            ProjectItem spDataItem = Helpers2.AddFileToProject(dte, project, targetFolder, "SharePointProjectItem.spdata", spDataContent, true, false, out contentTypeFolder);

            //2. put the elements.xml in that folder
            CreatedProjectItem = Helpers2.AddFile(dte, contentTypeFolder, evaluatedTargetFileName, elementsXmlContent, true, this.Open);

            if (DeploymentTypeIsSet)
            {
              Helpers2.SetDeploymentType(CreatedProjectItem, this.DeploymentType);
            }
            if (DeploymentPathIsSet)
            {
              Helpers2.SetDeploymentPath(CreatedProjectItem, evaluatedDeploymentPath);
            }

            //Trace.WriteLine("AddElementsDefinitionAction FindProjectItemByPath " + targetFolder);
            ProjectItem elementFolder = Helpers.FindProjectItemByPath(project, targetFolder);

            Helpers2.AddVSElementToVSPackage(dte, project, elementFolder);

            CreatedProjectFolder = elementFolder;
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
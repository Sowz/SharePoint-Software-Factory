#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.IO;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
    /// Exports a given list template
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class ExportListTemplate : AddFileToItemAction
    {
        private string _ListTemplateName = "";
        private string _SiteUrl = "";
        private Guid _ListId;

        [Input(Required = true)]
        public string ListTemplateName
        {
            get { return _ListTemplateName; }
            set { _ListTemplateName = value; }
        }

        [Input(Required = true)]
        public string SiteUrl
        {
            get { return _SiteUrl; }
            set { _SiteUrl = value; }
        }

        [Input(Required = true)]
        public Guid ListId
        {
            get { return _ListId; }
            set { _ListId = value; }
        }

        public override void Execute()
        {

            string temppath = Path.Combine(Path.GetTempPath(), "SPSFListExport" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(temppath);

            //export all to a temp folder
            DTE dte = GetService(typeof(DTE)) as DTE;

            SharePointBrigdeHelper helper = new SharePointBrigdeHelper(dte);
            helper.ExportListAsTemplate(SiteUrl, _ListId, temppath);

            //add the exported items to the project
            foreach (string s in Directory.GetFiles(temppath))
            {
                if (SourceFileName != "")
                {
                    SourceFileName += ";";
                }
                SourceFileName += s;
            }

            //now add all collected files to the project as element files
            base.Execute();

            //delete temp folder
            Directory.Delete(temppath, true);

        }

        /// <summary>
        /// Removes the previously added reference, if it was created
        /// </summary>
        public override void Undo()
        {
        }
    }
}
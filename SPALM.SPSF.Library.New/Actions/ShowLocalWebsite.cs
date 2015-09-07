#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;

    [ServiceDependency(typeof(DTE))]
    public class ShowLocalWebsite : ConfigurableAction
    {
        private string _Url = "";
        private bool _AttachCurrentSelection = false;

        [Input(Required = true)]
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        [Input(Required = false)]
        public bool AttachCurrentSelection
        {
          get { return _AttachCurrentSelection; }
          set { _AttachCurrentSelection = value; }
        }
      
        protected string GetBasePath()
        {
            return base.GetService<IConfigurationService>(true).BasePath;
        }

        public override void Execute()
        {
          DTE dte = GetService<DTE>(true);

          try
          {           
            string websiteBasePath = GetBasePath();
            string url = websiteBasePath + "\\" + _Url;

            if(AttachCurrentSelection)
            {
              url += "#" + Helpers.GetSelectedType(dte);
            }

            Helpers.LogMessage(dte, this, "Opening page " + url);

            Window win = dte.Windows.Item(EnvDTE.Constants.vsWindowKindCommandWindow);
            CommandWindow comwin = (CommandWindow)win.Object;
            comwin.SendInput("nav \"" + url + "\"", true);
          }
          catch (Exception ex)
          {
            Helpers.LogMessage(dte, this, ex.ToString());
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
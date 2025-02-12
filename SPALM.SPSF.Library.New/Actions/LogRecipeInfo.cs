#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.ComponentModel.Design;
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Practices.RecipeFramework.Services;

    /// <summary>
    /// Displays the help page of the current recipe. Takes the argument "RecipeName" and
    /// creates a URL HTML/RecipeName/index.html
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class LogRecipeInfo : ConfigurableAction
    {
        protected string GetBasePath()
        {
            return base.GetService<IConfigurationService>(true).BasePath;
        }

        public override void Execute()
        {
            DTE dte = GetService<DTE>(true);
            string recipeName = "";
            try
            {
                IDictionaryService dictionary = GetService<IDictionaryService>();
                recipeName = dictionary.GetValue("RecipeName").ToString();

                if (recipeName != "")
                {
                    Helpers.LogMessage(dte, this, " ");
                    Helpers.LogMessage(dte, this, "Executing recipe '" + recipeName + "'");
                }
            }
            catch (Exception)
            {
                //Helpers.LogMessage(dte, this, "Could not find recipe name in argument 'RecipeName'");
                return;
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
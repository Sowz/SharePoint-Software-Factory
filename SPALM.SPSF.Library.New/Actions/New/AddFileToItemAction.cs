#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;

    /// <summary>
    /// Adds a template file as a child to the given project item
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class AddFileToItemAction : BaseItemAction
    {
      public AddFileToItemAction()
        : base()
      {
      }

        public override void Execute()
        {
            if (ExcludeCondition)
            {
                return;
            }
            if (!AdditionalCondition)
            {
              return;
            }

            DTE dte = (DTE)this.GetService(typeof(DTE));
            Project project = this.GetTargetProject(dte);

            base.Execute();            
        }      
    }    
}
#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using System;
    using System.Windows.Forms;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
    public class SolutionExplorerExpandProjects : ConfigurableAction
    {
        public override void Execute()
        {
          try
          {
            DTE dte = GetService<DTE>(true);

            //Helpers.LogMessage(dte, this, "Collapsing project tree");

            UIHierarchy tree = null;
            Window2 explorer = null;

            explorer = dte.Windows.Item(EnvDTE.Constants.vsWindowKindSolutionExplorer) as Window2;
            tree = explorer.Object as UIHierarchy;

             foreach(UIHierarchyItem node in tree.UIHierarchyItems)
             {
               ExpandCollapseNodes(node, false, tree);
             }
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.ToString());
          }
        }

        private void ExpandCollapseNodes(UIHierarchyItem node, bool expanded, UIHierarchy tree)
        {
          foreach(UIHierarchyItem subNode in node.UIHierarchyItems)
          {           
            ExpandCollapseNodes(subNode, expanded, tree);
          }

          if (node.Object is SolutionFolder)
          {
            node.UIHierarchyItems.Expanded = true;
          }
          else if (node.Object is Solution)
          {
            node.UIHierarchyItems.Expanded = true;
          }
          else if (node.Object is Project)
          {
            if (((Project)node.Object).Object is SolutionFolder)
            {
              //are there projects in the solution folder
              SolutionFolder f = ((Project)node.Object).Object as SolutionFolder;
              bool expandit = false;
              foreach (ProjectItem pi in f.Parent.ProjectItems)
              {
                if (pi.Object is Project)
                {
                  //solutionfolder contains a child project, dont close
                  expandit = true;
                }
              }
              node.UIHierarchyItems.Expanded = expandit;
            }
            else
            {
              node.UIHierarchyItems.Expanded = false;
            }
          }
          else
          {
            node.UIHierarchyItems.Expanded = false;
            //bug in VS
            //if still expanded
            if (node.UIHierarchyItems.Expanded == true)
            {
              node.Select(vsUISelectionType.vsUISelectionTypeSelect);
              tree.DoDefaultAction();
            }
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
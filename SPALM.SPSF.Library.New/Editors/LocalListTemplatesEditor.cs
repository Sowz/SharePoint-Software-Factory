namespace SPALM.SPSF.Library
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using EnvDTE;
    using SPALM.SPSF.Library.Editors;

    /// <summary>
  /// Provides a list of local installed list templates
  /// </summary>
  public class LocalListTemplatesEditor : ListEditor
  {
    public override List<NameValueItem> GetItems(DTE dte, IServiceProvider provider)
    {
      List<NameValueItem> list = new List<NameValueItem>();

      string featuresfolder = Helpers.GetSharePointHive() + @"\TEMPLATE\FEATURES";
      foreach(string s in Directory.GetFiles(featuresfolder, "*.xml", SearchOption.AllDirectories))
      {
        try
        {
          XmlDocument doc = new XmlDocument();
          doc.Load(s);

          XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
          nsmgr.AddNamespace("ns", "http://schemas.microsoft.com/sharepoint/");

          foreach(XmlNode node in doc.SelectNodes("/ns:Elements/ns:ListTemplate", nsmgr))
          {
            try
            {
              if (node.Attributes["Hidden"] != null)
              {
                if (node.Attributes["Hidden"].Value.ToLower() == "false")
                {
                  AddItem(node, list);
                }
              }
              else
              {
                AddItem(node, list);
              }
            }
            catch (Exception)
            {
            }
          }
        }
        catch(Exception)
        {
        }
      }

      return list;
    }

    private void AddItem(XmlNode node,List<NameValueItem> list)
    {
      string name = node.Attributes["Name"].Value;
      string title = Helpers.GetResourceValue(node.Attributes["DisplayName"].Value);
      string description = Helpers.GetResourceValue(node.Attributes["Description"].Value);

      NameValueItem item = new NameValueItem();
      item.ItemType = "ListTemplate";
      item.Name = title;
      item.Description = description;
      item.Value = node.Attributes["Type"].Value;
      list.Add(item); 
    }
  }
}

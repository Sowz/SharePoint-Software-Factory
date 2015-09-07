namespace SPALM.SPSF.Library
{
    using System;
    using System.Collections.Generic;
    using EnvDTE;
    using SPALM.SPSF.Library.Editors;

    public class ItemEventReceiverTypesEditor : ListEditor
  {
    public override List<NameValueItem> GetItems(DTE dte, IServiceProvider provider)
    {
      List<NameValueItem> list = new List<NameValueItem>();
      AddRecipeParameters(provider,list, "/RecipeParameters/SPEventReceivers/SPItemEventReceiver", "", new XmlNodeHandler("Name", "Name", "", "Description"));
      return list;
    }

  }

  public class ListEventReceiverTypesEditor : ListEditor
  {
    public override List<NameValueItem> GetItems(DTE dte, IServiceProvider provider)
    {
      List<NameValueItem> list = new List<NameValueItem>();
      AddRecipeParameters(provider, list, "/RecipeParameters/SPEventReceivers/SPListEventReceiver", "", new XmlNodeHandler("Title", "ID", "Group", "Description"));
      return list;
    }
  }

  public class WebEventReceiverTypesEditor : ListEditor
  {
    public override List<NameValueItem> GetItems(DTE dte, IServiceProvider provider)
    {
      List<NameValueItem> list = new List<NameValueItem>();
      AddRecipeParameters(provider, list, "/RecipeParameters/SPEventReceivers/SPWebEventReceiver", "", new XmlNodeHandler("Name", "Name", "", "Description"));
      return list;
    }
  }

  public class EmailEventReceiverTypesEditor : ListEditor
  {
    public override List<NameValueItem> GetItems(DTE dte, IServiceProvider provider)
    {
      List<NameValueItem> list = new List<NameValueItem>();
      AddRecipeParameters(provider, list, "/RecipeParameters/SPEventReceivers/SPEmailEventReceiver", "", new XmlNodeHandler("Name", "Name", "", "Description"));
      return list;
    }
  }  
}

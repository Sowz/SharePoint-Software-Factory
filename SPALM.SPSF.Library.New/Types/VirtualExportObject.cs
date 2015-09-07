namespace SPALM.SPSF.Library
{
  public class VirtualExportObject
  {
    // Fields
    private string id;
    private string url;
    private string includeDescendants;
    private string type;
    
    // Methods
    public VirtualExportObject()
    {
    }

    // Properties
    public string Id
    {
      get
      {
        return this.id;
      }
      set
      {
        this.id = value;
      }
    }

    public string Url
    {
      get
      {
        return this.url;
      }
      set
      {
        this.url = value;
      }
    }

    public string IncludeDescendants
    {
      get
      {
        return this.includeDescendants;
      }
      set
      {
        this.includeDescendants = value;
      }
    }

    public string Type
    {
      get
      {
        return this.type;
      }
      set
      {
        this.type = value;
      }
    }    
  }
}

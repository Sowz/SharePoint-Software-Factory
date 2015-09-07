namespace SPALM.SPSF.Library.ValueProviders
{
    using System;
    using Microsoft.Practices.RecipeFramework;
    using Microsoft.Win32;

    public class DefaultCompanyNameProvider : ValueProvider
  {
    public override bool OnBeginRecipe(object currentValue, out object newValue)
    {
      if (currentValue != null)
      {
          newValue = null;
          return false;
      }

      try
      {
        string org = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", "RegisteredOrganization", "");
        if (!string.IsNullOrEmpty(org))
        {
            newValue = org;
            return true;
        }
      }
      catch(Exception)
      {
      }

      newValue = "MyCompany";
      return true;
    }
  }
}

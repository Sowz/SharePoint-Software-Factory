namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
  /// returns the path to stsadm commmand
  /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class SPPathProvider : ValueProvider
    {
      public override bool OnBeforeActions(object currentValue, out object newValue)
      {
        return SetValue(currentValue, out newValue);
      }

      public override bool OnBeginRecipe(object currentValue, out object newValue)
      {
        return SetValue(currentValue, out newValue);
      }

      private bool SetValue(object currentValue, out object newValue)
      {
        if (currentValue != null)
        {
          newValue = null;
          return false;
        }

        DTE dte = this.GetService<DTE>(true);
        
				//are we on moss 2010??? are we on 64bit

				//Environment.SpecialFolder.CommonProgramFiles
				//  - return "program files on 32bit systems
				//  - return "program files (x86)" on 64 bit systems
				//
				
				newValue = Helpers.GetCommonProgramsFolder() + "\\Microsoft Shared\\web server extensions\\" + Helpers.GetInstalledSharePointVersion() + "\\BIN\\stsadm.exe";
        return true;
      }      
    }
}

namespace SPALM.SPSF.Library.ValueProviders
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    /// <summary>
  /// returns the separator used for file naming
  /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class NameSeparatorProvider : ValueProvider
    {
      public override bool OnBeforeActions(object currentValue, out object newValue)
      {
          newValue = SPSFConstants.NameSeparator;
		return true;
      }

    }
}

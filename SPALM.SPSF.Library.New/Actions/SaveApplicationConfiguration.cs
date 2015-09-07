#region Using Directives



#endregion

namespace SPALM.SPSF.Library.Actions
{
    using EnvDTE;
    using Microsoft.Practices.ComponentModel;
    using Microsoft.Practices.RecipeFramework;

    [ServiceDependency(typeof(DTE))]
	public class SaveApplicationConfiguration : ConfigurableAction
	{
		[Input(Required = false)]
		public object KeyValue
		{
			get { return _KeyValue; }
			set { _KeyValue = value; }
		}
		private object _KeyValue = null;

		[Input(Required = true)]
		public string KeyName
		{
			get { return _KeyName; }
			set { _KeyName = value; }
		}
		private string _KeyName = "";


		public override void Execute()
		{
			DTE dte = GetService<DTE>(true);

			if (_KeyValue == null)
			{
				_KeyValue = "";
			}

			Helpers.SetApplicationConfigValue(dte, _KeyName, _KeyValue.ToString());

		}

		/// <summary>
		/// Removes the previously added reference, if it was created
		/// </summary>
		public override void Undo()
		{
		}
	}
}
using System;
using System.Windows.Forms;

namespace RulesUI
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// normally we get this through the IoC
			IEditorView<Customer> view = new CustomerView();

			// ask user for a new customer that complies with some rules
			view.SetTitle("New customer");
			var result = view.GetData(
				CustomerIs.Valid, 
				CustomerIs.From(AddressIs.Valid));

			if (!result.IsSuccess)
			{
				MessageBox.Show("Exiting. Reason: " + result.ErrorMessage);
				return;
			}

			MessageBox.Show("Let's edit customer now with more strict rule");

			var customer = result.Value;

			// display customer in the view and ask user to make it
			// comply with the rule set that is more strict
			view.SetTitle("Editing " + customer.Name);
			view.BindData(customer);

			result = view.GetData(
				CustomerIs.Valid, 
				CustomerIs.From(AddressIs.Valid, AddressIs.In(Country.Russia)));

			if (result.IsSuccess)
			{
				MessageBox.Show("Congratulations for getting through rule-driven sample");
			}
		}
	}
}
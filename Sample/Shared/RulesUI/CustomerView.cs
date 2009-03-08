using System;
using System.Rules;
using System.Windows.Forms;

namespace RulesUI
{
	public partial class CustomerView : Form, IEditorView<Customer>
	{
		public CustomerView()
		{
			InitializeComponent();

			_validator = new Validator<Customer>(_name, errorProvider1);

			_validator
				.Bind(_name, i => i.Name)
				.Bind(_email, i => i.Email)
				.Bind(_street1, i => i.Address.Street1)
				.Bind(_street2, i => i.Address.Street2)
				.Bind(_country, i=> i.Address.Country)
				.Bind(_zip, i=> i.Address.Zip);

			EnumUtil<Country>.Values.ForEach(c => _country.Items.Add(c));
			_country.SelectedIndex = 0;
		}

		Rule<Customer>[] _rules;
		readonly Validator<Customer> _validator;


		public Result<Customer> GetData(params Rule<Customer>[] rules)
		{
			_rules = rules;

			// we pretend to ask workspace (retrieved from IoC) 
			// for the current presentation framework (windows.forms)
			// to display us
			if (DialogResult.OK == ShowDialog())
			{
				return Result.Success(GetData());
			}

			return Result<Customer>.Error("User gave up.");
		}

		public void BindData(Customer customer)
		{
			_name.Text = customer.Name;
			_email.Text = customer.Email;

			// logically this could be moved to a nested address control
			_street1.Text = customer.Address.Street1;
			_street2.Text = customer.Address.Street2;
			_country.SelectedItem = customer.Address.Country;
			_zip.Text = customer.Address.Zip;
			
		}

		public void SetTitle(string text)
		{
			Text = text;
		}

		Customer GetData()
		{
			var address = new Address(_street1.Text, _street2.Text, 
				_zip.Text, EnumUtil.Parse<Country>(_country.Text));
			return new Customer(_name.Text, address, _email.Text);
		}

		private void _ok_Click(object sender, EventArgs e)
		{
			var customer = GetData();
			if (_validator.RunRules(customer, _rules) == RuleLevel.None)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}
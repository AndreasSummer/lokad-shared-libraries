namespace RulesUI
{
	public sealed class Customer
	{
		readonly Address _address;
		readonly string _name;
		readonly string _email;

		public Customer(string name, Address address, string email)
		{
			_address = address;
			_email = email;
			_name = name;
		}


		public string Email
		{
			get { return _email; }
		}

		public string Name
		{
			get { return _name; }
		}

		public Address Address
		{
			get { return _address; }
		}
	}
}
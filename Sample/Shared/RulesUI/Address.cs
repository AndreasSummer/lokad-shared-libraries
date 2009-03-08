namespace RulesUI
{
	public sealed class Address
	{
		readonly string _street1;
		readonly string _street2;
		readonly string _zip;
		readonly Country _country;

		public Country Country
		{
			get { return _country; }
		}

		public Address(string street1, string street2, string zip, Country country)
		{
			_street1 = street1;
			_country = country;
			_street2 = street2;
			_zip = zip;
		}

		public string Street1
		{
			get { return _street1; }
		}

		public string Street2
		{
			get { return _street2; }
		}

		public string Zip
		{
			get { return _zip; }
		}
	}
}
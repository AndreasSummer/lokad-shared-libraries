using System;
using System.Rules;

namespace RulesUI
{
	static class AddressIs
	{
		// simple static rule
		public static void Valid(Address address, IScope scope)
		{
			scope.Validate(() => address.Street1, StringIs.Limited(10, 256));
			scope.Validate(() => address.Street2, StringIs.Limited(256));
			scope.Validate(() => address.Country, Is.NotDefault);
			scope.Validate(() => address.Zip, StringIs.Limited(10));

			switch (address.Country)
			{
				case Country.USA:
					scope.Validate(() => address.Zip, StringIs.Limited(5, 10));
					break;
				case Country.France:
					break;
				case Country.Russia:
					scope.Validate(() => address.Zip, StringIs.Limited(6, 6));
					break;
				default:
					scope.Validate(() => address.Zip, StringIs.Limited(1, 64));
					break;
			}
		}


		// another dynamic rule
		public static Rule<Address> In(Country country)
		{
			return (address, scope) => scope.Validate(() => address.Country, (c, s) =>
			{
				if (c != country)
					s.Error("We are working only in {0} now.", country);
			});
		}
	}
}
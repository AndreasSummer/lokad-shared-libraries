using System;
using System.Rules;

namespace RulesUI
{
	public static class CustomerIs
	{
		public static void Valid(Customer customer, IScope scope)
		{
			scope.Validate(() => customer.Name, StringIs.Limited(3, 64));

			if (customer.Address.Country == Country.Russia)
			{
				scope.Validate(() => customer.Email, (s, scope1) =>
				{
					if (!string.IsNullOrEmpty(s))
						scope1.Error(
							@"Russian customers do not have emails. 
Use pigeons instead.");
				});
			}
			else
			{
				scope.Validate(() => customer.Email, StringIs.ValidEmail);
			}
		}

		// dynamic rule
		public static Rule<Customer> From(params Rule<Address>[] addressRules)
		{
			return (customer, scope) => scope.Validate(() => customer.Address, addressRules);
		}
	}
}
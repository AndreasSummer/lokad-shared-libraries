namespace Lokad.Api.Core
{
	static class GlobalSetup
	{
		public static readonly Identity Identity = new Identity()
			{
				Username = "rinat@domain.tld",
				Password = "NeverStorePwdsInCode"
			};
	}
}
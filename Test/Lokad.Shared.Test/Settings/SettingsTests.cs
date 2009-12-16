using System.Collections.Generic;
using NUnit.Framework;
using Lokad.Testing;

namespace Lokad.Settings
{
	[TestFixture]
	public sealed class SettingsTests
	{
		readonly ISettingsProvider _provider = new Dictionary<string, string>
				{
					{"Path/1", "1"},
					{"Path/2", "2"},
					{"Root/3", "3"}
				}.AsSettingsProvider();

		[Test]
		public void Test()
		{
			_provider.GetValue("Path/1").ShouldBe("1");
			_provider.GetValue("Path/?").ShouldFail();
		}

		[Test]
		public void Filtering()
		{
			var provider = _provider.FilteredByPrefix("Path/");

			provider.GetValue("Path/1").ShouldFail();
			provider.GetValue("1").ShouldBe("1");
		}
	}
}
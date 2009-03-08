using NUnit.Framework;

namespace System.Rules
{
	[TestFixture]
	public sealed class TrackScopeTests
	{
		[Test]
		public void Test_Nesting()
		{
			using(var t = new TrackScope())
			{
				ScopeTestHelper.RunNesting(0,t);
				Assert.IsTrue(t.IsError());
			}
		}
	}
}
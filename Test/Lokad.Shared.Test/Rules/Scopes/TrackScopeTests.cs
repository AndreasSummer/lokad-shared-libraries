#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class TrackScopeTests
	{
		[Test]
		public void Test_Nesting()
		{
			using (var t = new TrackScope())
			{
				ScopeTestHelper.RunNesting(0, t);
				Assert.IsTrue(t.IsError());
			}
		}
	}
}
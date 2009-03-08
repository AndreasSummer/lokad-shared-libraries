#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace System.Rules
{
	[TestFixture]
	public sealed class SimpleScopeTests
	{
		[Test]
		public void Nesting_Works()
		{
			IScope t = new SimpleScope("Test", (path, level, message) =>
				{
					Assert.AreEqual("Test.Child", path);
					Assert.AreEqual(RuleLevel.Error, level);
					Assert.AreEqual("Message", message);
				}, level => { });

			using (var scope = t.Create("Child"))
			{
				scope.Error("Message");
				Assert.AreEqual(RuleLevel.Error, scope.Level);
			}
			Assert.AreEqual(RuleLevel.Error, t.Level);
		}
	}
}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class DelayedScopeTests
	{
		[Test]
		public void Test()
		{
			int nameCounter = 0;
			int runCounter = 0;
			Func<string> func = () => (nameCounter++).ToString();

			var t = new DelayedScope(func, (func1, level, s) =>
				{
					func1();
					runCounter++;
				});

			Assert.AreEqual(0, nameCounter);
			ScopeTestHelper.RunNesting(0, t);
			Assert.AreEqual(1, nameCounter);
			Assert.AreEqual(6, runCounter);
		}

		[Test]
		public void Nesting()
		{
			IScope s = new DelayedScope(() => "Name", (provider, level, message) =>
				{
					Assert.AreEqual("Name.Child", provider());
					Assert.AreEqual(level, RuleLevel.Warn);
					Assert.AreEqual("Message", message);
				});

			using (var child = s.Create("Child"))
			{
				child.Warn("Message");
				Assert.AreEqual(RuleLevel.Warn, child.Level);
			}
			Assert.AreEqual(RuleLevel.Warn, s.Level);
		}
	}
}
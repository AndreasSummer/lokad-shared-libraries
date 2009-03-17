#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad.Rules
{
	static class ScopeTestHelper
	{
		public static void FireErrors(IScope t)
		{
			t.Error("ErrA");
			using (var scope = t.Create("Nested"))
			{
				scope.Error("ErrB");
			}
			t.Error("ErrC");
		}

		public static void ShouldHave(Exception ex, params string[] args)
		{
			foreach (var s in args)
			{
				StringAssert.Contains(s, ex.Message);
			}
		}

		public static void ShouldNotHave(Exception ex, params string[] args)
		{
			foreach (var s in args)
			{
				Assert.IsFalse(ex.Message.Contains(s));
			}
		}

		public static void ShouldBeClean(Exception ex)
		{
			Assert.IsFalse(
				ex.Message.Contains("Scope") ||
					ex.Message.Contains("Dispose") ||
						ex.Message.Contains("Write"),
				"Exception {0} should not contain debug info", ex);
		}

		public static void RunNesting(int nil, IScope scope)
		{
			scope.Write(RuleLevel.None, "None1");
			using (var child = scope.Create("Group1"))
			{
				child.Write(RuleLevel.Warn, "Warn1");
				using (var grand = child.Create("Group2"))
				{
					grand.Write(RuleLevel.None, "None2");
					grand.Error("Error1");
				}
				child.Write(RuleLevel.None, "None3");
			}
			scope.Write(RuleLevel.None, "None4");
		}
	}
}
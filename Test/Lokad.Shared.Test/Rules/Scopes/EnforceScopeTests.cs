#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace System.Rules
{
	[TestFixture]
	public sealed class EnforceScopeTests
	{
		[Test, Expects.RuleException]
		public void Test()
		{
			var Test = 1;
			try
			{
				using (var t = DelayedScope.ForEnforce(() => Test, Scope.WhenError))
				{
					ScopeTestHelper.FireErrors(t);
				}
			}
			catch (RuleException ex)
			{
				ScopeTestHelper.ShouldBeClean(ex);
				ScopeTestHelper.ShouldHave(ex, "ErrA");
				ScopeTestHelper.ShouldNotHave(ex, "ErrB", "ErrC");
				throw;
			}
		}
	}
}
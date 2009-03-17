#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class ValidationScopeTests
	{
		[Test, Expects.RuleException]
		public void All_Problems_Are_Collected_Properly()
		{
			try
			{
				using (var t = Scope.ForValidation("Test", Scope.WhenError))
				{
					ScopeTestHelper.FireErrors(t);
				}
			}
			catch (RuleException ex)
			{
				ScopeTestHelper.ShouldBeClean(ex);
				ScopeTestHelper.ShouldHave(ex, "ErrA", "ErrB", "ErrC");
				throw;
			}
		}

		[Test, Expects.RuleException]
		public void Test_Nesting()
		{
			try
			{
				using (var t = Scope.ForValidation("Test", Scope.WhenError))
				{
					ScopeTestHelper.RunNesting(0, t);
				}
			}
			catch (RuleException ex)
			{
				ScopeTestHelper.ShouldBeClean(ex);
				ScopeTestHelper.ShouldHave(ex, "None1", "Warn1", "Group1", "Group2", "None3");
				throw;
			}
		}
	}
}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Linq;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class RuleAssertTests
	{
		[Test]
		public void UseCases()
		{
			RuleAssert.IsNone(1, Is.NotDefault);
			RuleAssert.IsError(1, Is.Default);
			RuleAssert.IsWarn(1, (i, scope) => scope.Warn("Warning"));
		}

		[Test, Expects.RuleException]
		public void UseCase_For_That()
		{
			var visitor = new Visitor
				{
					Programs = new[]
						{
							new Program
								{
									Name = "Some"
								},
							new Program
								{
									Active = false
								},
						}
				};

			//Assert.IsTrue(visitor.Programs.Exists(p => p.Active),
			//    "visitor should have at least one active program");

			RuleAssert.That(() => visitor,
				v => v.Programs.Exists(p => p.Active),
				v => v.Programs.Length > 1);
		}

		[Test, Expects.RuleException]
		public void Expect()
		{
			RuleAssert.For<int>(Is.Default).ExpectNone(1);
		}
	}
}
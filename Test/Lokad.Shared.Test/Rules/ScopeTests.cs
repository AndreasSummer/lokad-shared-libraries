#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using Lokad.Testing;
using NUnit.Framework;

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class ScopeTests
	{
		static readonly Rule<int> Error = Is.AtLeast(0);

		static readonly Rule<int> MustBeLucky = (i, scope) =>
			{
				if (i == 13)
					scope.Warn("Unlucky number");
			};

		[Test]
		public void Test()
		{
			RuleAssert.IsTrue(() => Scope.IsValid(1, MustBeLucky, Error));
			RuleAssert.IsTrue(() => Scope.IsWarn(13, MustBeLucky, Error));
			RuleAssert.IsTrue(() => Scope.IsError(-1, MustBeLucky, Error));
		}

		[Test]
		public void UseCases()
		{
			var one = 12;
			var many = new[] {12, 14};

			Scope.Validate(one, MustBeLucky, Error);
			Scope.ValidateMany(many, MustBeLucky, Error);

			Scope.GetMessages(one, "one", MustBeLucky, Error);
			Scope.GetMessagesForMany(many, "many", MustBeLucky, Error);

			Scope.GetMessages(() => one, MustBeLucky, Error);
			Scope.GetMessagesForMany(() => many, MustBeLucky, Error);

			const RuleLevel level = RuleLevel.None;
			Scope.WhenNone(level);
			Scope.WhenWarn(level);
			Scope.WhenError(level);
			Scope.WhenAny(level);
		}

		[Test]
		public void Messaging_UseCases()
		{
			Scope.GetMessages(string.Empty, scope =>
				{
					scope.Error("Message");
					scope.Error("Message {0}", 1);

					scope.Warn("Message");
					scope.Warn("Message {0}", 1);

					scope.Info("Message");
					scope.Info("Message {0}", 1);
				});
		}
	}
}
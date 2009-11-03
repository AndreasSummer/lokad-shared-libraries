#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad.Testing.Test
{
	[TestFixture]
	public sealed class ExtendOptionalTests
	{
		static readonly IEquatable<string> Text = "Text";

		// ReSharper disable InconsistentNaming

		static readonly Result<string, int> ResultGood = "Good";
		static readonly Result<string, int> Result11 = 11;
		static readonly Result<int> Result10 = 10;
		static readonly Result<int> ResultError = "Error";
		static readonly Maybe<int> Maybe10 = 10;
		static readonly Maybe<int> MaybeNot = Maybe<int>.Empty;

		[Test]
		public void Use_cases_1()
		{
			ResultGood
				.ShouldPass()
				.ShouldPassCheck(i => i == "Good")
				.ShouldPassWith("Good")
				.ShouldBe("Good");

			Result11
				.ShouldFailWith(11)
				.ShouldFail()
				.ShouldBe(11);

			Result10
				.ShouldPass()
				.ShouldPassWith(10)
				.ShouldPassCheck(i => i == 10)
				.ShouldBe(10);

			ResultError
				.ShouldFail()
				.ShouldFailWith("Error")
				.ShouldBe("Error");

			Maybe10
				.ShouldPass()
				.ShouldPassCheck(i => i == 10)
				.ShouldPassWith(10)
				.ShouldBe(10)
				.ShouldBe(true);

			MaybeNot
				.ShouldFail()
				.ShouldBe(false);
		}

		[Test]
		public void Test()
		{
			Text
				.ShouldBeEqualTo("Text").ShouldBeEqualTo("Text");
		}
	}
}
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
	public sealed class MaybeIsTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void EmptyOr()
		{
			RuleAssert.For(MaybeIs.EmptyOr(StringIs.WithoutUppercase))
				.ExpectNone(Maybe<string>.Empty, "lowercase")
				.ExpectError(null, "UpperCase");
		}

		[Test]
		public void ValidAnd()
		{
			RuleAssert.For(MaybeIs.ValidAnd(StringIs.WithoutUppercase))
				.ExpectNone("lowercase")
				.ExpectError(null, Maybe.String, "Uppercase");
		}
	}
}
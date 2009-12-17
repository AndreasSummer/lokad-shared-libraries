#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using Lokad.Testing;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class MaybeParseTests
	{
		// ReSharper disable InconsistentNaming

		enum Tri
		{
			Good,
			Bad,
			Ugly
		}

		[Test]
		public void Enum()
		{
			MaybeParse.Enum<Tri>("Good").ShouldBe(Tri.Good);
			MaybeParse.Enum<Tri>("BAD").ShouldBe(Tri.Bad);

			MaybeParse.Enum<Tri>("ugly", false).ShouldFail();
			MaybeParse.Enum<Tri>("").ShouldFail();
			MaybeParse.Enum<Tri>(null).ShouldFail();
		}

		[Test]
		public void Decimal()
		{
			MaybeParse.Decimal("12.1").ShouldBe(12.1m);
			MaybeParse.Decimal("???").ShouldFail();
		}

		[Test]
		public void Int32()
		{
			MaybeParse.Int32("12").ShouldBe(12);
			MaybeParse.Int32("12.1").ShouldFail();
			MaybeParse.Int32(null).ShouldFail();
			MaybeParse.Int32("???").ShouldFail();
		}

		[Test]
		public void Single()
		{
			MaybeParse.Single("12.1").ShouldBe(12.1f);
			MaybeParse.Single("???").ShouldFail();
			MaybeParse.Single(null).ShouldFail();
		}

		[Test]
		public void Double()
		{
			MaybeParse.Double("12.1").ShouldBe(12.1d);
			MaybeParse.Double("???").ShouldFail();
			MaybeParse.Double(null).ShouldFail();
		}
	}
}
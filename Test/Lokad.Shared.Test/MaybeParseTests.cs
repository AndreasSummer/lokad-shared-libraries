#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Globalization;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class MaybeParseTests
	{
		public static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;
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
			MaybeParse.Decimal(12.1m.ToString()).ShouldBe(12.1m);
			MaybeParse.Decimal("???").ShouldFail();
			MaybeParse.Decimal(null).ShouldFail();

			const NumberStyles styles = NumberStyles.Number;
			

			MaybeParse.Decimal(null, styles, Invariant).ShouldFail();
			MaybeParse.Decimal("??", styles, Invariant).ShouldFail();
			MaybeParse.Decimal("12.1", styles, Invariant).ShouldBe(12.1m);

			MaybeParse.DecimalInvariant(null).ShouldFail();
			MaybeParse.DecimalInvariant("??").ShouldFail();
			MaybeParse.DecimalInvariant("12.1").ShouldBe(12.1m);

		}

		[Test]
		public void Int32()
		{
			MaybeParse.Int32("12").ShouldBe(12);
			MaybeParse.Int32("12.1").ShouldFail();
			MaybeParse.Int32(null).ShouldFail();
			MaybeParse.Int32("???").ShouldFail();


			MaybeParse.Int32Invariant("12").ShouldBe(12);
			MaybeParse.Int32Invariant("12.1").ShouldFail();
			MaybeParse.Int32Invariant(null).ShouldFail();
			MaybeParse.Int32Invariant("???").ShouldFail();
		}

		[Test]
		public void Int64()
		{
			MaybeParse.Int64("12").ShouldBe(12);
			MaybeParse.Int64("12.1").ShouldFail();
			MaybeParse.Int64(null).ShouldFail();
			MaybeParse.Int64("???").ShouldFail();


			MaybeParse.Int64Invariant("12").ShouldBe(12);
			MaybeParse.Int64Invariant("12.1").ShouldFail();
			MaybeParse.Int64Invariant(null).ShouldFail();
			MaybeParse.Int64Invariant("???").ShouldFail();
		}

		[Test]
		public void Single()
		{
			MaybeParse.Single(12.1f.ToString()).ShouldBe(12.1f);
			MaybeParse.Single("???").ShouldFail();
			MaybeParse.Single(null).ShouldFail();

			MaybeParse.SingleInvariant("12.1").ShouldBe(12.1f);
			MaybeParse.SingleInvariant("???").ShouldFail();
			MaybeParse.SingleInvariant(null).ShouldFail();
		}

		[Test]
		public void Double()
		{
			MaybeParse.Double(12.1d.ToString()).ShouldBe(12.1d);
			MaybeParse.Double("???").ShouldFail();
			MaybeParse.Double(null).ShouldFail();


			MaybeParse.DoubleInvariant("12.1").ShouldBe(12.1d);
			MaybeParse.DoubleInvariant("???").ShouldFail();
			MaybeParse.DoubleInvariant(null).ShouldFail();
		}
	}
}
#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using Lokad.Api.Legacy;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Lokad.Api.Core.Legacy
{
	[TestFixture]
	public class SerieOperationsTests
	{
		[Test]
		public void GetEnd()
		{
			var timeSeries = new[]
				{
					new TimeValue {Time = new DateTime(2001, 1, 1), Value = 0.0},
					new TimeValue {Time = new DateTime(2001, 1, 2), Value = 1.0},
					new TimeValue {Time = new DateTime(2001, 1, 3), Value = 2.0}
				};

			var boundary = new DateTime(2001, 1, 2);

			var expectedResult = new[]
				{
					timeSeries[1],
					timeSeries[2]
				};

			var result = SerieOperations.GetEnd(timeSeries, boundary);

			Assert.That(result, Is.EquivalentTo(expectedResult));
		}

		[Test]
		public void Merge()
		{
			var timeSerie1 = new[]
				{
					new TimeValue {Time = new DateTime(2001, 1, 1), Value = 0.0},
					new TimeValue {Time = new DateTime(2001, 1, 2), Value = 1.0},
					new TimeValue {Time = new DateTime(2001, 1, 3), Value = 2.0}
				};

			var timeSerie2 = new[]
				{
					new TimeValue {Time = new DateTime(2001, 1, 2), Value = 3.0},
					new TimeValue {Time = new DateTime(2001, 1, 3), Value = 4.0},
					new TimeValue {Time = new DateTime(2001, 1, 4), Value = 5.0}
				};

			var merged1and2 = new[]
				{
					timeSerie1[0],
					timeSerie2[0],
					timeSerie2[1],
					timeSerie2[2]
				};

			var merged2and1 = new[]
				{
					timeSerie1[0],
					timeSerie1[1],
					timeSerie1[2]
				};

			var actualMergedWithEmpty = SerieOperations.Merge(timeSerie1, new TimeValue[0]);
			Assert.That(actualMergedWithEmpty, Is.EquivalentTo(timeSerie1),
				"Merge with empty increment should have no effect.");

			var actualMerged1and2 = SerieOperations.Merge(timeSerie1, timeSerie2);
			Assert.That(actualMerged1and2, Is.EquivalentTo(merged1and2), "Unexpected merge.");

			var actualMerged2and1 = SerieOperations.Merge(timeSerie2, timeSerie1);
			Assert.That(actualMerged2and1, Is.EquivalentTo(merged2and1), "Unexpected merge.");
		}

		[Test]
		public void PruneOverlap()
		{
			var timeSerie1 = new[]
				{
					new TimeValue {Time = new DateTime(2001, 1, 1), Value = 0.0},
					new TimeValue {Time = new DateTime(2001, 1, 2), Value = 1.0},
					new TimeValue {Time = new DateTime(2001, 1, 3), Value = 2.0}
				};

			var timeSerie2 = new[]
				{
					new TimeValue {Time = new DateTime(2001, 1, 2), Value = 3.0},
					new TimeValue {Time = new DateTime(2001, 1, 3), Value = 4.0},
					new TimeValue {Time = new DateTime(2001, 1, 4), Value = 5.0}
				};

			var nonOverlap2minus1 = new[]
				{
					timeSerie2[2]
				};

			var actualNullNonOverlap = SerieOperations.PruneOverlap(new TimeValue[0], null);
			Assert.IsNull(actualNullNonOverlap, "Unexpected pruning.");

			var actualNonOverlap1minusEmpty =
				SerieOperations.PruneOverlap(new TimeValue[0], timeSerie1);
			Assert.That(actualNonOverlap1minusEmpty, Is.EquivalentTo(timeSerie1), "Unexpected pruning.");

			var actualNonOverlapEmptyMinus1 =
				SerieOperations.PruneOverlap(timeSerie1, new TimeValue[0]);
			Assert.That(actualNonOverlapEmptyMinus1, Is.EquivalentTo(new TimeValue[0]), "Unexpected pruning.");

			var actualNonOverlap2minus1 =
				SerieOperations.PruneOverlap(timeSerie1, timeSerie2);
			Assert.That(actualNonOverlap2minus1, Is.EquivalentTo(nonOverlap2minus1), "Unexpected pruning.");
		}

		[Test]
		public void Split()
		{
			var list = new List<int>(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10});
			var result = SerieOperations.Split(3, list);
			Assert.That(4, Is.EqualTo(result.Count));
			Assert.That(result[0].ToArray(), Is.EquivalentTo(new[] {1, 2, 3}));
			Assert.That(result[1].ToArray(), Is.EquivalentTo(new[] {4, 5, 6}));
			Assert.That(result[2].ToArray(), Is.EquivalentTo(new[] {7, 8, 9}));
			Assert.That(result[3].ToArray(), Is.EquivalentTo(new[] {10}));
		}

		[Test]
		public void Transform()
		{
			var values = Range.Array(13, i => TimeValue.From(new DateTime(2001, 1, i + 1), i + 1));

			var expectedResult = new[]
				{
					new TimeValue {Time = new DateTime(2000, 12, 27), Value = 3.0},
					new TimeValue {Time = new DateTime(2001, 1, 3), Value = 42.0},
					new TimeValue {Time = new DateTime(2001, 1, 10), Value = 46.0}
				};

			var period = Period.Week;
			DateTime? periodStart = new DateTime(2001, 1, 3);

			var result = SerieOperations.Transform(values, period, periodStart);

			Assert.That(3, Is.EqualTo(result.Length));
			Assert.AreEqual(result[0].Time, expectedResult[0].Time);
			Assert.AreEqual(result[0].Value, expectedResult[0].Value);
			Assert.AreEqual(result[1].Time, expectedResult[1].Time);
			Assert.AreEqual(result[1].Value, expectedResult[1].Value);
			Assert.AreEqual(result[2].Time, expectedResult[2].Time);
			Assert.AreEqual(result[2].Value, expectedResult[2].Value);
		}

		[Test]
		public void GetValue()
		{
			var timeSerie = new[]
				{
					TimeValue.From(new DateTime(2000, 12, 27), 3.0),
					TimeValue.From(new DateTime(2001, 1, 3), 42.0),
					TimeValue.From(new DateTime(2001, 1, 10), 46.0)
				};

			double? v = SerieOperations.GetValue(timeSerie, new DateTime(2000, 12, 27));
			Assert.IsTrue(v.HasValue, "#A00");
			Assert.AreEqual(3.0, v.Value, "#A01");

			v = SerieOperations.GetValue(timeSerie, new DateTime(2001, 1, 3));
			Assert.IsTrue(v.HasValue, "#A02");
			Assert.AreEqual(42.0, v.Value, "#A03");

			v = SerieOperations.GetValue(timeSerie, new DateTime(2001, 1, 10));
			Assert.IsTrue(v.HasValue, "#A04");
			Assert.AreEqual(46.0, v.Value, "#A05");

			v = SerieOperations.GetValue(timeSerie, new DateTime(1999, 1, 1));
			Assert.IsFalse(v.HasValue, "#A06");

			v = SerieOperations.GetValue(timeSerie, new DateTime(2001, 1, 4));
			Assert.IsFalse(v.HasValue, "#A07");
		}
	}
}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class TupleTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void Test_tuple5()
		{
			var t1 = CreateTuple5();
			var copy = Tuple.From(t1.Item1, t1.Item2, t1.Item3, t1.Item4, t1.Item5);
			var t2 = CreateTuple5();

			Assert.AreEqual(t1, copy);
			Assert.AreNotEqual(t1, t2);
			Assert.AreEqual(t1.GetHashCode(), copy.GetHashCode());
			Assert.AreNotEqual(t1.GetHashCode(), t2.GetHashCode());
			Assert.AreEqual(t1.ToString(), copy.ToString());
			Assert.AreNotEqual(t1.ToString(), t2.ToString());

			Assert.IsTrue(t1 == copy);
			Assert.IsTrue(t1 != t2);
		}

		[Test]
		public void Test_tuple6()
		{
			var t1 = CreateTuple6();
			var copy = Tuple.From(t1.Item1, t1.Item2, t1.Item3, t1.Item4, t1.Item5, t1.Item6);
			var t2 = CreateTuple6();

			Assert.AreEqual(t1, copy);
			Assert.AreNotEqual(t1, t2);
			Assert.AreEqual(t1.GetHashCode(), copy.GetHashCode());
			Assert.AreNotEqual(t1.GetHashCode(), t2.GetHashCode());
			Assert.AreEqual(t1.ToString(), copy.ToString());
			Assert.AreNotEqual(t1.ToString(), t2.ToString());

			Assert.IsTrue(t1 == copy);
			Assert.IsTrue(t1 != t2);
		}

		[Test]
		public void Test_tuple7()
		{
			var t1 = CreateTuple7();
			var copy = Tuple.From(t1.Item1, t1.Item2, t1.Item3, t1.Item4, t1.Item5, t1.Item6, t1.Item7);
			var t2 = CreateTuple7();

			Assert.AreEqual(t1, copy);
			Assert.AreNotEqual(t1, t2);
			Assert.AreEqual(t1.GetHashCode(), copy.GetHashCode());
			Assert.AreNotEqual(t1.GetHashCode(), t2.GetHashCode());
			Assert.AreEqual(t1.ToString(), copy.ToString());
			Assert.AreNotEqual(t1.ToString(), t2.ToString());

			Assert.IsTrue(t1 == copy);
			Assert.IsTrue(t1 != t2);
		}

		[Test]
		public void Test_appending()
		{
			var tuple = CreateTuple7();

			var appended = Tuple
				.From(tuple.Item1, tuple.Item2)
				.Append(tuple.Item3)
				.Append(tuple.Item4)
				.Append(tuple.Item5)
				.Append(tuple.Item6)
				.Append(tuple.Item7);

			Assert.AreEqual(tuple, appended);
		}

		static Tuple<bool, DateTime, object, string, double, Guid, Guid> CreateTuple7()
		{
			return Tuple.From(
				Rand.NextBool(),
				Rand.NextDate(),
				(object) null,
				Rand.NextString(10, 23),
				Rand.NextDouble(),
				Rand.NextGuid(),
				Rand.NextGuid());
		}

		static Tuple<object, bool, DateTime, string, double, Guid> CreateTuple6()
		{
			return Tuple.From(
				(object) null,
				Rand.NextBool(),
				Rand.NextDate(),
				Rand.NextString(10, 23),
				Rand.NextDouble(),
				Rand.NextGuid());
		}

		static Tuple<object, bool, DateTime, string, double> CreateTuple5()
		{
			return Tuple.From(
				(object) null,
				Rand.NextBool(),
				Rand.NextDate(),
				Rand.NextString(10, 23),
				Rand.NextDouble());
		}
	}
}
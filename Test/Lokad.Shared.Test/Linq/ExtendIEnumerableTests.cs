#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad.Linq
{
	[TestFixture]
	public sealed class ExtendIEnumerableTests
	{
		// ReSharper disable InconsistentNaming
		[Test]
		public void ForEach_Works()
		{
			int sum = 0;

			new[] {1, 2, 3, 4}.ForEach(i => sum += i);

			Assert.AreEqual(10, sum);
		}

		[Test]
		public void ForEach2_Works()
		{
			int sum = 0;

			new[] {1, 2, 3, 4}.ForEach((i, k) => sum += (i + k));

			Assert.AreEqual(16, sum);
		}

		[Test]
		public void Exists_Works()
		{
			Assert.IsTrue(new[] {1, 2, 3, 4}.Exists(i => i == 3));
			Assert.IsFalse(new[] {1, 2, 3, 4}.Exists(i => i == 5));
		}

		[Test]
		public void Append_Works()
		{
			CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5}, Range.Create(1, 3).Append(4, 5).ToArray());
		}

		[Test]
		public void Prepend_Works()
		{
			CollectionAssert.AreEqual(new[] {4, 5, 1, 2, 3}, Range.Create(1, 3).Prepend(4, 5).ToArray());
		}

		[Test]
		public void Append_Works_For_Ranges()
		{
			CollectionAssert.AreEqual(new[] {1, 2, 3, 4}, Range.Create(1, 2).Append(new[] {3, 4}).ToArray());
		}

#if !SILVERLIGHT2
		[Test]
		public void ToSet_Works()
		{
			CollectionAssert.AreEquivalent(new[] {1, 2, 3}, new[] {1, 2, 3, 2}.ToSet().ToArray());
			CollectionAssert.AreEquivalent(new[] {1D, 2D, 3D}, new[] {1, 2, 3, 2}.ToSet(i => (double) i).ToArray());
		}
#endif

		[Test]
		public void Apply_Does_Not_Enumerate()
		{
			Enumerable.Repeat(1, 10).Apply(i => { throw new InvalidOperationException(); });
			Enumerable.Repeat(1, 10).Apply((i, int1) => { throw new InvalidOperationException(); });
		}

		[Test]
		public void Apply_Works()
		{
			var sum = 0;
			var ints = new[] {1, 2, 3, 4};
			CollectionAssert.AreEqual(ints, ints.Apply(i => sum += i).ToArray());
			Assert.AreEqual(10, sum);
		}

		[Test]
		public void Apply2_Works()
		{
			var sum = 0;
			var ints = new[] {1, 2, 3, 4};
			CollectionAssert.AreEqual(ints, ints.Apply((i, k) => sum += (i + k)).ToArray());
			Assert.AreEqual(16, sum);
		}

		[Test]
		public void MaxMin_Work_For_Structs()
		{
			var ints = new[] {0, 5, 3, 0};
			Assert.AreEqual(5, ints.Max(Comparer<int>.Default), "Max");
			Assert.AreEqual(0, ints.Min(Comparer<int>.Default), "Min");
		}

		[Test]
		public void MaxMin_Work_For_Classes()
		{
			var ints = new[] {0, 2, 5, 3}.Select(i => i == 0 ? null : (object) i);
			Assert.AreEqual(5, ints.Max(Comparer<object>.Default), "Max");
			Assert.AreEqual(2, ints.Min(Comparer<object>.Default), "Min");
		}

		[Test, Expects.ArgumentException]
		public void Max_Throws_On_Empty_Struct_Sequence()
		{
			new int[0].Max(Comparer<int>.Default);
		}

		[Test, Expects.ArgumentException]
		public void Min_Throws_On_Empty_Struct_Sequence()
		{
			new int[0].Min(Comparer<int>.Default);
		}

		[Test]
		public void MaxMin_Return_Null_On_Empty_Ref_Sequence()
		{
			Assert.IsNull(new object[0].Max(Comparer<object>.Default), "Max");
			Assert.IsNull(new object[0].Min(Comparer<object>.Default), "Min");
		}


		[Test]
		public void Slice_Works_For_Full_Arrays()
		{
			var sliced = Range
				.Create(8)
				.Slice(4)
				.ToArray();

			var expected = new[]
				{
					new[] {0, 1, 2, 3},
					new[] {4, 5, 6, 7}
				};

			CollectionAssert.AreEqual(expected, sliced);
		}

		[Test]
		public void Slice_Works_For_Partial_Collections()
		{
			var sliced = Range
				.Create(7)
				.Slice(4)
				.ToArray();

			var expected = new[]
				{
					new[] {0, 1, 2, 3},
					new[] {4, 5, 6}
				};

			CollectionAssert.AreEqual(expected, sliced);
		}

		[Test]
		public void Slice_Works_For_Empty_Collections()
		{
			CollectionAssert.AreEqual(new int[0][], new int[0].Slice(5).ToArray());
		}

		[Test, Expects.ArgumentOutOfRangeException]
		public void Slice_Detects_Invalid_SliceSize()
		{
			new[] {1}.Slice(-1).ToArray();
		}

		//overload

		[Test]
		public void Weighted_Slice_Works()
		{
			var sliced = Range
				.Create(8)
				.Slice(4, i => i, 15)
				.ToArray();

			var expected = new[]
				{
					new[] {0, 1, 2, 3},
					new[] {4, 5, 6},
					new[] {7}
				};
			CollectionAssert.AreEqual(expected, sliced);
		}

		[Test]
		public void Weighted_Slice_Works_For_Empty_Collections()
		{
			CollectionAssert.AreEqual(new int[0][], new int[0].Slice(5, i => i, 5).ToArray());
		}

		[Test]
		public void Distinct()
		{
			var sequence = Range.Array(10).Distinct(i => i%3).ToArray();

			CollectionAssert.AreEqual(new[] {0, 1, 2}, sequence);
		}

		[Test]
		public void ToArray()
		{
			var actual = Range.Create(10).ToArray((i, index) => i + index);
			var expected = Range.Array(10, i => i*2);
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void ToJaggedArray()
		{
			var list = new List<IEnumerable<int>>
				{
					new List<int>
						{
							1,
							2,
							3,
						},
					new List<int>
						{
							6,
							5,
							4
						},
					new List<int>()
				};

			var actual = list.ToJaggedArray();
			var expected = new[] {new[] {1, 2, 3}, new[] {6, 5, 4}, new int[0]};

			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void FirstOrEmpty()
		{
			var obj1 = new object();
			var obj2 = new object();
			var obj3 = new object();

			var seq1 = new[] {obj1, obj2, obj3};
			var seq2 = new object[0];

			seq1.FirstOrEmpty(o => o == obj3)
				.ShouldBe(obj3);

			seq1.FirstOrEmpty(o => o == null)
				.ShouldFail();
			
			seq1.FirstOrEmpty()
				.ShouldBe(obj1);

			seq2.FirstOrEmpty()
				.ShouldFail();
		}

		[Test]
		public void ToIndexed()
		{
			var enumerable = Range.Create(10).ToIndexed();

			Assert.IsTrue(enumerable.First().IsFirst);

			foreach (var indexer in enumerable)
			{
				Assert.AreEqual(indexer.Index, indexer.Value);
				Assert.AreEqual(indexer.IsFirst, indexer.Index == 0);
			}
		}

		[Test]
		public void ToIndexDictionary()
		{
			var dict = Range.Create(10).ToIndexDictionary();

			for (int i = 0; i < 10; i++)
			{
				Assert.AreEqual(i, dict[i]);
			}
		}

		[Test]
		public void SelectValues()
		{
			var array = Range.Array(10, i => Rand.NextMaybe(i));
			Assert.AreEqual(array.Sum(s => s.GetValue(0)), array.SelectValues().Sum());
		}
	}
}
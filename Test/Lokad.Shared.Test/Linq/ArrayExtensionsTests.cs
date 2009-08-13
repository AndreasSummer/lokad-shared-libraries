#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Linq;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad.Linq
{
	[TestFixture]
	public sealed class ArrayExtensionsTests
	{
		[Test, Expects.ArgumentNullException]
		public void Slice_Detects_Null_Argument()
		{
			ExtendArray.SliceArray<int>(null, 3);
		}

		[Test, Expects.ArgumentOutOfRangeException]
		public void Slice_Detects_Zero_SliceLength()
		{
			new int[0].SliceArray(0);
		}

		[Test]
		public void Slice_Works_For_Empty_Arrays()
		{
			CollectionAssert.AreEqual(new int[0][], new int[0].SliceArray(4));
		}

		[Test]
		public void Slice_Works_For_Full_Slices()
		{
			var slices = Range.Array(8).SliceArray(4);

			var expected = new[]
				{
					new[] {0, 1, 2, 3},
					new[] {4, 5, 6, 7}
				};

			CollectionAssert.AreEqual(expected, slices);
		}

		[Test]
		public void Slice_Works_For_Incomplete_Slices()
		{
			var slices = Range.Array(7).SliceArray(4);

			var expected = new[]
				{
					new[] {0, 1, 2, 3},
					new[] {4, 5, 6}
				};

			CollectionAssert.AreEqual(expected, slices);
		}

		[Test]
		public void ForEach()
		{
			var i = Rand.Next(20);

			Range.Array(i, n => 1).ForEach(n => i -= 1);
			Assert.AreEqual(i, 0);
		}

		[Test]
		public void Append()
		{
			var i = Rand.Next(10);
			var a = Range.Array(i).Append(Range.Array(i));

			for (int j = 0; j < i; j++)
			{
				Assert.AreEqual(a[j], a[j + i]);
			}
		}

		[Test]
		public void Convert_With_Int()
		{
			var ints = Range.Array(Rand.Next(10)).Convert((n, i) => n - i);

			foreach (var i in ints)
			{
				Assert.AreEqual(0, i);
			}
		}

		[Test]
		public void Empty_jagged_array2()
		{
			var obj = new object[0,0];
			object[][] objects = obj.ToJaggedArray();
			Assert.AreEqual(0, objects.Length);
		}

		[Test]
		public void Non_empty_jagger_array2()
		{
			var obj = new[,] {{1, 2}, {3, 4}};
			int[][] actual = obj.ToJaggedArray();

			var expected = new[] {new[] {1, 2}, new[] {3, 4}};
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void Shifted_jagged_array2()
		{
			var instance = Array.CreateInstance(typeof (int), new[] {2, 2}, new[] {1, 1});
			var array = (int[,]) instance;

			array[1, 1] = 1;
			array[1, 2] = 2;
			array[2, 1] = 3;
			array[2, 2] = 4;
			var expected = new[] {new[] {1, 2}, new[] {3, 4}};
			CollectionAssert.AreEqual(expected, array.ToJaggedArray());
		}
	}
}
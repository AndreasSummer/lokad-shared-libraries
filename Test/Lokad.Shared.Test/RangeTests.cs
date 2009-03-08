#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Linq;
using NUnit.Framework;

namespace System
{
	[TestFixture]
	public sealed class RangeTests
	{
		[Test]
		public void Test_Create()
		{
			var array = Range.Create(5).ToArray();
			CollectionAssert.AreEqual(new[] {0, 1, 2, 3, 4}, array);
		}

		[Test]
		public void Generator()
		{
			var array = Range.Create(4, i => i + 1).ToArray();
			CollectionAssert.AreEqual(new[] {1, 2, 3, 4}, array);
		}

		[Test]
		public void Array()
		{
			CollectionAssert.AreEqual(new[] {1, 2, 3, 4}, Range.Array(4, i => i + 1));
		}

		[Test]
		public void Test_Simple_Array()
		{
			CollectionAssert.AreEqual(Range.Array(5), new [] {0,1,2,3,4});
		}

		[Test]
		public void Empty()
		{
			CollectionAssert.IsEmpty(Range.Empty<int>().ToArray());
			Assert.AreSame(Range.Empty<int>(), Range.Empty<int>());
		}
	}
}
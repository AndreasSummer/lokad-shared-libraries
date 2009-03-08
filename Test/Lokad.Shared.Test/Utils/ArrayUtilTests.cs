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
	public sealed class ArrayUtilTests
	{
		[Test]
		public void IsNullOrEmpty_Works()
		{
			Assert.IsTrue(ArrayUtil.IsNullOrEmpty(null));
			Assert.IsTrue(ArrayUtil.IsNullOrEmpty(new int[0]));
			Assert.IsFalse(ArrayUtil.IsNullOrEmpty(Enumerable.Repeat(1, 1).ToArray()));
		}
	}
}
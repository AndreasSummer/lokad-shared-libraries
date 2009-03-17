#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Linq;
using NUnit.Framework;

namespace Lokad
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
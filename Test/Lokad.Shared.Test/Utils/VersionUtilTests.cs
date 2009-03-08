#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace System
{
	[TestFixture]
	public sealed class VersionUtilTests
	{
		[Test]
		public void Test()
		{
			Assert.AreEqual(new Version(1, 0, 0, 0), new Version(1, 0).Normalize());
			Assert.AreEqual(new Version(1, 2, 0, 0), new Version(1, 2).Normalize());
			Assert.AreEqual(new Version(1, 2, 3, 0), new Version(1, 2, 3).Normalize());
			Assert.AreEqual(new Version(1, 2, 3, 4), new Version(1, 2, 3, 4).Normalize());
		}
	}
}
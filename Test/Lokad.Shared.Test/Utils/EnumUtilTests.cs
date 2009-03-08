#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace System
{
	[TestFixture]
	public sealed class EnumUtilTests
	{
		enum Tri
		{
			None,
			True,
			False
		}

		[Test]
		public void Test_Parse()
		{
			Assert.AreEqual(Tri.True, EnumUtil.Parse<Tri>("true"));
			Assert.AreEqual(Tri.False, EnumUtil.Parse<Tri>("False"));
		}

		[Test]
		public void Test_Values()
		{
			Assert.AreEqual(new[] {Tri.None, Tri.True, Tri.False}, EnumUtil<Tri>.Values);
		}
	}
}
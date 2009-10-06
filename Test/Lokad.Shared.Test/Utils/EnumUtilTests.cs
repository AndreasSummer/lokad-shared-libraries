#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class EnumUtilTests
	{
		// ReSharper disable InconsistentNaming

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

		[Test]
		public void ToIdentifier()
		{
			Assert.AreEqual("Tri_None", EnumUtil.ToIdentifier(Tri.None));
		}
	}
}
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
		static readonly Tuple<DateTime, int, double, int, int> _t1 =
			new Tuple<DateTime, int, double, int, int>(DateTime.MinValue, 1, Math.PI, 3, 4);

		static readonly Tuple<DateTime, int, double, int, int> _t2 = Tuple.From(DateTime.MinValue, 1, Math.PI, 3, 4);
		static readonly Tuple<DateTime, int, double, int, int> _t3 = Tuple.From(DateTime.MinValue, 1, Math.PI, 3, 5);

		[Test]
		public void Test5_Equality()
		{
			Assert.AreEqual(_t1, _t2);
			Assert.AreNotEqual(_t1, _t3);
		}

		[Test]
		public void Test5_Operators()
		{
			Assert.IsTrue(_t1 == _t2, "#1");
			Assert.IsTrue(_t1 != _t3, "#2");
		}

		[Test]
		public void Test5_Hash()
		{
			var h1 = _t1.GetHashCode();
			var h2 = _t2.GetHashCode();
			var h3 = _t3.GetHashCode();

			Assert.AreNotEqual(0, h1, "#1");
			Assert.AreNotEqual(0, h3, "#2");
			Assert.AreEqual(h1, h2, "#3");
			Assert.AreNotEqual(h2, h3, "#4");
		}
	}
}
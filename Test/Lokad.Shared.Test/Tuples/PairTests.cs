#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using NUnit.Framework;

namespace System
{
	[TestFixture]
	public sealed class PairTests
	{
		static readonly Tuple<DateTime, int> _t1 = new Tuple<DateTime, int>(DateTime.MinValue, 1);
		static readonly Tuple<DateTime, int> _t2 = Tuple.From(DateTime.MinValue, 1);
		static readonly Pair<DateTime, int> _t3 = Tuple.From(DateTime.Now, 1);


		[Test]
		public void Test_Equality()
		{
			Assert.AreEqual(_t1, _t2);
			Assert.AreNotEqual(_t1, _t3);
		}

		[Test]
		public void Test_Operators()
		{
			Assert.IsTrue(_t1 == _t2, "#1");
			Assert.IsTrue(_t1 != _t3, "#2");
		}

		[Test]
		public void Test_Hash()
		{
			var h1 = _t1.GetHashCode();
			var h2 = _t2.GetHashCode();
			var h3 = _t3.GetHashCode();

			Assert.AreNotEqual(0, h1, "#1");
			Assert.AreNotEqual(0, h3, "#2");
			Assert.AreEqual(h1, h2, "#3");
			Assert.AreNotEqual(h2, h3, "#4");
		}

		[Test]
		public void Test_Fields()
		{
			Assert.AreEqual(_t3.Item1, _t3.Key);
			Assert.AreEqual(_t3.Item2, _t3.Value);
		}
	}
}
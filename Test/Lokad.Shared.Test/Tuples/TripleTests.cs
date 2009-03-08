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
	public sealed class TripleTests
	{
		static readonly Tuple<DateTime, int, double> _t1 = new Tuple<DateTime, int, double>(DateTime.MinValue, 1, Math.PI);
		static readonly Tuple<DateTime, int, double> _t2 = Tuple.From(DateTime.MinValue, 1, Math.PI);
		static readonly Triple<DateTime, int, double> _t3 = Tuple.From(DateTime.MinValue, 1, Math.E);

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
#if !SILVERLIGHT2
		[Test, Ignore]
		public void Test()
		{
			// this test is used as sample only
			var set = new HashSet<Triple<int, bool, bool>>
				{
					Tuple.From(1, true, false),
					Tuple.From(1, true, true)
				};

			Assert.IsTrue(set.Contains(Tuple.From(1, true, false)));
			Assert.IsFalse(set.Contains(Tuple.From(2, true, true)));
		}
#endif
	}
}
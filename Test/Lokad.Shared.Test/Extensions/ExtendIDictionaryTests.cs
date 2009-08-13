#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class ExtendIDictionaryTests
	{
		[Test]
		public void GetValue()
		{
			var d = new Dictionary<int, int>
				{
					{1, 2}
				};
			Assert.AreEqual(2, d.GetValue(1, 0));
			Assert.AreEqual(0, d.GetValue(2, 0));

			Assert.AreEqual(Maybe<int>.Empty, d.GetValue(10));
			Assert.AreEqual(2, d.GetValue(1).Value);
		}
	}
}
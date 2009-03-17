#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Lokad.Data.SqlClient
{
	[TestFixture]
	public sealed class SqlServerGuidComparerTest
	{
		[Test]
		public void Test_Sorting()
		{
			var order = new[] {0, 1, 2, 3, 4, 5, 6, 7, 9, 8, 15, 14, 13, 12, 11, 10};

			var source = new string('0', 32);

			// create guids
			var guids = Range
				.Create(16, () => source.ToCharArray())
				.Apply((a, i) => a[i*2 + 1] = '1')
				.Select((a, i) => Tuple.From(i, new Guid(new string(a))));

			var sorted = guids
				.OrderBy(g => g.Item2, _comparer)
				.Select(g => g.Item1);

			CollectionAssert.AreEqual(order, sorted.ToArray());
		}

		[Test]
		public void Test_Equals()
		{
			Assert.AreEqual(0, _comparer.Compare(Guid.Empty, Guid.Empty));
			var guid = Guid.NewGuid();
			Assert.AreEqual(0, _comparer.Compare(guid, guid));
		}

		static readonly IComparer<Guid> _comparer = new SqlServerGuidComparer();
	}
}
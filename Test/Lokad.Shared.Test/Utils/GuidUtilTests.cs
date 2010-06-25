using System;
using System.Collections.Generic;
using Lokad.Data.SqlClient;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class GuidUtilTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void Combs_are_SQL_Ordered()
		{
			var initial = Range.Array(50, n =>
				{
					SystemUtil.Sleep(20.Milliseconds());
					return GuidUtil.NewComb();
				});

			var copy = new List<Guid>(initial);
			copy.Sort(new SqlServerGuidComparer());
			CollectionAssert.AreEqual(initial, copy);
		}

		[Test]
		public void Sortables_are_string_ordered()
		{
			var initial = Range.Array(1000, n =>
				{
					SystemUtil.Sleep(20.Milliseconds());
					return GuidUtil.NewStringSortable().ToString();
				});

			var copy = new List<String>(initial);
			copy.Sort();
			CollectionAssert.AreEqual(initial, copy);
		}
	}
}
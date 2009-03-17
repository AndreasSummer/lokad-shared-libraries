#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using Lokad.Diagnostics.CodeAnalysis;

namespace Lokad.Data.SqlClient
{
	/// <summary>
	/// This class compares two guids according to the SQL server ordering rules.
	/// </summary>
	[Serializable]
	[Immutable]
	public sealed class SqlServerGuidComparer : IComparer<Guid>
	{
		/// <summary>
		/// Singleton instance of the <see cref="SqlServerGuidComparer"/>
		/// </summary>
		public static readonly IComparer<Guid> Instance = new SqlServerGuidComparer();

		static readonly int[] _importance = new[] {3, 2, 1, 0, 5, 4, 7, 6, 9, 8, 15, 14, 13, 12, 11, 10};

		/// <summary>
		/// Compares two guids and returns a value indicating whether one is less than, equal to, or greater than the other.
		/// </summary>
		/// <param name="x">The first guid to compare.</param>
		/// <param name="y">The second guid to compare.</param>
		/// <returns>
		/// Value indicating relation between x and y
		/// </returns>
		public int Compare(Guid x, Guid y)
		{
			var a = x.ToByteArray();
			var b = y.ToByteArray();

			for (int i = _importance.Length - 1; i >= 0; i--)
			{
				var compare = _importance[i];
				var c = a[compare].CompareTo(b[compare]);
				if (c != 0)
				{
					return c;
				}
			}
			return 0;
		}
	}
}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if SILVERLIGHT2

using System.Globalization;

namespace System
{
	/// <summary>
	/// Extends string for the silverlight compatibility
	/// </summary>
	public static class ExtendString
	{
		/// <summary>
		/// Converts string to uppercase, using the invariant culture.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns>string in the upper case</returns>
		public static string ToUpperInvariant(this string source)
		{
			return source.ToUpper(CultureInfo.InvariantCulture);
		}
	}
}

#endif
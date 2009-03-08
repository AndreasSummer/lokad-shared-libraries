#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion
using System;
using System.Linq;

namespace Lokad.Api
{
	static class DebuggingExtensions
	{
		public static string ToDebug(this SegmentForSerie[] segments)
		{
			return string.Format("[segments: {0}, values: {1}]", segments.Length, segments.Sum(s => s.Values.Length));
		}

		public static string ToDebug(this Guid guid)
		{
			return guid.ToString().Substring(0, 8);
		}

		public static string ToDebug(this SerieSegmentPage page)
		{
			Enforce.Argument(() => page);
			return string.Format("[cursor1: {0}, more:{1}, values: {2}]",
				page.Cursor.Cursor1.ToDebug(),
				page.ThereAreMorePages ? 1 : 0,
				page.Segments.ToDebug());
		}
	}
}
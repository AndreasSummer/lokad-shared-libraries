#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Xml.Serialization;

namespace Lokad.Api
{
	/// <summary>
	/// Collection of value segments for the series and
	/// the associated paging information
	/// </summary>
	[Serializable]
	public sealed class SerieSegmentPage
	{
		/// <summary>
		/// Cursor for paging across large value sets
		/// </summary>
		/// <value>The cursor.</value>
		public SegmentCursor Cursor { get; set; }

		/// <summary>
		/// Gets or sets the value segments associated with this page.
		/// </summary>
		/// <value>The segments.</value>
		public SegmentForSerie[] Segments { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether there are more pages.
		/// </summary>
		/// <value><c>true</c> if there are more pages; otherwise, <c>false</c>.</value>
		[XmlAttribute]
		public bool ThereAreMorePages { get; set; }
	}
}
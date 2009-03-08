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
	/// Helper class used for paging across large value sets
	/// </summary>
	[Serializable]
	public sealed class SegmentCursor
	{
		/// <summary>
		/// Primary cursor.
		/// </summary>
		/// <value>The cursor1.</value>
		/// <remarks>This field is populated by the Lokad framework 
		/// and does not need to be changed or accessed</remarks>
		[XmlAttribute]
		public Guid Cursor1 { get; set; }

		/// <summary>
		/// Secondary cursor
		/// </summary>
		/// <value>The cursor2.</value>
		/// <remarks>This field is populated by the Lokad framework 
		/// and does not need to be changed or accessed</remarks>
		[XmlAttribute]
		public DateTime Cursor2 { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SegmentCursor"/> class.
		/// </summary>
		public SegmentCursor()
		{
			Cursor1 = Guid.Empty;
			Cursor2 = DateTime.MinValue;
		}
	}
}
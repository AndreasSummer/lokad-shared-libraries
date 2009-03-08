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
	/// Collection of series along with the associated information for paging
	/// </summary>
	[Serializable]
	public class SerieInfoPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SerieInfoPage"/> class.
		/// </summary>
		public SerieInfoPage()
		{
			Cursor = Guid.Empty;
			Series = new SerieInfo[0];
		}

		/// <summary>
		/// Paging cursor
		/// </summary>
		/// <value>The cursor.</value>
		[XmlAttribute]
		public Guid Cursor { get; set; }

		/// <summary>
		/// Series that are associated with this page
		/// </summary>
		/// <value>The series.</value>
		public SerieInfo[] Series { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether there are more pages.
		/// </summary>
		/// <value><c>true</c> if there are more pages; otherwise, <c>false</c>.</value>
		[XmlAttribute]
		public bool ThereAreMorePages { get; set; }
	}
}
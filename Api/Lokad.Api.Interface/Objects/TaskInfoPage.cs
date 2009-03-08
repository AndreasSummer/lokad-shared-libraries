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
	/// Collection of <see cref="TaskInfo"/> retrieved by paging
	/// </summary>
	[Serializable]
	public class TaskInfoPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskInfoPage"/> class.
		/// </summary>
		public TaskInfoPage()
		{
			Cursor = Guid.Empty;
		}

		/// <summary>
		/// Cursor that identifies this page.
		/// </summary>
		/// <value>The cursor.</value>
		[XmlAttribute]
		public Guid Cursor { get; set; }

		/// <summary>
		/// Tasks associated with this page
		/// </summary>
		/// <value>The tasks.</value>
		public TaskInfo[] Tasks { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether there are more pages.
		/// </summary>
		/// <value><c>true</c> if there are more pages; otherwise, <c>false</c>.</value>
		[XmlAttribute]
		public bool ThereAreMorePages { get; set; }
	}
}
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
	/// Information about Lokad account
	/// </summary>
	[Serializable]
	public class AccountInfo
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		[XmlAttribute]
		public Guid AccountID { get; set; }

		/// <summary>
		/// Human-readable unique identifier
		/// </summary>
		[XmlAttribute]
		public long AccountHRID { get; set; }

		/// <summary>
		/// Gets or sets the serie count for the account.
		/// </summary>
		/// <value>The serie count for the account.</value>
		/// <remarks>Introduced in version 2.0.2.0</remarks>
		[XmlAttribute]
		public int SerieCount { get; set; }
	}
}
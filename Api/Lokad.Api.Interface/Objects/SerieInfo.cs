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
	/// Basic information about the serie.
	/// </summary>
	[Serializable]
	public class SerieInfo
	{
		/// <summary>
		/// Unique identifier for the serie
		/// </summary>
		/// <value>The serie ID.</value>
		[XmlAttribute]
		public Guid SerieID { get; set; }

		/// <summary>
		/// Gets or sets the name for the serie.
		/// </summary>
		/// <value>The name.</value>
		[XmlAttribute]
		public string Name { get; set; }
	}
}
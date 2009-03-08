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
	/// Collection of tags associated with the serie
	/// </summary>
	[Serializable]
	public class TagsForSerie
	{
		/// <summary>
		/// Unique identifier for the serie.
		/// </summary>
		/// <value>The serie ID.</value>
		[XmlAttribute]
		public Guid SerieID { get; set; }

		/// <summary>
		/// Collection of tags that provide additional
		/// meta-information for the serie
		/// </summary>
		/// <value>The tags.</value>
		public string[] Tags { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TagsForSerie"/> class.
		/// </summary>
		public TagsForSerie()
		{
			Tags = new string[0];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TagsForSerie"/> class
		/// that is associated with the specified <paramref name="serie"/>.
		/// </summary>
		/// <param name="serie">The serie to link to.</param>
		/// <param name="tags">The tags.</param>
		public TagsForSerie(SerieInfo serie, string[] tags)
		{
			SerieID = serie.SerieID;
			Tags = tags;
		}
	}
}
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
	/// Collection of values that represent part 
	/// of the time serie
	/// </summary>
	[Serializable]
	public class SegmentForSerie
	{
		/// <summary>
		/// Unique identifier for the serie
		/// </summary>
		/// <value>The serie ID.</value>
		[XmlAttribute]
		public Guid SerieID { get; set; }

		/// <summary>
		/// Time values that represent entire time serie
		/// or its part
		/// </summary>
		/// <value>The values.</value>
		public TimeValue[] Values { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SegmentForSerie"/> class.
		/// </summary>
		public SegmentForSerie()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SegmentForSerie"/> class.
		/// that is associated with the specified <paramref name="serie"/>.
		/// </summary>
		/// <param name="serie">The serie to associate with.</param>
		/// <param name="values">The values.</param>
		public SegmentForSerie(SerieInfo serie, TimeValue[] values)
		{
			SerieID = serie.SerieID;
			Values = values;
		}
	}
}
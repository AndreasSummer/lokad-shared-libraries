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
	/// Single value point for a time serie.
	/// </summary>
	[Serializable]
	public struct TimeValue
	{
		/// <summary>
		/// Time associated with the value point.
		/// </summary>
		/// <value>The time.</value>
		[XmlAttribute]
		public DateTime Time { get; set; }

		/// <summary>
		/// Value that represents this object.
		/// </summary>
		/// <value>The value.</value>
		[XmlAttribute]
		public double Value { get; set; }

		/// <summary>
		/// Creates new <see cref="TimeValue"/> out of the provided
		/// <paramref name="time"/> and <paramref name="value"/>
		/// </summary>
		/// <param name="time">The time.</param>
		/// <param name="value">The value.</param>
		/// <returns>new instance of <see cref="TimeValue"/></returns>
		public static TimeValue From(DateTime time, double value)
		{
			return new TimeValue
				{
					Time = time,
					Value = value
				};
		}
	}
}
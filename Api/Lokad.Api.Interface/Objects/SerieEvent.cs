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
	/// Event that could be associated with the <see cref="SerieInfo"/>
	/// </summary>
	[Serializable]
	public class SerieEvent
	{
		/// <summary>
		/// Time at which the segment occurs or starts
		/// </summary>
		/// <value>The time.</value>
		[XmlAttribute]
		public DateTime Time { get; set; }

		/// <summary> Duration of the event in days </summary>
		/// <remarks> Use <see cref="TimeSpan.TotalDays"/> and <see cref="TimeSpan.FromDays"/>
		/// for converting to and from this value in .NET </remarks>
		/// <value>Duration of the event in days.</value>
		[XmlAttribute]
		public double DurationDays { get; set; }

		/// <summary> Decription of the event </summary>
		/// <value>The name.</value>
		[XmlAttribute]
		public string Name { get; set; }

		/// <summary> Indicates the time, since that this event is known. </summary>
		/// <value>The time, since that this event is known.</value>
		/// <remarks><para>Default value (<see cref="DateTime.MinValue"/>) means that event
		/// has been known long before it has happened.</para>
		/// <para>Generaly you would leave this value as it is.</para> </remarks>
		[XmlAttribute]
		public DateTime KnownSince { get; set; }
	}
}
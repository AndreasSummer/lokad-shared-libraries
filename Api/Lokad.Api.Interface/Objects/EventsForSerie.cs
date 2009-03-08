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
	/// Collection of events for a given serie
	/// </summary>
	[Serializable]
	public class EventsForSerie
	{
		/// <summary>
		/// Gets or sets the serie ID.
		/// </summary>
		/// <value>The serie ID.</value>
		[XmlAttribute]
		public Guid SerieID { get; set; }
		/// <summary>
		/// Gets or sets the events.
		/// </summary>
		/// <value>The events.</value>
		public SerieEvent[] Events { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EventsForSerie"/> class.
		/// </summary>
		public EventsForSerie()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EventsForSerie"/> class
		/// associated with the specified <paramref name="serie"/>.
		/// </summary>
		/// <param name="serie">The serie to associate with.</param>
		/// <param name="events">The events.</param>
		public EventsForSerie(SerieInfo serie, SerieEvent[] events)
		{
			SerieID = serie.SerieID;
			Events = events;
		}
	}
}
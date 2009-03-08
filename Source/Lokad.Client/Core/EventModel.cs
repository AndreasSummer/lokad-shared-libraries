#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Lokad.Client
{
	/// <summary>
	/// Model class for Lokad Events
	/// </summary>
	[Immutable]
	public sealed class EventModel
	{
		/// <summary>
		/// Gets the date that this event is known since (can be MinValue)
		/// </summary>
		/// <value>Date this event is known since.</value>
		public DateTime KnownSince
		{
			get { return _knownSince; }
		}

		/// <summary>
		/// Gets the start date that this event is bound to
		/// </summary>
		/// <value>Date this event is bound to.</value>
		public DateTime Starts
		{
			get { return _starts; }
		}

		/// <summary>
		/// Name of the event
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Duration of the event
		/// </summary>
		/// <value>The duration.</value>
		public TimeSpan Duration
		{
			get { return _duration; }
		}

		/// <summary>
		/// Gets the duration of the event expressed in human-readable form (for data-binding)
		/// </summary>
		/// <value>The duration of the event in human-readable form.</value>
		public string DurationAsString
		{
			get { return string.Format(CultureInfo.CurrentUICulture, "{0} day(s)", _duration.TotalDays.Round(4)); }
		}

		readonly DateTime _starts;
		readonly string _name;
		readonly TimeSpan _duration;
		readonly DateTime _knownSince;

		/// <summary>
		/// Initializes a new instance of the <see cref="EventModel"/> class.
		/// </summary>
		/// <param name="starts">The start date of the event.</param>
		/// <param name="name">The name of the event.</param>
		/// <param name="duration">The duration of the event.</param>
		/// <param name="knownSince">The time that this event is known since.</param>
		public EventModel(DateTime starts, string name, double duration, DateTime knownSince)
		{
			_starts = starts;
			_knownSince = knownSince;
			_name = name;
			_duration = TimeSpan.FromDays(duration);
		}

		/// <summary>
		/// Converts this event to tuple for equality checks and testing purposes
		/// </summary>
		/// <returns>tuple represeting this event</returns>
		public Quad<string, TimeSpan, DateTime, DateTime> ToTuple()
		{
			return Tuple.From(Name.ToLowerInvariant(), Duration, Starts, KnownSince);
		}
	}
}
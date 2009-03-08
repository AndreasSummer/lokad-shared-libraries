#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Diagnostics;

namespace Lokad.Api
{
	/// <summary>
	/// Strongly-typed counters for <see cref="ITimeSerieApi"/> 
	/// as hosted by the <see cref="TimeSerieApiDecorator"/>
	/// </summary>
	sealed class TimeSerieApiCounters : ExecutionCounterGroup<ITimeSerieApi>
	{
		internal readonly ExecutionCounter Ctor;
		internal readonly ExecutionCounter AddSeries;
		internal readonly ExecutionCounter DeleteSeries;
		internal readonly ExecutionCounter GetSeries;
		internal readonly ExecutionCounter UpdateSerieSegments;
		internal readonly ExecutionCounter SetTags;
		internal readonly ExecutionCounter SetEvents;
		internal readonly ExecutionCounter GetSerieSegments;
		internal readonly ExecutionCounter GetTags;
		internal readonly ExecutionCounter GetEvents;

		public TimeSerieApiCounters()
		{
			Ctor = CreateCounterForCtor(() => new TimeSerieApiDecorator(null, null, null, null), 0, 0);

			AddSeries = CreateCounter(i => i.AddSeries(null, null), 1, 0);
			DeleteSeries = CreateCounter(i => i.DeleteSeries(null, null), 1, 0);
			GetSeries = CreateCounter(i => i.GetSeries(null, Guid.Empty, 0), 1, 1);
			UpdateSerieSegments = CreateCounter(i => i.UpdateSerieSegments(null, null), 2, 0);
			SetTags = CreateCounter(i => i.SetTags(null, null), 2, 0);
			SetEvents = CreateCounter(i => i.SetEvents(null, null), 2, 0);
			GetSerieSegments = CreateCounter(i => i.GetSerieSegments(null, null, null, 0), 2, 2);
			GetTags = CreateCounter(i => i.GetTags(null, null), 1, 2);
			GetEvents = CreateCounter(i => i.GetEvents(null, null), 1, 2);

			Register();
		}
	}
}
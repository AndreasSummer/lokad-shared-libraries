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
	/// Strongly-typed counters for the <see cref="IForecastApi"/>
	/// </summary>
	sealed class ForecastApiCounters : ExecutionCounterGroup<IForecastApi>
	{
		internal readonly ExecutionCounter Ctor;
		internal readonly ExecutionCounter AddTasks;
		internal readonly ExecutionCounter GetTasks;
		internal readonly ExecutionCounter UpdateTasks;
		internal readonly ExecutionCounter DeleteTasks;
		internal readonly ExecutionCounter GetForecasts;
		internal readonly ExecutionCounter GetTasksBySerieID;

		public ForecastApiCounters()
		{
			Ctor = CreateCounterForCtor(() => new ForecastApiDecorator(null,null,null,null), 0, 0);

			AddTasks = CreateCounter(i => i.AddTasks(null, null), 1, 0);
			GetTasks = CreateCounter(i => i.GetTasks(null, Guid.Empty, 0), 1, 1);
			UpdateTasks = CreateCounter(i => i.UpdateTasks(null, null), 1, 0);
			DeleteTasks = CreateCounter(i => i.DeleteTasks(null, null), 1, 0);
			GetForecasts = CreateCounter(i => i.GetForecasts(null, null), 1, 1);
			GetTasksBySerieID = CreateCounter(i => i.GetTasksBySerieIDs(null, null), 1, 1);

			Register();
		}
	}
}
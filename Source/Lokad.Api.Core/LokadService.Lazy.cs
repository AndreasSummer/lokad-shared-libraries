#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lokad.Api
{
	// Paging methods
	partial class LokadService : ILazyLokadService
	{
		IEnumerable<SerieInfo> ILazyLokadService.GetSeries()
		{
			return _series.GetSerieEnumerator(_identity);
		}

		IEnumerable<SegmentForSerie> ILazyLokadService.GetSerieSegments(IEnumerable<SerieInfo> series)
		{
			Enforce.Argument(() => series);
			return _series.GetSerieSegmentEnumerator(_identity, series.Select(s => s.SerieID));
		}

		IEnumerable<EventsForSerie> ILazyLokadService.GetEvents(IEnumerable<SerieInfo> series)
		{
			Enforce.Argument(() => series);
			return _series.GetEventsEnumerator(_identity, series.Select(s => s.SerieID));
		}

		IEnumerable<TagsForSerie> ILazyLokadService.GetTags(IEnumerable<SerieInfo> series)
		{
			Enforce.Argument(() => series);
			return _series.GetTagEnumerator(_identity, series.Select(s => s.SerieID));
		}

		IEnumerable<Forecast> ILazyLokadService.GetForecasts(IEnumerable<TaskInfo> tasks)
		{
			Enforce.Argument(() => tasks);
			return _forecasts.GetForecastEnumerator(_identity, tasks.Select(t => t.TaskID));
		}

		IEnumerable<TaskInfo> ILazyLokadService.GetTasks()
		{
			return _forecasts.GetTaskEnumerator(_identity);
		}

		IEnumerable<TaskInfo> ILazyLokadService.GetTasks(IEnumerable<SerieInfo> series)
		{
			Enforce.Argument(() => series);
			return _forecasts.GetTaskEnumerator(_identity, series.Select(s => s.SerieID));
		}

		IEnumerable<SerieInfo> ILazyLokadService.GetSeriesWithPrefix(string prefix)
		{
			Enforce.ArgumentNotEmpty(() => prefix);

			return _legacy.GetSerieEnumerator(_identity, prefix)
				.Select(s => new SerieInfo
				{
					SerieID = s.SerieID,
					Name = s.Name.Remove(0, prefix.Length)
				});
		}
	}
}
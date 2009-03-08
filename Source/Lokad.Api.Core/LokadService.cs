#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Lokad.Api
{
	[Immutable]
	sealed partial class LokadService : ILokadService
	{
		readonly IAccountApi _accounts;
		readonly IForecastApi _forecasts;
		readonly Identity _identity;
		readonly ITimeSerieApi _series;
		readonly ILegacyApi _legacy;
		readonly ISystemApi _system;

		internal LokadService(Identity identity, ITimeSerieApi series, IForecastApi forecasts,
			IAccountApi accounts, ILegacyApi legacy, ISystemApi system)
		{
			Enforce.Argument(() => identity, ApiRules.Identity);
			Enforce.Arguments(
				() => series,
				() => forecasts,
				() => accounts,
				() => legacy,
				() => system);

			_identity = identity;
			_legacy = legacy;
			_series = series;
			_forecasts = forecasts;
			_accounts = accounts;
			_system = system;
			_lazy = this;
		}

		AccountInfo ILokadService.GetAccountInfo()
		{
			return _accounts.GetAccountInfo(_identity);
		}

		void ILokadService.SetPartner(long partnerHRID)
		{
			_accounts.SetPartner(_identity, partnerHRID);
		}

		void ILokadService.AddSeries(IEnumerable<SerieInfo> series)
		{
			Enforce.Argument(() => series);
			_series.AddSeriesInBatch(_identity, series);
		}

		SerieInfo[] ILokadService.GetSeries()
		{
			return _lazy.GetSeries().ToArray();
		}

		void ILokadService.AddSeriesWithPrefix(IEnumerable<SerieInfo> series, string prefix)
		{
			Enforce.Argument(() => series);
			Enforce.ArgumentNotEmpty(() => prefix);

			_series.AddSeriesInBatchWithPrefix(_identity, series, prefix);
		}

		SerieInfo[] ILokadService.GetSeriesWithPrefix(string prefix)
		{
			Enforce.ArgumentNotEmpty(() => prefix);
			return _lazy.GetSeriesWithPrefix(prefix).ToArray();
		}

		void ILokadService.DeleteSeries(IEnumerable<SerieInfo> series)
		{
			Enforce.Argument(() => series);
			_series.DeleteSeriesInBatch(_identity, series.Select(s => s.SerieID));
		}

		void ILokadService.SetTags(IEnumerable<TagsForSerie> tagsForSerie)
		{
			Enforce.Argument(() => tagsForSerie);
			_series.SetTagsInBatch(_identity, tagsForSerie);
		}

		TagsForSerie[] ILokadService.GetTags(IEnumerable<SerieInfo> series)
		{
			Enforce.Argument(() => series);
			return _lazy.GetTags(series).ToArray();
		}

		EventsForSerie[] ILokadService.GetEvents(IEnumerable<SerieInfo> series)
		{
			Enforce.Argument(() => series);
			return _lazy.GetEvents(series).ToArray();
		}

		void ILokadService.SetEvents(IEnumerable<EventsForSerie> eventsForSerie)
		{
			Enforce.Argument(() => eventsForSerie);
			_series.SetEventsInBatch(_identity, eventsForSerie);
		}

		void ILokadService.UpdateSerieSegments(IEnumerable<SegmentForSerie> segments)
		{
			Enforce.Argument(() => segments);
			_series.UpdateSerieSegmentsInBatch(_identity, segments);
		}

		SegmentForSerie[] ILokadService.GetSerieSegments(IEnumerable<SerieInfo> series)
		{
			Enforce.Argument(() => series);
			return _lazy.GetSerieSegments(series).ToArray();
		}
		

		TaskInfo[] ILokadService.GetTasks()
		{
			return _lazy.GetTasks().ToArray();
		}

		TaskInfo[] ILokadService.GetTasks(IEnumerable<SerieInfo> series)
		{
			Enforce.Argument(() => series);
			return _lazy.GetTasks(series).ToArray();
		}

		Forecast[] ILokadService.GetForecasts(IEnumerable<TaskInfo> tasks)
		{
			Enforce.Argument(() => tasks);
			return _lazy.GetForecasts(tasks).ToArray();
		}

		void ILokadService.AddTasks(IEnumerable<TaskInfo> tasks)
		{
			Enforce.Argument(() => tasks);
			_forecasts.AddTasksInBatch(_identity, tasks);
		}

		void ILokadService.DeleteTasks(IEnumerable<TaskInfo> tasks)
		{
			Enforce.Argument(() => tasks);
			_forecasts.DeleteTasksInBatch(_identity, tasks.Select(t => t.TaskID));
		}

		void ILokadService.UpdateTasks(IEnumerable<TaskInfo> tasks)
		{
			Enforce.Argument(() => tasks);
			_forecasts.UpdateTasksInBatch(_identity, tasks);
		}

		Guid ILokadService.ReportIssue(Report report)
		{
			Enforce.Argument(() => report);
			return _system.AddReport(_identity, report);
		}

		readonly ILazyLokadService _lazy;

		ILazyLokadService ILokadService.Lazy
		{
			get { return this; }
		}
	}
}
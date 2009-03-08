using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Lokad.Api
{
	using Limits = LokadApiRequestLimits;

	/// <summary>
	/// Extensions for the <see cref="ILokadApi"/>
	/// </summary>
	public static class ILokadApiExtensions
	{
		/// <summary>
		/// Retrieves tags for the specified series.
		/// </summary>
		/// <param name="self">The series API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="serieIDs">The IDs of series to retrieve tags for.</param>
		/// <returns>enumerator over tag collections</returns>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		public static IEnumerable<TagsForSerie> GetTagEnumerator(this ITimeSerieApi self, Identity identity, IEnumerable<Guid> serieIDs)
		{
			Enforce.Arguments(() => serieIDs, () => identity);

			foreach (var slice in serieIDs.Slice(Limits.GetTags_Series))
			{
				foreach (var result in self.GetTags(identity, slice))
				{
					yield return result;
				}
			}
		}

		/// <summary>
		/// Updates the series with the provided segments.
		/// Values must be ordered by <see cref="TimeValue.Time"/>
		/// </summary>
		/// <param name="series">The series API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="segments">The segments.</param>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		public static void UpdateSerieSegmentsInBatch(this ITimeSerieApi series, Identity identity, IEnumerable<SegmentForSerie> segments)
		{
			Enforce.Arguments(() => segments, () => identity, () => series);

			var compressed = segments.Select(c => CompressSegment(c));
			var slices = UpdateSegmentStream(compressed)
				.Slice(Limits.UpdateSerieSegments_Segments,
					i => i.Values.Length,
					Limits.UpdateSerieSegments_Values);

			foreach (var slice in slices)
			{
				series.UpdateSerieSegments(identity, slice);
			}
		}

		internal static SegmentForSerie CompressSegment(SegmentForSerie segment)
		{
			var values = segment.Values;
			var length = values.Length;

			// there is nothing to compress
			if (length <= 2)
				return segment;

			// we do not touch boundaries and delete 0s everywhere else
			return new SegmentForSerie
				{
					SerieID = segment.SerieID,
					Values = values
						.Where((v, i) => (i == 0) || (i == (length - 1)) || v.Value != 0)
						.ToArray()
				};
		}

		static IEnumerable<SegmentForSerie> UpdateSegmentStream(IEnumerable<SegmentForSerie> segments)
		{
			// breaks down the incoming stream into pieces
			foreach (var segment in segments)
			{
				if (segment.Values.Length < Limits.UpdateSerieSegments_Values)
				{
					yield return segment;
					continue;
				}

				var valueSlices = segment.Values.Slice(Limits.UpdateSerieSegments_Values);
				foreach (var slice in valueSlices)
				{
					yield return new SegmentForSerie
					{
						SerieID = segment.SerieID,
						Values = slice
					};
				}
			}
		}

		static IEnumerable<SegmentForSerie> GetSerieSegmentsStream(ITimeSerieApi series, Identity identity, IEnumerable<Guid[]> slices)
		{
			foreach (var slice in slices)
			{
				foreach (var segment in GetSerieSegmentsStream(series, identity, slice))
				{
					yield return segment;
				}
			}
		}

		static IEnumerable<SegmentForSerie> GetSerieSegmentsStream(ITimeSerieApi series, Identity identity, Guid[] serieIDs)
		{
			var cursor = new SegmentCursor();

			while (true)
			{
				var page = series.GetSerieSegments(identity, serieIDs, cursor, Limits.GetSerieSegments_RecommendedPage);

				cursor = page.Cursor;

				foreach (var segment in page.Segments)
				{
					yield return segment;
				}

				if (!page.ThereAreMorePages)
					yield break;
			}
		}

		static IEnumerable<SegmentForSerie> Aggregate(IEnumerable<SegmentForSerie> stream)
		{
			var segments = new List<SegmentForSerie>(3);

			foreach (var segment in stream)
			{
				if (segments.IsEmpty() || segments[0].SerieID == segment.SerieID)
				{
					segments.Add(segment);
					continue;
				}

				yield return new SegmentForSerie
				{
					SerieID = segments[0].SerieID,
					Values = segments.SelectMany(s => s.Values).ToArray()
				};

				segments.Clear();
				segments.Add(segment);
			}
			if (!segments.IsEmpty())
			{
				yield return new SegmentForSerie
				{
					SerieID = segments[0].SerieID,
					Values = segments.SelectMany(s => s.Values).ToArray()
				};
			}
		}

		/// <summary> Retrieves the values for the specified series </summary>
		/// <param name="series">The series API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="serieIDs">The IDs of the series.</param>
		/// <returns>enumerator of the values for each serie</returns>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="ITimeSerieApi.GetSerieSegments"/>
		public static IEnumerable<SegmentForSerie> GetSerieSegmentEnumerator(this ITimeSerieApi series, Identity identity,  IEnumerable<Guid> serieIDs)
		{
			Enforce.Arguments(() => series, () => identity, () => serieIDs);

			var slices = serieIDs.Slice(Limits.GetSegments_Series);
			var stream = GetSerieSegmentsStream(series, identity, slices);

			return Aggregate(stream);
		}


		/// <summary> Sets the tags for the specified series, overwriting them. </summary>
		/// <param name="series">The series API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="tagsForSerie">The tags for serie.</param>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="ITimeSerieApi.SetTags"/>
		public static void SetTagsInBatch(this ITimeSerieApi series, Identity identity, IEnumerable<TagsForSerie> tagsForSerie)
		{
			Enforce.Arguments(() => series, () => identity, () => tagsForSerie);

			var slices = tagsForSerie.Slice(
				Limits.SetTags_Series,
				tfs => tfs.Tags.Length,
				Limits.SetTags_TagsPerRequest);

			foreach (var slice in slices)
			{
				series.SetTags(identity, slice.ToArray());
			}
		}


		/// <summary> Deletes the series from the Lokad account. </summary>
		/// <param name="series">The series API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="serieIDs">IDs of the series to delete</param>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="ITimeSerieApi.DeleteSeries"/>
		public static void DeleteSeriesInBatch(this ITimeSerieApi series, Identity identity, IEnumerable<Guid> serieIDs)
		{
			Enforce.Arguments(() => series, () => identity, () => serieIDs);

			foreach (var slice in serieIDs.Slice(Limits.DeleteSeries))
			{
				series.DeleteSeries(identity, slice.ToArray());
			}
		}

		/// <summary>
		/// Deletes the tasks from the Lokad account
		/// </summary>
		/// <param name="api">The forecasting API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="taskIDs">IDs of the tasks to delete.</param>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="IForecastApi.DeleteTasks"/>
		public static void DeleteTasksInBatch(this IForecastApi api, Identity identity, IEnumerable<Guid> taskIDs)
		{
			Enforce.Arguments(() => api, () => identity, () => taskIDs);
		
			foreach (var batch in taskIDs.Slice(Limits.DeleteTasks))
			{
				api.DeleteTasks(identity, batch);
			}
		}


		/// <summary>
		/// Retrieves details of all series within the current account.
		/// </summary>
		/// <param name="series">The series API.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>enumerator with the serie information</returns>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="ITimeSerieApi.GetSeries"/>
		public static IEnumerable<SerieInfo> GetSerieEnumerator(this ITimeSerieApi series, Identity identity)
		{
			Enforce.Arguments(() => series, () => identity);

			var cursor = Guid.Empty;

			while (true)
			{
				var page = series.GetSeries(identity, cursor, Limits.GetSeries_RecommendedPage);

				cursor = page.Cursor;

				foreach (var task in page.Series)
				{
					yield return task;
				}

				if (!page.ThereAreMorePages)
					yield break;
			}
		}

		/// <summary>
		/// Enumerates series in the repository based on the serie prefix
		/// </summary>
		/// <param name="api">The API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns></returns>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="ILegacyApi.GetSeriesByPrefix"/>
		[NotNull]
		public static IEnumerable<SerieInfo> GetSerieEnumerator([NotNull]this ILegacyApi api, [NotNull]Identity identity, string prefix)
		{
			Enforce.Arguments(() => api, () => identity);
			Enforce.ArgumentNotEmpty(() => prefix);

			var cursor = Guid.Empty;

			while (true)
			{
				var page = api.GetSeriesByPrefix(identity, prefix, cursor,  Limits.GetSeriesByPrefix_RecommendedPage);

				cursor = page.Cursor;

				foreach (var task in page.Series)
				{
					yield return task;
				}

				if (!page.ThereAreMorePages)
					yield break;
			}
		}

		///// <summary> Enumerates series in the repository based on their names </summary>
		///// <param name="api">The API.</param>
		///// <param name="identity">The identity.</param>
		///// <param name="serieNames">The serie names.</param>
		///// <returns></returns>
		///// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		///// <seealso cref="ILegacyApi.GetSeriesByNames"/>
		//[NotNull]
		//public static IEnumerable<SerieInfo> GetSerieEnumerator([NotNull]this ILegacyApi api, [NotNull]Identity identity, IEnumerable<string> serieNames)
		//{
		//    Enforce.Argument(identity, "identity");
		//    Enforce.Argument(api, "api");

		//    foreach (var slice in serieNames.Slice(Limits.GetSeriesLegacy))
		//    {
		//        foreach (var info in api.GetSeriesByNames(identity, slice))
		//        {
		//            yield return info;
		//        }
		//    }
		//}

		/// <summary>
		/// Adds series to the Lokad account. Serie identifiers are updated properly
		/// </summary>
		/// <param name="api">The series API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="series">series to add to the account, they must have unique names</param>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="ITimeSerieApi.AddSeries"/>
		public static void AddSeriesInBatch(this ITimeSerieApi api, Identity identity, IEnumerable<SerieInfo> series)
		{
			Enforce.Arguments(() => api, () => identity, () => series);

			foreach (var infos in series.Slice(Limits.AddSeries))
			{
				var localCopy = infos;
				api
					.AddSeries(identity, infos)
					.ForEach((id, i) => localCopy[i].SerieID = id);
			}
		}

		/// <summary>
		/// Adds <paramref name="series"/> to the Lokad account, while prefixing them with the series.
		/// Serie identifiers are updated properly.
		/// </summary>
		/// <param name="api">The series API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="series">series to add to the account, they must have unique names</param>
		/// <param name="prefix">The prefix.</param>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="ITimeSerieApi.AddSeries"/>
		public static void AddSeriesInBatchWithPrefix(this ITimeSerieApi api, Identity identity, IEnumerable<SerieInfo> series, string prefix)
		{
			Enforce.Arguments(() => api, () => identity, () => series);
			Enforce.ArgumentNotEmpty(() => prefix);

			foreach (var infos in series.Slice(Limits.AddSeries))
			{
				var localCopy = infos;
				api
					.AddSeries(identity, infos.Convert(s => new SerieInfo { Name = prefix + s.Name }))
					.ForEach((id, i) => localCopy[i].SerieID = id);
			}
		}


		/// <summary>
		/// Sets events for the specified series (overwriting them)
		/// </summary>
		/// <param name="api">The API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="eventsForSerie">The events for serie.</param>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="ITimeSerieApi.SetEvents"/>
		public static void SetEventsInBatch(this ITimeSerieApi api, Identity identity, IEnumerable<EventsForSerie> eventsForSerie)
		{
			Enforce.Arguments(() => api, () => identity, () => eventsForSerie);

			var slices = eventsForSerie.Slice(
				Limits.SetEvents_Series,
				i => i.Events.Length,
				Limits.SetEvents_EventsPerRequest);

			foreach (var slice in slices)
			{
				api.SetEvents(identity, slice);
			}
		}


		/// <summary>
		/// Retrieves events for the specified series.
		/// </summary>
		/// <param name="api">The API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="serieIDs">The IDs of series to retrieve events for.</param>
		/// <returns>enumerator of the events</returns>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="ITimeSerieApi.GetEvents"/>
		public static IEnumerable<EventsForSerie> GetEventsEnumerator(this ITimeSerieApi api, Identity identity, IEnumerable<Guid> serieIDs)
		{
			Enforce.Arguments(() => api, () => identity, () => serieIDs);

			foreach (var slice in serieIDs.Slice(Limits.GetEvents_Series))
			{
				foreach (var result in api.GetEvents(identity, slice))
				{
					yield return result;
				}
			}
		}


		/// <summary>
		/// Retrieves forecasts for the specified tasks.
		/// </summary>
		/// <param name="api">The API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="taskIDs">The tasks to retrieve forecasts for.</param>
		/// <returns>enumerator with the forecast results</returns>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="IForecastApi.GetForecasts"/>
		public static IEnumerable<Forecast> GetForecastEnumerator(this IForecastApi api, Identity identity, IEnumerable<Guid> taskIDs)
		{
			Enforce.Arguments(() => api, () => identity, () => taskIDs);

			foreach (var slice in taskIDs.Slice(Limits.GetForecasts))
			{
				foreach (var forecast in api.GetForecasts(identity, slice.ToArray()))
				{
					yield return forecast;
				}
			}
		}


		/// <summary>
		/// Retrieves all tasks for the current account
		/// </summary>
		/// <param name="api">The API.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>
		/// enumerator of all tasks within this account
		/// </returns>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="IForecastApi.GetTasks"/>
		public static IEnumerable<TaskInfo> GetTaskEnumerator(this IForecastApi api, Identity identity)
		{
			Enforce.Arguments(() => api, () => identity);

			var cursor = Guid.Empty;

			while (true)
			{
				var page = api.GetTasks(identity, cursor, Limits.GetTasks_RecommendedPage);

				cursor = page.Cursor;

				foreach (var task in page.Tasks)
				{
					yield return task;
				}

				if (!page.ThereAreMorePages)
					yield break;
			}
		}

		/// <summary>
		/// Enumerates tasks in the repository, given the series IDs
		/// </summary>
		/// <param name="api">The API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="serieIDs">The serie IDs.</param>
		/// <returns>enumerator over the results</returns>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="IForecastApi.GetTasksBySerieIDs"/>
		[NotNull]
		public static IEnumerable<TaskInfo> GetTaskEnumerator([NotNull] this IForecastApi api, [NotNull] Identity identity, [NotNull] IEnumerable<Guid> serieIDs)
		{
			Enforce.Arguments(() => api, () => identity, () => serieIDs);

			foreach (var guids in serieIDs.Slice(Limits.GetTasks_Series))
			{
				foreach (var task in api.GetTasksBySerieIDs(identity, guids))
				{
					yield return task;
				}
			}
		}


		/// <summary>
		/// Adds the specified tasks to the current account.
		/// </summary>
		/// <param name="api">The API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="tasks">The tasks to add.</param>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="ITimeSerieApi.AddSeries"/>
		public static void AddTasksInBatch(this IForecastApi api, Identity identity, IEnumerable<TaskInfo> tasks)
		{
			Enforce.Arguments(() => api, () => identity, () => tasks);

			foreach (var slice in tasks.Slice(Limits.AddTasks))
			{
				var items = slice.ToArray();
				api.AddTasks(identity, items).ForEach((guid, i) => items[i].TaskID = guid);
			}
		}


		/// <summary>
		/// Updates the tasks.
		/// </summary>
		/// <param name="api">The API.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="tasks">The tasks to update.</param>
		/// <remarks>This method breaks the data into smaller requests, as needed</remarks>
		/// <seealso cref="IForecastApi.UpdateTasks"/>
		public static void UpdateTasksInBatch(this IForecastApi api, Identity identity, IEnumerable<TaskInfo> tasks)
		{
			Enforce.Arguments(() => api, () => identity, () => tasks);

			foreach (var slice in tasks.Slice(Limits.UpdateTasks))
			{
				api.UpdateTasks(identity, slice.ToArray());
			}
		}
	}
}
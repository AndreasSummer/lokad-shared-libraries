#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Rules;

namespace Lokad.Api
{
	/// <summary>
	/// Class that aids in testing code that leverages Lokad API
	/// </summary>
	public sealed class TestHelper
	{
		private static string MakeValidName()
		{
			for (int i = 0; i < 10; i++)
			{
				var name = Rand.NextString(2, 2);
				if (!Scope.IsError(name, ApiRules.ValidName))
					return name;
			}
			throw new InvalidOperationException("Something is wrong with the probabilities");
		}

		/// <summary>
		/// Creates up to <paramref name="count"/> of <see cref="SerieInfo"/>
		/// </summary>
		/// <param name="count">Number of series to create</param>
		/// <returns>array of newly created <see cref="SerieInfo"/></returns>
		public static SerieInfo[] CreateSeries(int count)
		{
			return Range.Array(count, i => new SerieInfo {Name = string.Format("Serie_{0} ({1})", i, MakeValidName())});
		}

		/// <summary>
		/// Creates up to <paramref name="count"/> of <see cref="SerieInfo"/> with the preset SerieIDs
		/// </summary>
		/// <param name="count">Number of series to create</param>
		/// <returns>array of newly created <see cref="SerieInfo"/></returns>
		public static SerieInfo[] CreateFakeSeries(int count)
		{
			return Range.Array(count, i => new SerieInfo
				{
					Name = string.Format("Serie_{0} ({1})", i, MakeValidName()),
					SerieID = Guid.NewGuid()
				});
		}

		/// <summary>
		/// Creates pseudo-random values for the provided series
		/// </summary>
		/// <param name="headers">Series to create values for</param>
		/// <param name="count">Number of values per serie</param>
		/// <returns>new segments with the values</returns>
		public static SegmentForSerie[] CreateValues(IEnumerable<SerieInfo> headers, int count)
		{
			return headers.Select(h => new SegmentForSerie
				{
					SerieID = h.SerieID,
					Values = Range.Array(count, i => new TimeValue
						{
							Time = new DateTime(2008, 1, 1).AddHours(i),
							Value = Rand.Next(5) == 0 ? 0 : Rand.NextDouble().Round(5)
						})
				}).ToArray();
		}

		/// <summary>
		/// Creates pseudo-random tasks for the provided series.
		/// </summary>
		/// <param name="headers">Series to create tasks for.</param>
		/// <returns>new tasks</returns>
		public static TaskInfo[] CreateTasks(IEnumerable<SerieInfo> headers)
		{
			var set = EnumUtil<Period>.Values.ToSet();
			set.Remove(Period.None);
			var periods = set.ToArray();
			return headers.Select((h,i) => new TaskInfo
				{
					FuturePeriods = (i % 12)+1,
					Period = Rand.NextItem(periods),
					SerieID = h.SerieID,
					PeriodStart = new DateTime(2008,1,1).AddHours(i%648)
				})
				.ToArray();
		}

		/// <summary>
		/// Creates sample tags for the provided series
		/// </summary>
		/// <param name="series">Series to create tags for.</param>
		/// <returns>new tag collections</returns>
		public static TagsForSerie[] CreateTags(IEnumerable<SerieInfo> series)
		{
			return series.Select((s, i) => new TagsForSerie
				{
					SerieID = s.SerieID,
					Tags =  new[] {"Tag_A", string.Format("Tag_{0} ({1})", i, MakeValidName())}
				}).ToArray();
		}

		/// <summary>
		/// Creates sample events for the provided series
		/// </summary>
		/// <param name="series">Series to create events for.</param>
		/// <param name="count">The count.</param>
		/// <returns>new event collections</returns>
		public static EventsForSerie[] CreateEvents(IEnumerable<SerieInfo> series, int count)
		{
			return series.Select((s, i) => new EventsForSerie
				{
					SerieID = s.SerieID,
					Events = Range.Array(count, n => new SerieEvent
						{
							DurationDays = (Rand.NextDouble() * 2).Round(5),
							Name = string.Format("Event_{0} ({1})", i, MakeValidName()),
							Time = new DateTime(2008, 1, 1).AddHours(i),
							KnownSince = Rand.Next(4)==3 ? new DateTime(2008,1,1).AddHours(-i) : DateTime.MinValue
						})
				}).ToArray();
		}

		/// <summary>
		/// Converts <see cref="SerieInfo"/> into tuple for testing
		/// </summary>
		/// <param name="serieInfo">The serie info.</param>
		/// <returns>tuple representing the serie</returns>
		public static Tuple<string, Guid> ToTuple(SerieInfo serieInfo)
		{
			return Tuple.From(serieInfo.Name, serieInfo.SerieID);
		}

		/// <summary>
		/// Converts <see cref="TaskInfo"/> into tuple for testing
		/// </summary>
		/// <param name="task">The task to convert.</param>
		/// <returns>tuple representing the task</returns>
		public static Tuple<Guid, Guid, int, Period, DateTime> ToTuple(TaskInfo task)
		{
			return Tuple.From(task.SerieID, task.TaskID, task.FuturePeriods, task.Period, task.PeriodStart);
		}

		/// <summary>
		/// Flattens <see cref="TagsForSerie"/> into exact tuple representation
		/// of the original object (order is ignored)
		/// </summary>
		/// <param name="tagsForSerie">The tags for serie.</param>
		/// <returns>array of tuples representing the original object</returns>
		public static Tuple<Guid, string>[] ToTuples(TagsForSerie tagsForSerie)
		{
			return tagsForSerie.Tags.Convert(t => Tuple.From(tagsForSerie.SerieID, t));
		}

		/// <summary>
		/// Flattens <see cref="EventsForSerie"/> into exact tuple representation
		/// of the original object (order is considered)
		/// </summary>
		/// <param name="eventsForSerie">The events for serie.</param>
		/// <returns>array of tuples representing the original object</returns>
		public static Tuple<Guid, string, DateTime, double, int>[] ToTuples(EventsForSerie eventsForSerie)
		{
			return eventsForSerie
				.Events
				.Select((e,i) => Tuple.From(eventsForSerie.SerieID, e.Name, e.Time, e.DurationDays,i))
				.ToArray();
		}

		/// <summary>
		/// Flattens <see cref="SegmentForSerie"/> into exact tuple representation
		/// of the original object (order is considered)
		/// </summary>
		/// <param name="segmentForSerie">The segment for serie.</param>
		/// <returns>array of tuples representing the original object</returns>
		public static Tuple<Guid, DateTime, double>[] ToTuples(SegmentForSerie segmentForSerie)
		{
			return segmentForSerie
				.Values
				.Select((value, i) => Tuple.From(segmentForSerie.SerieID, value.Time, value.Value))
				.ToArray();
		}
	}
}
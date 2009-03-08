#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lokad.Api
{
	/// <summary>
	/// Service that provides access to Lokad API, while taking care of
	/// the paging details.
	/// </summary>
	public interface ILokadService
	{
		/// <summary> Gets the details for the current account.</summary>
		/// <returns>details for the current account</returns>
		[NotNull]
		AccountInfo GetAccountInfo();

		/// <summary> Sets the partner HR ID for the current account.</summary>
		/// <param name="partnerHRID">The partner id.</param>
		void SetPartner(long partnerHRID);

		/// <summary> Adds series to the Lokad account. </summary>
		/// <param name="series">series to add to the account, they must have unique names</param>
		/// <remarks>It is expected that the series have just been created 
		/// (and thus have empty SerieID); proper SerieIDs will be set by this operation.</remarks>
		/// <seealso cref="ApiRules.IllegalCharacters"/>
		void AddSeries([NotNull] IEnumerable<SerieInfo> series);

		/// <summary> Retrieves details of all series for current account. </summary>
		/// <returns>array of results</returns>
		[NotNull]
		SerieInfo[] GetSeries();

		/// <summary>
		/// Adds the <paramref name="series"/> to the Lokad account, 
		/// while adding the <paramref name="prefix"/> to their names
		/// </summary>
		/// <param name="series">The series.</param>
		/// <param name="prefix">The prefix.</param>
		/// <remarks>It is expected that the series have just been created 
		/// (and thus have empty <see cref="SerieInfo.SerieID"/>); proper 
		/// <see cref="SerieInfo.SerieID"/> will be set by this operation.</remarks>
		/// <seealso cref="ApiRules.IllegalCharacters"/>
		void AddSeriesWithPrefix([NotNull] IEnumerable<SerieInfo> series, string prefix);

		/// <summary>
		/// Retrieves the series that match the specified <paramref name="prefix"/>. 
		/// The <paramref name="prefix"/> is dropped from the names.
		/// </summary>
		/// <param name="prefix">The prefix.</param>
		/// <returns>array of series that have prefix removed from their names</returns>
		/// <seealso cref="ApiRules.IllegalCharacters"/>
		[NotNull]
		SerieInfo[] GetSeriesWithPrefix([NotNull] string prefix);

		/// <summary> Deletes the series from the Lokad account.</summary>
		/// <param name="series">The series to delete.</param>
		void DeleteSeries([NotNull] IEnumerable<SerieInfo> series);

		/// <summary> Sets the tags for the specified series, overwriting them. </summary>
		/// <param name="tagsForSerie">The tags for serie.</param>
		/// <seealso cref="ApiRules.IllegalCharacters"/>
		void SetTags([NotNull] IEnumerable<TagsForSerie> tagsForSerie);

		/// <summary> Retrieves tags for the specified series. </summary>
		/// <param name="series">The series to retrieve tags for.</param>
		/// <returns>array of results</returns>
		[NotNull]
		TagsForSerie[] GetTags([NotNull] IEnumerable<SerieInfo> series);

		/// <summary> Retrieves events for the specified series. </summary>
		/// <param name="series">The series to retrieve events for.</param>
		/// <returns>array of the events</returns>
		/// <seealso cref="ITimeSerieApi.GetEvents"/>
		[NotNull]
		EventsForSerie[] GetEvents([NotNull] IEnumerable<SerieInfo> series);

		/// <summary> Sets events for the specified series (overwriting them) </summary>
		/// <param name="eventsForSerie">The events for serie.</param>
		/// <seealso cref="ITimeSerieApi.SetEvents"/>
		/// <seealso cref="ApiRules.IllegalCharacters"/>
		void SetEvents([NotNull] IEnumerable<EventsForSerie> eventsForSerie);

		/// <summary>
		/// Updates the series with the provided segments.
		/// Values must be ordered by <see cref="TimeValue.Time"/>
		/// </summary>
		/// <param name="segments">The segments.</param>
		/// <seealso cref="ITimeSerieApi.UpdateSerieSegments"/>
		void UpdateSerieSegments([NotNull] IEnumerable<SegmentForSerie> segments);

		/// <summary> Retrieves the values for the specified series </summary>
		/// <param name="series">The series to retrieve values for.</param>
		/// <returns>array of serie values</returns>
		/// <seealso cref="ITimeSerieApi.GetSerieSegments"/>
		[NotNull]
		SegmentForSerie[] GetSerieSegments([NotNull] IEnumerable<SerieInfo> series);

		/// <summary> Retrieves all tasks for the current account  </summary>
		/// <returns> array of all tasks within this account</returns>
		/// <seealso cref="IForecastApi.GetTasks"/>
		[NotNull]
		TaskInfo[] GetTasks();

		/// <summary> Retrieves array of tasks for the given series </summary>
		/// <param name="series">The series to retrieve tasks for.</param>
		/// <returns>array of the matching tasks</returns>
		[NotNull]
		TaskInfo[] GetTasks([NotNull] IEnumerable<SerieInfo> series);

		/// <summary> Retrieves forecasts for the specified tasks. </summary>
		/// <param name="tasks">The tasks to retrieve forecasts for.</param>
		/// <returns>array of the forecast results</returns>
		/// <seealso cref="IForecastApi.GetForecasts"/>
		[NotNull]
		Forecast[] GetForecasts([NotNull] IEnumerable<TaskInfo> tasks);

		/// <summary> Adds the specified tasks to the current account. </summary>
		/// <param name="tasks">The tasks to add.</param>
		/// <seealso cref="ITimeSerieApi.AddSeries"/>
		/// <remarks>It is expected that the tasks have just been created 
		/// (and thus have empty <see cref="TaskInfo.TaskID"/>); proper <see cref="TaskInfo.TaskID"/> 
		/// will be set by this operation.</remarks>
		/// <seealso cref="ApiRules.IllegalCharacters"/>
		void AddTasks([NotNull] IEnumerable<TaskInfo> tasks);

		/// <summary> Deletes the specified tasks from the current account. </summary>
		/// <param name="tasks">The tasks to delete.</param>
		/// <seealso cref="IForecastApi.DeleteTasks"/>
		void DeleteTasks([NotNull] IEnumerable<TaskInfo> tasks);

		/// <summary> Updates the tasks. </summary>
		/// <param name="tasks">The tasks to update.</param>
		/// <seealso cref="IForecastApi.UpdateTasks"/>
		/// <seealso cref="ApiRules.IllegalCharacters"/>
		void UpdateTasks([NotNull] IEnumerable<TaskInfo> tasks);

		/// <summary>
		/// Creates new support ticket
		/// </summary>
		/// <param name="report">The report.</param>
		/// <returns>unique identifier for the new issue</returns>
		[NotNull]
		Guid ReportIssue([NotNull] Report report);

		/// <summary>
		/// Exposes some operations from this class as lazy enumerators
		/// </summary>
		/// <value>Interface that exposes <see cref="ILazyLokadService"/> methods.</value>
		ILazyLokadService Lazy { get; }
	}
}
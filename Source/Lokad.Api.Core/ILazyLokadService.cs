using System.Collections.Generic;

namespace Lokad.Api
{
	/// <summary>
	/// Interface that exposes SDK methods as lazy
	/// </summary>
	public interface ILazyLokadService
	{
		/// <summary> Retrieves forecasts for the specified tasks. </summary>
		/// <param name="tasks">The tasks to retrieve forecasts for.</param>
		/// <returns>lazy enumerator over the results</returns>
		/// <seealso cref="IForecastApi.GetForecasts"/>
		IEnumerable<Forecast> GetForecasts(IEnumerable<TaskInfo> tasks);
		/// <summary> Retrieves array of tasks for the given series </summary>
		/// <param name="series">The series to retrieve tasks for.</param>
		/// <returns>lazy enumerator over the matching tasks</returns>

		IEnumerable<TaskInfo> GetTasks(IEnumerable<SerieInfo> series);

		/// <summary> Retrieves all tasks for the current account  </summary>
		/// <returns> lazy enumerator over all tasks within this account</returns>
		/// <seealso cref="IForecastApi.GetTasks"/>
		IEnumerable<TaskInfo> GetTasks();

		/// <summary> Retrieves details of all series for current account. </summary>
		/// <returns>lazy enumerator over all series within this account</returns>
		IEnumerable<SerieInfo> GetSeries();

		/// <summary> Retrieves the values for the specified series </summary>
		/// <param name="series">The series to retrieve values for.</param>
		/// <returns>lazy enumerator over the serie segments</returns>
		/// <seealso cref="ITimeSerieApi.GetSerieSegments"/>
		IEnumerable<SegmentForSerie> GetSerieSegments(IEnumerable<SerieInfo> series);


		/// <summary> Retrieves events for the specified series. </summary>
		/// <param name="series">The series to retrieve events for.</param>
		/// <returns>lazy enumerator over the results</returns>
		/// <seealso cref="ITimeSerieApi.GetEvents"/>
		IEnumerable<EventsForSerie> GetEvents(IEnumerable<SerieInfo> series);

		/// <summary> Retrieves tags for the specified series. </summary>
		/// <param name="series">The series to retrieve tags for.</param>
		/// <returns>lazy enumerator over the tags</returns>
		IEnumerable<TagsForSerie> GetTags(IEnumerable<SerieInfo> series);

		/// <summary>
		/// Retrieves the series that match the specified <paramref name="prefix"/>. 
		/// The <paramref name="prefix"/> is dropped from the names.
		/// </summary>
		/// <param name="prefix">The prefix.</param>
		/// <returns>lazy enumerator over series that have prefix removed from their names</returns>
		/// <seealso cref="ApiRules.IllegalCharacters"/>
		IEnumerable<SerieInfo> GetSeriesWithPrefix(string prefix);
	}

}
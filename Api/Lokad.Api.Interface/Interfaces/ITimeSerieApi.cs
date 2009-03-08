#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Api
{
	/// <summary>
	/// Operations for adding, changing and deleting source data for forecasts
	/// </summary>
	public interface ITimeSerieApi
	{
		/// <summary>
		/// Adds new series to the current Lokad account.
		/// </summary>
		/// <param name="identity">The identity that represents account to work with.</param>
		/// <param name="series">The series to be added.</param>
		/// <returns>unique identifiers for the series in matching order</returns>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		Guid[] AddSeries(Identity identity, SerieInfo[] series);

		/// <summary>
		/// Deletes the specified series from the current Lokad account.
		/// </summary>
		/// <param name="identity">The identity that represents the account to work with.</param>
		/// <param name="serieIDs">IDs of the series to be deleted.</param>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		void DeleteSeries(Identity identity, Guid[] serieIDs);

		/// <summary>
		/// <para>Allows to page through <see cref="SerieInfo"/> collection associated with the current account.</para>
		/// </summary>
		/// <param name="identity">The identity that represents account to work with.</param>
		/// <param name="cursor">The cursor (use <see cref="Guid.Empty"/> for the first page and then <see cref="SerieInfoPage.Cursor"/> for the pages afterwards).</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns>page with the series</returns>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		SerieInfoPage GetSeries(Identity identity, Guid cursor, int pageSize);

		/// <summary>
		/// Allows to update series with the values. Inclusive segment defined by the 
		/// <see cref="SegmentForSerie"/> gets overwritten by the provided one
		/// </summary>
		/// <param name="identity">The identity that represents the account to work with.</param>
		/// <param name="segments">The segments to be uploaded.</param>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		void UpdateSerieSegments(Identity identity, SegmentForSerie[] segments);


		/// <summary>
		/// Gets the value segments for the specified series.
		/// </summary>
		/// <param name="identity">The identity that represents the account to work with.</param>
		/// <param name="serieIDs">Unique identifiers for the series</param>
		/// <param name="cursor">The cursor (use the empty cursor for the first page and <see cref="SerieSegmentPage.Cursor"/> for all subsequent calls).</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns>page with the values</returns>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		SerieSegmentPage GetSerieSegments(Identity identity, Guid[] serieIDs, SegmentCursor cursor, int pageSize);

		/// <summary>
		/// Sets the tags for the specified series, completely overwriting the old tags, if present.
		/// </summary>
		/// <param name="identity">The identity that represents the account to work with.</param>
		/// <param name="tagsForSerie">The tags for serie.</param>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		void SetTags(Identity identity, TagsForSerie[] tagsForSerie);

		/// <summary>
		/// Sets the events for the specified series. Old events get completely overwritten, if present.
		/// </summary>
		/// <param name="identity">The identity that represents the account to work with.</param>
		/// <param name="eventsForSerie">The events for serie.</param>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		void SetEvents(Identity identity, EventsForSerie[] eventsForSerie);


		/// <summary>
		/// Retrieves tags for the specified series
		/// </summary>
		/// <param name="identity">The identity that represents the account to work with.</param>
		/// <param name="serieIDs">Unique identifiers for series to retrieve tags for</param>
		/// <returns>tags for the series. </returns>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		TagsForSerie[] GetTags(Identity identity, Guid[] serieIDs);


		/// <summary>
		/// Retrieves events for the specified series
		/// </summary>
		/// <param name="identity">The identity that represents the account to work with.</param>
		/// <param name="serieIDs">Unique identifiers for series to retrieve tags for.</param>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		EventsForSerie[] GetEvents(Identity identity, Guid[] serieIDs);
	}
}
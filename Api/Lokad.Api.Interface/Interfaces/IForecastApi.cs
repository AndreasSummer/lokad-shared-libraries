#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Api
{
	/// <summary>
	/// Operations to set up forecast requests and retrieve the results
	/// </summary>
	public interface IForecastApi
	{
		/// <summary>
		/// Creates multiple tasks for the specified Lokad Account
		/// </summary>
		/// <param name="identity">The identity that represents the account to work with.</param>
		/// <param name="tasks">The tasks to create.</param>
		/// <returns>Array of unique identifiers for the created tasks</returns>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		Guid[] AddTasks(Identity identity, TaskInfo[] tasks);

		// gets all tasks for the account in 
		// paging and reliable mode
		// maxcount is enforced on the server


		/// <summary> Allows to page through the <see cref="TaskInfo"/> 
		/// collections associated with the Lokad account  </summary>
		/// <param name="identity">The identity that represents Lokad account to work with.</param>
		/// <param name="cursor">The cursor for paging (use <see cref="Guid.Empty"/> for the first page and <see cref="TaskInfoPage.Cursor"/> for all subsequent pages).</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns>page that contains <see cref="TaskInfo"/> objects for the specified account</returns>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		TaskInfoPage GetTasks(Identity identity, Guid cursor, int pageSize);

		/// <summary> Retrieve tasks based on the provided <paramref name="serieIDs"/> </summary>
		/// <param name="identity">The identity that represents the account to work with.</param>
		/// <param name="serieIDs">The serie IDs.</param>
		/// <returns>Tasks that were found in the system</returns>
		TaskInfo[] GetTasksBySerieIDs(Identity identity, Guid[] serieIDs);

		/// <summary> Updates the tasks. </summary>
		/// <param name="identity">The identity that represents the Lokad account to work with.</param>
		/// <param name="tasks">The tasks.</param>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		void UpdateTasks(Identity identity, TaskInfo[] tasks);

		/// <summary> Deletes the specified tasks from the Lokad account. </summary>
		/// <param name="identity">The identity that represents the Lokad account to work with.</param>
		/// <param name="taskIDs">IDs of the tasks to delete.</param>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		void DeleteTasks(Identity identity, Guid[] taskIDs);

		/// <summary>
		/// Retrieves the forecasts associated with the specified tasks.
		/// </summary>
		/// <param name="identity">The identity that represents the account to work with.</param>
		/// <param name="taskIDs">Unique identifiers for the tasks.</param>
		/// <returns>array of the <see cref="Forecast"/> objects</returns>
		/// <remarks>
		/// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		/// </remarks>
		Forecast[] GetForecasts(Identity identity, Guid[] taskIDs);
	}
}
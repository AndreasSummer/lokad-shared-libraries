#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using Lokad.Diagnostics.Persist;

namespace Lokad.Diagnostics.Monitoring
{
	/// <summary>
	/// Interface that represents some sub-system to be monitored
	/// </summary>
	public interface IMonitoringApi
	{
		/// <summary>
		/// Retrieves all execution statistics from the system
		/// </summary>
		/// <returns></returns>
		ExecutionData[] GetStatistics();

		/// <summary>
		/// Resets all counters 
		/// </summary>
		void ResetCounters();

		/// <summary>
		/// Gets short descriptor for the sub-system
		/// </summary>
		/// <returns></returns>
		SystemData GetDescriptor();

		/// <summary> Retrieves up to <paramref name="exceptionCount"/> 
		/// exception statistics from the system (most important ones
		/// go first)
		/// </summary>
		/// <param name="exceptionCount">The exception count.</param>
		/// <returns>array of exception statistics</returns>
		ExceptionData[] GetExceptions(int exceptionCount);

		/// <summary>
		/// Retrieves information details from the system,
		/// filtering the details with the query first
		/// </summary>
		/// <param name="query">The query to apply.</param>
		/// <returns>result that matches the query </returns>
		MonitoringData[] GetData(string query);
	}
}

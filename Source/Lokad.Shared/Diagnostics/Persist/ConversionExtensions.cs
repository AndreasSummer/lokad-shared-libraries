#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Diagnostics.Persist
{
	/// <summary>
	/// Helper extensions for converting to/from data classes in the Diagnostics namespace
	/// </summary>
	public static class ConversionExtensions
	{
		/// <summary>
		/// Converts immutable statistics objects to the persistence objects
		/// </summary>
		/// <param name="statisticsArray">The immutable statistics objects.</param>
		/// <returns>array of persistence objects</returns>
		public static ExecutionData[] ToPersistence(this ExecutionStatistics[] statisticsArray)
		{
			return statisticsArray.Convert(es => new ExecutionData
				{
					CloseCount = es.CloseCount,
					Counters = es.Counters,
					Name = es.Name,
					OpenCount = es.OpenCount,
					RunningTime = es.RunningTime
				});
		}

		/// <summary>
		/// Converts persistence objects to immutable statistics objects
		/// </summary>
		/// <param name="dataArray">The persistence data objects.</param>
		/// <returns>array of statistics objects</returns>
		public static ExecutionStatistics[] FromPersistence(this ExecutionData[] dataArray)
		{
			return dataArray.Convert(
				d => new ExecutionStatistics(
					d.Name,
					d.OpenCount,
					d.CloseCount,
					d.Counters,
					d.RunningTime));
		}
	}
}
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

		/// <summary>
		/// Converts statistics classes to the data objects.
		/// </summary>
		/// <param name="statisticsArray">The statistics array.</param>
		/// <returns>array of data objects</returns>
		public static ExceptionData[] ToPersistence(this ExceptionStatistics[] statisticsArray)
		{
			return statisticsArray.Convert(es => new ExceptionData
				{
					Count = es.Count,
					ID = es.ID,
					Message = es.Message,
					Name = es.Name,
					Text = es.Text
				});
		}

		/// <summary>
		/// Converts data objects to statistics classes.
		/// </summary>
		/// <param name="dataArray">The persistence data array.</param>
		/// <returns>array of statistics objects</returns>
		public static ExceptionStatistics[] FromPersistence(this ExceptionData[] dataArray)
		{
			return dataArray.Convert(
				d => new ExceptionStatistics(
					d.ID,
					d.Count,
					d.Name,
					d.Message,
					d.Text));
		}

		/// <summary>
		/// Converts the descriptor instance to persistence object
		/// </summary>
		/// <param name="descriptor">The descriptor.</param>
		/// <returns>new persistence object</returns>
		public static SystemData ToPersistence(this SystemDescriptor descriptor)
		{
			return new SystemData
				{
					Configuration = descriptor.Configuration,
					Instance = descriptor.Instance,
					Name = descriptor.Name,
					Version = descriptor.Version
				};
		}

		/// <summary>
		/// Converts the descriptor instance to persistence object
		/// </summary>
		/// <param name="data">The persistence data.</param>
		/// <returns>new descriptor object</returns>
		public static SystemDescriptor FromPersistence(this SystemData data)
		{
			return new SystemDescriptor(
				data.Name,
				data.Version,
				data.Configuration,
				data.Instance);
		}
	}
}
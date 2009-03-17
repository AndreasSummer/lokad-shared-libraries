#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using System.Linq;

#if !SILVERLIGHT2

namespace Lokad.Diagnostics
{
	/// <summary>
	/// In-memory thread-safe collection of <see cref="ExecutionCounter"/>
	/// </summary>
	public sealed class ExecutionCounters
	{
		/// <summary>
		/// Default instance of this counter
		/// </summary>
		public static readonly ExecutionCounters Default = new ExecutionCounters();

		readonly object _lock = new object();
		readonly IList<ExecutionCounter> _counters = new List<ExecutionCounter>();

		/// <summary>
		/// Registers the execution counters within this collection.
		/// </summary>
		/// <param name="counters">The counters.</param>
		public void RegisterRange(IEnumerable<ExecutionCounter> counters)
		{
			lock (_lock)
			{
				_counters.AddRange(counters);
			}
		}

		/// <summary>
		/// Retrieves statistics for all exception counters in this collection
		/// </summary>
		/// <returns></returns>
		public IList<ExecutionStatistics> ToList()
		{
			lock (_lock)
			{
				return _counters.Select(c => c.ToStatistics()).ToList();
			}
		}

		/// <summary>
		/// Resets all counters.
		/// </summary>
		public void ResetAll()
		{
			lock (_lock)
			{
				foreach (var counter in _counters)
				{
					counter.Reset();
				}
			}
		}
	}
}

#endif
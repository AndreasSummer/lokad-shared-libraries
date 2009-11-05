#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if !SILVERLIGHT2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Lokad.Threading;

namespace Lokad.Diagnostics
{
	/// <summary>
	/// Exception counter that persists unique information about exceptions being entered in an internal dictionary. 
	/// Once it gets filled, exceptions with the least number of occurrences are discarded.
	/// </summary>
	/// <remarks>The class <see cref="ExceptionCounters"/> is designed as <strong>thread-safe</strong>.</remarks>
	public sealed class ExceptionCounters
	{
		/// <summary>
		/// Singleton instance of this counter
		/// </summary>
		public static readonly ExceptionCounters Default = new ExceptionCounters();

		readonly IDictionary<string, ExceptionCounter> _dictionary = new Dictionary<string, ExceptionCounter>();
		readonly ReaderWriterLockSlim _lockSlim = new ReaderWriterLockSlim();

		/// <summary>
		/// Gets or sets the number of exceptions this list can store.
		/// </summary>
		/// <value>The capacity.</value>
		public int CapacityThreshold { get; set; }

		/// <summary>
		/// Default capacity for the <see cref="ExceptionCounters"/>
		/// </summary>
		public const int DefaultCapacity = 100;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionCounters"/> class.
		/// </summary>
		/// <param name="exceptionThreshold">Maximum number of unique exceptions to keep at once.</param>
		public ExceptionCounters(int exceptionThreshold)
		{
			CapacityThreshold = exceptionThreshold;
			_dictionary = new Dictionary<string, ExceptionCounter>(exceptionThreshold, StringComparer.InvariantCultureIgnoreCase);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionCounters"/> class with the <see cref="DefaultCapacity"/>.
		/// </summary>
		public ExceptionCounters() : this(DefaultCapacity)
		{
		}

		/// <summary>
		/// Registers the provided exception in this counter
		/// </summary>
		/// <param name="ex">The exception to register</param>
		/// <returns></returns>
		/// <rereturns>unique identifier for the exception</rereturns>
		public Guid Add(Exception ex)
		{
			var text = ex.ToString();
			ExceptionCounter value;

			using (_lockSlim.GetUpgradeableReadLock())
			{
				if (_dictionary.TryGetValue(text, out value))
				{
					value.InterlockedIncrement();
					return value.ID;
				}

				value = new ExceptionCounter(ex);
				using (_lockSlim.GetWriteLock())
				{
					if (CapacityThreshold <= _dictionary.Count)
					{
						var mostRare = _dictionary
							.OrderBy(v => v.Value.Count)
							.First();
						_dictionary.Remove(mostRare);
					}
					_dictionary.Add(text, value);
				}
				return value.ID;
			}
		}

		/// <summary>
		/// Returns the list of all exceptions 
		/// </summary>
		/// <returns>list of <see cref="ExceptionCounter"/></returns>
		public IList<ExceptionStatistics> GetHistory()
		{
			using (_lockSlim.GetReadLock())
			{
				return _dictionary
					.Values
					.Select(e => new ExceptionStatistics(e.ID, e.Count, e.FirstInstance))
					.ToList();
			}
		}

		/// <summary>
		/// Clears this counter.
		/// </summary>
		public void Clear()
		{
			using (_lockSlim.GetWriteLock())
			{
				_dictionary.Clear();
			}
		}
	}
}

#endif
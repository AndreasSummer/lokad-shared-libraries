#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Diagnostics;
using System.Linq;
using System.Threading;

#if !SILVERLIGHT2

namespace Lokad.Diagnostics
{
	/// <summary>
	/// <para>Class to provide simple measurement of some method calls. 
	/// This class has been designed to provide light performance monitoring
	/// that could be used for instrumenting methods in production. It does 
	/// not use any locks.</para>
	/// <para>It does provide almost no concurrency support, yet these classes
	/// are mostly safe for the multi-threaded environments (and have been used there).</para>
	/// <para>The usage idea is simple - data is captured from the counters at regular intervals
	/// of time (i.e. 5-10 minutes). Counters are reset after that. Data itself is aggregated 
	/// on the monitoring side. If there are some bad values (i.e. due to some rare race condition)
	/// then the counter data is simply discarded.</para>
	/// </summary>
	public sealed class ExecutionCounter
	{
		long _openCount;
		long _closeCount;
		long _runningTime;

		readonly long[] _openCounters;
		readonly long[] _closeCounters;

		readonly string _name;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExecutionCounter"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="openCounterCount">The open counter count.</param>
		/// <param name="closeCounterCount">The close counter count.</param>
		public ExecutionCounter(string name, int openCounterCount, int closeCounterCount)
		{
			_name = name;
			_openCounters = new long[openCounterCount];
			_closeCounters = new long[closeCounterCount];
		}


		/// <summary>
		/// Open the specified counter and adds the provided values to the openCounters collection
		/// </summary>
		/// <param name="openCounters">The open counters.</param>
		/// <returns>timestamp for the operation</returns>
		public long Open(params long[] openCounters)
		{
			// abdullin: this is not really atomic and precise,
			// but we do not care that much
			unchecked
			{
				Interlocked.Increment(ref _openCount);
				
				for (int i = 0; i < openCounters.Length; i++)
				{
					Interlocked.Add(ref _openCounters[i], openCounters[i]);
				}
			}
			return Stopwatch.GetTimestamp();
		}


		/// <summary>
		/// Closes the specified timestamp.
		/// </summary>
		/// <param name="timestamp">The timestamp.</param>
		/// <param name="closeCounters">The close counters.</param>
		public void Close(long timestamp, params long[] closeCounters)
		{
			var runningTime = Stopwatch.GetTimestamp() - timestamp;

			// this counter has been reset after opening - discard
			if ((_openCount == 0) || (runningTime < 0))
				return;

			// abdullin: this is not really atomic and precise,
			// but we do not care that much
			unchecked
			{
				Interlocked.Add(ref _runningTime, runningTime);
				Interlocked.Increment(ref _closeCount);
				
				for (int i = 0; i < closeCounters.Length; i++)
				{
					Interlocked.Add(ref _closeCounters[i], closeCounters[i]);
				}
			}
		}

		/// <summary>
		/// Resets this instance.
		/// </summary>
		public void Reset()
		{
			_runningTime = 0;
			_openCount = 0;
			_closeCount = 0;

			for (int i = 0; i < _closeCounters.Length; i++)
			{
				_closeCounters[i] = 0;
			}

			for (int i = 0; i < _openCounters.Length; i++)
			{
				_openCounters[i] = 0;
			}
		}

		/// <summary>
		/// Converts this instance to <see cref="ExecutionStatistics"/>
		/// </summary>
		/// <returns></returns>
		public ExecutionStatistics ToStatistics()
		{
			long dateTimeTicks = _runningTime;
			if (Stopwatch.IsHighResolution)
			{
				double num2 = dateTimeTicks;
				num2 *= 10000000.0/Stopwatch.Frequency;
				dateTimeTicks = (long) num2;
			}

			return new ExecutionStatistics(
				_name,
				_openCount,
				_closeCount,
				_openCounters.Append(_closeCounters),
				dateTimeTicks);
		}
	}
}

#endif
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if SILVERLIGHT2

namespace System.Diagnostics
{
	/// <summary>
	/// Replacement for missing stopwatch, based on the article from
	/// http://blog.tiaan.com/link/2009/02/03/stopwatch-silverlight
	/// This is a low-precision stop-watch
	/// </summary>
	public class Stopwatch
	{
		/// <summary>
		/// Determines if the stopwatch is high or low resolution 
		/// (always false for this implementation)
		/// </summary>
		public static readonly bool IsHighResolution = false;

		/// <summary>
		/// Polling frequency
		/// </summary>
		public static readonly long Frequency = TimeSpan.TicksPerSecond;

		/// <summary>
		/// Gets the time elapsedsince the stopwatch has been started.
		/// </summary>
		/// <value>The elapsed.</value>
		public TimeSpan Elapsed
		{
			get
			{
				if (!_startUtc.HasValue)
				{
					return TimeSpan.Zero;
				}
				if (!_endUtc.HasValue)
				{
					return (DateTime.UtcNow - _startUtc.Value);
				}
				return (_endUtc.Value - _startUtc.Value);
			}
		}

		/// <summary>
		/// Gets the elapsed time in milliseconds.
		/// </summary>
		/// <value>The elapsed time inmilliseconds.</value>
		public long ElapsedMilliseconds
		{
			get { return ElapsedTicks/TimeSpan.TicksPerMillisecond; }
		}

		/// <summary>
		/// Gets the elapsed time in ticks.
		/// </summary>
		/// <value>The elapsed time in ticks.</value>
		public long ElapsedTicks
		{
			get { return Elapsed.Ticks; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is running.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is running; otherwise, <c>false</c>.
		/// </value>
		public bool IsRunning { get; private set; }

		DateTime? _startUtc;
		DateTime? _endUtc;

		/// <summary>
		/// Gets the timestamp.
		/// </summary>
		/// <returns>current timestamp</returns>
		public static long GetTimestamp()
		{
			return DateTime.UtcNow.Ticks;
		}

		/// <summary>
		/// Resets this instance.
		/// </summary>
		public void Reset()
		{
			Stop();
			_endUtc = null;
			_startUtc = null;
		}

		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start()
		{
			if (IsRunning)
			{
				return;
			}
			if ((_startUtc.HasValue) &&
				(_endUtc.HasValue))
			{
				// Resume the timer from its previous state
				_startUtc = _startUtc.Value +
					(DateTime.UtcNow - _endUtc.Value);
			}
			else
			{
				// Start a new time-interval from scratch
				_startUtc = DateTime.UtcNow;
			}
			IsRunning = true;
			_endUtc = null;
		}

		/// <summary>
		/// Stops this instance.
		/// </summary>
		public void Stop()
		{
			if (IsRunning)
			{
				IsRunning = false;
				_endUtc = DateTime.UtcNow;
			}
		}

		/// <summary>
		/// Creates and starts a new stopwatch instance
		/// </summary>
		/// <returns>new stopwatch instance</returns>
		public static Stopwatch StartNew()
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}
	}
}

#endif
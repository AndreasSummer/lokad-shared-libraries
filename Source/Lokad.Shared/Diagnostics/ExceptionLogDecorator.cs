#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if !SILVERLIGHT2

namespace System.Diagnostics
{
	/// <summary>
	/// Simple decorator for <see cref="ILog"/> that saves 
	/// all incoming exceptions to the <see cref="ExceptionCounters"/>
	/// </summary>
	public sealed class ExceptionLogDecorator : ILog
	{
		readonly ILog _log;
		readonly ExceptionCounters _counter;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionLogDecorator"/> class.
		/// </summary>
		/// <param name="log">The log.</param>
		/// <param name="counter">The counter.</param>
		public ExceptionLogDecorator(ILog log, ExceptionCounters counter)
		{
			_log = log;
			_counter = counter;
		}

		void ILog.Log(LogLevel level, object message)
		{
			_log.Log(level, message);
		}

		void ILog.Log(LogLevel level, Exception ex, object message)
		{
			var id = _counter.Add(ex);
			_log.Log(level, ex, StringUtil.FormatInvariant("(Ex:{1}) {0}. ", message, id));
		}

		bool ILog.IsEnabled(LogLevel level)
		{
			return _log.IsEnabled(level);
		}
	}
}

#endif
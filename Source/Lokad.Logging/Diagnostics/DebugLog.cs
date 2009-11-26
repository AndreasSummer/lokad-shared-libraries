#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if !SILVERLIGHT2

using System;
using System.Diagnostics;
using Lokad.Quality;

namespace Lokad.Diagnostics
{
	/// <summary>
	/// <see cref="ILog"/> that writes to the <see cref="Trace.Listeners"/>, if the
	/// <em>DEBUG</em> symbol is defined
	/// </summary>
	[Serializable]
	[NoCodeCoverage, Immutable, UsedImplicitly]
	public sealed class DebugLog : ILog
	{
		/// <summary>  Singleton instance </summary>
		[UsedImplicitly] public static readonly ILog Instance = new DebugLog("");

		/// <summary>
		/// Named provider for the <see cref="DebugLog"/>
		/// </summary>
		[UsedImplicitly] public static readonly ILogProvider Provider =
			new LambdaLogProvider(s => new DebugLog(s));

		readonly string _logName;

		/// <summary>
		/// Initializes a new instance of the <see cref="DebugLog"/> class.
		/// </summary>
		/// <param name="logName">Name of the log.</param>
		public DebugLog(string logName)
		{
			_logName = logName;
		}


		void ILog.Log(LogLevel level, object message)
		{
			if (string.IsNullOrEmpty(_logName))
			{
				Debug.WriteLine(message, string.Format("[{0,-5}]", level));
			}
			else
			{
				Debug.WriteLine(message, string.Format("[{1,-5}] {0}", _logName, level));
			}

			Debug.Flush();
		}

		void ILog.Log(LogLevel level, Exception ex, object message)
		{

			if (string.IsNullOrEmpty(_logName))
			{
				var category = string.Format("[{0,-5}]", level);

				Debug.WriteLine(message, category);
				Debug.WriteLine(ex, category);
			}
			else
			{
				var category = string.Format("[{1,-5}] {0}", _logName, level);

				Debug.WriteLine(message, category);
				Debug.WriteLine(ex, category);
			}

			Debug.Flush();
		}

		bool ILog.IsEnabled(LogLevel level)
		{
			return true;
		}
	}
}

#endif
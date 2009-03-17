#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if !SILVERLIGHT2
using System;
using System.Diagnostics;
using Lokad.Diagnostics.CodeAnalysis;

namespace Lokad.Diagnostics
{
	/// <summary>
	/// <see cref="ILog"/> that writes to the <see cref="Trace.Listeners"/>
	/// </summary>
	[Immutable]
	[Serializable]
	[NoCodeCoverage]
	public sealed class TraceLog : ILog
	{
		/// <summary>  Singleton instance </summary>
		public static readonly ILog Instance = new TraceLog();

		/// <summary>
		/// Named provider for the <see cref="TraceLog"/>
		/// </summary>
		public static readonly INamedProvider<ILog> Provider =
			new NamedProvider<ILog>(s => Instance);

		TraceLog()
		{
		}

		void ILog.Log(LogLevel level, object message)
		{
			Trace.WriteLine(message, level.ToString());
		}

		void ILog.Log(LogLevel level, Exception ex, object message)
		{
			Trace.WriteLine(message, level.ToString());
			Trace.WriteLine(ex, level.ToString());
		}

		bool ILog.IsEnabled(LogLevel level)
		{
			return true;
		}
	}
}

#endif
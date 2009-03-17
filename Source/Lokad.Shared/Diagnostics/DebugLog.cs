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
	/// <see cref="ILog"/> that writes to the <see cref="Debug.Listeners"/>
	/// </summary>
	[Immutable]
	[Serializable]
	[NoCodeCoverage]
	public sealed class DebugLog : ILog
	{
		/// <summary>
		/// Singleton instance of the <see cref="DebugLog"/>
		/// </summary>
		public static readonly ILog Instance = new DebugLog();

		/// <summary>
		/// Named provider for the <see cref="DebugLog"/>
		/// </summary>
		public static readonly INamedProvider<ILog> Provider =
			new NamedProvider<ILog>(s => Instance);

		DebugLog()
		{
		}

		void ILog.Log(LogLevel level, object message)
		{
			Debug.WriteLine(message, level.ToString());
		}

		void ILog.Log(LogLevel level, Exception ex, object message)
		{
			Debug.WriteLine(message, level.ToString());
			Debug.WriteLine(ex, level.ToString());
		}

		bool ILog.IsEnabled(LogLevel level)
		{
			return true;
		}
	}
}

#endif
#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Diagnostics.CodeAnalysis;

namespace System.Diagnostics
{
	/// <summary>
	/// <see cref="ILog"/> that does not do anything
	/// </summary>
	[NoCodeCoverage]
	[Immutable]
	public sealed class NullLog : ILog
	{
		/// <summary>
		/// Singleton instance of the <see cref="ILog"/>
		/// </summary>
		public static readonly ILog Instance = new NullLog();

		/// <summary>
		/// Named provider for the <see cref="NullLog"/>
		/// </summary>
		public static readonly INamedProvider<ILog> Provider =
			new NamedProvider<ILog>(s => Instance);

		NullLog()
		{
		}

		void ILog.Log(LogLevel level, object message)
		{
		}

		void ILog.Log(LogLevel level, Exception ex, object message)
		{
		}

		bool ILog.IsEnabled(LogLevel level)
		{
			return false;
		}
	}
}
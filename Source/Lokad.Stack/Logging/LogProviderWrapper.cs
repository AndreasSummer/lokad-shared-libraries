#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Logging
{
	/// <summary>
	/// Uses <see cref="log4net"/> as the logging backend
	/// </summary>
	sealed class LogProviderWrapper : INamedProvider<ILog>
	{
		/// <summary>
		/// Singleton instance of the <see cref="INamedProvider{TValue}"/> for <see cref="ILog"/>
		/// </summary>
		public static readonly INamedProvider<ILog> Instance = new LogProviderWrapper();

		LogProviderWrapper()
		{
		}

		/// <summary>
		/// Creates new named log
		/// </summary>
		/// <param name="key">Name of the log to use</param>
		/// <returns></returns>
		public ILog Get(string key)
		{
			return LogWrapper.GetByName(key);
		}
	}
}
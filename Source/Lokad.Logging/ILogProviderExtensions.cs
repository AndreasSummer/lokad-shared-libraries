#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using Lokad.Quality;

namespace Lokad
{
	/// <summary>
	/// Extension methods for the <see cref="INamedProvider{TValue}"/>
	/// of <see cref="ILog"/>
	/// </summary>
	[NoCodeCoverage, UsedImplicitly]
	public static class ILogProviderExtensions
	{
		/// <summary>
		/// Creates new log using the type as name.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static ILog CreateLog<T>(this INamedProvider<ILog> logProvider) where T : class
		{
			return logProvider.Get(typeof (T).Name);
		}
	}
}
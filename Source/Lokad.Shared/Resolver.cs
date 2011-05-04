#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Globalization;
using Lokad.Quality;

namespace Lokad
{
	/// <summary>
	/// Implementation of the <see cref="IResolver"/> that uses delegates
	/// to wire up the resolution logics and wraps all exceptions with the
	/// <see cref="ResolutionException"/>
	/// </summary>
	[Serializable]
	[Immutable]
	public sealed class Resolver : IResolver
	{
		readonly Func<Type, object> _resolver;
		readonly Func<Type, string, object> _namedResolver;

		/// <summary>
		/// Initializes a new instance of the <see cref="Resolver"/> class.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		/// <param name="namedResolver">The named resolver.</param>
		public Resolver(Func<Type, object> resolver, Func<Type, string, object> namedResolver)
		{
			if (resolver == null) throw new ArgumentNullException("resolver");
			if (namedResolver == null) throw new ArgumentNullException("namedResolver");

			_resolver = resolver;
			_namedResolver = namedResolver;
		}

		/// <summary>
		/// Resolves this instance.
		/// </summary>
		/// <typeparam name="TService">The type of the service.</typeparam>
		/// <returns>
		/// requested instance of <typeparamref name="TService"/>
		/// </returns>
		/// <exception cref="ResolutionException">if there is some resolution problem</exception>
		public TService Get<TService>()
		{
			try
			{
				return (TService) _resolver(typeof (TService));
			}
			catch (Exception ex)
			{
			    Type valueType = typeof (TService);
			    throw new ResolutionException(string.Format(CultureInfo.InvariantCulture, "Error while resolving {0}", valueType), ex);
			}
		}

		/// <summary>
		/// Resolves the specified service type.
		/// </summary>
		/// <typeparam name="TService">The type of the service.</typeparam>
		/// <param name="name">The name.</param>
		/// <returns>
		/// requested instance of <typeparamref name="TService"/>
		/// </returns>
		/// <exception cref="ResolutionException">if there is resolution problem</exception>
		public TService Get<TService>(string name)
		{
			try
			{
				return (TService) _namedResolver(typeof (TService), name);
			}
			catch (Exception ex)
			{
			    Type valueType = typeof (TService);
			    throw new ResolutionException(string.Format(CultureInfo.InvariantCulture, "Error while resolving {0} with key '{1}'", valueType, (object) name), ex);
			}
		}
	}
}
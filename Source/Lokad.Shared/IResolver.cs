#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad
{
	/// <summary>
	/// Generic resolution interface for the applications, 
	/// where proper infrastructure could not be setup
	/// </summary>
	/// <remarks>There are no Generic resolution methods 
	/// (like Resolve(Type service)), for the purpose of enforcing 
	/// explicit resolution logics </remarks>
	public interface IResolver
	{
		/// <summary>
		/// Resolves this instance.
		/// </summary>
		/// <typeparam name="TService">The type of the service.</typeparam>
		/// <returns>requested instance of <typeparamref name="TService"/></returns>
		/// <exception cref="ResolutionException">if there is some resolution problem</exception>
		TService Get<TService>();

		/// <summary>
		/// Resolves the specified service type.
		/// </summary>
		/// <typeparam name="TService">The type of the service.</typeparam>
		/// <param name="name">The name.</param>
		/// <returns>requested instance of <typeparamref name="TService"/></returns>
		/// <exception cref="ResolutionException">if there is resolution problem</exception>
		TService Get<TService>(string name);
	}
}
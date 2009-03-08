#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using Mono.Cecil;

namespace Lokad.Quality
{
	/// <summary>
	/// Extension helpers for <see cref="CustomAttribute"/>
	/// </summary>
	public static class CustomAttributeExtensions
	{
		/// <summary>
		/// Checks by full name if the provided <paramref name="attribute"/>
		/// matches the provided <typeparamref name="TAttribute"/>
		/// </summary>
		/// <typeparam name="TAttribute">type to check against</typeparam>
		/// <param name="attribute">attribute to check</param>
		/// <returns>
		/// 	<c>true</c> if the specified attribute matches the type; otherwise, <c>false</c>.
		/// </returns>
		public static bool Is<TAttribute>(this CustomAttribute attribute)
		{
			return attribute.Constructor.DeclaringType.Is<TAttribute>();
		}
	}
}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using Mono.Cecil;

namespace Lokad.Quality
{
	/// <summary>
	/// Helper extensions for the <see cref="ParameterDefinition"/>
	/// </summary>
	public static class ParameterDefinitionExtensions
	{
		/// <summary>
		/// Checks by full name if the provided <paramref name="parameter"/>
		/// matches the provided <typeparamref name="TType"/>
		/// </summary>
		/// <typeparam name="TType">type to check against</typeparam>
		/// <param name="parameter">parameter to check</param>
		/// <returns>
		/// 	<c>true</c> if the specified parameter matches the type; otherwise, <c>false</c>.
		/// </returns>		
		public static bool Is<TType>(this ParameterDefinition parameter)
		{
			return parameter.ParameterType.Is<TType>();
		}
	}
}
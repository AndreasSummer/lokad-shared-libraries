#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Mono.Cecil;

namespace Lokad.Quality
{
	/// <summary>
	/// Helper extensions for the <see cref="TypeReference"/>
	/// </summary>
	public static class TypeReferenceExtensions
	{
		/// <summary>
		/// Determines whether the provided name has name mathcing to the <typeparamref name="TType"/> 
		/// </summary>
		/// <typeparam name="TType">type to compare with</typeparam>
		/// <param name="definition">The definition.</param>
		/// <returns>
		/// 	<c>true</c> if the specified definition matches the provided type, overwise <c>false</c>.
		/// </returns>
		public static bool Is<TType>(this TypeReference definition)
		{
			return definition.FullName == CecilUtil<TType>.MonoName;
		}

		/// <summary>
		/// Determines whether the provided <see cref="TypedReference"/>
		/// has specified attribute (matching is done by full name)
		/// </summary>
		/// <typeparam name="TAttribute">The type of the attribute.</typeparam>
		/// <param name="self">The <see cref="TypeDefinition"/> to check.</param>
		/// <returns>
		/// 	<c>true</c> if the type has the specified attribute otherwise, <c>false</c>.
		/// </returns>
		public static bool Has<TAttribute>(this TypeReference self) where TAttribute : Attribute
		{
			if (typeof(TAttribute) == typeof(SerializableAttribute))
			{
				throw new InvalidOperationException("SerializableAttribute should be checked by inspecting type flags");
			}

			foreach (CustomAttribute attribute in self.CustomAttributes)
			{
				if (attribute.Is<TAttribute>())
					return true;
			}
			return false;
		}
	}
}
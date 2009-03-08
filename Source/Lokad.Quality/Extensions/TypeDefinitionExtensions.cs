#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace Lokad.Quality
{
	/// <summary>
	/// Helper extensions for the <see cref="TypeDefinition"/>
	/// </summary>
	public static class TypeDefinitionExtensions
	{
		/// <summary>
		/// Gets the interfaces.
		/// </summary>
		/// <param name="definition">The definition.</param>
		/// <returns>enumerator over the interfaces</returns>
		public static IEnumerable<TypeReference> GetInterfaces(this TypeDefinition definition)
		{
			return definition.Interfaces.Cast<TypeReference>();
		}

		/// <summary>
		/// Retrieves fields for the <see cref="TypeDefinition"/>
		/// </summary>
		/// <param name="self">The type definition.</param>
		/// <returns>lazy collection of the fields</returns>
		public static IEnumerable<FieldDefinition> GetFields(this TypeDefinition self)
		{
			return self.Fields.Cast<FieldDefinition>();
		}

		/// <summary>
		/// Gets the methods found on this <see cref="TypeDefinition"/>.
		/// Inheritance is not considered.
		/// </summary>
		/// <param name="self">The self.</param>
		/// <returns>lazy collection of methods</returns>
		public static IEnumerable<MethodDefinition> GetMethods(this TypeDefinition self)
		{
			return self.Methods.Cast<MethodDefinition>();
		}
	}
}
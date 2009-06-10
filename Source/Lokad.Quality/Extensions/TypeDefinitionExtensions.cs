#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
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
		/// <param name="definition">The definition.</param>
		/// <returns>lazy collection of methods</returns>
		public static IEnumerable<MethodDefinition> GetMethods(this TypeDefinition definition)
		{
			return definition.Methods.Cast<MethodDefinition>();
		}

		/// <summary>
		/// Gets the constructors found on this <see cref="TypeDefinition"/>.
		/// Inheritance is not considered.
		/// </summary>
		/// <param name="definition">The definition.</param>
		/// <returns>lazy collection of methods</returns>
		public static IEnumerable<MethodDefinition> GetConstructors(this TypeDefinition definition)
		{
			return definition.Constructors.Cast<MethodDefinition>();
		}


		/// <summary>
		/// Resolves the specified definition to <see cref="Type"/>.
		/// </summary>
		/// <param name="definition">The definition.</param>
		/// <returns>.NET Type</returns>
		public static Type Resolve(this TypeDefinition definition)
		{
			var name = string.Format("{0}, {1}", definition.FullName, definition.Module.Assembly.Name);
			return Type.GetType(name, true);
		}


		/// <summary>
		/// Verifies that the specified <paramref name="check"/> is satisfied
		/// </summary>
		/// <param name="definitions">The definitions.</param>
		/// <param name="check">The check.</param>
		/// <returns>the same enumerable</returns>
		/// <exception cref="QualityException">if any definitions do not pass the check</exception>
		public static IEnumerable<TypeDefinition> Should(this IEnumerable<TypeDefinition> definitions,
			Predicate<TypeDefinition> check)
		{
			QualityAssert.TypesPass(definitions, check);
			return definitions;
		}

		/// <summary>
		/// Selects only types with the specified attribute
		/// </summary>
		/// <typeparam name="TAttribute">The type of the attribute.</typeparam>
		/// <param name="definitions">The definitions.</param>
		/// <returns>filtered sequence</returns>
		public static IEnumerable<TypeDefinition> With<TAttribute>(this IEnumerable<TypeDefinition> definitions) where TAttribute : Attribute
		{
			return definitions.Where(d => d.Has<TAttribute>());
		}
	}
}
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
	using Provider = IProvider<TypeReference, TypeDefinition>;

	/// <summary>
	/// Helper methods to simplify the usage of Mono.Cecil
	/// </summary>
	public static class CecilExtensions
	{
		/// <summary>
		/// Gets all methods for the provided <see cref="TypeDefinition"/>. 
		/// This includes walking all the way up the inheritance chain, excluding
		/// the <see cref="object"/>
		/// </summary>
		/// <param name="self">The <see cref="TypeDefinition"/> to check.</param>
		/// <param name="provider">The resolution provider.</param>
		/// <returns>lazy collection of methods</returns>
		/// <seealso cref="Codebase"/>
		public static IEnumerable<MethodDefinition> GetAllMethods(this TypeDefinition self,
			IProvider<TypeReference, TypeDefinition> provider)
		{
			return self.GetInheritance(provider).SelectMany(p => TypeDefinitionExtensions.GetMethods(p));
		}

		/// <summary>
		/// Gets the inheritance tree of the provided type.
		/// </summary>
		/// <param name="definition">The definition.</param>
		/// <param name="provider">The provider.</param>
		/// <returns>lazy enumerator</returns>
		public static IEnumerable<TypeDefinition> GetInheritance(this TypeDefinition definition,
			IProvider<TypeReference, TypeDefinition> provider)
		{
			var current = definition;
			var interfaces = new HashSet<TypeReference>();
			while (true)
			{
				yield return current;

				interfaces.AddRange(current.GetInterfaces());

				if (current.BaseType == null)
					break;
				if (current.BaseType.FullName == typeof (object).FullName)
					break;

				current = provider.Get(current.BaseType);
			}

			foreach (var reference in interfaces)
			{
				yield return provider.Get(reference);
			}
		}


		/// <summary>
		/// Gets all fields for the provided <see cref="TypeDefinition"/>. 
		/// This includes walking all the way up the inheritance chain, excluding
		/// the <see cref="object"/>
		/// </summary>
		/// <param name="self">The <see cref="TypeDefinition"/> to check.</param>
		/// <param name="provider">The resolution provider.</param>
		/// <returns>lazy collection of fields</returns>
		/// <seealso cref="Codebase"/>
		public static IEnumerable<FieldDefinition> GetAllFields(this TypeDefinition self,
			IProvider<TypeReference, TypeDefinition> provider)
		{
			return self.GetInheritance(provider).SelectMany(p => p.GetFields());
		}
	}
}
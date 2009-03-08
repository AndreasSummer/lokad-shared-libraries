#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Mono.Cecil;

namespace Lokad.Quality
{
	/// <summary>
	/// Helper extensions for the <see cref="AssemblyDefinition"/>
	/// </summary>
	public static class AssemblyDefinitionExtensions
	{
		/// <summary>
		/// Gets the modules.
		/// </summary>
		/// <param name="self">The self.</param>
		/// <returns>list of modules for the provided assembly definition</returns>
		public static IEnumerable<ModuleDefinition> GetModules(this AssemblyDefinition self)
		{
			return self.Modules.Cast<ModuleDefinition>();
		}

		/// <summary>
		/// Gets all assembly references for the modules in the provided <see cref="AssemblyDefinition"/>.
		/// </summary>
		/// <param name="self">The assembly definition to check.</param>
		/// <returns>enumerable of the assembly references</returns>
		/// <remarks>This is a lazy routine</remarks>
		public static IEnumerable<AssemblyNameReference> GetAssemblyReferences(this AssemblyDefinition self)
		{
			return self.GetModules().SelectMany(m => m.AssemblyReferences.Cast<AssemblyNameReference>());
		}

		/// <summary>
		/// Gets all <see cref="TypeDefinition"/> entries for the modules in the provided
		/// <see cref="AssemblyDefinition"/>. 
		/// </summary>
		/// <param name="self">The assembly definition to scan.</param>
		/// <returns>This is a lazy routine</returns>
		public static IEnumerable<TypeDefinition> GetAllTypes(this AssemblyDefinition self)
		{
			return self.Modules.Cast<ModuleDefinition>().SelectMany(m => m.Types.Cast<TypeDefinition>());
		}

		/// <summary>
		/// Gets all the <see cref="TypeDefinition"/> important types for the modules in the
		/// provided <see cref="AssemblyDefinition"/>
		/// </summary>
		/// <param name="self">The <see cref="AssemblyDefinition"/> to check.</param>
		/// <returns>lazy enumerator over the results</returns>
		public static IEnumerable<TypeDefinition> GetTypes(this AssemblyDefinition self)
		{
			return self.Modules
				.Cast<ModuleDefinition>()
				.SelectMany(m => m.Types.Cast<TypeDefinition>())
				.Where(t => !t.Has<CompilerGeneratedAttribute>() && !t.Name.StartsWith("<") && !t.Name.StartsWith("__"));
		}

		/// <summary>
		/// Gets the type references for the modules in the provided <paramref name="assemblyDefinition"/>.
		/// </summary>
		/// <param name="assemblyDefinition">The assembly definition.</param>
		/// <returns>lazy enumerator over the results</returns>
		public static IEnumerable<TypeReference> GetTypeReferences(this AssemblyDefinition assemblyDefinition)
		{
			return assemblyDefinition.Modules
				.Cast<ModuleDefinition>()
				.SelectMany(m => m.TypeReferences.Cast<TypeReference>());
		}
	}
}
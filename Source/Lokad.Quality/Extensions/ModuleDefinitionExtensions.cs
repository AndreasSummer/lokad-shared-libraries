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
	/// Extensions for the <see cref="ModuleDefinition"/>
	/// </summary>
	public static class ModuleDefinitionExtensions
	{
		/// <summary>
		/// Gets the assembly references.
		/// </summary>
		/// <param name="moduleDefinition">The module definition.</param>
		/// <returns>lazy enumerator over the results</returns>
		public static IEnumerable<AssemblyNameReference> GetAssemblyReferences(this ModuleDefinition moduleDefinition)
		{
			return moduleDefinition.AssemblyReferences.Cast<AssemblyNameReference>();
		}

		/// <summary>
		/// Gets the module references.
		/// </summary>
		/// <param name="moduleDefinition">The module definition.</param>
		/// <returns>lazy enumerator over the results</returns>
		public static IEnumerable<ModuleReference> GetModuleReferences(this ModuleDefinition moduleDefinition)
		{
			return moduleDefinition.ModuleReferences.Cast<ModuleReference>();
		}
	}
}
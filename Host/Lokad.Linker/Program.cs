#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using System.Linq;
using Lokad.Quality;
using Mono.Cecil;

namespace Lokad.Linker
{
	class Program
	{
		static void Main(string[] args)
		{
			// dependency chain

			var inputName = args[0];
			var oldName = args[1];
			var linkedName = args[2];
			var outputName = args[3];

			var linked = AssemblyFactory.GetAssembly(linkedName);
			var target = AssemblyFactory.GetAssembly(inputName);
			var old = AssemblyFactory.GetAssembly(oldName);

			foreach (var module in target.GetModules())
			{
				var obsolete = module
					.GetAssemblyReferences(old.Name.FullName)
					.ToArray();

				foreach (var reference in obsolete)
				{
					reference.Name = linked.Name.Name;
					reference.Hash = linked.Name.Hash;
				}
			}

			AssemblyFactory.SaveAssembly(target, outputName);
		}
	}

	public static class Pending
	{
		public static IEnumerable<AssemblyNameReference> GetAssemblyReferences(this ModuleDefinition module, string fullName)
		{
			return module.GetAssemblyReferences().Where(nr => nr.FullName == fullName);
		}
	}
}
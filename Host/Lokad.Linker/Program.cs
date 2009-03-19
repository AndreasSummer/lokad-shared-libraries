using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lokad.Quality;
using Mono.Cecil;

namespace Lokad.Linker
{
	class Program
	{
		static void Main(string[] args)
		{
			// dependency chain

			var targetName = args[0];
			var oldName = args[1];
			var linkedName = args[2];
			var newName = args[3];

			var linked = AssemblyFactory.GetAssembly(linkedName);
			var target = AssemblyFactory.GetAssembly(targetName);
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

			AssemblyFactory.SaveAssembly(target, newName);
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

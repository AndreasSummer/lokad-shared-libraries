#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Linq;
using Mono.Cecil;
using NUnit.Framework;

namespace Lokad.Quality.Test
{
	[TestFixture]
	public sealed class ModuleDefinitionExtensionsTests
	{
		readonly ModuleDefinition[] _moduleDefinitions;
		// ReSharper disable InconsistentNaming

		public ModuleDefinitionExtensionsTests()
		{
			_moduleDefinitions = GlobalSetup.Codebase.Assemblies.SelectMany(a => a.GetModules()).ToArray();
		}

		[Test]
		public void GetAssemblyReferences()
		{
			var referenced = _moduleDefinitions.SelectMany(m => m.GetAssemblyReferences()).ToArray(r => r.Name);

			var expected =
				"mscorlib,Lokad.Quality,Mono.Cecil,System.Core,nunit.framework,Lokad.Shared,Lokad.Shared.Test".Split(',');

			CollectionAssert.AreEquivalent(expected, referenced);
		}
	}
}
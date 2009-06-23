#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;

namespace Lokad.Quality
{
	/// <summary>
	/// Base class that serves as validation target for the Lokad
	/// quality rules
	/// </summary>
	[Immutable]
	public sealed class Codebase : IProvider<TypeReference, TypeDefinition>
	{
		/// <summary>
		/// Methods declared in the assemblies being checked
		/// </summary>
		/// <value>The collection of methods.</value>
		public MethodDefinition[] Methods
		{
			get { return _methods; }
		}

		/// <summary>
		/// Types declared in the assemblies being checked
		/// </summary>
		/// <value>The collection of types.</value>
		public TypeDefinition[] Types
		{
			get { return _types; }
		}

		/// <summary>
		/// Gets the assemblies being checked.
		/// </summary>
		/// <value>The assemblies.</value>
		public AssemblyDefinition[] Assemblies
		{
			get { return _assemblies.Convert(a => a.Value); }
		}

		readonly TypeDefinition[] _types;
		readonly MethodDefinition[] _methods;

		readonly IDictionary<string, TypeDefinition> _typeDictionary;

		readonly AssemblyDefinition[] _references;
		readonly Pair<string, AssemblyDefinition>[] _assemblies;


		/// <summary>
		/// Saves the entire codebase to the specified folder.
		/// </summary>
		/// <param name="path">The path.</param>
		public void SaveTo(string path)
		{
			foreach (var def in _assemblies)
			{
				var fileName = Path.GetFileName(def.Key);
				var fullName = Path.Combine(path, fileName);
				AssemblyFactory.SaveAssembly(def.Value, fullName);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Codebase"/> class.
		/// </summary>
		/// <param name="assembliesToAnalyze">The assemblies to load.</param>
		public Codebase(params string[] assembliesToAnalyze)
		{
			_assemblies = assembliesToAnalyze
				.Convert(n => Tuple.From(Path.GetFileName(n), AssemblyFactory.GetAssembly(n)));

			var defs = _assemblies.Convert(p => p.Value);

			_types = defs
				.SelectMany(m => m.GetTypes())
				.ToArray();

			_methods = _types
				.SelectMany(t => t.GetMethods())
				.ToArray();

			_references = defs
				.SelectMany(m => m
					.GetAssemblyReferences()
					.Select(nr => m.Resolver.Resolve(nr)))
				.ToArray();

			var referencedTypes = _references
				.SelectMany(r => r.GetAllTypes())
				.Where(t => t.IsPublic)
				.ToArray();

			_typeDictionary = new Dictionary<string, TypeDefinition>();

			_types
				.Append(referencedTypes)
				.ForEach(t => _typeDictionary[t.FullName] = t);
		}

		/// <summary>
		/// Gets all the external type references in the codebase.
		/// </summary>
		/// <returns>lazy enumerator over the results.</returns>
		public IEnumerable<TypeReference> GetAllTypeReferences()
		{
			return _assemblies.SelectMany(a => a.Value.GetTypeReferences());
		}

		


		TypeDefinition IProvider<TypeReference, TypeDefinition>.Get(TypeReference key)
		{
			if (key is GenericInstanceType)
			{
				key = (key as GenericInstanceType).ElementType;
			}

			var name = key.FullName;

			TypeDefinition definition;
			if (!_typeDictionary.TryGetValue(name, out definition))
			{
				throw Errors.KeyInvalid(key);
			}
			return definition;
		}

		/// <summary>
		/// Gets the type based on the .NET type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public TypeDefinition Find<T>()
		{
			return _typeDictionary[CecilUtil<T>.MonoName];
		}

		/// <summary>
		/// Looks up the type based on the .NET type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>matching type definition</returns>
		public Maybe<TypeDefinition> Find(Type type)
		{
			return _typeDictionary.GetValue(CecilUtil.GetMonoName(type));
		}

		/// <summary>
		/// Finds the specified reference.
		/// </summary>
		/// <param name="reference">The reference.</param>
		/// <returns>matching type definition</returns>
		public Maybe<TypeDefinition> Find(TypeReference reference)
		{
			return _typeDictionary.GetValue(reference.FullName);
		}
	}
}
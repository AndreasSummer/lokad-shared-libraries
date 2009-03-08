#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Lokad.Quality;

namespace Lokad.ReNamespace
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("Usage: 'InputFolder' 'OutputFolder'");
				Environment.Exit(1);
			}

			var inputFolder = args[0];
			var outputFolder = args[1];

			Directory.CreateDirectory(outputFolder);
			Environment.CurrentDirectory = inputFolder;

			var names = new[] {"Lokad.Shared.dll", "Lokad.Quality.dll", "Lokad.Stack.dll"};
			

			ChangeNamespaces(outputFolder, names.Convert(n => Path.Combine(inputFolder, n)));
		}

		static void ChangeNamespaces(string output, params string[] assemblies)
		{
			var codebase = new Codebase(assemblies);

			var changes = new Dictionary<string, string>();

			foreach (var type in codebase.Types)
			{
				var oldName = type.FullName;
				type.Namespace = type.Namespace.Replace("System", "Lokad");

				if (type.IsPublic)
				{
					changes.Add(oldName, type.FullName);
				}
			}

			foreach (var type in codebase.GetAllTypeReferences())
			{
				if (changes.ContainsKey(type.FullName))
				{
					type.Namespace = type.Namespace.Replace("System", "Lokad");
				}
			}
			codebase.SaveTo(output);

			var docs = assemblies
				.Convert(a => Path.ChangeExtension(a, "xml"));

			foreach (var fileName in docs)
			{
				var text = File.ReadAllText(fileName);
				var builder = new StringBuilder(text);

				foreach (var pair in changes)
				{
					builder.Replace(pair.Key, pair.Value);
				}

				var newPath = Path.Combine(output, Path.GetFileName(fileName));

				using (var writer = File.CreateText(newPath))
				{
					writer.Write(builder.ToString());
				}
			}
		}
	}
}
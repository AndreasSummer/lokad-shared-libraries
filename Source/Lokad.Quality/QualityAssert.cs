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
	/// Helper class for verifying code quality assertions.
	/// </summary>
	public static class QualityAssert
	{
		static QualityException Error(string message, params object[] args)
		{
			return new QualityException(string.Format(message, args));
		}

		/// <summary>
		/// Verifies that every definition in the specified sequence passes <paramref name="check"/>;
		/// </summary>
		/// <param name="definitions">The definitions.</param>
		/// <param name="check">The check.</param>
		/// <exception cref="QualityException">if any definitions fail.</exception>
		public static void TypesPass(IEnumerable<TypeDefinition> definitions, Predicate<TypeDefinition> check)
		{
			var failing = definitions.Where(t => !check(t));
			if (!failing.Any())
				return;

			var types = failing
				.Select(t => t.FullName)
				.Join(Environment.NewLine);

			throw Error("Failing types:\r\n{0}", types);
		}

		/// <summary>
		/// Verifies that every definition in the specified sequence passes <paramref name="check"/>;
		/// </summary>
		/// <param name="definitions">The definitions.</param>
		/// <param name="check">The check.</param>
		/// <exception cref="QualityException">if any definitions fail.</exception>
		public static void MethodsPass(IEnumerable<MethodDefinition> definitions, Predicate<MethodDefinition> check)
		{
			var failing = definitions.Where(t => !check(t));
			if (!failing.Any())
				return;

			var types = failing
				.Select(t => t.ToString())
				.Join(Environment.NewLine);

			throw Error("Failing methods:\r\n{0}", types);
		}
	}
}
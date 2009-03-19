#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Lokad.Quality
{
	/// <summary>
	/// Extensions for the <see cref="MethodDefinition"/>
	/// </summary>
	public static class MethodDefinitionExtensions
	{
		/// <summary>
		/// Gets the instructions for the specified method.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns>enumerator over the instructions within the method</returns>
		public static IEnumerable<Instruction> GetInstructions(this MethodDefinition method)
		{
			if (!method.HasBody)
				return Enumerable.Empty<Instruction>();

			return method.Body.Instructions.Cast<Instruction>();
		}

		/// <summary>
		/// Gets the methods referenced from this <paramref name="definition"/>.
		/// </summary>
		/// <param name="definition">The definition.</param>
		/// <returns>enumerator over the method references</returns>
		public static IEnumerable<MethodReference> GetReferencedMethods(this MethodDefinition definition)
		{
			return definition
				.GetInstructions()
				.Select(i => i.Operand as MethodReference)
				.Where(r => r != null);
		}

		//public static bool Exists(this MethodDefinition method, Predicate<Instruction> instructionCheck)
		//{
		//    return method.GetInstructions().Exists(instructionCheck);
		//}

		/// <summary>
		/// Gets the parameters from the specified <paramref name="definition"/>.
		/// </summary>
		/// <param name="definition">The definition to explore.</param>
		/// <returns>enumerator over the parameters</returns>
		public static IEnumerable<ParameterDefinition> GetParameters(this MethodDefinition definition)
		{
			return definition.Parameters.Cast<ParameterDefinition>();
		}

		/// <summary>
		/// Determines whether the provided <see cref="MethodDefinition"/> 
		/// has specified attribute (matching is done by full name)
		/// </summary>
		/// <typeparam name="TAttribute">The type of the attribute.</typeparam>
		/// <param name="reference">The <see cref="MethodDefinition"/> to check.</param>
		/// <returns>
		/// 	<c>true</c> if the specified attribute is found otherwise, <c>false</c>.
		/// </returns>
		public static bool Has<TAttribute>(this MethodDefinition reference) where TAttribute : Attribute
		{
			foreach (CustomAttribute attribute in reference.CustomAttributes)
			{
				if (attribute.Constructor.DeclaringType.Is<TAttribute>())
					return true;
			}

			return false;
		}


		/// <summary>
		/// Verifies that the specified <paramref name="check"/> is satisfied
		/// </summary>
		/// <param name="definitions">The definitions.</param>
		/// <param name="check">The check.</param>
		/// <returns>the same enumerable</returns>
		/// <exception cref="QualityException">if any definitions do not pass the check</exception>
		public static IEnumerable<MethodDefinition> Should(this IEnumerable<MethodDefinition> definitions,
			Predicate<MethodDefinition> check)
		{
			QualityAssert.MethodsPass(definitions, check);
			return definitions;
		}
	}


}
#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Rules;

namespace Lokad.Quality
{
	/// <summary>
	/// Shared maintainability rules
	/// </summary>
	public static class MaintainabilityRules
	{

		/// <summary>
		/// Ensures that <see cref="NoCodeCoverageAttribute"/> is not used on complex methods.
		/// </summary>
		/// <param name="maxInstructions">Method is considered to be complex when number
		/// of IL instructions in it exceeds the <paramref name="maxInstructions"/>.</param>
		/// <returns>new rule instance</returns>
		public static Rule<Codebase> Ncca_Is_Used_Properly(int maxInstructions)
		{
			return (codebase, scope) => Ncca_Is_Used_Properly(codebase, scope, maxInstructions);
		}

		static void Ncca_Is_Used_Properly(Codebase codebase, IScope scope, int maxInstructions)
		{
			var methods = codebase.Methods
				.Where(m => m.HasBody && m.Body.Instructions.Count > maxInstructions)
				.Where(m => m.Has<NoCodeCoverageAttribute>() ||
					m.DeclaringType.Has<NoCodeCoverageAttribute>());

			foreach (var method in methods)
			{
				scope.Error("Method is too complex to be marked with NCCA: {0}", method);
			}
		}


		/// <summary>
		/// Ensures that classes marked with <see cref="ImmutableAttribute"/> have only
		/// readonly fields
		/// </summary>
		/// <param name="codebase">The codebase to run against.</param>
		/// <param name="scope">The scope to report to.</param>
		public static void Immutable_Types_Should_Be_Immutable(Codebase codebase, IScope scope)
		{
			var decorated = codebase.Types
				.Where(t => t.Has<ImmutableAttribute>());

			var failing = decorated
				.Where(t => t.GetAllFields(codebase)
					.Count(f => !f.IsInitOnly && !f.IsStatic) > 0);

			foreach (var definition in failing)
			{
				scope.Error("Type should be immutable: {0}", definition);
			}
		}
	}
}
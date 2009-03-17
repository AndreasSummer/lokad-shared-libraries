#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if !SILVERLIGHT2

using Lokad.Quality;
using Lokad.Rules;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class CodeQualityTests
	{
		internal static readonly Codebase Codebase = new Codebase("Lokad.Shared.dll");

		[Test]
		public void Maintainability()
		{
			Scope.Validate(Codebase,
				MaintainabilityRules.Immutable_Types_Should_Be_Immutable,
				MaintainabilityRules.Ncca_Is_Used_Properly(38));
		}
	}
}

#endif
#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion
#if !SILVERLIGHT2
using System.Rules;
using Lokad.Quality;

using NUnit.Framework;


namespace System
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
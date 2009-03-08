#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace System.Rules
{
	static class BusinessRules
	{
		// actual rules.

		// we assume that good citizenship is enforced (no need to check for nulls)
		public static void NameRules(string value, IScope scope)
		{
			// rules are plain .NET code
			if (value.Length == 0) scope.Error("String can not be empty");
			if (value.Length > 2500) scope.Error("Length must be less than 2500");
		}

		public static void SanitationRules(string value, IScope scope)
		{
			if (value.Contains("-")) scope.Error("Can not contain '-'");
		}

		public static void VisitorRules(Visitor visitor, IScope scope)
		{
			// rules can validate members in the same manner
			scope.Validate(visitor.Name, "Name", NameRules);
			scope.Validate(visitor.Programs, "Programs", ProgramsRules);
		}

		public static void ProgramsRules(Program[] programs, IScope scope)
		{
			// Validating items of a collection
			if (programs.Length > 2500)
			{
				scope.Error("Only 2500 programs are allowed");
			}
			else
			{
				// every item will be validated (or only till the first exception hits,
				// that's for the scope to decide
				scope.ValidateInScope(programs, ProgramRules);
			}
		}

		public static void ProgramRules(Program program, IScope scope)
		{
			// custom logic
			if (!program.Active) scope.Error("Program must be active");
			// passing member validation down to the next ruleset 
			// (note, that we have multiple rulesets here)
			scope.Validate(program.Name, "Name", NameRules, SanitationRules);
		}
	}
}
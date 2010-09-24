#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Linq;
using Lokad.Testing;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class RuleExtensionsTests
	{
		[Test]
		public void Test_Valid_Nested_Object()
		{
			Enforce.That(Visitor.CreateValid(), BusinessRules.VisitorRules);
		}

		[Test]
		public void Test_Valid_Collection()
		{
			var array = Range.Array(10, i => Program.CreateValid());
			Enforce.That(array, BusinessRules.ProgramRules);
		}

		[Test, Expects.RuleException]
		public void Nested_Object_Throws_On_Invalid_Collection()
		{
			var v = Visitor.CreateValid();
			v.Programs = v.Programs.Append(new Program());

			Enforce.That(v, BusinessRules.VisitorRules);
		}

		[Test, Expects.ArgumentException]
		public void Null_Collection_Is_Detected()
		{
			var v = Visitor.CreateValid();
			v.Programs = null;
			Enforce.Argument(() => v, BusinessRules.VisitorRules);
		}

		[Test, Expects.RuleException]
		public void Null_Object_Is_Detected()
		{
			var v = Visitor.CreateValid();
			v.Programs[0] = null;
			Enforce.That(v, BusinessRules.VisitorRules);
		}

		public class Domain
		{
			public string[] Properties { get; set; }
			public double[] Values { get; set; }
			public string Property { get; set; }
			public double Value { get; set; }
		}

		[Test]
		public void Usage_patterns_for_building_rules()
		{
			var domain = new Domain
				{
					Properties = Range.Array(10, i => i.ToString()),
					Values = Range.Array(10, i => (double) (i + 1)),
					Property = "Something",
					Value = Math.PI
				};

			Enforce.That(() => domain, Usage_Rule);
			Scope.GetMessages(new Domain(), "domain", Usage_Rule);
		}

		static void Usage_Rule(Domain domain, IScope scope)
		{
			scope.Validate(domain.Property, "Property", StringIs.NotEmpty);
			scope.Validate(domain.Value, "Value", Is.NotDefault);

			scope.ValidateMany(domain.Properties, "Properties", StringIs.NotEmpty);
			//scope.ValidateMany(domain.Values, "Values", 100, Is.NotDefault);

			scope.ValidateMany(domain.Values, "Values", Is.NotDefault);
			//scope.ValidateMany(domain.Values, "Values", 100, Is.NotDefault);

			scope.Validate(() => domain.Property, StringIs.NotEmpty);
			scope.Validate(() => domain.Value, Is.NotDefault);

			// medium trust
			scope.Validate(domain, d => d.Property, StringIs.NotEmpty);
			scope.Validate(domain, d => d.Value, Is.NotDefault);

			scope.ValidateMany(() => domain.Properties, StringIs.NotEmpty);
			scope.ValidateMany(() => domain.Values, Is.NotDefault);

			// medium trust
			scope.ValidateMany(domain, d => d.Properties, StringIs.NotEmpty);
			scope.ValidateMany(domain, d => d.Values, Is.NotDefault);

			scope.ValidateInScope(domain, (domain1, scope1) => { });
			scope.ValidateInScope(new[] {domain}, (domain1, scope1) => { });
		}
	}
}
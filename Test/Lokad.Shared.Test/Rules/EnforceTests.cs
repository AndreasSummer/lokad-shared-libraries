#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using System.Linq;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class EnforceTests
	{
		// ReSharper disable InconsistentNaming

		static void ClassUsagePatterns<T>(T param, IEnumerable<T> collection, Rule<T> rule) where T : class
		{
			Enforce.Argument(() => param);
			Enforce.Argument(() => param, rule);
			Enforce.Argument(() => collection, rule);
			Enforce.Arguments(() => param, () => param);
			Enforce.Arguments(() => param, () => param, () => param);
			Enforce.Arguments(() => param, () => param, () => param, () => param);
			Enforce.Arguments(() => param, () => param, () => param, () => param, () => param);


			Enforce.That(() => param);
			Enforce.That(() => param, rule);
			Enforce.That(() => collection, rule);

			Enforce.That(param, rule);
			Enforce.That(collection, rule);

			var local = param;
			Enforce.NotNull(() => local);
		}

		[Test]
		public void Simple_usage_patterns()
		{
			Enforce.That(true);
			Enforce.That(true, "Check");
			Enforce.That(true, "Format {0}", 1);

			var value = new object();
			Enforce.NotNull(value);
			Enforce.NotNull(value, "value");
		}

		static void ValueUsagePatterns<T>(T param, IEnumerable<T> collection, Rule<T> rule)
		{
			Enforce.Argument(() => param, rule);
			Enforce.Argument(() => collection, rule);

			Enforce.That(() => param);
			Enforce.That(() => param, rule);
			Enforce.That(() => collection, rule);

			Enforce.That(param, rule);
			Enforce.That(collection, rule);
		}


		static void StringUsagePatterns(string param, Rule<string> rule)
		{
			Enforce.ArgumentNotEmpty(() => param);
			Enforce.Argument(() => param, rule);
		}

		[Test]
		public void Test()
		{
			var i = new
				{
					Value = 10
				};
			ClassUsagePatterns(i, Enumerable.Repeat(i, 4), (p, scope) =>
				scope.Validate(i.Value, "Value", Is.AtLeast(10)));
			ValueUsagePatterns(1, Range.Create(30), (int1, scope) => { });
			StringUsagePatterns("1", StringIs.Without('+'));
		}

		[Test, Expects.ArgumentException]
		public void ArgumentNotEmpty_On_Null()
		{
			var v = string.Empty;
			Enforce.ArgumentNotEmpty(() => v);
		}

		[Test, Expects.ArgumentNullException]
		public void ArgumentNotEmpty_On_Empty()
		{
			// ReSharper disable ConvertToConstant
			string v = null;
			// ReSharper restore ConvertToConstant
			Enforce.ArgumentNotEmpty(() => v);
		}

		[Test, Expects.InvalidOperationException]
		public void NotNull_variable()
		{
			object arg = null;
			Enforce.NotNull(() => arg);
		}

		const string Value = null;

		[Test, Expects.InvalidOperationException]
		public void NotNull_handles_bad_arguments()
		{
			Enforce.NotNull(() => Value);
		}

		[Test, Expects.ArgumentNullException]
		public void Argument_handles_bad_references()
		{
			Enforce.Argument(() => Value);
		}

		[Test, Expects.ArgumentNullException]
		public void ArgumentNotEmpty_handles_bad_references()
		{
			Enforce.ArgumentNotEmpty(() => Value);
		}
	}
}
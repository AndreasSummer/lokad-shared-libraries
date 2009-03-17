#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Lokad.Diagnostics.CodeAnalysis;

namespace Lokad.Rules
{
	/// <summary>
	/// Helper class to simplify testing of rules.
	/// </summary>
	/// <typeparam name="TTarget">The type of the target for rules.</typeparam>
	[Immutable]
	public sealed class RuleAssert<TTarget>
	{
		readonly Rule<TTarget>[] _rules;

		/// <summary>
		/// Initializes a new instance of the <see cref="RuleAssert{TTarget}"/> class.
		/// </summary>
		/// <param name="rules">The rules.</param>
		public RuleAssert(params Rule<TTarget>[] rules)
		{
			Enforce.Argument(() => rules);
			_rules = rules;
		}

		/// <summary>
		/// Expects that every single item in <paramref name="testCases"/>
		/// returns <see cref="RuleLevel.Error"/>.
		/// </summary>
		/// <param name="testCases">The test cases.</param>
		/// <returns>same instance for inlining</returns>
		/// <exception cref="RuleException">when expectation is not met</exception>
		public RuleAssert<TTarget> ExpectError(params TTarget[] testCases)
		{
			Expect(RuleLevel.Error, testCases);
			return this;
		}

		/// <summary>
		/// Expects that every single item in <paramref name="testCases"/>
		/// returns <see cref="RuleLevel.Warn"/>.
		/// </summary>
		/// <param name="testCases">The test cases.</param>
		/// <returns>same instance for inlining</returns>
		/// <exception cref="RuleException">when expectation is not met</exception>
		public RuleAssert<TTarget> ExpectWarn(params TTarget[] testCases)
		{
			Expect(RuleLevel.Warn, testCases);
			return this;
		}

		/// <summary>
		/// Expects that every single item in <paramref name="testCases"/>
		/// returns <see cref="RuleLevel.None"/>.
		/// </summary>
		/// <param name="testCases">The test cases.</param>
		/// <returns>same instance for inlining</returns>
		/// <exception cref="RuleException">when expectation is not met</exception>
		public RuleAssert<TTarget> ExpectNone(params TTarget[] testCases)
		{
			Expect(RuleLevel.None, testCases);
			return this;
		}

		void Expect(RuleLevel level, IEnumerable<TTarget> testCases)
		{
			foreach (var testCase in testCases)
			{
				var messages = Scope.GetMessages(testCase, "testCase", _rules);

				if (level != messages.Level)
				{
					var builder = new StringBuilder();
					builder.AppendFormat("Expected '{0}', but got '{1}'.{2}", level, messages.Level, Environment.NewLine);

					builder.AppendFormat("Value: {0}{1}", testCase, Environment.NewLine);

					if (messages.Count > 0)
					{
						builder.Append("Messages:");
						foreach (var message in messages)
						{
							builder.AppendLine().Append(message);
						}
					}
					throw new RuleException(builder.ToString(), "rule");
				}
			}
		}
	}

	/// <summary> Helper class to simplify testing with rules. </summary>
	public static class RuleAssert
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RuleAssert{TTarget}"/> class.
		/// </summary>
		/// <param name="rules">The rules.</param>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <returns>new instance of the rule tester.</returns>
		public static RuleAssert<TTarget> For<TTarget>(params Rule<TTarget>[] rules)
		{
			Enforce.Argument(() => rules);
			return new RuleAssert<TTarget>(rules);
		}


		/// <summary>
		/// Expects that the <paramref name="rules"/> return <see cref="RuleLevel.Error"/>,
		/// when executed against the <paramref name="testCase"/>.
		/// </summary>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="testCase">The test case.</param>
		/// <param name="rules">The rules.</param>
		/// <exception cref="RuleException">when the expectation is not met.</exception>
		public static void IsError<TTarget>(TTarget testCase, params Rule<TTarget>[] rules)
		{
			For(rules).ExpectError(testCase);
		}

		/// <summary>
		/// Expects that the <paramref name="rules"/> return <see cref="RuleLevel.None"/>,
		/// when executed against the <paramref name="testCase"/>.
		/// </summary>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="testCase">The test case.</param>
		/// <param name="rules">The rules.</param>
		/// <exception cref="RuleException">when the expectation is not met.</exception>
		public static void IsNone<TTarget>(TTarget testCase, params Rule<TTarget>[] rules)
		{
			For(rules).ExpectNone(testCase);
		}

		/// <summary>
		/// Expects that the <paramref name="rules"/> return <see cref="RuleLevel.Warn"/>,
		/// when executed against the <paramref name="testCase"/>.
		/// </summary>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="testCase">The test case.</param>
		/// <param name="rules">The rules.</param>
		/// <exception cref="RuleException">when the expectation is not met.</exception>
		public static void IsWarn<TTarget>(TTarget testCase, params Rule<TTarget>[] rules)
		{
			For(rules).ExpectWarn(testCase);
		}

		/// <summary>
		/// Checks that the specified <paramref name="argumentReference"/> 
		/// passes the check specified in <paramref name="expressions"/>
		/// </summary>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="argumentReference">The argument reference.</param>
		/// <param name="expressions">The expressions to check against.</param>
		/// <exception cref="RuleException">If the check fails</exception>
		public static void That<TTarget>(Func<TTarget> argumentReference,
			params Expression<Predicate<TTarget>>[] expressions)
		{
			Enforce.That(argumentReference, expressions.Convert(e => Is.True(e)));
		}

		/// <summary>
		/// Determines whether the specified expression is true.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <exception cref="RuleException">if the check fails</exception>
		public static void IsTrue(Expression<Func<bool>> expression)
		{
			Enforce.With<RuleException>(expression.Compile()(), "Expression should return 'true': {0}.", expression.Body);
		}

		/// <summary>
		/// Determines whether the specified expression is true.
		/// </summary>
		/// <param name="expression">The expression.</param>
		public static void IsFalse(Expression<Func<bool>> expression)
		{
			Enforce.With<RuleException>(!expression.Compile()(), "Expression should return 'false': {0}.", expression.Body);
		}
	}
}
#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using Lokad.Rules;

namespace Lokad.Testing
{
	/// <summary>
	/// 	Helper class for testing equality of Lokad model classes and printing out the detailed errors
	/// </summary>
	public static class ModelAssert
	{
		/// <summary>
		/// 	Asserts that the two models are equal
		/// </summary>
		/// <typeparam name="TModel">
		/// 	The type of the model.
		/// </typeparam>
		/// <param name="expected">The expected value.</param>
		/// <param name="actual">The actual value.</param>
		/// <param name="format">
		/// 	The format for the exception message.
		/// </param>
		/// <param name="args">
		/// 	The arguments for the exception message.
		/// </param>
		/// <exception cref="FailedAssertException">When check fails</exception>
		public static void AreEqual<TModel>(TModel expected, TModel actual, string format, params object[] args)
		{
			ModelEqualityTester.ThrowIfNotModel<TModel>();
			var messages = GetEqualityMessages(expected, actual);

			if (!messages.IsSuccess)
			{
				var rules = new RuleException(messages);
				throw new FailedAssertException(string.Format(format, args), rules);
			}
		}

		/// <summary>
		/// 	Asserts that the two models are equal
		/// </summary>
		/// <typeparam name="TModel">
		/// 	The type of the model.
		/// </typeparam>
		/// <param name="expected">The expected value.</param>
		/// <param name="actual">The actual value.</param>
		/// <exception cref="FailedAssertException">When check fails</exception>
		public static void AreEqual<TModel>(TModel expected, TModel actual)
		{
			AreEqual(expected, actual, "Models of type '{0}' should be equal.", typeof(TModel).Name);
		}


		/// <summary>
		/// 	Asserts that the two models are not equal
		/// </summary>
		/// <typeparam name="TModel">
		/// 	The type of the model.
		/// </typeparam>
		/// <param name="expected">The expected value.</param>
		/// <param name="actual">The actual value.</param>
		/// <param name="format">
		/// 	The format for the exception message.
		/// </param>
		/// <param name="args">
		/// 	The arguments for the exception message.
		/// </param>
		/// <exception cref="FailedAssertException">When check fails</exception>
		public static void AreNotEqual<TModel>(TModel expected, TModel actual, string format, params object[] args)
		{
			ModelEqualityTester.ThrowIfNotModel<TModel>();
			var messages = GetEqualityMessages(expected, actual);

			if (messages.IsSuccess)
			{
				throw new FailedAssertException(string.Format(format, args));
			}
		}

		/// <summary>
		/// 	Asserts that the two models are not equal
		/// </summary>
		/// <typeparam name="TModel">
		/// 	The type of the model.
		/// </typeparam>
		/// <param name="expected">The expected value.</param>
		/// <param name="actual">The actual value.</param>
		/// <exception cref="FailedAssertException">When check fails</exception>
		public static void AreNotEqual<TModel>(TModel expected, TModel actual)
		{
			AreNotEqual(expected, actual, "Models of type '{0}' should not be equal.", typeof(TModel).Name);
		}

		static RuleMessages GetEqualityMessages<TModel>(TModel expected, TModel actual)
		{
			var name = typeof (TModel).Name;
			return Scope.GetMessages(name, scope =>
				{
					var result = ModelEqualityTester.TestEquality(scope, expected, actual);
					if (!result)
					{
						scope.Error("Equality check has failed");
					}
				});
		}


		/// <summary>
		/// 	Asserts that the two model collections are equal
		/// </summary>
		/// <typeparam name="TModel">
		/// 	The type of the model.
		/// </typeparam>
		/// <param name="expected">The expected collection.</param>
		/// <param name="actual">The actual collection.</param>
		/// <exception cref="FailedAssertException">When check fails</exception>
		public static void AreEqualMany<TModel>(ICollection<TModel> expected, ICollection<TModel> actual)
		{
			AreEqualMany(
				expected, 
				actual, 
				"Models of type '{0}' should be equal.", 
				typeof(TModel).Name);
		}


		/// <summary>
		/// Asserts that the two model collections are equal
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="expected">The expected collection.</param>
		/// <param name="actual">The actual collection.</param>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		/// <exception cref="FailedAssertException">When check fails</exception>
		public static void AreEqualMany<TModel>(
			ICollection<TModel> expected, 
			ICollection<TModel> actual,
			string format, params object[] args)
		{
			ModelEqualityTester.ThrowIfNotModel<TModel>();

			var messages = GetEqualityMessages(expected, actual);

			if (!messages.IsSuccess)
			{
				var rules = new RuleException(messages);
				var message = string.Format(format, args);
				throw new FailedAssertException(message, rules);
			}
		}
	}
}
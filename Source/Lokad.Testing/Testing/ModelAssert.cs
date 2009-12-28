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
			RunEqualityCheck(expected, actual, string.Format(format, args));
		}

		static void RunEqualityCheck<TModel>(TModel expected, TModel actual, string text)
		{
			var name = typeof (TModel).Name;
			var messages = Scope.GetMessages(name, scope =>
				{
					var result = ModelEqualityTester.TestEquality(scope, expected, actual);
					if (!result)
					{
						scope.Error("Equality check has failed");
					}
				});

			if (!messages.IsSuccess)
			{
				var rules = new RuleException(messages);
				throw new FailedAssertException(text, rules);
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
			ModelEqualityTester.ThrowIfNotModel<TModel>();
			var message = string.Format("Models of type '{0}' should be equal.", typeof (TModel).Name);
			RunEqualityCheck(expected, actual, message);
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
			ModelEqualityTester.ThrowIfNotModel<TModel>();
			var message = string.Format("Models of type '{0}' should be equal.", typeof(TModel).Name);
			RunEqualityCheck(expected, actual, message);
		}
	}
}
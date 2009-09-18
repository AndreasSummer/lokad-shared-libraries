#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Linq.Expressions;

namespace Lokad.Testing
{
	/// <summary>
	/// Extends <see cref="Result{TValue,TError}"/> for the purposes of testing
	/// </summary>
	public static class ExtendResult2
	{
		/// <summary>
		/// Asserts that the result is equal to some error
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="error">The error.</param>
		/// <returns>same result instance for further inlining</returns>
		public static Result<TValue, TError> ShouldFailWith<TValue, TError>(this Result<TValue, TError> result, TError error)
		{
			Assert.IsFalse(result.IsSuccess, "Result should be a failure");

			Assert.IsTrue(result.Error.Equals(error),
							"Result should have expected failure{0}Expected: {1}.{0}Was: {2}.",
							Environment.NewLine, error, result.Error);

			return result;
		}

		/// <summary>
		/// Asserts that the result is equal to some error
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <param name="result">The result.</param>
		/// <returns>same result instance for further inlining</returns>
		public static Result<TValue, TError> ShouldFail<TValue, TError>(this Result<TValue, TError> result)
		{
			Assert.IsFalse(result.IsSuccess, "Result should be a failure");


			return result;
		}

		/// <summary>
		/// Asserts that the result is valid and equal to some value
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		/// <returns>
		/// same result instance for further inlining
		/// </returns>
		public static Result<TValue, TError> ShouldPassWith<TValue, TError>(this Result<TValue, TError> result, TValue value)
		{
			Assert.IsTrue(result.IsSuccess, "Result should be a success");
			Assert.IsTrue(result.Value.Equals(value), "Result should have value: {0}", value);

			return result;
		}

		/// <summary>
		/// Checks that the result has a value matching to the provided expression in tests.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="expression">The expression.</param>
		/// <returns>same result instance for inlining</returns>
		public static Result<TValue, TError> ShouldPassCheck<TValue, TError>(this Result<TValue, TError> result,
			Expression<Func<TValue, bool>> expression)
		{
			Assert.IsTrue(result.IsSuccess, "result should be valid");
			var check = expression.Compile();
			Assert.IsTrue(check(result.Value), "Expression should be true: '{0}'.", expression.Body.ToString());
			return result;
		}

		/// <summary>
		/// Ensures that the result is a success in tests.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <param name="result">The result.</param>
		/// <returns>same result instance for inlining</returns>
		public static Result<TValue, TError> ShouldPass<TValue, TError>(this Result<TValue, TError> result)
		{
			Assert.IsTrue(result.IsSuccess, "Result should be valid");
			return result;
		}
	}
}
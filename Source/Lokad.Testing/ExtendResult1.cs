#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lokad.Quality;

namespace Lokad.Testing
{
	/// <summary>
	/// Extends <see cref="Result{T}"/> for the purposes of testing
	/// </summary>
	[UsedImplicitly]
	public static class ExtendResult1
	{
		/// <summary>
		/// Asserts that the result is equal to some error
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="result">The result.</param>
		/// <returns>same result instance for further inlining</returns>
		public static Result<TValue> ShouldFail<TValue>(this Result<TValue> result)
		{
			Assert.IsFalse(result.IsSuccess, "Result should be a failure");
			return result;
		}

		/// <summary>
		/// Asserts that the result is equal to some error
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="error">The error.</param>
		/// <returns>
		/// same result instance for further inlining
		/// </returns>
		public static Result<TValue> ShouldFailWith<TValue>(this Result<TValue> result, string error)
		{
			Assert.IsFalse(result.IsSuccess, "result should be a failure");
			Assert.IsTrue(result.Error == error, "Error should be equal to '{0}'", error);
			return result;
		}

		/// <summary>
		/// Asserts that the result is equal to some error
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <typeparam name="TEnum">The type of the enum describing the failure.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="enum">The @enum.</param>
		/// <returns>
		/// same result instance for further inlining
		/// </returns>
		public static Result<TValue> ShouldFailWith<TValue, TEnum>(this Result<TValue> result, TEnum @enum)
			where TEnum : struct
		{	
			var s = EnumUtil.ToIdentifier(@enum);
			return result.ShouldFailWith(s);
		}


		///// <summary>
		///// Asserts that the result is equal to some error
		///// </summary>
		///// <typeparam name="TValue">The type of the value.</typeparam>
		///// <typeparam name="TError">The type of the error.</typeparam>
		///// <param name="result">The result.</param>
		///// <param name="error">The error.</param>
		///// <returns>same result instance for further inlining</returns>
		//public static Result<TValue> ShouldFail<TValue, TError>(this Result<TValue> result, TError error)
		//    where TError : struct
		//{
		//    Assert.IsFalse(result.IsSuccess, "Result should be a failure");

		//    var unwrappedError = typeof(TError).Name + "_" + error;

		//    Assert.IsTrue(result.Error.Equals(unwrappedError),
		//        "Result should have expected failure{0}Expected: {1}.{0}Was: {2}.",
		//        Environment.NewLine, error, result.Error);

		//    return result;
		//}


		/// <summary>
		/// Asserts that the result is valid and equal to some value
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		/// <returns>
		/// same result instance for further inlining
		/// </returns>
		public static Result<TValue> ShouldPassWith<TValue>(this Result<TValue> result, TValue value)
		{
			if (!result.IsSuccess)
				Assert.Fail("Result should be valid. It had error instead: '{0}'", result.Error);

			var equatable = value as IEquatable<TValue>;

			if (equatable != null)
			{
				Assert.IsTrue(equatable.EqualsTo(result.Value), "Result should be equal to: '{0}'", value);
			}
			else
			{
				Assert.IsTrue(result.Value.Equals(value), "Result should be equal to: '{0}'", value);
			}
			

			return result;
		}

		/// <summary>
		/// Asserts that the result is valid and equal to some value
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		/// <returns>
		/// same result instance for further inlining
		/// </returns>
		public static Result<TValue> ShouldBe<TValue>(this Result<TValue> result, TValue value)
		{
			return ShouldPassWith(result, value);
		}

		/// <summary>
		/// Asserts that the result is equal to some error
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="error">The error.</param>
		/// <returns>
		/// same result instance for further inlining
		/// </returns>
		public static Result<TValue> ShouldBe<TValue>(this Result<TValue> result, string error)
		{
			return ShouldFailWith(result, error);
		}

		/// <summary>
		/// Checks that the result has a value matching to the provided expression in tests.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="expression">The expression.</param>
		/// <returns>same result instance for inlining</returns>
		public static Result<TValue> ShouldPassCheck<TValue>(this Result<TValue> result,
			Expression<Func<TValue, bool>> expression)
		{
			if (!result.IsSuccess)
				Assert.Fail("Result should be valid. It had error instead: '{0}'", result.Error);

			
			var check = expression.Compile();
			Assert.IsTrue(check(result.Value), "Expression should be true: '{0}'.", expression.Body.ToString());
			return result;
		}


		/// <summary>
		/// Checks that the result has a value matching to the provided expression in tests.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="result">The result.</param>
		/// <returns>same result instance for inlining</returns>
		public static Result<TValue> ShouldPass<TValue>(this Result<TValue> result)
		{
			if (!result.IsSuccess)
				Assert.Fail("Result should be valid. It had error instead: '{0}'", result.Error);
			
			return result;
		}
		

		/// <summary>
		/// Ensures that the specified collections are equal in tests.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="anotherCollection">Another collection.</param>
		/// <returns>same instance for inlining</returns>
		public static ICollection<TValue> ShouldBeEqualTo<TValue>(this ICollection<TValue> collection,
			ICollection<TValue> anotherCollection)
			where TValue : IEquatable<TValue>
		{
			Assert.IsTrue(collection.EqualsTo(anotherCollection), "Collections should be equal");
			return collection;
		}

		/// <summary>
		/// Ensures that the specified collection is empty in tests.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="collection">The collection.</param>
		/// <returns>same instance for inlining</returns>
		public static ICollection<TValue> ShouldBeEmpty<TValue>(this ICollection<TValue> collection)
			where TValue : IEquatable<TValue>
		{
			Assert.IsTrue(collection.Count == 0, "collection should be empty");
			return collection;
		}
	}
}
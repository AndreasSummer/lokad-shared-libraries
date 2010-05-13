#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Linq;
using Lokad.Rules;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class Result1Tests
	{
		// ReSharper disable InconsistentNaming

		[Test, Expects.InvalidOperationException]
		public void Access_Error()
		{
			var result = Result<int>.CreateError("Failed");
			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(result.Error, "Failed");
			Assert.IsNull(result.Value, "This should fail");
		}

		[Test, Expects.ArgumentNullException]
		public void Reference_Value_Is_Checked_For_Null()
		{
			// if you have R#, this should be highlighted
			Result.CreateSuccess(default(object));
		}

		[Test]
		public void Value_can_be_default()
		{
			Result.CreateSuccess(default(int));
		}

		[Test]
		public void Implicit_value_conversion()
		{
			Result<int> result = 10;
			RuleAssert.IsTrue(() => result.IsSuccess);
		}

		readonly Result<int> ResultSuccess = 10;
		readonly Result<int> ResultError = Result<int>.CreateError("Error");

		[Test]
		public void Apply()
		{
			ResultError.Apply(i => Assert.Fail());
			int applied = 0;
			ResultSuccess.Apply(i => applied = i);
			Assert.AreEqual(10, applied);
		}

		[Test]
		public void Convert()
		{
			Assert.AreEqual(Result.CreateSuccess("10"), ResultSuccess.Convert(i => i.ToString()), "#1");
			Assert.AreEqual(Result<string>.CreateError("Error"), ResultError.Convert(i => i.ToString()),"#2");
		}

		[Test]
		public void Combine()
		{
			var error1 = Result<int>.CreateError("E1");
			var error1s = Result<string>.CreateError("E1");
			Func<int, Result<string>> fails = i => { throw new InvalidOperationException(); };

			Assert.AreEqual(error1s, error1.Combine(fails));
			Assert.AreEqual(error1s, ResultSuccess.Combine(i => error1s));
			Assert.AreEqual(Result.CreateSuccess("10"), ResultSuccess.Combine(i => Result.CreateSuccess(i.ToString())));
		}

		[Test]
		public void ToMaybe()
		{
			Assert.AreEqual(Maybe<int>.Empty, ResultError.ToMaybe());
			Assert.AreEqual(Maybe.From(10), ResultSuccess.ToMaybe());

			Assert.AreEqual(Maybe<int>.Empty, ResultError.ToMaybe(i => i));
			Assert.AreEqual(Maybe.From(10), ResultSuccess.ToMaybe(i => i));
		}

		[Test]
		public void Create_formatted_error()
		{
			var result = Result<int>.CreateError("Error {0}", 1);
			Assert.AreEqual("Error 1", result.Error);
		}

		[Test]
		public void Equality_members()
		{
			// Silverlight does not contain Hashset
			var hashset = new[] { ResultSuccess }.ToDictionary(r => r);

			Assert.IsTrue(hashset.ContainsKey(ResultSuccess));
			Assert.IsFalse(hashset.ContainsKey(ResultError));
			Assert.IsTrue(hashset.ContainsKey(10));
		}

		static void Throw(string message)
		{
			throw new InvalidOperationException();
		}

		[Test]
		public void Success_with_apply_handle()
		{
			var val = 0;
			ResultSuccess
				.Apply(x => val = x)
				.Handle(Throw)
				.Apply(x => val += x);

			Assert.AreEqual(ResultSuccess.Value * 2, val);
		}

		[Test, Expects.InvalidOperationException]
		public void Failure_with_apply_handle()
		{
			ResultError
				.Apply(x => Assert.Fail())
				.Handle(Throw);
		}

		[Test]
		public void ExposeException()
		{
			var exception = ResultSuccess.ExposeException(s => Errors.InvalidOperation("should not fail"));
			Assert.AreEqual(exception, ResultSuccess.Value);
		}

		[Test, Expects.InvalidOperationException]
		public void ExposeException_with_failure()
		{
			ResultError.ExposeException(s => Errors.InvalidOperation(s));
		}
	}
}
using System;
using Lokad.Diagnostics;
using Lokad.Rules;
using Lokad.Testing;
using NUnit.Framework;
using System.Linq;

namespace Lokad
{
	using MyResult = Result<string, Result2Tests.Failure>;

	[TestFixture]
	public sealed class Result2Tests
	{
		// ReSharper disable InconsistentNaming
		
		public enum Failure
		{
			None,
			FatalError,
			ItIsRaining
		}

		[Test, Expects.InvalidOperationException]
		public void Access_Success()
		{
			var result = MyResult.CreateSuccess("value");
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual("value", result.Value);
			Assert.IsNull(result.Error, "this should fail");
		}

		[Test, Expects.InvalidOperationException]
		public void Access_Error()
		{
			var result = MyResult.CreateError(Failure.ItIsRaining);
			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(result.Error, Failure.ItIsRaining);
			Assert.IsNull(result.Value, "This should fail");
		}

		[Test, Expects.ArgumentNullException]
		public void Reference_Value_Is_Checked_For_Null()
		{
			// if you have R#, this should be highlighted
			MyResult.CreateSuccess(default(string));
		}

		[Test]
		public void Value_Can_Be_Default()
		{
			Result<int,int>.CreateSuccess(default(int));
		}

		[Test]
		public void Implicit_value_Conversion()
		{
			MyResult result = "value";
			RuleAssert.IsTrue(() => result.IsSuccess);
		}

		[Test]
		public void Implicit_error_conversion()
		{
			MyResult result = Failure.FatalError;
			RuleAssert.IsFalse(() => result.IsSuccess);

		}


		readonly MyResult ResultSuccess = "Hi";
		readonly MyResult ResultError = Failure.ItIsRaining;

		[Test]
		public void Apply_with_error()
		{
			ResultError.Apply(i => Assert.Fail());
		}

		[Test]
		public void Apply_with_value()
		{
			var applied = string.Empty;
			ResultSuccess.Apply(i => applied = i);
			Assert.AreEqual(ResultSuccess.Value, applied);
		}

		[Test]
		public void Convert_and_equal()
		{
			Assert.AreEqual(ResultSuccess, ResultSuccess.Convert(i => i.ToString()), "#1");
			Assert.AreEqual(ResultError, ResultError.Convert(i => i.ToString()), "#2");
		}

		[Test]
		public void Combine()
		{
			var error1 = MyResult.CreateError(Failure.ItIsRaining);
			var error1s = MyResult.CreateError(Failure.ItIsRaining);
			Func<string, MyResult> fails = i => { throw new InvalidOperationException(); };

			Assert.AreEqual(error1s, error1.Combine(fails));
			Assert.AreEqual(error1s, ResultSuccess.Combine(i => error1s));
			Assert.AreEqual(MyResult.CreateSuccess("Hi!"), ResultSuccess.Combine(i => MyResult.CreateSuccess(i + "!")));
		}

		[Test]
		public void ToMaybe()
		{
			Assert.AreEqual(Maybe<string>.Empty, ResultError.ToMaybe(i => i));
			Assert.AreEqual(Maybe.From("Hi"), ResultSuccess.ToMaybe(i => i));
		}

		[Test]
		public void Equality_members()
		{
			// Silverlight does not contain Hashset
			var hashset = new[] {ResultSuccess}.ToDictionary(r => r);

			Assert.IsTrue(hashset.ContainsKey(ResultSuccess));
			Assert.IsFalse(hashset.ContainsKey(ResultError));
			Assert.IsTrue(hashset.ContainsKey("Hi"));
		}

		 void Throw(Failure failure)
		{
			Assert.AreEqual(ResultError.Error, failure);
			throw new InvalidOperationException();
		}

		[Test]
		public void Success_with_apply_handle()
		{
			var val = "";
			ResultSuccess
				.Apply(x => val = x)
				.Handle(Throw)
				.Apply(x => val += x);

			Assert.AreEqual(ResultSuccess.Value + ResultSuccess.Value, val);
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
			ResultError.ExposeException(s => Errors.InvalidOperation(s.ToString()));
		}
	}
}
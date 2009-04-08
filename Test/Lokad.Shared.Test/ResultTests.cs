#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Diagnostics;
using Lokad.Rules;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class ResultTests
	{
		[Test, Expects.InvalidOperationException]
		public void Access_Success()
		{
			var result = Result.Success(SystemDescriptor.Default.Version);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(SystemDescriptor.Default.Version, result.Value);
			Assert.IsNull(result.ErrorMessage, "this should fail");
		}

		[Test, Expects.InvalidOperationException]
		public void Access_Error()
		{
			var result = Result<int>.Error("Failed");
			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(result.ErrorMessage, "Failed");
			Assert.IsNull(result.Value, "This should fail");
		}

		[Test, Expects.ArgumentNullException]
		public void Reference_Value_Is_Checked_For_Null()
		{
			// if you have R#, this should be highlighted
			Result.Success(default(object));
		}

		[Test]
		public void Value_Can_Be_Default()
		{
			Result.Success(default(int));
		}

		[Test]
		public void Implicit_Conversion()
		{
			Result<int> result = 10;
			RuleAssert.IsTrue(() => result.IsSuccess);
		}

		readonly Result<int> Result10 = Result.Success(10);
		readonly Result<int> ResultEmpty = Result<int>.Error("Error");

		[Test]
		public void Apply()
		{
			ResultEmpty.Apply(i => Assert.Fail());
			int applied = 0;
			Result10.Apply(i => applied = i);
			Assert.AreEqual(10, applied);
		}

		[Test]
		public void Convert()
		{
			Assert.AreEqual(Result.Success("10"), Result10.Convert(i => i.ToString()));
			Assert.AreEqual(Result<string>.Error("Error"), ResultEmpty.Convert(i => i.ToString()));
		}

		[Test]
		public void Combine()
		{
			var error1 = Result<int>.Error("E1");
			var error1s = Result<string>.Error("E1");
			Func<int, Result<string>> fails = i => { throw new InvalidOperationException(); };

			Assert.AreEqual(error1s, error1.Combine(fails));
			Assert.AreEqual(error1s, Result10.Combine(i => error1s));
			Assert.AreEqual(Result.Success("10"), Result10.Combine(i => Result.Success(i.ToString())));
		}

		[Test]
		public void ToMaybe()
		{
			Assert.AreEqual(Maybe<int>.Empty, ResultEmpty.ToMaybe(i => i));
			Assert.AreEqual(Maybe.From(10), Result10.ToMaybe(i => i));
		}
	}
}
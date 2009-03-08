#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace System
{
	[TestFixture]
	public sealed class ActionPolicyTests
	{
		#region Setup/Teardown

		[TearDown]
		public void TearDown()
		{
			SystemUtil.Reset();
		}

		#endregion

		static void Expect<T>(Action action) where T : Exception
		{
			try
			{
				action();
				Assert.Fail("Exception expected");
			}
			catch (T)
			{
			}
		}
		private static void RaiseTimeout()
		{
			throw new TimeoutException();
		}
		private static void RaiseArgument()
		{
			throw new ArgumentException();
		}
		private static void Nothing()
		{
		}

		[Test]
		public void WaitAndRetry()
		{
			TimeSpan slept = TimeSpan.Zero;
			SystemUtil.SetSleep(span => slept += span);

			var policy = ActionPolicy
				.Handle<TimeoutException>()
				.WaitAndRetry(Range.Create(5, i => i.Seconds()));

			Expect<ArgumentException>(() => policy.Do(RaiseArgument));
			Assert.AreEqual(TimeSpan.Zero, slept);

			Expect<TimeoutException>(() => policy.Do(RaiseTimeout));
			Assert.AreEqual(10.Seconds(), slept);
		}

		[Test]
		public void WaitAndRetry_WithAction()
		{
			TimeSpan slept = TimeSpan.Zero;
			SystemUtil.SetSleep(span => slept += span);
			int count = 0;

			var policy = ActionPolicy
				.Handle<TimeoutException>()
				.WaitAndRetry(Range.Create(5, i => i.Seconds()), (ex,s) => count += 1);

			// non-handled
			Expect<ArgumentException>(() => policy.Do(RaiseArgument));
			Assert.AreEqual(TimeSpan.Zero, slept);
			Assert.AreEqual(0, count);

			// handled succeeds
			Raise<TimeoutException>(5, policy);
			Assert.AreEqual(10.Seconds(), slept);
			Assert.AreEqual(5, count);

			// handled fails
			Expect<TimeoutException>(6, policy);
			Assert.AreEqual(20.Seconds(), slept);
			Assert.AreEqual(10, count);

		}

		[Test]
		public void Retry()
		{
			var policy = ActionPolicy
				.Handle<TimeoutException>()
				.Retry(2);

			// non-handled exception
			Expect<ArgumentException>(() => policy.Do(RaiseArgument));
			// handled succeeds
			Raise<TimeoutException>(2,policy);
			// handled fails
			Expect<TimeoutException>(3, policy);
		}

		[Test]
		public void Retry_With_Action()
		{
			int counter = 0;
			var policy = ActionPolicy
				.Handle<TimeoutException>()
				.Retry(2, (ex,i) => counter += 1);

			// non-handled exception
			Expect<ArgumentException>(() => policy.Do(RaiseArgument));
			Assert.AreEqual(0, counter);

			// handled succeeds
			Raise<TimeoutException>(2, policy);
			Assert.AreEqual(2, counter);
			// handled fails
			Expect<TimeoutException>(3, policy);
			Assert.AreEqual(4, counter);
		}

		[Test]
		public void RetryForever()
		{
			int counter = 0;
			var policy = ActionPolicy
				.Handle<TimeoutException>()
				.RetryForever(ex => counter += 1);

			// non-handled exception
			Expect<ArgumentException>(() => policy.Do(RaiseArgument));
			Assert.AreEqual(0, counter);

			// handled succeeds
			Raise<TimeoutException>(11, policy);
			Assert.AreEqual(11, counter);
		}

#if !SILVERLIGHT2

		[Test]
		public void CircuitBreaker()
		{
			var policy = ActionPolicy
				.Handle<TimeoutException>()
				.CircuitBreaker(1.Minutes(), 2);


			var time = new DateTime(2008, 1, 1);
			SystemUtil.SetTime(time);

			// non-handled
			policy.Do(Nothing);
			Expect<ArgumentException>(() => policy.Do(RaiseArgument));

			// handled below
			
			// Raise 
			Expect<TimeoutException>(() => policy.Do(RaiseTimeout));
			Expect<TimeoutException>(() => policy.Do(RaiseTimeout));
			// Trigger
			Expect<TimeoutException>(() => policy.Do(RaiseArgument));
			Expect<TimeoutException>(() => policy.Do(Nothing));

			// Elapse and pass
			SystemUtil.SetTime(time.AddMinutes(1));
			policy.Do(Nothing);

			// Raise
			Expect<TimeoutException>(() => policy.Do(RaiseTimeout));
			Expect<TimeoutException>(() => policy.Do(RaiseTimeout));

			// Elapse and rearm
			SystemUtil.SetTime(time.AddMinutes(1));
			Expect<TimeoutException>(() => policy.Do(RaiseTimeout));

			// Non-elapse and trigger
			SystemUtil.SetTime(time.AddSeconds(119));
			Expect<TimeoutException>(() => policy.Do(Nothing));

			// Elapse and pass
			SystemUtil.SetTime(time.AddMinutes(2));
			policy.Do(Nothing);
		}

#endif

		static void Expect<TException>(int count, ActionPolicy policy)
			where TException : Exception, new()
		{
			Expect<TException>(() => Raise<TException>(count, policy));
		}

		static void Raise<TException>(int count, ActionPolicy policy)
			where TException : Exception, new()
		{
			int counter = 0;

			policy.Do(() =>
			{
				if (counter < count)
				{
					counter++;
					throw new TException();
				}
			});
		}

		[Test, Expects.TimeoutException]
		public void Null_Policy()
		{
			ActionPolicy.Null.Do(RaiseTimeout);
		}
	}
}
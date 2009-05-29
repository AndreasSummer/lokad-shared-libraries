#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace Lokad.Threading
{
	[TestFixture]
	public sealed class WaitTests
	{
		// ReSharper disable InconsistentNaming


		[Test]
		[ExpectedException(typeof(TimeoutException))]
		public void Expired_Request_Throws_Timeout_Exception()
		{
			Func<int> longRequest = () =>
			{
				Thread.Sleep(1000);
				return 1;
			};

			var waitFor = new WaitFor<int>(1.Milliseconds());
			waitFor.Run(longRequest);
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Non_Expired_Request_Throws_Inner_Exception()
		{
			Func<int> request = () =>
			{
				throw new ArgumentOutOfRangeException();
			};

			new WaitFor<int>(1.Minutes()).Run(request);
		}

		[Test]
		public void Proper_Request_Returns_Value()
		{
			Func<int> request = () =>
			{
				Thread.Sleep(1);
				return 1;
			};

			var waitFor = new WaitFor<int>(1.Minutes());
			var result = waitFor.Run(request);
			Assert.AreEqual(1, result);
		}

		[Test]
		public void Closures_Work_Properly()
		{
			int counter = 0;
			Func<int> request = () => counter++;

			var waitFor = new WaitFor<int>(1.Minutes());

			Enumerable.Range(0, 5).ForEach(n => Assert.AreEqual(n, waitFor.Run(request)));

			Assert.AreEqual(5, counter);
		}

		[Test]
		[Explicit]
		public void Count_OverHead()
		{
			var waitFor = new WaitFor<int>(1.Minutes());

			var watch = Stopwatch.StartNew();
			
			const int count = 10000;
			for (int i = 0; i < count; i++)
			{
				waitFor.Run(() => 1);
			}
			var ticks = watch.ElapsedTicks;
			Console.WriteLine("Overhead is {0} ms", TimeSpan.FromTicks(ticks / count).TotalMilliseconds);
		}

		private static int LocalStack()
		{
			throw new InvalidOperationException("TEST");
		}

		[Test]
		public void Stack_Is_Persisted()
		{
			
			try
			{
				WaitFor<int>.Run(10.Minutes(), LocalStack);
			}
			catch (Exception e)
			{
				StringAssert.Contains("LocalStack", e.ToString());
			}
		}
	}
}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Lokad.Threading
{
	[TestFixture]
	public sealed class ParallelExtensionsTests
	{
		// ReSharper disable InconsistentNaming
		[Test]
		public void SelectInParallel_for_empty_array()
		{
			var results = (new int[0]).SelectInParallel(i => 2*i);

			CollectionAssert.IsEmpty(results);
		}

		[Test]
		public void SelectInParallel_for_array()
		{
			var results = Range
				.Array(10, i => i)
				.SelectInParallel(i => i*2);

			CollectionAssert.AreEquivalent(results, Range.Array(10, i => i*2));
		}


		[Test]
		[ExpectedException(typeof (InvalidOperationException))]
		public void SelectInParallel_Exception()
		{
			Range.Array(10)
				.SelectInParallel(i =>
					{
						if (i == 5) throw new InvalidOperationException("Part of the test.");
						return i*2;
					});
		}


		[Test]
		public void SelectInParallel_SmokeTest()
		{
			var results = Range
				.Array(100, i => i)
				.SelectInParallel(i => i*2);

			CollectionAssert.AreEquivalent(results, Range.Array(100, i => i*2));
		}

		[Test]
		public void SelectInParallel_is_ordered()
		{
			const int count = 100000;
			var results = Range.Array(count).SelectInParallel(i => i * 2);

			for (int i = 0; i < count; i++)
			{
				Assert.AreEqual(i*2, results[i], "#A00");
			}
		}

		[Test, Explicit]
		public void ContextTesting()
		{
			const int outerCount = 100000;

			Func<int, double> operation = i =>
				{
					double val = 0;
					var limit = Rand.Next(5000, 10000);
					for (int j = 0; j < limit; j++)
					{
						val = Math.Sin(val + j);
					}
					return val;
				};

			using (Counter("Lokad"))
			{
				Range
					.Array(outerCount)
					.SelectInParallel(operation);
			}

			var wait = new WaitFor<double>(2.Seconds());

			using (Counter("Lokad+WaitFor"))
			{
				Range
					.Array(outerCount)
					.SelectInParallel(i => wait.Run(() => operation(i)));
			}
		}

		static IDisposable Counter(string name)
		{
			var watch = Stopwatch.StartNew();
			return new DisposableAction(() => Console.WriteLine("'{0}' took {1}", name, watch.Elapsed));
		}

		[Test, Explicit]
		public void Test()
		{
			Console.WriteLine("CPUcount: {0}", Environment.ProcessorCount);
			var watch = Stopwatch.StartNew();
			var work = Range.Array(10, i => i);

			var parallel = work.SelectInParallel(span =>
				{
					SystemUtil.Sleep(span.Seconds());
					return string.Format("This operation took {0} seconds", span);
				});

			foreach (var span in parallel)
			{
				Console.WriteLine("@{0}s: {1}", watch.Elapsed.TotalSeconds.Round(2), span);
			}

			Console.WriteLine("Total running time: {0}", watch.Elapsed);
		}
	}
}
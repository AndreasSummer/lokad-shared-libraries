#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if !SILVERLIGHT2
using System;
using System.Linq;
using NUnit.Framework;

namespace Lokad.Diagnostics
{
	[TestFixture]
	public sealed class ExceptionCounterTests
	{
		static readonly Exception Ex1 = Capture(() => Stack1("E1"));
		static readonly Exception Ex2 = Capture(() => Stack2("E1"));
		static readonly Exception Ex3 = Capture(() => Stack1("E2"));

		// ReSharper disable InconsistentNaming

		static void Stack2(string msg)
		{
			Stack1(msg);
		}

		static void Stack1(string msg)
		{
			throw new Exception(msg);
		}

		static Exception Capture(Action caller)
		{
			try
			{
				caller();
			}
			catch (Exception ex)
			{
				return ex;
			}
			throw new InvalidOperationException();
		}

		[Test]
		public void Same_Exceptions_Get_Counted_Properly()
		{
			var counter = new ExceptionCounters();
			var id1 = counter.Add(Ex1);
			var id2 = counter.Add(Ex1);

			var history = counter.GetHistory();
			Assert.AreEqual(1, history.Count, "A01");
			Assert.AreEqual(2, history[0].Count, "A02");
			Assert.AreEqual(id1, id2);
			Assert.AreEqual(id1, history[0].ID);
		}

		[Test]
		public void Different_Exceptions_Get_Counted_Properly()
		{
			var counter = new ExceptionCounters();
			counter.Add(Ex1);
			counter.Add(Ex2);
			counter.Add(Ex3);

			var history = counter.GetHistory();
			Assert.AreEqual(3, history.Count);

			history.ForEach(h => Assert.AreEqual(1, h.Count));
		}

		[Test]
		public void Clear_Works()
		{
			var counter = new ExceptionCounters();
			counter.Add(Ex1);
			counter.Add(Ex1);

			counter.Clear();
			Assert.AreEqual(0, counter.GetHistory().Count);
		}

		[Test]
		public void Merging_statistics()
		{
			var counter1 = new ExceptionCounters();
			var counter2 = new ExceptionCounters();

			counter1.Add(Ex1);
			counter2.Add(Ex1);


			var history1 = counter1
				.GetHistory();


			var history2 = counter2.GetHistory();

			var merged = history1
				.Concat(history2)
				.GroupBy(s => s.Text)
				.ToArray(g =>
					{
						var first = g.First();
						return new ExceptionStatistics(first.ID, g.Sum(s => s.Count), first.Name, first.Message, first.Text);
					});

			Assert.AreEqual(1, merged.Length);
			Assert.AreEqual(2, merged[0].Count);
		}

		[Test]
		public void ExceptionCounters_Get_Merge_Properly()
		{
			var counter1 = new ExceptionCounters();
			var counter2 = new ExceptionCounters();
			var counter3 = new ExceptionCounters();

			counter1.Add(Ex1);
			counter1.Add(Ex1);
			counter1.Add(Ex2);
			counter1.Add(Ex3);

			counter2.Add(Ex2);
			counter2.Add(Ex2);
			counter2.Add(Ex3);

			counter3.Add(Ex3);

			var mergingCounter = new ExceptionCounters();
			mergingCounter.Add(new[] { counter1, counter2, counter3 } );

			var history = mergingCounter.GetHistory();
			Assert.AreEqual(3, history.Count);
			Assert.AreEqual(2, history[0].Count);
			Assert.AreEqual(3, history[1].Count);
			Assert.AreEqual(3, history[2].Count);
		}
	}
}

#endif
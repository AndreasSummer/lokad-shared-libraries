#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion
#if !SILVERLIGHT2
using System.Linq;
using NUnit.Framework;

namespace System.Diagnostics
{
	[TestFixture]
	public sealed class ExceptionCounterTests
	{
		private static readonly Exception ex1 = Capture(() => Stack1("E1"));
		private static readonly Exception ex2 = Capture(() => Stack2("E1"));
		private static readonly Exception ex3 = Capture(() => Stack1("E2"));

		private static void Stack2(string msg)
		{
			Stack1(msg);
		}

		private static void Stack1(string msg)
		{
			throw new Exception(msg);
		}

		private static Exception Capture(Action caller)
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
			var id1 = counter.Add(ex1);
			var id2 = counter.Add(ex1);

			var history = counter.GetHistory();
			Assert.AreEqual(1, history.Count, "A01");
			Assert.AreEqual(2, history[0].Count, "A02");
			Assert.AreEqual(id1,id2);
			Assert.AreEqual(id1, history[0].ID);
		}

		[Test]
		public void Different_Exceptions_Get_Counted_Properly()
		{
			var counter = new ExceptionCounters();
			counter.Add(ex1);
			counter.Add(ex2);
			counter.Add(ex3);

			var history = counter.GetHistory();
			Assert.AreEqual(3, history.Count);

			history.ForEach(h => Assert.AreEqual(1, h.Count));
		}

		[Test]
		public void Clear_Works()
		{
			var counter = new ExceptionCounters();
			counter.Add(ex1);
			counter.Add(ex1);

			counter.Clear();
			Assert.AreEqual(0, counter.GetHistory().Count);
		}
	}
}
#endif
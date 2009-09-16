using Lokad.Rules;
using NUnit.Framework;
using Lokad.Diagnostics.Persist;

#if !SILVERLIGHT2

namespace Lokad.Diagnostics
{
	[TestFixture]
	public sealed class ExecutionCounterTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void Async_counter_reset_is_handled()
		{
			var c = Create();
			var timer = c.Open();

			c.Reset();
			c.Close(timer);

			Assert.AreEqual(0, c.ToStatistics().CloseCount);
		}

		[Test]
		public void Normal_flow()
		{
			var c = Create(1,1);
			var timer = c.Open(5);
			SystemUtil.Sleep(1.Milliseconds());
			c.Close(timer,5);

			var statistics = c.ToStatistics();

			RuleAssert.That(() => statistics,
				s => s.CloseCount == 1,
				s => s.OpenCount == 1,
				s => s.RunningTime > 0,
				s => s.Counters[0] == 5,
				s => s.Counters[1] == 5);
		}

		[Test]
		public void Conversion()
		{
			var c = Create();
			var timer = c.Open();

			var statistics = new[]{c.ToStatistics()};
			XmlUtil.TestXmlSerialization(statistics.ToPersistence());

		}

		static ExecutionCounter Create()
		{
			return new ExecutionCounter("null", 0, 0);
		}

		static ExecutionCounter Create(int openGroup, int closeGroup)
		{
			return new ExecutionCounter("null", openGroup, closeGroup);
		}
		
	}
}

#endif
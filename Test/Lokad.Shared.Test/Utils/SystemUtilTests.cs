#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class SystemUtilTests
	{
		// ReSharper disable InconsistentNaming

		[TearDown]
		public void TearDown()
		{
			SystemUtil.Reset();
		}

		[Test]
		public void Fixed_Now()
		{
			var now = SystemUtil.Now;
			SystemUtil.SetDateTimeProvider(() => DateTime.MaxValue);
			Assert.AreNotEqual(now, SystemUtil.Now);
			Assert.AreEqual(SystemUtil.Now, SystemUtil.Now);
		}

		[Test]
		public void Utc_diff()
		{
			var time = DateTime.Now;
			var diff = time - time.ToUniversalTime();

			SystemUtil.SetTime(DateTime.Now);

			Assert.AreEqual(diff, SystemUtil.Now - SystemUtil.UtcNow);
		}

		[Test]
		public void Utc_diff_offset()
		{
			var now1Local = DateTime.Now;
			var diff1 = now1Local - now1Local.ToUniversalTime();

			var now2 = SystemUtil.NowOffset;
			var now2Local = now2.DateTime;
			var now2Utc = now2.UtcDateTime;
			Assert.AreEqual(diff1, now2Local - now2Utc);
			Assert.AreEqual(TimeSpan.Zero, new DateTimeOffset(now2Local) - new DateTimeOffset(now2Utc));
			Assert.AreEqual(now2, new DateTimeOffset(now2Local));
			Assert.AreEqual(now2, new DateTimeOffset(now2Utc));
		}

		[Test]
		public void Normal_Now()
		{
			var time0 = DateTime.Now;
			var time1 = SystemUtil.Now;
			var time2 = DateTime.Now;

			Assert.IsTrue(time0 <= time1 && time1 <= time2);
		}

		[Test]
		public void Normal_UtcNow()
		{
			var time0 = DateTime.UtcNow;
			var time1 = SystemUtil.UtcNow;
			var time2 = DateTime.UtcNow;

			Assert.IsTrue(time0 <= time1 && time1 <= time2);
		}

		[Test]
		public void Test_Sleep()
		{
			SystemUtil.Sleep(TimeSpan.FromMilliseconds(0));

			var flag = false;
			SystemUtil.SetSleep(s =>
				{
					Assert.AreEqual(1.Seconds(), s);
					flag = true;
				});

			SystemUtil.Sleep(1.Seconds());
			Assert.IsTrue(flag);
		}

		[Test]
		public void GetHashCode_works_with_nulls()
		{
			SystemUtil.GetHashCode("test", null);
		}
	}
}
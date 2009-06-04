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

		public void Test_Now()
		{
			var now = SystemUtil.Now;
			SystemUtil.SetDateTimeProvider(() => DateTime.MaxValue);
			Assert.AreNotEqual(now, SystemUtil.Now);
			Assert.AreEqual(SystemUtil.Now, SystemUtil.Now);
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
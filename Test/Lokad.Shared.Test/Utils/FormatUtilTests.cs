#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class FormatUtilTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void Formatting_large_interval()
		{
			StringAssert.Contains("years from now", FormatUtil.TimeOffsetUtc(DateTime.MaxValue));
			StringAssert.Contains("years ago", FormatUtil.TimeOffsetUtc(DateTime.MinValue));
		}

		[Test]
		public void Formatting_large_size()
		{
			Assert.AreEqual("8 EB", FormatUtil.SizeInBytes(long.MaxValue));
		}
	}
}
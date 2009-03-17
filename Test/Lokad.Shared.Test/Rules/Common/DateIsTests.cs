#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class DateIsTests
	{
		[Test]
		public void Test()
		{
			RuleAssert.For<DateTime>(DateIs.SqlCompatible)
				.ExpectNone(new DateTime(1753, 1, 1), DateTime.MaxValue)
				.ExpectError(DateTime.MinValue, new DateTime(1753, 1, 1).AddSeconds(-1));
		}
	}
}
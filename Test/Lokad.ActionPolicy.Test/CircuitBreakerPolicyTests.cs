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
	public sealed class CircuitBreakerPolicyTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void Stack_trace_is_preserved()
		{
			var policy = ActionPolicy
				.Handle<InvalidOperationException>()
				.CircuitBreaker(10.Seconds(), 6);

			SystemUtil.SetSleep(s => { });

			StackTest<InvalidOperationException>.Check(policy);
		}

		[TearDown]
		public void TearDown()
		{
			SystemUtil.Reset();
		}
	}
}
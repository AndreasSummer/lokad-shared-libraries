#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Exceptions;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class RetryPolicyTests
	{
		// ReSharper disable InconsistentNaming

		static void MyStack1()
		{
			throw new InvalidOperationException();
		}

		static void MyStack2()
		{
			MyStack1();
		}

		[Test]
		public void Test()
		{
			RetryPolicy.Implementation(MyStack2, exception => false, () => new RetryState(exception => { }));
		}
	}
}
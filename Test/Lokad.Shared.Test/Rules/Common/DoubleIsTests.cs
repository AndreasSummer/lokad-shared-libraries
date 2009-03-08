#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace System.Rules
{
	[TestFixture]
	public sealed class DoubleIsTests
	{
		[Test]
		public void Valid()
		{
			RuleAssert.For<double>(DoubleIs.Valid)
				.ExpectError(double.NaN, 1/0D, double.PositiveInfinity, double.NegativeInfinity)
				.ExpectNone(1D, 0D, Math.PI);
		}
	}
}
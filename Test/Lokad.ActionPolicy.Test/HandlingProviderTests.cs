#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class HandlingProviderTests
	{
		readonly IProvider<int, int> _failing = Provider<int>.For<int>(i => { throw new UnauthorizedAccessException(); });

		[Test, Expects.ResolutionException]
		public void Exception_Is_Wrapped_Properly()
		{
			HandlingProvider
				.For(_failing, ActionPolicy.Null)
				.Get(1);
		}
	}
}
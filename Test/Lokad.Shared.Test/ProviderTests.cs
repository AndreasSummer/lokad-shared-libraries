#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class ProviderTests
	{
		static readonly IProvider<int, int> _provider =
			new Dictionary<int, int> {{1, 1}, {2, 4}}.AsProvider();

		[Test, Expects.ResolutionException]
		public void Exception()
		{
			_provider.Get(0);
		}

		[Test]
		public void Resolution()
		{
			Assert.AreEqual(4, _provider.Get(2));
		}
	}
}
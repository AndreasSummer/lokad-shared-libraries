#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using NUnit.Framework;

namespace System
{
	[TestFixture]
	public sealed class NamedProviderTests
	{
		static readonly INamedProvider<int> _provider =
			new Dictionary<string, int> {{"1", 1}, {"2", 2}}.AsProvider();


		[Test, Expects.ResolutionException]
		public void Exception()
		{
			_provider.Get("0");
		}

		[Test]
		public void Resolution()
		{
			Assert.AreEqual(1, _provider.Get("1"));
		}
	}
}
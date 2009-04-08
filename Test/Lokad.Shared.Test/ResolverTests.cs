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
	public sealed class ResolverTests
	{
		[Test, Expects.ResolutionException]
		public void Test_Wrap1()
		{
			var t = new Resolver(type => { throw new InvalidOperationException("Failed"); }, (type, s) => null);
			t.Get<ICommand>();
		}

		[Test, Expects.ResolutionException]
		public void Test_Wrap2()
		{
			var t = new Resolver(type => null, (type, s) => { throw new InvalidOperationException("Failed"); });
			t.Get<ICommand>("Name");
		}
	}
}
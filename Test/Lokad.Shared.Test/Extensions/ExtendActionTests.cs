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
	public sealed class ExtendActionTests
	{
		[Test]
		public void Test()
		{
			bool flag = false;
			Action act = () => { flag = true; };

			using (act.AsDisposable())
			{
				Assert.IsFalse(flag);
			}
			Assert.IsTrue(flag);
		}
	}
}
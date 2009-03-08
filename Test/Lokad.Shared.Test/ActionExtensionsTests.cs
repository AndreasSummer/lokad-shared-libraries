#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace System
{
	[TestFixture]
	public sealed class ActionExtensionsTests
	{
		[Test]
		public void Test()
		{
			bool flag = false;
			Action act = () => { flag = true; };

			using (act.AsDisposable())
			{
			}
			Assert.IsTrue(flag);
		}
	}
}
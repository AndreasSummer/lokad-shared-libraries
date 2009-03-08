#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace System
{
	[TestFixture]
	public sealed class StringUtilTests
	{
		[Test]
		public void Test_MemberNameToCaption()
		{
			Assert.AreEqual("Lokad - Account Id", StringUtil.MemberNameToCaption("Lokad.AccountId"));
		}
	}
}
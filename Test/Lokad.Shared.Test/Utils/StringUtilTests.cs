#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace Lokad
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
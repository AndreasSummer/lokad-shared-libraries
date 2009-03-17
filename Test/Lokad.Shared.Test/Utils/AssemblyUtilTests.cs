#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class AssemblyUtilTests
	{
		[Test]
		public void Configuration_Is_Retrieved_Properly()
		{
			Assert.AreEqual("TestConfig", AssemblyUtil.GetAssemblyConfiguration().Value);
		}

		[Test]
		public void Description_Is_Retrieved_Properly()
		{
			Assert.AreEqual("Test Assembly", AssemblyUtil.GetAssemblyDescription().Value);
		}
	}
}
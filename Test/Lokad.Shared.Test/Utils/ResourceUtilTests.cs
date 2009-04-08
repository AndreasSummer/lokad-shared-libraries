#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.IO;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class ResourceUtilTests
	{
		[Test, Expects.ArgumentNullException]
		public void Test_Null()
		{
			ResourceUtil<ResourceUtilTests>.GetStream(null);
		}

		[Test]
		public void Test_Stream()
		{
			var stream = ResourceUtil<ResourceUtilTests>.GetStream("Hello.txt");
			using (stream)
			{
				var end = new StreamReader(stream).ReadToEnd();
				Assert.AreEqual("Lokad.Shared.Test", end);
			}
		}
	}
}
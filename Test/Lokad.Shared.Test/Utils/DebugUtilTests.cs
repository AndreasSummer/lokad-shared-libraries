#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class DebugUtilTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void UseCases()
		{
			var foo = new
				{
					Message = "Hello",
					Source = "Anonymous class"
				};

			DebugUtil.Write(() => foo);
			DebugUtil.Write(foo);
			DebugUtil.Write("String {0}", 1);

			DebugUtil.WriteLine(() => foo);
			DebugUtil.WriteLine(foo);
			DebugUtil.WriteLine("String {0}", 1);
		}
	}
}
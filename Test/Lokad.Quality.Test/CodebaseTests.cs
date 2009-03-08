#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace Lokad.Quality.Test
{
	[TestFixture]
	public sealed class CodebaseTests
	{
		sealed class C
		{
		}

		[Test]
		public void Find_Works_With_Nested_Classes()
		{
			var find = GlobalSetup.Codebase.Find<C>();
			Assert.IsTrue(find.IsNestedPrivate);
		}
	}
}
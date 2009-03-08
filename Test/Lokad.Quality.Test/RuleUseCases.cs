#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace Lokad.Quality.Test
{
	public static class Fire
	{
		public static void Extend(this object self)
		{
			throw new NotImplementedException();
		}
	}

	[TestFixture]
	public class RuleUseCases
	{
		[Test]
		public void Count_Methods_With_Exceptions()
		{
			var codebase = GlobalSetup.Codebase;

			var throwingMethods = codebase.Methods
				.Where(m => m
					.GetInstructions()
					.Exists(i => i.Creates<NotImplementedException>()))
				.ToArray();

			Assert.AreEqual(1, throwingMethods.Length);

			//if (throwingMethods.Length > 0)
			//    CollectionAssert.IsEmpty(throwingMethods);
		}

		[Test]
		public void Count_Methods_That_Extend_Object()
		{
			var codebase = GlobalSetup.Codebase;

			var methods = codebase.Methods
				.Where(m => m.Has<ExtensionAttribute>())
				.Where(m => m.Parameters[0].Is<object>());

			Assert.AreEqual(1, methods.Count());

			//CollectionAssert.IsEmpty(methods.ToArray());
		}
	}
}
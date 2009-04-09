#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad.Testing
{
	[TestFixture]
	public sealed class IEquatableExtensionsTests
	{
		sealed class Equatable : IEquatable<Equatable>
		{
			readonly int _key;

			public Equatable(int key)
			{
				_key = key;
			}

			public bool Equals(Equatable other)
			{
				return _key == other._key;
			}
		}

		[Test]
		public void X_EqualsTo_Y()
		{
			// generic
			Assert.IsTrue(1.EqualsTo(1));
			Assert.IsFalse(2.EqualsTo(1));

			//specialized
			var e1 = new Equatable(1);
			var e2 = new Equatable(2);
			Assert.IsFalse(e1.EqualsTo(e2));
			Assert.IsTrue(e1.EqualsTo(e1));
		}

		[Test]
		public void Xs_EqualsTo_Ys()
		{
			var array1 = new[] {1, 2, 3};
			var array2 = new[] {1, 2, 4};
			Assert.IsTrue(array1.EqualsTo(array1));
			Assert.IsFalse(array1.EqualsTo(array2));

			// specialized
			var e1 = new Equatable(1);
			var e2 = new Equatable(2);
			var a1 = new[] {e1, e2};
			var a2 = new[] {e1, e1};

			Assert.IsTrue(a1.EqualsTo(a1));
			Assert.IsFalse(a1.EqualsTo(a2));
		}
	}
}
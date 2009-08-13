#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Security.Cryptography;
using NUnit.Framework;

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class BufferIsTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void Check_hash_roundtrips()
		{
			var buffer = new byte[Rand.Next(2, 64)];
			var prov = new RNGCryptoServiceProvider();
			prov.GetBytes(buffer);

			var hash = BufferUtil.CalculateSimpleHashCode(buffer);

			Enforce.That(() => buffer, BufferIs.WithValidHash(hash));

			unchecked
			{
				buffer[Rand.Next(buffer.Length)] += 1;
			}

			Assert.IsFalse(Scope.IsValid(buffer, BufferIs.WithValidHash(hash)));
		}

		[Test]
		public void Limited()
		{
			RuleAssert.For(BufferIs.Limited(10))
				.ExpectNone(new byte[10], new byte[9])
				.ExpectError(null, new byte[11], new byte[12]);
		}
	}
}
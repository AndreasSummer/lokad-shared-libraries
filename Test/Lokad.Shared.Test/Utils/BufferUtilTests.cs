#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Security.Cryptography;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class BufferUtilTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void Test_Zeroes()
		{
			var bytes1 = new byte[101];
			var bytes2 = new byte[100];

			var hash1 = BufferUtil.CalculateSimpleHashCode(bytes1);
			var hash2 = BufferUtil.CalculateSimpleHashCode(bytes2);
			Assert.AreNotEqual(0, hash1);
			Assert.AreNotEqual(0, hash2);
			Assert.AreNotEqual(hash1, hash2);
		}

		[Test]
		public void Hash_random()
		{
			var length = Rand.Next(1, 100);
			var b = new byte[length];
			new RNGCryptoServiceProvider().GetBytes(b);


			var hash1 = BufferUtil.CalculateSimpleHashCode(b);
			var hash2 = BufferUtil.CalculateSimpleHashCode(b);

			Assert.AreEqual(hash1, hash2);


			unchecked
			{
				b[Rand.Next(length)] += 1;
			}

			Assert.AreNotEqual(hash1, BufferUtil.CalculateSimpleHashCode(b));
		}

		[Test]
		public void Hashing_short()
		{
			var b = new byte[] {1, 2};
			BufferUtil.CalculateSimpleHashCode(b);
		}

		[Test]
		public void Mess_detection()
		{
			var length = Rand.Next(1, 100);
			var b = new byte[length];
			new RNGCryptoServiceProvider().GetBytes(b);

			var hash = BufferUtil.CalculateSimpleHashCode(b);
			for (int i = 0; i < length; i++)
			{
				unchecked
				{
					b[i] ^= 1;
					Assert.AreNotEqual(hash, BufferUtil.CalculateSimpleHashCode(b), "Messing at {0} of {1}", i, length);
					b[i] ^= 1;
					Assert.AreEqual(hash, BufferUtil.CalculateSimpleHashCode(b), "Restoring at {0} of {1}", i, length);
				}
			}
		}

		[Test, Expects.ArgumentNullException]
		public void Null()
		{
// ReSharper disable AssignNullToNotNullAttribute
			BufferUtil.CalculateSimpleHashCode(null);
// ReSharper restore AssignNullToNotNullAttribute
		}
	}
}
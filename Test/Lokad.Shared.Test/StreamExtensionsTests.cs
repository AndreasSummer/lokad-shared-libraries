#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;

namespace Lokad
{
#if !SILVERLIGHT2
	[TestFixture]
	public sealed class StreamExtensionsTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void Pump()
		{
			var list = Enumerable.Range(0, 1000).ToList();
			using (var stream = new MemoryStream())
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(stream, list);

				stream.Seek(0, SeekOrigin.Begin);
				using (var target = new MemoryStream())
				{
					stream.PumpTo(target, 20);

					target.Seek(0, SeekOrigin.Begin);
					var list1 = formatter.Deserialize(target) as List<int>;
					CollectionAssert.AreEqual(list, list1);
				}
				formatter.Serialize(stream, list);
			}
		}

		[Test]
		public void Compression_with_opened_streams()
		{
			var array = Range.Array(10);
			using (var stream = new MemoryStream())
			{
				using (var compress = stream.Compress(true))
				{
					XmlUtil.SerializeArray(array, compress);
				}
				stream.Seek(0, SeekOrigin.Begin);
				using (var decompress = stream.Decompress(true))
				{
					var i = XmlUtil<int[]>.Deserialize(decompress);
					CollectionAssert.AreEqual(array, i);
				}
			}
		}

		[Test]
		public void Default_compression()
		{
			var array = Range.Array(10);
			byte[] buffer;
			using (var stream = new MemoryStream())
			{
				using (var compress = stream.Compress())
				{
					XmlUtil.SerializeArray(array, compress);
				}
				buffer = stream.ToArray();
			}
			using (var stream = new MemoryStream(buffer))
			{
				using (var decompress = stream.Decompress())
				{
					var i = XmlUtil<int[]>.Deserialize(decompress);
					CollectionAssert.AreEqual(array, i);
				}
			}
		}
	}
#endif
}
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
	}
#endif
}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.IO;
using NUnit.Framework;

#if !SILVERLIGHT2

namespace Lokad
{
	[TestFixture]
	public sealed class XmlUtilTests
	{
		public sealed class Item
		{
		}

		[Test]
		public void Use_Cases()
		{
			var item = new Item();
			var items = new Item[0];

			// strings
			XmlUtil.Serialize(item);
			XmlUtil.SerializeArray(items);
			XmlUtil<string[]>.Serialize(new string[0]);

			using (var stream = new MemoryStream())
			{
				XmlUtil.Serialize(item, stream);
				XmlUtil.SerializeArray(items, stream);
				XmlUtil<string[]>.Serialize(new string[0], stream);
			}
			using (var stream = new StringWriter())
			{
				XmlUtil.Serialize(item, stream);
				XmlUtil.SerializeArray(items, stream);
				XmlUtil<string[]>.Serialize(new string[0], stream);
			}
		}
	}
}

#endif
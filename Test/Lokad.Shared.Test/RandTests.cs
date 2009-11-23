#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;
using System.Linq;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
namespace Lokad
{
	[TestFixture]
	public sealed class RandTests
	{
		[SetUp]
		public void SetUp()
		{
			Rand.ResetToDefault(1);
		}

		[TestFixtureTearDown]
		public void Dispose()
		{
			Rand.ResetToDefault();
		}

		public enum Option
		{
			No,
			Yes,
			Maybe
		}

		/// <summary>
		/// Tests this instance.
		/// </summary>
		[Test]
		public void Test()
		{
			Assert.AreEqual(534011718, Rand.Next());
			Assert.AreEqual(1, Rand.Next(10));
			Assert.AreEqual(34, Rand.Next(30, 40));
			Assert.AreEqual(0.77160412202198247d, Rand.NextDouble());
			Assert.AreEqual(3, Rand.NextItem(new[] {1, 2, 3, 4}));
			Assert.AreEqual(Option.Yes, Rand.NextEnum<Option>());
			Assert.AreNotEqual(0, Rand.NextString(1, 5).Length);
			Assert.AreEqual(new Guid("aefd513f-48a7-b49d-b3f3-172961cc2bcb"), Rand.NextGuid());
			Assert.AreEqual(new DateTime(590668128477790000), Rand.NextDate());
			Assert.AreEqual(new DateTime(633936311847180000), Rand.NextDate(2009, 2010));

			Assert.AreEqual(true, Rand.NextBool(1));
			Assert.AreEqual(false, Rand.NextBool(0));
			Assert.AreEqual(false, Rand.NextBool());

			Assert.AreEqual("accusam", Rand.String.NextWord());
			Assert.AreEqual("Sed stet amet in invidunt.", Rand.String.NextSentence(1, 6));
			var actual = Rand.String.NextText(20,90);

			Assert.IsTrue(actual.Contains(Environment.NewLine));
			Assert.AreEqual(537, actual.Length);
			Assert.AreEqual(87, actual.Count(c => c==' '));


			Assert.AreEqual(new[]
				{
					new Guid("df4ddcf6-69a5-8870-5860-e15353afd241"),
					new Guid("276152fa-f318-d27b-3871-ca2c3c022647"),
					new Guid("7e1926f3-d485-71aa-6ff0-f25ceb0e691c"),
				}, Rand.NextGuids(3));

			Assert.AreEqual(new[]
				{
					new Guid("820f5bd2-a0e3-6002-7ba6-339e434b553a"),
					new Guid("962d5f99-bb54-a826-f0b8-27011ee62907"),
					new Guid("a66a7dad-c516-850f-161d-0a2d76d22ca4"),
				}, Rand.NextGuids(1, 4));
		}

		[Test]
		public void Next_1()
		{
			var array = Range.Array(100, i => Rand.Next(30));
			CollectionAssert.Contains(array, 0);
		}

		[Test]
		public void NextItems_shuffles_array_when_all_items_are_selected()
		{
			var expected = Range.Array(10);
			CollectionAssert.AreEquivalent(expected, Rand.NextItems(expected, 10));
		}

		[Test]
		public void NextItems_returns_subset()
		{
			var expected = Range.Array(10);
			var actual = Rand.NextItems(expected, 5);
			CollectionAssert.IsSubsetOf(actual, expected);
			Assert.AreEqual(5,actual.Length);
		}

		[Test]
		public void NextDate()
		{
			Assert.LessOrEqual(new DateTime(2008,1,1), Rand.NextDate(2008, 3000));
			Assert.Greater(new DateTime(2008,1,1), Rand.NextDate(1900,2008));
		}

#if !SILVERLIGHT2

		
		public sealed class XmlTestClass
		{
			public string Member { get; set; }
		}

		[Test]
		public void NextString_Is_Serializable()
		{
			XmlUtil.Serialize(new XmlTestClass
				{
					Member = Rand.NextString(10, 1000)
				});
		}
#endif
		//[Test]
		//public void Next_String()
		//{
		//    for (int i = 48; i < 122; i++)
		//    {
		//        Console.WriteLine("'" + ((char)i) + "'");
		//    }
		//var range = Enumerable
		//    .Range(char.MinValue, char.MaxValue - char.MinValue)
		//    .Select(i => (char)i);

		//range
		//    .GroupBy(c => char.GetUnicodeCategory(c))
		//    .ForEach(g => Console.WriteLine("{0} - {1}", g.Key, g.Count()));

		//range.Select(c =>
		//    {
		//        try
		//        {
		//            XmlUtil.Serialize(new string(c, 1));
		//            return new { c, Ex = "Good" };

		//        }
		//        catch (Exception ex)
		//        {
		//            return new { c, Ex = ex.GetType().Name };
		//        }
		//    })
		//    .Where(p => p.Ex != "Good")
		//    .GroupBy(p => char.GetUnicodeCategory(p.c))
		//    .ForEach(g => Console.WriteLine("{0} - {1}", g.Key, g.Count()));

		//var c1 = range.Where(c => char.IsPunctuation(c));
		//Console.WriteLine(new string(c1.ToArray()));
		//Console.WriteLine("{0},{1}", c1.Min(c => (int)c), c1.Max(c => (int)c));

		//}
	}
}
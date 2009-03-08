using System.Linq;
using NUnit.Framework;

namespace System
{
	[TestFixture]
	public sealed class RandTests
	{
		[SetUp]
		public void SetUp()
		{
			Rand.Reset(() =>
				{
					var r = new Random(1);
					return i => r.Next(i);
				});
		}

		[TestFixtureTearDown]
		public void Dispose()
		{
			Rand.ResetToDefault();
		}
		public enum Option { No, Yes, Maybe }

		[Test]
		public void Test()
		{
			Assert.AreEqual(534011718, Rand.Next());
			Assert.AreEqual(1, Rand.Next(10));
			Assert.AreEqual(34, Rand.Next(30, 40));
			Assert.AreEqual(0.77160412202198247d, Rand.NextDouble());
			Assert.AreEqual(3, Rand.NextItem(new[] { 1,2,3,4}));
			Assert.AreEqual(Option.Yes, Rand.NextEnum<Option>());
			Assert.AreNotEqual(0,Rand.NextString(1,5).Length);
		}

		[Test]
		public void Next_1()
		{
			var array = Range.Array(100, i => Rand.Next(30));
			CollectionAssert.Contains(array, 0);
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
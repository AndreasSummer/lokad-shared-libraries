//#region (c)2008 Lokad - New BSD license

//// Copyright (c) Lokad 2008 
//// Company: http://www.lokad.com
//// This code is released under the terms of the new BSD licence

//#endregion

//using NUnit.Framework;

//namespace System.Diagnostics
//{
//    [TestFixture]
//    public sealed class SystemDescriptorTests
//    {
//        private static readonly SystemDescriptor _descriptor = new SystemDescriptor
//            {
//                Version = new Version(1, 2),
//                Configuration = "DEBUG",
//                Instance = "localhost",
//                Name = "Test"
//            };
//#if !SILVERLIGHT2
//        [Test]
//        public void Xml_Works()
//        {
//            XmlUtil.TestXmlSerialization(_descriptor);
//        }
//#endif
//        [Test]
//        public void Test_ToString()
//        {
//            Assert.IsNotEmpty(_descriptor.ToString());
//            StringAssert.Contains(_descriptor.Name, _descriptor.ToString());
//        }
//    }
//}
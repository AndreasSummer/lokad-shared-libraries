#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class TypeUtilTests
	{

		[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
		sealed class MyAttribute : Attribute{ }
		// ReSharper disable InconsistentNaming

		[MyAttribute]
		[MyAttribute]
		public sealed class ClassWith2Attributes
		{
		}

		[MyAttribute]
		public sealed class ClassWithAttribute
		{
		}

		[Test]
		public void GetAttributes_Works_For_Multiple_Instances()
		{
			var attributes = typeof(ClassWith2Attributes).GetAttributes<MyAttribute>(false);
			Assert.AreEqual(2, attributes.Length);
		}

		[Test]
		public void GetAttributes_Returns_Empty()
		{
			Assert.AreEqual(0, typeof(int).GetAttributes<MyAttribute>(false).Length);
		}

		[Test, Expects.InvalidOperationException]
		public void GetAttribute_Throws_Exception_On_Multiple_Attributes()
		{
			typeof(ClassWith2Attributes).GetAttribute<MyAttribute>(false);
		}

		[Test]
		public void GetAttribute_Works()
		{
			var result = typeof(ClassWithAttribute).GetAttribute<MyAttribute>(false);
			Assert.IsNotNull(result, "1");
			Assert.AreEqual(typeof(MyAttribute), result.GetType(), "2");
		}

		[Test]
		public void GetAttribute_Returns_Null()
		{
			Assert.IsNull(typeof(int).GetAttribute<MyAttribute>(false));
		}
	}
}
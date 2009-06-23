#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Linq;
using NUnit.Framework;

namespace Lokad.Quality.Test
{
	[TestFixture]
	public sealed class TypeDefinitionExtensionsTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void With()
		{
			var definitions = GlobalSetup.Codebase
				.Types.With<ElementAttribute>().ToArray();

			var expected = new[] {GlobalSetup.Codebase.Find(typeof (Fire)).Value};

			CollectionAssert.AreEquivalent(expected, definitions);
		}

		[Test]
		public void Resolve()
		{
			var resolve = GlobalSetup.Codebase
				.Find<ElementAttribute>().Resolve();

			Assert.AreEqual(typeof (ElementAttribute), resolve);
		}

		[Test]
		public void GetConstructors_of_empty_class()
		{
			var ctors = GlobalSetup.Codebase
				.Find(typeof (Fire)).Value
				.GetConstructors();

			CollectionAssert.IsEmpty(ctors);
		}

		[Test]
		public void GetConstructors_of_class()
		{
			var ctors = GlobalSetup.Codebase
				.Find<ElementAttribute>()
				.GetConstructors();

			CollectionAssert.IsNotEmpty(ctors);
			Assert.AreEqual(1, ctors.Count());
			Assert.AreEqual(".ctor", ctors.First().Name);
		}

		[UsedImplicitly]
		sealed class ClassWithProperty
		{
			internal string Property { get; set; }
		}

		[Test]
		public void GetProperties()
		{
			var properties = GlobalSetup.Codebase
				.Find<ClassWithProperty>()
				.GetProperties();

			CollectionAssert.IsNotEmpty(properties);
			Assert.AreEqual(1, properties.Count());
			Assert.AreEqual("Property", properties.First().Name);
		}

		[Test]
		public void GetProperties_of_empty_class()
		{
			CollectionAssert.IsEmpty(GlobalSetup.Codebase
				.Find(typeof (Fire)).Value
				.GetProperties());
		}
	}
}
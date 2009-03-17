#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections;
using NUnit.Framework;

namespace Lokad.Container
{
	[TestFixture]
	public sealed class ComponentAttributeTests
	{
		[Component("MyComponent", Scope = RegistrationScope.Factory)]
		public sealed class C1
		{
		}

		[Component(Scope = RegistrationScope.Singleton)]
		public sealed class C2
		{
		}

		[Component(typeof (IList))]
		public sealed class Service
		{
		}

		[Component]
		public sealed class Component
		{
		}

		[Test]
		public void Scope_Is_Passed_Properly()
		{
			var result = typeof (C1).GetAttribute<ComponentAttribute>(false);
			Assert.AreEqual(RegistrationScope.Factory, result.Scope);
		}

		[Test]
		public void Default_Registration_Works()
		{
			var result = typeof (Component).GetAttribute<ComponentAttribute>(false);

			Assert.AreEqual(RegistrationScope.Singleton, result.Scope);
			Assert.AreEqual(RegistrationType.Type, result.Type);
		}


		[Test]
		public void Name_Registration_Works()
		{
			var result = typeof (C1).GetAttribute<ComponentAttribute>(false);

			Assert.AreEqual(RegistrationType.Name, result.Type);
			Assert.AreEqual("MyComponent", result.Name);
		}

		[Test]
		public void Service_Registration_Works()
		{
			var result = typeof (Service).GetAttribute<ComponentAttribute>(false);

			Assert.AreEqual(RegistrationType.Service, result.Type);
			Assert.AreEqual(typeof (IList), result.Service);
		}
	}
}
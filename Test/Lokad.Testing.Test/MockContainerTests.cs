#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;
using Rhino.Mocks.Exceptions;

namespace Lokad.Testing.Test
{
	[TestFixture]
	public sealed class MockContainerTests
	{
		// ReSharper disable ClassNeverInstantiated.Local
		// ReSharper disable UnusedParameter.Local

		sealed class DependsOnClass
		{
			public DependsOnClass(Component a)
			{
			}
		}

		sealed class DependsOnService
		{
			public readonly IService Service;

			public DependsOnService(IService service)
			{
				Service = service;
			}

			public void CallService()
			{
				Service.Call();
			}
		}

		public sealed class Component : IService
		{
			public void Call()
			{
				throw new NotImplementedException();
			}
		}

		public interface IService
		{
			void Call();
		}

		// ReSharper disable InconsistentNaming
		[Test]
		[ExpectedException(typeof (ResolutionException))]
		public void Component_is_not_registered()
		{
			using (var c = new MockContainer<DependsOnClass>())
			{
				Assert.IsNotNull(c.Subject);
			}
		}

		[Test]
		public void Component_is_registered_as_type()
		{
			using (var c = new MockContainer<DependsOnClass>())
			{
				c.Register<Component>();
				Assert.IsNotNull(c.Subject);
			}
		}

		[Test]
		public void Component_is_registered_as_instance()
		{
			using (var c = new MockContainer<DependsOnClass>())
			{
				c.Register(new Component());
				Assert.IsNotNull(c.Subject);
			}
		}


		[Test]
		public void Mocks_are_used_where_possible()
		{
			using (var c = MockContainer.For<DependsOnService>())
			{
				Assert.IsNotNull(c.Subject);
			}
		}

		[Test]
		public void Mocks_are_registered_as_singletons()
		{
			using (var c = MockContainer.For<DependsOnService>())
			{
				Assert.AreSame(c.Subject.Service, c.Resolve<IService>());
			}
		}

		[Test]
		public void Calls_are_wired_automatically()
		{
			using (var c = MockContainer.For<DependsOnService>())
			{
				c.Subject.CallService();
			}
		}

		[Test]
		public void AssertWasCalled_is_verified()
		{
			using (var c = MockContainer.For<DependsOnService>())
			{
				c.Subject.CallService();
				c.AssertWasCalled<IService>(s => s.Call());
			}
		}

		[Test]
		[ExpectedException(typeof (ExpectationViolationException))]
		public void AssertWasCalled_is_not_verified()
		{
			using (var c = MockContainer.For<DependsOnService>())
			{
				c.AssertWasCalled<IService>(s => s.Call());
			}
		}
	}
}
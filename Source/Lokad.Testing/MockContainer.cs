#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Autofac;
using Autofac.Builder;
using Lokad.Quality;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Lokad.Testing
{
	/// <summary>
	/// Container that automatically resolves all unknown dependencies as stubs
	/// </summary>
	[Immutable, UsedImplicitly]
	public class MockContainer : IDisposable
	{
		readonly IContainer _container = new Autofac.Container();


		/// <summary>
		/// Creates strongly-typed mocking container
		/// </summary>
		/// <typeparam name="TSubject">The type of the subject.</typeparam>
		/// <returns>new container instance</returns>
		public static MockContainer<TSubject> For<TSubject>()
		{
			return new MockContainer<TSubject>();
		}

		/// <summary>
		/// Gets the actial container.
		/// </summary>
		/// <value>The container.</value>
		public IContainer Container
		{
			get { return _container; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MockContainer"/> class.
		/// </summary>
		public MockContainer()
		{
			_container = new Autofac.Container();
			_container.AddRegistrationSource(new RhinoRegistrationSource());
		}

		/// <summary>
		/// Registers the specified component in this container.
		/// </summary>
		/// <typeparam name="TComponent">The type of the component.</typeparam>
		public void Register<TComponent>()
		{
			var builder = new ContainerBuilder();
			builder.Register<TComponent>();
			builder.Build(_container);
		}

		/// <summary>
		/// Resolves the specified service from the container
		/// </summary>
		/// <typeparam name="TService">The type of the service.</typeparam>
		/// <returns></returns>
		public TService Resolve<TService>()
		{
			return _container.Resolve<TService>();
		}

		void IDisposable.Dispose()
		{
			_container.Dispose();
		}

		/// <summary>
		/// Stubs the specified action against the specified service.
		/// </summary>
		/// <typeparam name="TService">The type of the service.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="action">The action.</param>
		/// <returns>method options for the additional configuration</returns>
		public IMethodOptions<TResult> Stub<TService, TResult>(Func<TService, TResult> action) where TService : class
		{
			return _container.Resolve<TService>().Stub(t => action(t));
		}
	}


	/// <summary>
	/// Extends the <see cref="MockContainer"/> and autoregisters the specified subject.
	/// </summary>
	/// <typeparam name="TSubject">The type of the subject.</typeparam>
	public sealed class MockContainer<TSubject> : MockContainer
	{
		/// <summary>
		/// Testing subject
		/// </summary>
		public readonly TSubject Subject;

		/// <summary>
		/// Initializes a new instance of the <see cref="MockContainer{TSubject}"/> class.
		/// </summary>
		public MockContainer()
		{
			Register<TSubject>();
			Subject = Resolve<TSubject>();
		}
	}
}
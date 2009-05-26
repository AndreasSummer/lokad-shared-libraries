#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad.Testing.Test
{
	public abstract class ContainerFixture<TSubject, TInterface>
		where TSubject : TInterface
	{
		MockContainer<TSubject> _container;

		protected MockContainer<TSubject> Container
		{
			get { return _container; }
		}

		protected TInterface Interface
		{
			get { return _container.Subject; }
		}

		protected TSubject Implementation
		{
			get { return _container.Subject; }
		}

		[SetUp]
		public void SetUp()
		{
			_container = new MockContainer<TSubject>();
		}

		[TearDown]
		public void TearDown()
		{
			((IDisposable) _container).Dispose();
		}
	}
}
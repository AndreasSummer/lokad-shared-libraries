#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using Autofac;
using Autofac.Builder;
using NUnit.Framework;

namespace Lokad.Container
{
	[TestFixture]
	public sealed class PendingAutofacExtensionsTests
	{
		// ReSharper disable InconsistentNaming
		interface IMyService
		{
		}

		sealed class MyComponent : IMyService
		{
		}

		[Test]
		public void Build_case()
		{
			using (var container = new Autofac.Container())
			{
				container.Build(b => b.Register<MyComponent>().As<IMyService>());

				var component = container.Resolve<IMyService>();
				Assert.IsTrue(component is MyComponent);
			}
		}

		[Test]
		public void Pre_build_case()
		{
			var builder = new ContainerBuilder();
			builder.Register<MyComponent>().As<IMyService>();

			using (var container = new Autofac.Container())
			{
				builder.Build(container);
				var component = container.Resolve<IMyService>();
				Assert.IsTrue(component is MyComponent);
			}
		}
	}
}
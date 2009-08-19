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
	public sealed class ExtendContainerBuilderTests
	{
		// ReSharper disable InconsistentNaming
		[Test]
		public void RegisterModule()
		{
			var container = new Autofac.Container();
			container.Build(cb => cb.RegisterModule<TestModule>());
			Assert.AreEqual(1, container.Resolve<int>());
		}

		sealed class TestModule : Module
		{
			protected override void Load(ContainerBuilder builder)
			{
				builder.Register(1);
			}
		}
	}
}
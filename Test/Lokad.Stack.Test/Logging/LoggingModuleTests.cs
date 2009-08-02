#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using Autofac.Builder;
using NUnit.Framework;

namespace Lokad.Logging
{
	[TestFixture]
	public sealed class LoggingModuleTests
	{
		// ReSharper disable InconsistentNaming
		[Test]
		public void Test_Console()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new LoggingModule());
			using (var build = builder.Build())
			{
				Assert.IsNotNull(build.Resolve<ILog>());
				Assert.IsNotNull(build.Resolve<INamedProvider<ILog>>());
			}
		}

		[TearDown]
		public void TearDown()
		{
			LoggingStack.Reset();
		}
	}
}
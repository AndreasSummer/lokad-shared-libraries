#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using Autofac;
using Autofac.Builder;

namespace Lokad.Container
{
	/// <summary>
	/// Extensions for the <see cref="ContainerBuilder"/>
	/// </summary>
	public static class ExtendContainerBuilder
	{
		/// <summary>
		/// Registers the module.
		/// </summary>
		/// <typeparam name="TModule">The type of the module.</typeparam>
		/// <param name="containerBuilder">The container builder.</param>
		public static void RegisterModule<TModule>(this ContainerBuilder containerBuilder) where TModule : IModule, new()
		{
			containerBuilder.RegisterModule(new TModule());
		}
	}
}
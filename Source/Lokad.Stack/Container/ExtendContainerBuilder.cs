#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Builder;

namespace Lokad.Container
{
	/// <summary>
	/// Extensions for the <see cref="ContainerBuilder"/>
	/// </summary>
	public static class ExtendContainerBuilder
	{
		/// <see cref="AutoRegisterType"/>
		public static void AutoRegister<T>(this ContainerBuilder builder)
		{
			AutoRegisterType(typeof (T), builder);
		}

		/// <see cref="AutoRegisterType"/>
		public static void AutoRegister(this ContainerBuilder builder, IEnumerable<Type> types)
		{
			foreach (var type in types)
			{
				AutoRegisterType(type, builder);
			}
		}

		/// <summary>
		/// Performs autoregistration of all types in this assembly, 
		/// using the attrubite-based rules
		/// </summary>
		/// <param name="builder"></param>
		public static void AutoRegisterAssembly(this ContainerBuilder builder)
		{
			builder.AutoRegisterAssembly(Assembly.GetCallingAssembly());
		}

		/// <summary>
		/// Performs autoregistration of all types in the specified assembly, 
		/// using the attribute-based rules
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="assembly"></param>
		public static void AutoRegisterAssembly(this ContainerBuilder builder, Assembly assembly)
		{
			var components = assembly
				.GetExportedTypes()
				.MarkedWith<ComponentAttribute>(false);
			builder.AutoRegister(components);
		}

		/// <summary>
		/// Runs auto-registration based on the <see cref="ComponentAttribute"/>
		/// </summary>
		/// <param name="type"></param>
		/// <param name="builder"></param>
		static void AutoRegisterType(Type type, ContainerBuilder builder)
		{
			var attributes = type.GetAttributes<ComponentAttribute>(false);

			foreach (var attribute in attributes)
			{
				var register = builder.Register(type).WithScope(GetScope(attribute.Scope));

				switch (attribute.Type)
				{
					case RegistrationType.Type:
						break;
					case RegistrationType.Name:
						register.Named(attribute.Name);
						break;
					case RegistrationType.Service:
						register.As(attribute.Service);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		/// <summary>
		/// Converts <see cref="RegistrationScope"/> to <see cref="InstanceScope"/>
		/// </summary>
		/// <param name="scope"></param>
		/// <returns></returns>
		static InstanceScope GetScope(RegistrationScope scope)
		{
			switch (scope)
			{
				case RegistrationScope.Singleton:
					return InstanceScope.Singleton;
				case RegistrationScope.Container:
					return InstanceScope.Container;
				case RegistrationScope.Factory:
					return InstanceScope.Factory;
				default:
					throw new ArgumentOutOfRangeException("scope");
			}
		}

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
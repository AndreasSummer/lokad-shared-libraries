#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Container;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;

namespace Lokad.Container
{
	/// <summary>
	/// Extensions for the <see cref="ContainerBuilder"/>
	/// </summary>
	public static class ContainerBuilderExtensions
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
		private static void AutoRegisterType(Type type, ContainerBuilder builder)
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
		private static InstanceScope GetScope(RegistrationScope scope)
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
		/// Loads decorators into the container
		/// </summary>
		/// <param name="container"></param>
		/// <param name="decorators"></param>
		public static void LoadDecorators(this IContainer container, IEnumerable<DecoratorInfo> decorators)
		{
			var builder = new ContainerBuilder();

			foreach (var info in decorators)
			{
				// register decorators
				builder.Register(info.DecoratorClass)
					.FactoryScoped()
					.OwnedByContainer();

				// intercept classes that are already registered
				var typedService = new TypedService(info.Service);
				IComponentRegistration registration;
				if (container.TryGetDefaultRegistrationFor(typedService, out registration))
				{
					ApplyDecorator(info, registration);
				}
			}

			// make sure that future registrations will also be intercepted
			var dict = decorators.ToDictionary(di => new TypedService(di.Service) as Service);
			container.ComponentRegistered += (sender, e) =>
				{
					var services = e.ComponentRegistration.Descriptor.Services;
					var matchingService = services.FirstOrDefault(dict.ContainsKey);
					if (matchingService != null)
					{
						ApplyDecorator(dict[matchingService], e.ComponentRegistration);
					}
				};

			builder.Build(container);
		}

		private static void ApplyDecorator(DecoratorInfo info1, IComponentRegistration registration)
		{
			registration.Activating += (sender, e) =>
				{
					var wrapper = e.Context.Resolve(info1.DecoratorClass, new NamedParameter("inner", e.Instance));
					e.Instance = wrapper;
				};
		}
	}
}
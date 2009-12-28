#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Autofac;
using Autofac.Component;
using Autofac.Component.Activation;
using Autofac.Component.Scope;
using Rhino.Mocks;

namespace Lokad.Testing
{
	sealed class RhinoRegistrationSource : IRegistrationSource
	{
		public bool TryGetRegistration(Service service, out IComponentRegistration registration)
		{
			if (service == null)
				throw new ArgumentNullException("service");

			registration = null;

			var typedService = service as TypedService;
			if ((typedService == null))
				return false;

			var descriptor = new Descriptor(
				new UniqueService(),
				new[] {service},
				typedService.ServiceType);

			registration = new Registration(
				descriptor,
				new DelegateActivator((c, p) =>
					{
						try
						{
							return MockRepository.GenerateStub(typedService.ServiceType);
						}
						catch (Exception ex)
						{
							throw Errors.Resolution(typedService.ServiceType, ex);
						}
					}),
				new ContainerScope(),
				InstanceOwnership.Container);

			return true;
		}
	}
}
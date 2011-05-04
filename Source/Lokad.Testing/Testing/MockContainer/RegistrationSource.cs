#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Rhino.Mocks;

namespace Lokad.Testing
{
	sealed class RhinoRegistrationSource : IRegistrationSource
	{
		public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
		{

			if (service == null)
				throw new ArgumentNullException("service");

			

			var typedService = service as TypedService;
			if ((typedService == null))
				yield break;


			var newGuid = Guid.NewGuid();
			var registration = new ComponentRegistration(
				newGuid,

				new DelegateActivator(typedService.ServiceType, (c, p) =>
				{
					try
					{
						return MockRepository.GenerateStub(typedService.ServiceType);
					}
					catch (Exception ex)
					{
					    Type valueType = typedService.ServiceType;
					    throw new ResolutionException(string.Format(CultureInfo.InvariantCulture, "Error while resolving {0}", valueType), ex);
					}
				}),
				new RootScopeLifetime(),
				InstanceSharing.Shared,
				InstanceOwnership.OwnedByLifetimeScope,
				new Service[] { new UniqueService(newGuid), typedService},
				new Dictionary<string, object>());

			yield return registration;
		}

		public bool IsAdapterForIndividualComponents
		{
			get { return false; }
		}
	}
}
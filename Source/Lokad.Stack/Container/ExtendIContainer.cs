#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Autofac.Builder;
using Lokad.Quality;

namespace Autofac
{
	/// <summary>
	/// Pending extensions. To be pulled upstream to Autofac, if they prove useful in multiple projects
	/// </summary>
	public static class ExtendIContainer
	{
		/// <summary>
		/// Builds the specified registration into the container.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="registration">The registration.</param>
		public static void Build([NotNull] this IContainer container, [NotNull] Action<ContainerBuilder> registration)
		{
			if (container == null) throw new ArgumentNullException("container");
			if (registration == null) throw new ArgumentNullException("registration");

			var builder = new ContainerBuilder();
			registration(builder);
			builder.Build(container);
		}
	}
}
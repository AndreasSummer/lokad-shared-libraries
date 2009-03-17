#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Container
{
	///<summary>
	/// This attribute marks the class for the type-based registration
	///</summary>
	/// <remarks>
	/// Attribute-based registration is used in cases, when it is
	/// needed to keep registration properties (i.e. scope) close
	/// to the component in the code.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class ComponentAttribute : Attribute
	{
		/// <summary>
		/// Service represented by the component
		/// </summary>
		public Type Service { get; private set; }

		///<summary>
		/// Name of the component
		///</summary>
		public string Name { get; private set; }

		/// <summary>
		/// Registration scope for the component
		/// </summary>
		public RegistrationScope Scope { get; set; }

		/// <summary>
		/// Registration type for the component
		/// </summary>
		public RegistrationType Type { get; private set; }

		/// <summary>
		/// Is used to create type registration
		/// </summary>
		public ComponentAttribute()
		{
			Type = RegistrationType.Type;
		}

		/// <summary>
		/// Is used to define name-based registration
		/// </summary>
		/// <param name="name">Name of the component</param>
		public ComponentAttribute(string name)
		{
			Name = name;
			Type = RegistrationType.Name;
		}

		/// <summary>
		/// Is used to define service-based registration
		/// </summary>
		/// <param name="service">Service type</param>
		public ComponentAttribute(Type service)
		{
			Service = service;
			Type = RegistrationType.Service;
		}
	}
}
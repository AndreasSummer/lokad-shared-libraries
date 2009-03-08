#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace System.Container
{
	/// <summary>
	/// Enum that defines registration modes for the auto-registration
	/// </summary>
	public enum RegistrationType
	{
		/// <summary>
		/// Component is to be registered by its type
		/// </summary>
		Type = 1,
		/// <summary>
		/// Component is to be registered by its name
		/// </summary>
		Name = 2,
		/// <summary>
		/// Component is to be registered as a service
		/// </summary>
		Service = 4
	}
}
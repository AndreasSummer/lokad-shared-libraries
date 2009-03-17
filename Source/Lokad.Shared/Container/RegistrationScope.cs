#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Container
{
	///<summary>
	/// Registration scope for the component
	///</summary>
	public enum RegistrationScope
	{
		/// <summary>
		/// Component will be created once in the root container
		/// </summary>
		Singleton,
		/// <summary>
		/// Component will be cached in the current container
		/// </summary>
		Container,
		/// <summary>
		/// Every component request will return new component
		/// </summary>
		Factory
	}
}
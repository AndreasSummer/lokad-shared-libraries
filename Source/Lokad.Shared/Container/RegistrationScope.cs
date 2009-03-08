#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace System.Container
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
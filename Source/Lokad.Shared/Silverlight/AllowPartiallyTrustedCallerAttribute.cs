#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion


#if SILVERLIGHT2

using System;

namespace System.Security
{
	/// <summary>
	/// Attribute marker to make code compatible with Silverlight
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class AllowPartiallyTrustedCallersAttribute : Attribute
	{

	}
}

#endif
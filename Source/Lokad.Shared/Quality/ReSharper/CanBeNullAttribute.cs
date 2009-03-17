#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Quality
{
	/// <summary>
	/// Indicates that the value of marked element could be <c>null</c> sometimes, so the check for <c>null</c> is necessary before its usage
	/// </summary>
	[AttributeUsage(
		AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate |
			AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	[NoCodeCoverage]
	public sealed class CanBeNullAttribute : Attribute
	{
	}
}
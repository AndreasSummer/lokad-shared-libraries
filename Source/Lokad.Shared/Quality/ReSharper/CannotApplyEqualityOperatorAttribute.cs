#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Quality
{
	/// <summary>
	/// Indicates that the value of marked type (or its derivatives) cannot be compared using '==' or '!=' operators.
	/// There is only exception to compare with <c>null</c>, it is permitted
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false,
		Inherited = true)]
	[NoCodeCoverage]
	public sealed class CannotApplyEqualityOperatorAttribute : Attribute
	{
	}
}
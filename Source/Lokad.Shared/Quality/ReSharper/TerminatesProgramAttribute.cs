#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Quality
{
	/// <summary>
	/// Indicates that the marked method unconditionally terminates control flow execution.
	/// For example, it could unconditionally throw exception
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	[NoCodeCoverage]
	public sealed class TerminatesProgramAttribute : Attribute
	{
	}
}
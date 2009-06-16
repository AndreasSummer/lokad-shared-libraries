#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Quality
{
	/// <summary>
	/// <para>Class is considered to be immutable, when all fields are read-only.
	/// This makes the class safe for the multi-threaded operations.</para>
	/// <para>This attribute is used as a marker for code validation that actually enforced the rule</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
	[NoCodeCoverage]
	public sealed class ImmutableAttribute : Attribute
	{
	}
}
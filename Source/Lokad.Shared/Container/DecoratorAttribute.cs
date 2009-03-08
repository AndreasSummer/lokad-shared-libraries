#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Diagnostics.CodeAnalysis;

namespace System.Container
{
	/// <summary>
	/// This attribute is used by code-gen or DSL tools to mark the decorator class
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	[NoCodeCoverage]
	public sealed class DecoratorAttribute : Attribute
	{
		/// <summary>
		/// Service that is implemented by the decorator
		/// </summary>
		public Type Service { private set; get; }

		/// <summary>
		/// Creates new instance of the <see cref="DecoratorAttribute"/>
		/// </summary>
		/// <param name="service"></param>
		public DecoratorAttribute(Type service)
		{
			Service = service;
		}
	}
}
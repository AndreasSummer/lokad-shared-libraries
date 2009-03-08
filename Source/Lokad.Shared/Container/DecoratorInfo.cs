#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Diagnostics.CodeAnalysis;

namespace System.Container
{
	/// <summary>
	/// This class contains the information about some decorator.
	/// </summary>
	[NoCodeCoverage]
	public sealed class DecoratorInfo
	{
		/// <summary>
		/// Actual decorator class
		/// </summary>
		public Type DecoratorClass { get; private set; }

		/// <summary>
		/// Inner service
		/// </summary>
		public Type Service { get; private set; }

		/// <summary>
		/// Create new instance of the <see cref="DecoratorInfo"/>
		/// </summary>
		/// <param name="decoratorClass"></param>
		/// <param name="service"></param>
		public DecoratorInfo(Type decoratorClass, Type service)
		{
			DecoratorClass = decoratorClass;
			Service = service;
		}
	}
}
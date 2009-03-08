#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Resources;

namespace Lokad.Client
{
	/// <summary> Provides strongly-typed links to naming specifics 
	/// of Lokad implementations. </summary>
	[Immutable]
	public sealed class Specifics
	{
		internal readonly string ForecastItemName;
		readonly ResourceManager _manager;

		/// <summary>
		/// Initializes a new instance of the <see cref="Specifics"/> class.
		/// </summary>
		/// <param name="manager">The resource manager to load from.
		/// Pass in null to load default values</param>
		public Specifics(ResourceManager manager)
		{
			_manager = manager;

			ForecastItemName = LoadString(() => ForecastItemName);
		}

		string LoadString(Func<string> reference)
		{
			var name = Reflect.Variable(reference).Name;
			return null == _manager ? name : _manager.GetString(name);
		}


	}
}
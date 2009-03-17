#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.IO;
using System.Reflection;

namespace Lokad
{
	/// <summary>
	/// Simple helper class to replace common "DataMother" helper
	/// used in tests.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class ResourceUtil<T>
	{
		static readonly Assembly _assembly = typeof (T).Assembly;

		/// <summary>
		/// Gets the stream for the associated resource from the <typeparamref name="T"/>
		/// namespace.
		/// </summary>
		/// <seealso cref="Assembly.GetManifestResourceStream(Type,string)"/>
		/// <param name="name">The name of the resource.</param>
		/// <returns></returns>
		public static Stream GetStream(string name)
		{
			if (name == null) throw new ArgumentNullException("name");
			return _assembly.GetManifestResourceStream(typeof (T), name);
		}
	}
}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Reflection;
using Lokad.Quality;

namespace Lokad.Diagnostics
{
	/// <summary>
	/// Represents information about any subsystem
	/// (to be used in monitoring and reports)
	/// </summary>
	[Serializable]
	[Immutable]
	[UsedImplicitly]
	public sealed class SystemDescriptor
	{
		readonly Version _version;

		/// <summary>
		/// Version of the system
		/// </summary>
		public Version Version
		{
			get { return _version; }
		}

		readonly string _name;

		/// <summary>
		/// Name of the system. I.e.: WebApp
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		readonly string _configuration;

		/// <summary>
		/// Configuration of the system. I.e.: DEBUG
		/// </summary>
		public string Configuration
		{
			get { return _configuration; }
		}

		readonly string _instance;

		/// <summary>
		/// Initializes a new instance of the <see cref="SystemDescriptor"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="version">The version.</param>
		/// <param name="configuration">The configuration.</param>
		/// <param name="instance">The instance.</param>
		public SystemDescriptor(string name, Version version, string configuration, string instance)
		{
			Enforce.Arguments(() => name, () => version, () => configuration, () => instance);

			_name = name;
			_instance = instance;
			_version = version;
			_configuration = configuration;
		}


		static SystemDescriptor _default;

		/// <summary>
		/// Gets the default descriptor for the current system.
		/// </summary>
		/// <value>The default descriptor.</value>
		public static SystemDescriptor Default
		{
			get { return _default; }
		}

		static SystemDescriptor()
		{
			var executing = Assembly.GetExecutingAssembly();
			_default = new SystemDescriptor(executing);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SystemDescriptor"/> class.
		/// </summary>
		/// <param name="assembly">The assembly to derive configuration from.</param>
		public SystemDescriptor(Assembly assembly)
		{
			Enforce.Argument(() => assembly);

			var configuration = AssemblyUtil.GetAssemblyConfiguration(assembly);
#if !SILVERLIGHT2
			_name = (Assembly.GetEntryAssembly() ?? assembly).GetName().Name;
			_instance = Environment.CommandLine + " @ " + Environment.MachineName;
#else
			_name = assembly.GetName().Name;
			_instance = "Silverlight";
#endif
			_version = assembly.GetName().Version;
			_configuration = configuration.IsSuccess ? configuration.Value : "Default";
		}

		/// <summary>
		/// Initializes the <see cref="Default"/> value with the provided <paramref name="descriptor"/>.
		/// </summary>
		/// <param name="descriptor">The descriptor.</param>
		public static void InitializeDefault(SystemDescriptor descriptor)
		{
			Enforce.Argument(() => descriptor);
			_default = descriptor;
		}

		/// <summary>
		/// Initializes the <see cref="Default"/> value with the 
		/// <see cref="SystemDescriptor"/> that derives its values from the calling
		/// assembly.
		/// </summary>
		
		public static void InitializeDefault()
		{
			var assembly = Assembly.GetCallingAssembly();
			_default = new SystemDescriptor(assembly);
		}

		/// <summary>
		/// Instance descriptor. I.e.: 127.0.0.1
		/// </summary>
		public string Instance
		{
			get { return _instance; }
		}

		/// <summary> <see cref="object.ToString"/> </summary>
		public override string ToString()
		{
			return string.Format("{0} ver. {1}, '{2}' at '{3}'", Name, Version, Configuration, Instance);
		}
	}
}
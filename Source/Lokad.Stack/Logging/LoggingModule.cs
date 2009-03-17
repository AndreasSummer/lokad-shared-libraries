#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Autofac;
using Autofac.Builder;

namespace Lokad.Logging
{
	/// <summary>
	/// Autofac extension module that provides integration with the log4net
	/// </summary>
	public sealed class LoggingModule : IModule
	{
		LoggingMode _mode = LoggingMode.Console;

		/// <summary>
		/// Informs the module to use the config file
		/// </summary>
		public bool UseConfig
		{
			get { return _mode == LoggingMode.Config; }
			set { _mode = value ? LoggingMode.Config : LoggingMode.Console; }
		}


		string _fileName;

		/// <summary>
		/// Informs the module to use the provided file
		/// </summary>
		public string FileName
		{
			get { return _fileName; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new InvalidOperationException();

				_fileName = value;
				_mode = LoggingMode.File;
			}
		}

		/// <summary>
		/// <see cref="IModule.Configure"/>
		/// </summary>
		/// <param name="container"></param>
		public void Configure(IContainer container)
		{
			switch (_mode)
			{
				case LoggingMode.Console:
					LoggingStack.UseConsole();
					break;
				case LoggingMode.Config:
					LoggingStack.UseConfig();
					break;
				case LoggingMode.File:
					LoggingStack.ConfigureFromFile(_fileName);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			var builder = new ContainerBuilder();

			// register log provider
			builder.Register(LoggingStack.GetLogProvider());
			builder.Register(c => c.Resolve<INamedProvider<ILog>>().Get("Default"));
			builder.Build(container);
		}
	}
}
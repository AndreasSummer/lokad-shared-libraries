#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository;
using ILog=System.ILog;

namespace Lokad.Logging
{
	///<summary>
	/// This class provides hookup routines to the logging library
	///</summary>
	public static class LoggingStack
	{
		/// <summary>
		/// Configures the logging system to write to console
		/// </summary>
		public static LogSyntax UseConsole()
		{
			var appender = ConfiguratorHelper.GetConsoleLog();
			Configure(appender);
			return new LogSyntax(appender);
		}

		/// <summary>
		/// Logging system is configured from App.config
		/// </summary>
		public static void UseConfig()
		{
			XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly()));
		}

		/// <summary>
		/// Resets logging configuration to the default state.
		/// </summary>
		public static void Reset()
		{
			LogManager.GetRepository(Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly()).ResetConfiguration();
		}

		/// <summary>
		/// File is used to configure the logging system
		/// </summary>
		/// <param name="fileName"></param>
		public static void ConfigureFromFile(string fileName)
		{
			XmlConfigurator.Configure(new FileInfo(fileName));
		}

		/// <summary> Defines logging to Windows Event Log and returns syntax
		/// to tweak the settings </summary>
		/// <param name="logName">Name of the event log.</param>
		/// <param name="applicationName">Name of the application.</param>
		/// <returns>configuration syntax</returns>
		public static LogSyntax UseEventLog(string logName, string applicationName)
		{
			Enforce.ArgumentNotEmpty(() => logName);
			Enforce.ArgumentNotEmpty(() => applicationName);

			var appender = ConfiguratorHelper.GetEventLogAppender(logName, applicationName);
			Configure(appender);
			return new LogSyntax(appender);
		}

		/// <summary>
		/// Defines logging to rolling text logs with one log per day
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>configuration syntax</returns>
		public static LogSyntax UseDailyLog(string path)
		{
			Enforce.ArgumentNotEmpty(() => path);

			EnsurePath(path);

			var appender = ConfiguratorHelper.GetDailyLog(path);
			Configure(appender);
			return new LogSyntax(appender);
		}

		/// <summary>
		/// Defines logging to rolling text logs with <paramref name="maxSize"/> 
		/// and <paramref name="numberOfBackups"/> to keep.
		/// </summary>
		/// <param name="path">The path to store logs in.</param>
		/// <param name="maxSize">Max size of the log.</param>
		/// <param name="numberOfBackups">The number of backups.</param>
		/// <returns>configuration syntax</returns>
		public static LogSyntax UseRollingLog(string path, long maxSize, int numberOfBackups)
		{
			Enforce.ArgumentNotEmpty(() => path);
			EnsurePath(path);

			var appender = ConfiguratorHelper.GetRollingLog(path, numberOfBackups, maxSize);
			Configure(appender);
			return new LogSyntax(appender);
		}

		static void EnsurePath(string path)
		{
			var directory = Path.GetDirectoryName(path);

			if (!string.IsNullOrEmpty(directory))
			{
				Directory.CreateDirectory(directory);
			}
		}

		/// <summary>
		/// Get log provider
		/// </summary>
		/// <returns>provider instance</returns>
		public static INamedProvider<ILog> GetLogProvider()
		{
			return LogProviderWrapper.Instance;
		}

		/// <summary>
		/// Gets the log with the "Default" name.
		/// </summary>
		/// <returns>new log instance</returns>
		public static ILog GetLog()
		{
			return LogWrapper.GetByName("Default");
		}

		static void Configure(IAppender appender)
		{
			var repository = LogManager.GetRepository(Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly());
			var config = (IBasicRepositoryConfigurator) repository;
			config.Configure(appender);
		}
	}
}
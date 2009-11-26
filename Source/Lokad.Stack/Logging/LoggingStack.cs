#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
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
using Lokad.Quality;

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
		/// <param name="configure">The configuration option.</param>
		/// <returns>log syntax</returns>
		public static LogSyntax UseConsoleLog([NotNull] Action<LogOptions> configure)
		{
			Enforce.Argument(() => configure);
			var options = ConfiguratorHelper.GetDefaultOptions();
			configure(options);
			var appender = ConfiguratorHelper.BuildConsoleLog(options);
			Configure(appender);
			return new LogSyntax(appender);
		}


		/// <summary>
		/// Configures the logging system to write to trace
		/// </summary>
		/// <param name="configure">The configuration option.</param>
		/// <returns>log syntax</returns>
		public static LogSyntax UseTraceLog([NotNull] Action<ListeningLogOptions> configure)
		{
			Enforce.Argument(() => configure);
			var options = ConfiguratorHelper.GetDefaultListeningOptions();
			configure(options);
			var appender = ConfiguratorHelper.BuildTraceLog(options);
			Configure(appender);
			return new LogSyntax(appender);
		}


		/// <summary>
		/// Configures the logging system to write to the debug listeners
		/// </summary>
		/// <param name="configure">The configuration option.</param>
		/// <returns>log syntax</returns>
		public static LogSyntax UseDebugLog([NotNull] Action<ListeningLogOptions> configure)
		{
			Enforce.Argument(() => configure);
			var options = ConfiguratorHelper.GetDefaultListeningOptions();
			configure(options);
			var appender = ConfiguratorHelper.BuildDebugLog(options);
			Configure(appender);
			return new LogSyntax(appender);
		}

		/// <summary>
		/// Configures the logging system to write to console
		/// </summary>
		/// <returns>log syntax</returns>
		public static LogSyntax UseTraceLog()
		{
			return UseTraceLog(c => { });
		}


		/// <summary>
		/// Configures the logging system to write to console
		/// </summary>
		/// <returns>log syntax</returns>
		public static LogSyntax UseDebugLog()
		{
			return UseTraceLog(c => { });
		}

		/// <summary>
		/// Configures the logging system to write to console
		/// </summary>
		/// <returns>log syntax</returns>
		public static LogSyntax UseConsoleLog()
		{
			return UseConsoleLog(c => { });
		}

		/// <summary>
		/// Configures the logging system to write to console with colors
		/// </summary>
		/// <param name="configure">The configuration options.</param>
		/// <returns>log syntax</returns>
		public static LogSyntax UseColoredConsoleLog([NotNull] Action<LogOptions> configure)
		{
			Enforce.Argument(() => configure);
			var options = ConfiguratorHelper.GetDefaultOptions();
			configure(options);
			var appender = ConfiguratorHelper.BuildColoredConsoleLog(options);
			Configure(appender);
			return new LogSyntax(appender);
		}


		/// <summary>
		/// Configures the logging system to write to console with colors
		/// </summary>
		/// <returns>log syntax</returns>
		public static LogSyntax UseColoredConsoleLog()
		{
			return UseColoredConsoleLog(l => { });
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
		public static ILogProvider GetLogProvider()
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
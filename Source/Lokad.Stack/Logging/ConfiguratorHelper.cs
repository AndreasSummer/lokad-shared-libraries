#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;

namespace Lokad.Logging
{
	using Colors = ColoredConsoleAppender.Colors;

	static class ConfiguratorHelper
	{
		internal static Level ToLog4Net(this LogLevel level)
		{
			switch (level)
			{
				case LogLevel.Min:
					return Level.All;
				case LogLevel.Debug:
					return Level.Debug;
				case LogLevel.Info:
					return Level.Info;
				case LogLevel.Warn:
					return Level.Warn;
				case LogLevel.Error:
					return Level.Error;
				case LogLevel.Fatal:
					return Level.Fatal;
				case LogLevel.Max:
					return Level.Off;
				default:
					throw new ArgumentOutOfRangeException("level");
			}
		}

		internal static RollingFileAppender GetDailyLog(string path)
		{
			var layout = new PatternLayout("%date [%thread] %-5level %logger [%property{NDC}] - %message%newline");
			layout.ActivateOptions();
			var log = new RollingFileAppender
				{
					File = path,
					AppendToFile = true,
					RollingStyle = RollingFileAppender.RollingMode.Date,
					DatePattern = "yyyyMMdd",
					Layout = layout
				};
			log.ActivateOptions();
			return log;
		}


		internal static EventLogAppender GetEventLogAppender(string logName, string applicationName)
		{
			var layout = new PatternLayout("%date [%thread] %-5level %logger [%property{NDC}] - %message%newline");
			layout.ActivateOptions();
			var appender = new EventLogAppender
				{
					ApplicationName = applicationName,
					LogName = logName,
					Layout = layout,
				};
			appender.ActivateOptions();
			return appender;
		}


		internal static RollingFileAppender GetRollingLog(string path, int numberOfBackups, long size)
		{
			var layout = new PatternLayout("%date [%thread] %-5level %logger [%property{NDC}] - %message%newline");
			layout.ActivateOptions();

			var log = new RollingFileAppender()
				{
					File = path,
					AppendToFile = true,
					RollingStyle = RollingFileAppender.RollingMode.Size,
					MaxSizeRollBackups = numberOfBackups,
					StaticLogFileName = true,
					MaxFileSize = size,
					Layout = layout
				};
			log.ActivateOptions();
			return log;
		}

		internal static ConsoleAppender GetConsoleLog()
		{
			var layout = new PatternLayout
				{
					ConversionPattern = "%timestamp [%thread] %level %logger %ndc - %message%newline"
				};
			layout.ActivateOptions();
			var appender = new ConsoleAppender
				{
					Layout = layout
				};
			appender.ActivateOptions();
			return appender;
		}

		internal static ColoredConsoleAppender GetColoredConsoleLog()
		{
			var layout = new PatternLayout
			{
				ConversionPattern = "%timestamp [%thread] %-5level %logger - %message%newline"
			};
			layout.ActivateOptions();
			var appender = new ColoredConsoleAppender
			{
				Layout = layout
			};

			Map(appender, Colors.Red, Level.Alert, Level.Critical, Level.Emergency, Level.Error, Level.Fatal, Level.Severe);
			Map(appender, Colors.Cyan | Colors.HighIntensity, Level.Info, Level.Notice);
			Map(appender, Colors.Yellow, Level.Warn);

			appender.ActivateOptions();
			return appender;
		}
		static void Map(ColoredConsoleAppender appender, Colors fore, params Level[] levels)
		{
			foreach (var level in levels)
			{
				appender.AddMapping(new ColoredConsoleAppender.LevelColors()
					{
						ForeColor = fore,
						Level = level
					});
			}
		}
	}
}
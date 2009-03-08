#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using log4net;
using ILog=System.ILog;

namespace Lokad.Logging
{
	/// <summary>
	/// Wrapper around <see cref="log4net.ILog"/>
	/// </summary>
	internal sealed class LogWrapper : ILog
	{
		private readonly log4net.ILog _inner;

		internal static ILog GetByName(string logName)
		{
			return new LogWrapper(LogManager.GetLogger(logName));
		}
		
		LogWrapper(log4net.ILog inner)
		{
			_inner = inner;
		}

		void ILog.Log(LogLevel level, Exception exception, object message)
		{
			switch (level)
			{
				case LogLevel.Debug:
					_inner.Debug(message, exception);
					break;
				case LogLevel.Info:
					_inner.Info(message, exception);
					break;
				case LogLevel.Warn:
					_inner.Warn(message, exception);
					break;
				case LogLevel.Error:
					_inner.Error(message, exception);
					break;
				case LogLevel.Fatal:
					_inner.Fatal(message, exception);
					break;
				default:
					throw new ArgumentOutOfRangeException("level");
			}
		}

		public bool IsEnabled(LogLevel level)
		{
			switch (level)
			{
				case LogLevel.Debug:
					return _inner.IsDebugEnabled;
				case LogLevel.Info:
					return _inner.IsInfoEnabled;
				case LogLevel.Warn:
					return _inner.IsWarnEnabled;
				case LogLevel.Error:
					return _inner.IsErrorEnabled;
				case LogLevel.Fatal:
					return _inner.IsFatalEnabled;
				default:
					throw new ArgumentOutOfRangeException("level");
			}
		}

		void ILog.Log(LogLevel level, object message)
		{
			switch (level)
			{
				case LogLevel.Debug:
					_inner.Debug(message);
					break;
				case LogLevel.Info:
					_inner.Info(message);
					break;
				case LogLevel.Warn:
					_inner.Warn(message);
					break;
				case LogLevel.Error:
					_inner.Error(message);
					break;
				case LogLevel.Fatal:
					_inner.Fatal(message);
					break;
				default:
					throw new ArgumentOutOfRangeException("level");
			}
		}
	}
}
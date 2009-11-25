#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Quality;
using Lokad.Rules;

namespace Lokad
{
	/// <summary> <see cref="IScope"/> that maintains scope path and writes inner messages 
	/// to the log  with the proper path
	/// </summary>
	[Serializable]
	[UsedImplicitly]
	public sealed class LogScope : IScope
	{
		readonly string _name;
		readonly ILogProvider _logProvider;
		readonly ILog _log;
		RuleLevel _level;

		readonly Action<RuleLevel> _dispose = level => { };

		void IDisposable.Dispose()
		{
			_dispose(_level);
		}

		internal LogScope(string name, ILogProvider provider, Action<RuleLevel> dispose)
		{
			_name = name;
			_dispose = dispose;
			_logProvider = provider;
			_log = _logProvider.Get(name);
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="LogScope"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="logProvider">The log provider.</param>
		public LogScope(string name, ILogProvider logProvider)
		{
			_name = name;
			_logProvider = logProvider;
			_dispose = level => { };
			_log = _logProvider.Get(name);
		}

		IScope IScope.Create(string name)
		{
			return new LogScope(Scope.ComposePath(_name, name), _logProvider, level =>
				{
					if (level > _level)
						_level = level;
				});
		}

		void IScope.Write(RuleLevel level, string message)
		{
			if (level > _level)
				_level = level;

			_log.Log(MatchLevel(level), message);
		}

		static LogLevel MatchLevel(RuleLevel level)
		{
			switch (level)
			{
				case RuleLevel.None:
					return LogLevel.Info;
				case RuleLevel.Warn:
					return LogLevel.Warn;
				case RuleLevel.Error:
					return LogLevel.Error;
				default:
					throw new ArgumentOutOfRangeException("level");
			}
		}

		RuleLevel IScope.Level
		{
			get { return _level; }
		}
	}
}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;

namespace Lokad.Rules
{
	/// <summary>
	/// <see cref="IScope"/> that maintains scope path, executes 
	/// <see cref="Messenger"/> delegate per every message.
	/// </summary>
	[Serializable]
	public sealed class SimpleScope : IScope
	{
		/// <summary>
		/// Delegate for relaying scope messages
		/// </summary>
		public delegate void Messenger(string path, RuleLevel level, string message);

		readonly string _name;
		readonly Messenger _messenger;
		RuleLevel _level;

		readonly Action<RuleLevel> _dispose = level => { };

		void IDisposable.Dispose()
		{
			_dispose(_level);
		}

		internal SimpleScope(string name, Messenger messenger, Action<RuleLevel> dispose)
		{
			_name = name;
			_messenger = messenger;
			_dispose = dispose;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleScope"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="messenger">The messenger.</param>
		public SimpleScope(string name, Messenger messenger)
		{
			_name = name;
			_messenger = messenger;
			_dispose = level => { };
		}

		IScope IScope.Create(string name)
		{
			return new SimpleScope(Scope.ComposePathInternal(_name, name), _messenger, level =>
				{
					if (level > _level)
						_level = level;
				});
		}

		void IScope.Write(RuleLevel level, string message)
		{
			if (level > _level)
				_level = level;

			_messenger(_name, level, message);
		}

		RuleLevel IScope.Level
		{
			get { return _level; }
		}

		internal static IScope ForEnforceArgument(string name, Predicate<RuleLevel> throwIf)
		{
			return new SimpleScope(name, (path, level, message) =>
				{
					if (throwIf(level))
						throw new ArgumentException(message, path);
				});
		}

		internal static IScope ForEnforce(string name, Predicate<RuleLevel> throwIf)
		{
			return new SimpleScope(name, (path, level, message) =>
				{
					if (throwIf(level))
						throw new RuleException(message, path);
				});
		}

		static IScope ForMessages(string name, Action<RuleMessages> action)
		{
			var messages = new List<RuleMessage>();
			return new SimpleScope(name,
				(path, level, message) => messages.Add(new RuleMessage(path, level, message)),
				level => action(new RuleMessages(messages, level)));
		}

		/// <summary>
		/// Gets the messages reported by the specified action to the scope.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="action">The action.</param>
		/// <returns>array of rule messages reported</returns>
		public static RuleMessages GetMessages(string name, Action<IScope> action)
		{
			if (name == null) throw new ArgumentNullException("name");

			var messages = RuleMessages.Empty;
			using (var scope = ForMessages(name, m => messages = m))
			{
				action(scope);
			}
			return messages;
		}

		internal static IScope ForValidation(string name, Predicate<RuleLevel> throwIf)
		{
			return ForMessages(name, messages =>
				{
					if (throwIf(messages.Level))
						throw new RuleException(messages);
				});
		}
	}
}
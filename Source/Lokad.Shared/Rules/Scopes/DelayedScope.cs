using System.Collections.Generic;

namespace System.Rules
{
	/// <summary>
	/// This scope is just like <see cref="SimpleScope"/>
	/// but it delays name resolution
	/// </summary>
	[Serializable]
	sealed class DelayedScope : IScope
	{
		public delegate void Messenger(Func<string> pathProvider, RuleLevel level, string message);

		readonly Func<string> _name;
		readonly Messenger _messenger;
		readonly Action<RuleLevel> _dispose = level => { };
		string _cachedName;

		RuleLevel _level;

		string GetName()
		{
			if (null == _cachedName)
			{
				_cachedName = _name();
			}
			return _cachedName;
		}

		public DelayedScope(Func<string> name, Messenger action)
		{
			_name = name;
			_messenger = action;
		}

		DelayedScope(Func<string> name, Messenger action, Action<RuleLevel> dispose)
		{
			_name = name;
			_messenger = action;
			_dispose = dispose;
		}

		void IDisposable.Dispose()
		{
			_dispose(_level);
		}

		IScope IScope.Create(string name)
		{
			return new DelayedScope(() => Scope.ComposePathInternal(GetName(), name), _messenger, level =>
				{
					if (level > _level)
						_level = level;
				});
		}

		void IScope.Write(RuleLevel level, string message)
		{
			if (level > _level)
				_level = level;

			_messenger(GetName, level, message);
		}

		RuleLevel IScope.Level
		{
			get { return _level; }
		}

		public static IScope ForEnforceArgument<T>(Func<T> argumentReference, Predicate<RuleLevel> predicate)
		{
			return new DelayedScope(
				() => Reflection.Reflect.VariableName(argumentReference),
				(func, level, s) =>
					{
						if (predicate(level))
							throw new ArgumentException(s, func());
					}
				);
		}

		public static IScope ForEnforce<T>(Func<T> reference, Predicate<RuleLevel> predicate)
		{
			return new DelayedScope(
				() => Reflection.Reflect.VariableName(reference),
				(func, level, s) =>
					{
						if (predicate(level))
							throw new RuleException(s, func());
					}
				);
		}

		static IScope ForMessages<T>(Func<T> reference, Action<RuleMessages> action)
		{
			var messages = new List<RuleMessage>();
			return new DelayedScope(
				() => Reflection.Reflect.VariableName(reference),
				(func, level, message) => messages.Add(new RuleMessage(func(), level, message)),
				level => action(new RuleMessages(messages, level)));
		}


		public static RuleMessages GetMessages<T>(Func<T> reference, Action<IScope> action)
		{
			var messages = RuleMessages.Empty;
			using (var scope = ForMessages(reference, m => messages = m))
			{
				action(scope);
			}
			return messages;
		}
	}
}
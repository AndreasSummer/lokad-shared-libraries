using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Rules;
using System.Windows.Forms;

namespace RulesUI
{
	/// <summary>
	/// Allows to bind validation results to <see cref="ErrorProvider"/>
	/// in a strongly-typed manner </summary>
	/// <typeparam name="TTarget">The type of the target.</typeparam>
	public sealed class Validator<TTarget>
	{
		private readonly Control _defaultControl;
		private readonly ErrorProvider _provider;
		private readonly string _scope = Guid.NewGuid().ToString();
		private readonly IList<Tuple<Control, string>> _bindings = new List<Tuple<Control, string>>();


		public Validator(Control defaultControl, ErrorProvider provider)
		{
			_defaultControl = defaultControl;
			_provider = provider;
		}

		public RuleLevel RunRules(TTarget item, params Rule<TTarget>[] rules)
		{
			_provider.Clear();
			var messages = Scope.GetMessages(item, _scope, rules);
			if (messages.Level != RuleLevel.None)
			{
				Display(messages);
			}

			return messages.Level;
		}

		public Validator<TTarget> Bind<TProperty>(Control control, Expression<Func<TTarget, TProperty>> expression)
		{
			var path = ScopePending.GetPath(expression);
			var fullPath = ScopePending.Compose(_scope, path);
			_bindings.Add(control, fullPath);
			return this;
		}

		private void Display(IEnumerable<RuleMessage> messages)
		{
			var used = messages.ToList();
			foreach (var binding in _bindings)
			{
				var binding1 = binding;
				var match = messages
					.Where(m => m.Path == binding1.Item2);
				DisplayErrors(binding.Item1, match);
				foreach (var message in match)
				{
					used.Remove(message);
				}
			}
			if (used.Count > 0)
			{
				DisplayErrors(_defaultControl, used);
			}
		}

		private void DisplayErrors(Control control, IEnumerable<RuleMessage> match)
		{
			var join = match
				.Select(m => m.Message);
			if (match.Count() != 0)
			{
				_provider.SetError(control, join.Join(Environment.NewLine));
			}
		}
	}
}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Rules;
using System.Windows.Forms;

namespace Lokad.Client.Forms
{
	/// <summary>
	/// Allows to bind validation results to <see cref="ErrorProvider"/>
	/// in a strongly-typed manner </summary>
	/// <typeparam name="TModel">The type of the target.</typeparam>
	[Immutable]
	public sealed class Validator<TModel>
	{
		readonly Control _defaultControl;
		readonly ErrorProvider _provider;
		readonly string _scope = Guid.NewGuid().ToString();
		readonly IList<Tuple<Control, string>> _bindings = new List<Tuple<Control, string>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="Validator{TModel}"/> class.
		/// </summary>
		/// <param name="defaultControl">The default control.</param>
		/// <param name="provider">The provider.</param>
		public Validator(Control defaultControl, ErrorProvider provider)
		{
			_defaultControl = defaultControl;
			_provider = provider;
		}

		/// <summary>
		/// Executes the rules, binds them to the UI and returns the messages
		/// </summary>
		/// <param name="model">The model to validate.</param>
		/// <param name="rules">The rules.</param>
		/// <returns>messages associated with the validation run</returns>
		public RuleMessages RunRules(TModel model, params Rule<TModel>[] rules)
		{
			Clear();
			var messages = Scope.GetMessages(model, _scope, rules);
			if (messages.Level != RuleLevel.None)
			{
				Display(messages.Where(rm => rm.Level > RuleLevel.None));
			}

			return messages;
		}

		/// <summary>
		/// Clears errors on all controls bound to this validator
		/// </summary>
		public void Clear()
		{
			// we can't call simply clear the entire validator, 
			// because the error provider
			// might be shared with other validators
			foreach (var tuple in _bindings)
			{
				_provider.SetError(tuple.Item1, string.Empty);
			}
		}

		/// <summary>
		/// Binds the specified control to some property expression.
		/// </summary>
		/// <typeparam name="TProperty">The type of the property to bind to.</typeparam>
		/// <param name="control">The control to bind to.</param>
		/// <param name="expression">The expression that points to some property.</param>
		/// <returns>same validator instance</returns>
		public Validator<TModel> Bind<TProperty>(Control control, Expression<Func<TModel, TProperty>> expression)
		{
			var path = ScopePending.GetPath(expression);
			var fullPath = Scope.ComposePath(_scope, path);
			_bindings.Add(control, fullPath);

			return this;
		}

		void Display(IEnumerable<RuleMessage> messages)
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

		void DisplayErrors(Control control, IEnumerable<RuleMessage> match)
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
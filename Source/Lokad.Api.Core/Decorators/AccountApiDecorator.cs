#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Rules;

namespace Lokad.Api
{
	/// <summary>
	/// Decorator for the <see cref="IAccountApi"/> that 
	/// adds validation and reliability layers
	/// </summary>
	[Immutable]
	public sealed class AccountApiDecorator : IAccountApi
	{
		readonly ActionPolicy _policy;
		readonly IAccountApi _inner;
		readonly INamedProvider<IScope> _scopes;
		readonly ILog _log;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccountApiDecorator"/> class.
		/// </summary>
		/// <param name="policy">The exception policy to use.</param>
		/// <param name="inner">The instance of <see cref="IAccountApi"/> to wrap.</param>
		/// <param name="scope">The validation scope to leverage.</param>
		/// <param name="log">The log.</param>
		public AccountApiDecorator(ActionPolicy policy, IAccountApi inner, INamedProvider<IScope> scope, ILog log)
		{
			Enforce.Arguments(() => policy, () => inner, () => scope, () => log);

			_policy = policy;
			_log = log;
			_scopes = scope;
			_inner = inner;
		}

		AccountInfo IAccountApi.GetAccountInfo(Identity identity)
		{
			_scopes.Validate(identity, "identity", ApiRules.Identity);

			_log.DebugFormat("GetAccountInfo()");

			return _policy.Get(() => _inner.GetAccountInfo(identity));
		}

		void IAccountApi.SetPartner(Identity identity, long partnerId)
		{
			_scopes.Validate(identity, "identity", ApiRules.Identity);

			_log.DebugFormat("SetPartner()");

			_policy.Do(() => _inner.SetPartner(identity, partnerId));
		}
	}
}
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
	/// Decorator for the <see cref="ISystemApi"/>
	/// </summary>
	[Immutable]
	public sealed class SystemApiDecorator : ISystemApi
	{
		readonly ActionPolicy _policy;
		readonly ISystemApi _inner;
		readonly ILog _log;
		readonly INamedProvider<IScope> _scopes;

		/// <summary>
		/// Initializes a new instance of the <see cref="SystemApiDecorator"/> class.
		/// </summary>
		/// <param name="inner">The inner.</param>
		/// <param name="scopes">The scopes.</param>
		/// <param name="log">The log.</param>
		/// <param name="policy">The policy.</param>
		public SystemApiDecorator(ActionPolicy policy, ISystemApi inner, INamedProvider<IScope> scopes, ILog log)
		{
			_inner = inner;
			_scopes = scopes;
			_log = log;
			_policy = policy;
		}

		Guid ISystemApi.AddReport(Identity identity, Report report)
		{
			using (var scope = _scopes.Get("AddReport"))
			{
				scope.Validate(identity.Username, "identity.Username", ApiRules.UserName);
				scope.Validate(report, "report", ApiRules.Report);
			}

			_log.Debug("AddReport");

			return _policy.Get(() => _inner.AddReport(identity, report));
		}
	}
}
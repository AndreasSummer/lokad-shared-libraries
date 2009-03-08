using System;
using System.Rules;

namespace Lokad.Api
{
	using Limits = LokadApiRequestLimits;

	/// <summary>
	/// Decorator for the <see cref="ILegacyApi"/>
	/// </summary>
	public sealed class LegacyApiDecorator : ILegacyApi
	{
		readonly ILegacyApi _inner;
		readonly INamedProvider<IScope> _scopes;
		readonly ILog _log;
		readonly ActionPolicy _policy;

		/// <summary> Initializes a new instance of the <see cref="LegacyApiDecorator"/> class. </summary>
		/// <param name="inner">The inner instance.</param>
		/// <param name="policy">The exception policy.</param>
		/// <param name="scopes">The scopes to report problems to.</param>
		/// <param name="log">The log.</param>
		public LegacyApiDecorator(ActionPolicy policy, ILegacyApi inner, INamedProvider<IScope> scopes, ILog log)
		{
			_inner = inner;
			_policy = policy;
			_scopes = scopes;
			_log = log;
		}

		//SerieInfo[] ILegacyApi.GetSeriesByNames(Identity identity, string[] serieNames)
		//{
		//    using (var scope = _scopes.Get("GetSeries"))
		//    {
		//        scope.Validate(identity, "identity", ApiRules.Identity);
		//        scope.ValidateMany(serieNames, "serieNames", LokadApiRequestLimits.GetSeriesLegacy, ApiRules.SerieName);
		//    }

		//    _log.DebugFormat("GetSeries({0})", serieNames.Length);

		//    return _handler.Get(() => _inner.GetSeriesByNames(identity, serieNames));
		//}


		SerieInfoPage ILegacyApi.GetSeriesByPrefix(Identity identity, string prefix, Guid cursor, int pageSize)
		{
			using (var scope = _scopes.Get("GetSerieInfos"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.Validate(pageSize, "pageSize", Limits.GetSeriesByPrefix_Page);
				scope.Validate(prefix, "prefix", 
					StringIs.Limited(2,32), 
					ApiRules.ValidName, 
					StringIs.Without('%'));
			}

			_log.DebugFormat("GetSeriesByPrefix({0} @ {1}, '{2}')", pageSize, cursor, prefix);

			return _policy.Get(() => _inner.GetSeriesByPrefix(identity, prefix, cursor, pageSize));
		}

	}
}
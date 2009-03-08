#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Rules;
using System.Linq;

namespace Lokad.Api
{

	using Limits = LokadApiRequestLimits;

	/// <summary>
	/// Decorator for the <see cref="ITimeSerieApi"/> that 
	/// adds validation and reliability layers
	/// </summary>
	[Immutable]
	public sealed class TimeSerieApiDecorator : ITimeSerieApi
	{
		readonly ActionPolicy _policy;
		readonly ITimeSerieApi _inner;
		readonly INamedProvider<IScope> _scopes;
		readonly ILog _log;
		readonly static TimeSerieApiCounters _countersFor = new TimeSerieApiCounters();

		/// <summary>
		/// Initializes a new instance of the <see cref="TimeSerieApiDecorator"/> class.
		/// </summary>
		/// <param name="policy">The exception policy.</param>
		/// <param name="inner">The inner instance to wrap.</param>
		/// <param name="scopes">The validation scopes.</param>
		/// <param name="log">The log.</param>
		public TimeSerieApiDecorator(ActionPolicy policy, ITimeSerieApi inner, INamedProvider<IScope> scopes, ILog log)
		{
			Enforce.Arguments(() => policy, () => inner, () => scopes, () => log);

			var timer = _countersFor.Ctor.Open();

			_policy = policy;
			_log = log;
			_scopes = scopes;
			_inner = inner;

			_countersFor.Ctor.Close(timer);
		}

		Guid[] ITimeSerieApi.AddSeries(Identity identity, SerieInfo[] series)
		{
			using (var scope = _scopes.Get("AddSeries"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.ValidateMany(series, "series", LokadApiRequestLimits.AddSeries, ApiRules.NewSerie);
			}

			_log.DebugFormat("AddSeries({0})", series.Length);

			return _policy.Get(() =>
				{
					var timer = _countersFor.AddSeries.Open(series.Length);
					var result = _inner.AddSeries(identity, series);
					_countersFor.AddSeries.Close(timer);
					return result;
				});
		}

		void ITimeSerieApi.DeleteSeries(Identity identity, Guid[] serieIDs)
		{
			using (var scope = _scopes.Get("DeleteSeries"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.ValidateMany(serieIDs, "serieIDs", LokadApiRequestLimits.DeleteSeries, Is.NotEqual(Guid.Empty));
			}

			_log.DebugFormat("DeleteSeries({0})", serieIDs.Length);

			_policy.Do(() =>
				{
					var timer = _countersFor.DeleteSeries.Open(serieIDs.Length);
					_inner.DeleteSeries(identity, serieIDs);
					_countersFor.DeleteSeries.Close(timer);
				});
		}

		SerieInfoPage ITimeSerieApi.GetSeries(Identity identity, Guid cursor, int pageSize)
		{
			using (var scope = _scopes.Get("GetSerieInfos"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.Validate(pageSize, "pageSize", Limits.GetSeries_Page);
			}

			_log.DebugFormat("GetSeries({0} @ {1})", pageSize, cursor);

			return _policy.Get(() =>
				{
					var timer = _countersFor.GetSeries.Open(pageSize);
					var result = _inner.GetSeries(identity, cursor, pageSize);
					_countersFor.GetSeries.Close(timer, result.Series.Length);
					return result;
				});
		}

		void ITimeSerieApi.UpdateSerieSegments(Identity identity, SegmentForSerie[] segments)
		{
			using (var scope = _scopes.Get("UpdateSerieSegments"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.Validate(segments, "segments", ApiRules.Segments);
			}

			if (_log.IsDebugEnabled())
			{
				_log.DebugFormat("UpdateSerieSegments({0})", segments.ToDebug());
			}

			var sum = segments.Sum(s => s.Values.Length);

			_policy.Do(() =>
				{
					var timer = _countersFor.UpdateSerieSegments.Open(segments.Length, sum);
					_inner.UpdateSerieSegments(identity, segments);
					_countersFor.UpdateSerieSegments.Close(timer);
				});

		}

		void ITimeSerieApi.SetTags(Identity identity, TagsForSerie[] tagsForSerie)
		{
			using (var scope = _scopes.Get("SetTags"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.Validate(tagsForSerie, "tagsForSerie", ApiRules.SetTags);
			}

			_log.DebugFormat("SetTags({0})", tagsForSerie.Length);

			var sum = tagsForSerie.Sum(t => t.Tags.Length);

			_policy.Do(() =>
				{
					var timer = _countersFor.SetTags.Open(tagsForSerie.Length, sum);
					_inner.SetTags(identity, tagsForSerie);
						_countersFor.SetTags.Close(timer);
				});
		}

		void ITimeSerieApi.SetEvents(Identity identity, EventsForSerie[] eventsForSerie)
		{
			using (var scope = _scopes.Get("SetEvents"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.Validate(eventsForSerie, "eventsForSerie", ApiRules.SetEvents);
			}

			_log.DebugFormat("SetEvents({0})", eventsForSerie.Length);

			var sum = eventsForSerie.Sum(e => e.Events.Length);

			_policy.Do(() =>
				{
					var timer = _countersFor.SetEvents.Open(eventsForSerie.Length, sum);
					_inner.SetEvents(identity, eventsForSerie);
					_countersFor.SetEvents.Close(timer);
				});
		}

		SerieSegmentPage ITimeSerieApi.GetSerieSegments(Identity identity, Guid[] serieIDs, SegmentCursor cursor, int pageSize)
		{
			using (var scope = _scopes.Get("GetSerieSegments"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.ValidateMany(serieIDs, "serieIDs", LokadApiRequestLimits.GetSegments_Series, Is.NotDefault);
				scope.Validate(cursor, "cursor");
				scope.Validate(pageSize, "pageSize", Limits.GetSerieSegments_Page);
			}

			_log.DebugFormat("GetSerieSegments({0} @ {1})", serieIDs.Length, cursor.Cursor1.ToDebug());

			return _policy.Get(() =>
				{
					var timer = _countersFor.GetSerieSegments.Open(serieIDs.Length, pageSize);
					var result = _inner.GetSerieSegments(identity, serieIDs, cursor, pageSize);

					var sum = result.Segments.Sum(s => s.Values.Length);
					_countersFor.GetSerieSegments.Close(timer, result.Segments.Length, sum);
					return result;
				});
		}

		TagsForSerie[] ITimeSerieApi.GetTags(Identity identity, Guid[] serieIDs)
		{
			using (var scope = _scopes.Get("GetTags"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.ValidateMany(serieIDs, "serieIDs", LokadApiRequestLimits.GetTags_Series, Is.NotDefault);
			}

			_log.DebugFormat("GetTags({0})", serieIDs.Length);

			return _policy.Get(() =>
				{
					var timer = _countersFor.GetTags.Open(serieIDs.Length);
					var result = _inner.GetTags(identity, serieIDs);
					var sum = result.Sum(t => t.Tags.Length);
					_countersFor.GetTags.Close(timer, result.Length, sum);
					return result;
				});
		}

		EventsForSerie[] ITimeSerieApi.GetEvents(Identity identity, Guid[] serieIDs)
		{
			using (var scope = _scopes.Get("GetEvents"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.ValidateMany(serieIDs, "serieIDs", LokadApiRequestLimits.GetEvents_Series, Is.NotDefault);
			}

			_log.DebugFormat("GetEvents({0})", serieIDs.Length);

			return _policy.Get(() =>
				{
					var timer = _countersFor.GetEvents.Open(serieIDs.Length);
					var result = _inner.GetEvents(identity, serieIDs);
					var sum = result.Sum(e => e.Events.Length);
					_countersFor.GetEvents.Close(timer, result.Length, sum);
					return result;
				});
		}
	}
}
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
	using Limits = LokadApiRequestLimits;

	/// <summary>
	/// Decorator for the <see cref="IForecastApi"/> that 
	/// adds validation and reliability layers
	/// </summary>
	[Immutable]
	public sealed class ForecastApiDecorator : IForecastApi
	{
		readonly ActionPolicy _policy;
		readonly IForecastApi _inner;
		readonly INamedProvider<IScope> _scopes;
		readonly ILog _log;
		readonly static ForecastApiCounters _counterFor = new ForecastApiCounters();


		/// <summary>
		/// Initializes a new instance of the <see cref="ForecastApiDecorator"/> class.
		/// </summary>
		/// <param name="policy">The exception policy.</param>
		/// <param name="inner">The instance to decorate.</param>
		/// <param name="scopes">The validation scopes to leverage.</param>
		/// <param name="log">The log.</param>
		public ForecastApiDecorator(ActionPolicy policy, IForecastApi inner, INamedProvider<IScope> scopes, ILog log)
		{
			Enforce.Arguments(() => policy, () => inner, () => scopes, () => log);

			var timer = _counterFor.Ctor.Open();

			_policy = policy;
			_log = log;
			_scopes = scopes;
			_inner = inner;

			_counterFor.Ctor.Close(timer);
		}


		Guid[] IForecastApi.AddTasks(Identity identity, TaskInfo[] tasks)
		{
			using (var scope = _scopes.Get("AddTasks"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.ValidateMany(tasks, "tasks", LokadApiRequestLimits.AddTasks, ApiRules.NewTask);
			}

			_log.DebugFormat("AddTasks({0})", tasks.Length);

			return _policy.Get(() =>
				{
					var timer = _counterFor.AddTasks.Open(tasks.Length);
					var result = _inner.AddTasks(identity, tasks);
					_counterFor.AddTasks.Close(timer);
					return result;
				});
		}

		TaskInfoPage IForecastApi.GetTasks(Identity identity, Guid cursor, int pageSize)
		{
			using (var scope = _scopes.Get("GetTasks"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.Validate(pageSize, "pageSize", LokadApiRequestLimits.GetTasks_Page);
			}

			_log.DebugFormat("GetTasks({0} @ {1})", pageSize, cursor);

			return _policy.Get(() =>
				{
					var timer = _counterFor.GetTasks.Open(pageSize);
					var result = _inner.GetTasks(identity, cursor, pageSize);
					_counterFor.GetTasks.Close(timer, result.Tasks.Length);
					return result;
				});
		}

		void IForecastApi.UpdateTasks(Identity identity, TaskInfo[] tasks)
		{
			using (var scope = _scopes.Get("UpdateTasks"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.ValidateMany(tasks, "tasks", LokadApiRequestLimits.UpdateTasks, ApiRules.NewTask);
			}

			_log.DebugFormat("UpdateTasks({0})", tasks.Length);

			_policy.Do(() =>
				{
					var timer = _counterFor.UpdateTasks.Open(tasks.Length);
					_inner.UpdateTasks(identity, tasks);
					_counterFor.UpdateTasks.Close(timer);
				});
		}

		void IForecastApi.DeleteTasks(Identity identity, Guid[] taskIDs)
		{
			using (var scope = _scopes.Get("DeleteTasks"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.ValidateMany(taskIDs, "taskIDs", LokadApiRequestLimits.DeleteTasks, Is.NotDefault);
			}

			_log.DebugFormat("DeleteTasks({0})", taskIDs);

			_policy.Do(() =>
				{
					var timer = _counterFor.DeleteTasks.Open(taskIDs.Length);
					_inner.DeleteTasks(identity, taskIDs);
					_counterFor.DeleteTasks.Close(timer);
				});
		}

		Forecast[] IForecastApi.GetForecasts(Identity identity, Guid[] taskIDs)
		{
			using (var scope = _scopes.Get("GetForecasts"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.ValidateMany(taskIDs, "taskIDs", LokadApiRequestLimits.GetForecasts, Is.NotDefault);
			}

			_log.DebugFormat("GetForecasts({0})", taskIDs.Length);

			return _policy.Get(() =>
				{
					var timer = _counterFor.GetForecasts.Open(taskIDs.Length);
					var result = _inner.GetForecasts(identity, taskIDs);
					_counterFor.GetForecasts.Close(timer, result.Length);
					return result;
				});
		}

		TaskInfo[] IForecastApi.GetTasksBySerieIDs(Identity identity, Guid[] serieIDs)
		{
			using (var scope = _scopes.Get("GetTasks"))
			{
				scope.Validate(identity, "identity", ApiRules.Identity);
				scope.ValidateMany(serieIDs, "serieIDs", LokadApiRequestLimits.GetTasks_Series, Is.NotDefault);
			}

			_log.DebugFormat("Legacy.GetTasks({0})", serieIDs.Length);

			return _policy.Get(() =>
				{
					var timer = _counterFor.GetTasksBySerieID.Open(serieIDs.Length);
					var result = _inner.GetTasksBySerieIDs(identity, serieIDs);
					_counterFor.GetTasksBySerieID.Close(timer, result.Length);
					return result;
				});
		}
	}
}
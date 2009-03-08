#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Diagnostics;
using System.Net;
using System.Rules;
using Lokad.Api.WebReference;

namespace Lokad.Api
{
	/// <summary>
	/// Helper class for manual connections to <see cref="LokadService"/>
	/// </summary>
	public static class ServiceFactory
	{
		// abdullin: this is static readonly on purpose to avoid copying values
		/// <summary>
		/// Connection string to the Lokad Sandbox server
		/// </summary>
		public static readonly string SandboxServer = "http://sandbox-ws.lokad.com/TimeSeries2.asmx";

		/// <summary>
		/// Connection string to the Lokad Production server
		/// </summary>
		public static readonly string ProductionServer = "http://ws.lokad.com/TimeSeries2.asmx";

		static ILokadApi ConnectTo(string server)
		{
			return new TimeSeries2
				{
					Url = server
				};
		}


		/// <summary>
		/// This Lokad API connector is good for the testing purposes. It is configured to provide
		/// maximum information on invalid entries and to report on any encountered exception
		/// with the full stack trace (instead of retrying on failures).
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="pwd">The PWD.</param>
		/// <param name="serviceUrl">The service URL.</param>
		/// <returns>new service instance</returns>
		[Obsolete("Use GetConnectorForTesting(ServiceConnection connection) instead")]
		public static ILokadService GetConnectorForTesting(string userName, string pwd, string serviceUrl)
		{
			return GetConnectorForTesting(new ServiceConnection(userName, pwd, new Uri(serviceUrl)));
		}

		/// <summary>
		/// This Lokad API connector is good for the testing purposes. It is configured to provide
		/// maximum information on invalid entries and to report on any encountered exception
		/// with the full stack trace (instead of retrying on failures).
		/// </summary>
		/// <returns>new service instance</returns>
		public static ILokadService GetConnectorForTesting(ServiceConnection connection)
		{
			Enforce.Argument(() => connection, ApiRules.ValidConnection);

			var api2 = ConnectTo(connection.Endpoint.ToString());

			var scopes = new NamedProvider<IScope>(s => Scope.ForValidation(s, Scope.WhenAny));
			var ex = ActionPolicy.Null;
			var log = TraceLog.Instance;

			var identity = new Identity
				{
					Username = connection.Username,
					Password = connection.Password
				};

			return new LokadService(identity,
				new TimeSerieApiDecorator(ex, api2, scopes, log),
				new ForecastApiDecorator(ex, api2, scopes, log),
				new AccountApiDecorator(ex, api2, scopes, log),
				new LegacyApiDecorator(ex, api2, scopes, log),
				new SystemApiDecorator(ex, api2, scopes, log));
		}

		/// <summary>
		/// Connects to the Lokad sandbox server for testing.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="pwd">The PWD.</param>
		/// <returns>new service instance</returns>
		public static ILokadService ConnectToSandboxForTesting(string userName, string pwd)
		{
			return GetConnectorForTesting(new ServiceConnection(userName, pwd, new Uri(SandboxServer)));
		}

		/// <summary>
		/// This method supports Lokad infrastructure and is not meant to be called directly
		/// </summary>
		/// <param name="identity">The identity.</param>
		/// <param name="series">The series API.</param>
		/// <param name="forecasts">The forecasts API.</param>
		/// <param name="accounts">The accounts API.</param>
		/// <param name="legacy">The legacy API.</param>
		/// <param name="system">The system API.</param>
		/// <returns>new service instance</returns>
		public static ILokadService GetConnectorForTesting(Identity identity, ITimeSerieApi series, IForecastApi forecasts,
			IAccountApi accounts, ILegacyApi legacy, ISystemApi system)
		{
			return new LokadService(identity, series, forecasts, accounts, legacy, system);
		}

		/// <summary>
		/// <para>This Lokad API connector provides reliability for the transport errors,
		/// while still running full validation of all data. All errors are logged to
		/// the trace for now. </para>
		/// </summary>
		/// <param name="connection">The connection that has to pass check against
		/// <see cref="ApiRules.ValidConnection"/></param>
		/// <returns>new service instance</returns>
		public static ILokadService GetConnector(ServiceConnection connection)
		{
			Enforce.Argument(() => connection, ApiRules.ValidConnection);

			var api2 = ConnectTo(connection.Endpoint.ToString());

			var scopes = new NamedProvider<IScope>(s => Scope.ForValidation(s, Scope.WhenError));

			var log = TraceLog.Instance;
			var ex = ActionPolicy
				.With(e => WebClientPolicy(e, log))
				.Retry(3);

			var identity = new Identity
				{
					Username = connection.Username,
					Password = connection.Password
				};

			return new LokadService(identity,
				new TimeSerieApiDecorator(ex, api2, scopes, log),
				new ForecastApiDecorator(ex, api2, scopes, log),
				new AccountApiDecorator(ex, api2, scopes, log),
				new LegacyApiDecorator(ex, api2, scopes, log),
				new SystemApiDecorator(ex, api2, scopes, log));
		}

		/// <summary>
		/// <para>This Lokad API connector provides reliability for the transport errors,
		/// while still running full validation of all data. All errors are logged to
		/// the trace for now. 
		/// </para>
		/// <para>Parameters must pass the check aginst <see cref="ApiRules.ValidConnection"/></para>
		/// </summary>
		/// <param name="userName">user name</param>
		/// <param name="pwd">password</param>
		/// <param name="serviceUrl">Url of the service</param>
		/// <returns>preconfigured connector instance</returns>
		[Obsolete("Use GetConnector(ServiceConnection connection) instead")]
		public static ILokadService GetConnector(string userName, string pwd, string serviceUrl)
		{
			return GetConnector(new ServiceConnection(userName, pwd, new Uri(serviceUrl)));
		}

		static bool WebClientPolicy(Exception ex, ILog log)
		{
			if (ex.Message.Contains("Authentication failed"))
			{
				return false;
			}

			if (ex is WebException)
			{
				log.Warn(ex, "Recoverable exception. Going to sleep");
				SystemUtil.Sleep(2.Seconds());
				return true;
			}
			log.Error(ex, "Unrecoverable exception");
			return false;
		}
	}
}
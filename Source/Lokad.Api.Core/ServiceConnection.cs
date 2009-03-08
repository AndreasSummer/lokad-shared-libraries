#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Api
{
	/// <summary>
	/// This class encapsulates all information needed to connect
	/// to Lokad services
	/// </summary>
	[Serializable]
	public sealed class ServiceConnection
	{
		readonly string _username;
		readonly string _password;
		readonly Uri _endpoint;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceConnection"/> class.
		/// </summary>
		/// <param name="username">The login.</param>
		/// <param name="password">The password.</param>
		/// <param name="endpoint">The endpoint.</param>
		public ServiceConnection(string username, string password, Uri endpoint)
		{
			_username = username;
			_password = password;
			_endpoint = endpoint;
		}

		/// <summary>
		/// Gets name of the Lokad account on the server
		/// </summary>
		/// <value>The userName.</value>
		public string Username
		{
			get { return _username; }
		}

		/// <summary>
		/// Gets password for the Lokad account on the server
		/// </summary>
		/// <value>The userName.</value>
		public string Password
		{
			get { return _password; }
		}

		/// <summary>
		/// Gets the endpoint Uri for the service.
		/// </summary>
		/// <value>The endpoint.</value>
		public Uri Endpoint
		{
			get { return _endpoint; }
		}
	}
}
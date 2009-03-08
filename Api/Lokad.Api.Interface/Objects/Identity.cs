#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Xml.Serialization;

namespace Lokad.Api
{
	/// <summary>
	/// Class used to pass authentication information
	/// to the Lokad Services
	/// </summary>
	[Serializable]
	public sealed class Identity
	{
		/// <summary>
		/// Username for your Lokad account.
		/// </summary>
		/// <value>The username.</value>
		[XmlAttribute]
		public string Username { get; set; }

		/// <summary>
		/// Password for your Lokad account.
		/// </summary>
		/// <value>The password.</value>
		[XmlAttribute]
		public string Password { get; set; }
	}
}
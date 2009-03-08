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
	/// Issue report to be created with <see cref="ISystemApi.AddReport"/>
	/// </summary>
	[Serializable]
	public sealed class Report
	{
		/// <summary>
		/// THe title of the report being submitted
		/// </summary>
		/// <value>The subject.</value>
		[XmlAttribute]
		public string Subject { get; set; }

		/// <summary>
		/// Message for the report
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; set; }

		/// <summary>
		/// Additional system-level information on the problem
		/// </summary>
		/// <value>The information.</value>
		public string Information { get; set; }
	}
}
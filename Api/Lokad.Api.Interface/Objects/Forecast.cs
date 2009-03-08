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
	/// Delivered forecast
	/// </summary>
	[Serializable]
	public class Forecast
	{
		/// <summary>
		/// Task ID for this forecast
		/// </summary>
		/// <value>The task ID.</value>
		[XmlAttribute]
		public Guid TaskID { get; set; }
		/// <summary>
		/// Error for a given forecast. Lower - the better.
		/// </summary>
		/// <value>The error.</value>
		[XmlAttribute]
		public double Error { get; set; }
		/// <summary>
		/// Gets or sets the values that represent this forecast.
		/// </summary>
		/// <value>The values.</value>
		public TimeValue[] Values { get; set; }
	}
}
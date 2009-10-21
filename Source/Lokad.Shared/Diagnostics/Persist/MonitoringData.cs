#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Lokad.Diagnostics.Persist
{
	/// <summary>
	/// Diagnostics: Persistence class for custom monitoring data in any supported form
	/// </summary>
	[Serializable]
	[DataContract]
	[DebuggerDisplay("{Name}: {Value}")]
	public sealed class MonitoringData
	{
		/// <summary>
		/// Gets or sets the name of this data item.
		/// </summary>
		/// <value>The name.</value>
		[XmlAttribute]
		[DataMember(Order = 1)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the type of the data.
		/// </summary>
		/// <value>The type.</value>
		[XmlAttribute]
		[DataMember(Order = 2)]
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the string representation of the data.
		/// </summary>
		/// <value>The value.</value>
		[DataMember(Order = 3)]
		public string Value { get; set; }
	}
}
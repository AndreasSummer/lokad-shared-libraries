#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Lokad.Diagnostics.Persist
{
	/// <summary>
	/// Class to persist aggregate performance for method calls
	/// </summary>
	[Serializable]
	public class ExecutionData
	{
		/// <summary>
		/// Name of the executing method
		/// </summary>
		[XmlAttribute]
		public string Name { get; set; }

		/// <summary>
		/// Number of times the counter has been opened
		/// </summary>
		[XmlAttribute, DefaultValue(0)]
		public long OpenCount { get; set; }

		/// <summary>
		/// Gets or sets the counter has been closed
		/// </summary>
		/// <value>The close count.</value>
		[XmlAttribute, DefaultValue(0)]
		public long CloseCount { get; set; }

		/// <summary>
		/// Total execution count of the method in ticks
		/// </summary>
		[XmlAttribute, DefaultValue(0)]
		public long RunningTime { get; set; }

		/// <summary>
		/// Method-specific counters
		/// </summary>
		public long[] Counters { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ExecutionData"/> class.
		/// </summary>
		public ExecutionData()
		{
			Counters = new long[0];
		}
	}
}
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
	/// Defines a time-series forecasting task.
	/// </summary>
	[Serializable]
	public class TaskInfo
	{
		/// <summary>
		/// Unique identifier for the task (use <see cref="Guid.Empty"/> for new tasks)
		/// </summary>
		/// <value>The task ID.</value>
		[XmlAttribute]
		public Guid TaskID { get; set; }

		/// <summary>
		/// Unique identifier for the serie that should be forecasted
		/// </summary>
		/// <value>The serie ID.</value>
		[XmlAttribute]
		public Guid SerieID { get; set; }

		/// <summary>
		/// Period to be used by the forecasting operations.
		/// </summary>
		/// <value>The period.</value>
		[XmlAttribute]
		public Period Period { get; set; }

		/// <summary>
		/// Gets or sets optional sample boundary for time-serie aggregation (use <see cref="DateTime.MinValue"/> for null).
		/// </summary>
		/// <value>Optional sample boundary for time-serie aggregation.</value>
		[XmlAttribute]
		public DateTime PeriodStart { get; set; }

		/// <summary>
		/// Gets or sets number of future periods to be computed in reports.
		/// </summary>
		/// <value>number of future periods to be computed in reports.</value>
		[XmlAttribute]
		public int FuturePeriods { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskInfo"/> class.
		/// </summary>
		public TaskInfo()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskInfo"/> class
		/// and associates it with the specified serie.
		/// </summary>
		/// <param name="serie">The serie to associate with.</param>
		public TaskInfo(SerieInfo serie)
		{
			SerieID = serie.SerieID;
		}
	}
}
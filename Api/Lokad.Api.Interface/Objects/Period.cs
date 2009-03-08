#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Api
{
	/// <summary>The <see cref="Period"/> enumeration is used to represent the
	/// usual time duration used for business purposes.
	/// </summary>
	/// <remarks>
	/// <para>It should be noted that the semantic of <see cref="Period"/> is
	/// more related to the economy as opposed to physics.</para>
	/// </remarks>
	[Serializable]
	public enum Period
	{
		/// <summary>
		/// Undefined value (for the API compliance to standards)
		/// </summary>
		None = 0,

		/// <summary>15 minutes</summary>
		QuarterHour,

		/// <summary>30 minutes</summary>
		HalfHour,

		/// <summary>Hour</summary>
		Hour,

		/// <summary>Day</summary>
		Day,

		/// <summary>Week</summary>
		Week,

		/// <summary>Month</summary>
		Month,

		/// <summary>Quarter</summary>
		Quarter,

		/// <summary>Semester</summary>
		Semester,

		/// <summary>Year</summary>
		Year
	}
}
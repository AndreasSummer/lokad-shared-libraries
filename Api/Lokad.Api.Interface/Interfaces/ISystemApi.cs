using System;

namespace Lokad.Api
{
	/// <summary>
	/// System-wide routines
	/// </summary>
	public interface ISystemApi
	{
		/// <summary> Creates new report in the Lokad API. </summary>
		/// <param name="identity">The identity of the reporter.</param>
		/// <param name="report">The report itself.</param>
		/// <returns>Unique identifier for the newly created report, that could be
		/// used as a reference later. </returns>
		Guid AddReport(Identity identity, Report report);
	}
}
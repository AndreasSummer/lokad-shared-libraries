using System;

namespace Lokad.Api
{
	/// <summary> The legacy methods that simplify migration from LAPIv1 to LAPIv2. </summary>
	public interface ILegacyApi
	{
		///// <summary> Retrieve serie information based on the provided <paramref name="serieNames"/></summary>
		///// <param name="identity">The identity that represents the account to work with.</param>
		///// <param name="serieNames">The serie names.</param>
		///// <para>.NET developers can leverage classes that hide the paging and batching complexity.</para>
		///// <returns>Series that were found in the system</returns>
		//SerieInfo[] GetSeriesByNames(Identity identity, string[] serieNames);

		/// <summary>
		/// Allows to page through <see cref="SerieInfo"/> collection associated with the specified <paramref name="prefix"/> and current account.
		/// </summary>
		/// <param name="identity">The identity that represents account to work with.</param>
		/// <param name="prefix">The prefix to look for.</param>
		/// <param name="cursor">The cursor (use <see cref="Guid.Empty"/> for the first page and then <see cref="SerieInfoPage.Cursor"/> for the pages afterwards).</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <returns>page with the series</returns>
		/// <remarks>
		/// .NET developers can leverage classes that hide the paging and batching complexity.
		/// </remarks>
		SerieInfoPage GetSeriesByPrefix(Identity identity, string prefix, Guid cursor, int pageSize);
	}
}
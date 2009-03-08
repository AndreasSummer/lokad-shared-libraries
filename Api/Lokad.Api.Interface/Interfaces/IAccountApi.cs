#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Api
{
	/// <summary>
	/// Available account operations
	/// </summary>
	public interface IAccountApi
	{
		/// <summary>
		/// Gets the account info for the provided <paramref name="identity"/>.
		/// Exception is thrown if the credentials are invalid
		/// </summary>
		/// <param name="identity">The identity.</param>
		/// <returns>account information for the provided identity</returns>
		AccountInfo GetAccountInfo(Identity identity);

		/// <summary>
		/// Sets the partner for a given <paramref name="identity"/>.
		/// </summary>
		/// <param name="identity">The identity.</param>
		/// <param name="partnerId"><see cref="AccountInfo.AccountHRID"/> of a partner or 0 to reset.</param>
		void SetPartner(Identity identity, long partnerId);
	}
}
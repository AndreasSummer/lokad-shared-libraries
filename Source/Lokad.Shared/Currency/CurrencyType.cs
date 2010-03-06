#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad
{
	///<summary>
	///	Currency types supported by the Lokad business
	///</summary>
	///<remarks>
	///	Hardcoded for simplicity, but could be switched to string codes, if needed.
	///</remarks>
	public enum CurrencyType
	{

		/// <summary>
		/// Undefined currency
		/// </summary>
		/// <remarks>this must be default!</remarks>
		None = 0x00,
		/// <summary>
		/// EUR
		/// </summary>
		Eur,
		/// <summary>
		/// American Dollar
		/// </summary>
		Usd,
		/// <summary>
		/// Australian dollar
		/// </summary>
		Aud,
		/// <summary>
		/// Canadian dollar
		/// </summary>
		Cad,
		/// <summary>
		/// Swiss franc
		/// </summary>
		Chf,
		/// <summary>
		/// Pound sterling
		/// </summary>
		Gbp,
		/// <summary>
		/// Japanese yen
		/// </summary>
		Jpy,
		/// <summary>
		/// Russian ruble
		/// </summary>
		Rub,
		/// <summary>
		/// Mexican Peso
		/// </summary>
		Mxn
	}
}
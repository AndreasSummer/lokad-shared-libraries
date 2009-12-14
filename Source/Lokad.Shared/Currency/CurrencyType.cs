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
	///	Hardcoded for simplicity, but could be switched to string codes, if needed
	///</remarks>
	public enum CurrencyType
	{
#pragma warning disable 1591
		None = 0x00,
		Eur,
		Usd,
		Aud,
		Cad,
		Chf,
		Gbp,
		Jpy,
	}
}
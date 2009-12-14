#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad
{
	/// <summary>
	/// 	Extensions around
	/// 	<see cref="CurrencyAmount" />
	/// </summary>
	public static class ExtendDecimal
	{
		/// <summary>
		/// 	Converts the specified deimal to a currency.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="currency">The currency.</param>
		/// <returns>
		/// 	new instance of the currency amount
		/// </returns>
		public static CurrencyAmount In(this decimal value, CurrencyType currency)
		{
			return new CurrencyAmount(currency, value);
		}
	}
}
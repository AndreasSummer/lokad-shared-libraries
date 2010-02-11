#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

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

		/// <summary>
		/// Rounds the specified decimal with the provided number 
		/// of fractional digits.
		/// </summary>
		/// <param name="value">The value to round.</param>
		/// <param name="digits">The digits.</param>
		/// <returns>rounded value</returns>
		/// <remarks>We can't use "Round" since it will collide with <see cref="decimal.Round(decimal,int)"/></remarks>
		public static Decimal RoundTo(this Decimal value, int digits)
		{
			return Math.Round(value, digits);
		}
	}
}
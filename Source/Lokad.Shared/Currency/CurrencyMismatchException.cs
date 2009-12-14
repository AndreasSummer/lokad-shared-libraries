#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Runtime.Serialization;

namespace Lokad
{
	/// <summary>
	/// 	Exception related to the situations when currency logic is violated.
	/// </summary>
	/// <remarks>
	/// 	All currency conversion operations should be handled by the
	/// 	appropriate currency manager, that will take care of fetching the
	/// 	proper conversion rates
	/// </remarks>
	[Serializable]
	public class CurrencyMismatchException : Exception
	{
		/// <summary>
		/// 	Initializes a new instance of the
		/// 	<see cref="CurrencyMismatchException" />
		/// 	class.
		/// </summary>
		public CurrencyMismatchException()
		{
		}

		/// <summary>
		/// 	Initializes a new instance of the
		/// 	<see cref="CurrencyMismatchException" />
		/// 	class.
		/// </summary>
		/// <param name="message">The message.</param>
		public CurrencyMismatchException(string message)
			: base(message)
		{
		}

		CurrencyMismatchException(
			SerializationInfo info,
			StreamingContext context)
			: base(info, context)
		{
		}

		internal static CurrencyMismatchException Create(string format, params object[] args)
		{
			var text = string.Format(format, args);

			return new CurrencyMismatchException(text);
		}
	}
}
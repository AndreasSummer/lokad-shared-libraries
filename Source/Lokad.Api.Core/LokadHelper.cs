#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Linq;
using System.Text;

namespace Lokad.Api
{
	/// <summary>
	/// Helper classes
	/// </summary>
	public static class LokadHelper
	{
		/// <summary>
		/// Removes the <see cref="ApiRules.IllegalCharacters"/> from the string, replacing them with the <em>_</em>.
		/// </summary>
		/// <param name="value">The value to check for invalida characters.</param>
		/// <param name="newChar">Replacement character (i.e. '<em>_</em>').</param>
		/// <returns>
		/// string that does not contain <see cref="ApiRules.IllegalCharacters"/>
		/// </returns>
		/// <remarks>We must replace illegal characters with some value,
		/// instead of simply removing them, to avoid situations, when the
		/// original string gets truncated below valid length.</remarks>
		public static string ReplaceInvalidCharacters(string value, char newChar)
		{
			Enforce.ArgumentNotEmpty(() => value);



			Enforce.Argument(!ApiRules.IllegalCharacters.Contains(newChar), "newChar",
				"Character must not match illegal characters.");

			if (value.IndexOfAny(ApiRules.IllegalCharacters) == -1)
				return value;

			var builder = new StringBuilder(value);

			foreach (var c in ApiRules.IllegalCharacters)
			{
				builder.Replace(c, newChar);
			}
			return builder.ToString();
		}
	}
}
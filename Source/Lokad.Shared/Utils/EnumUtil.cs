#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Linq;

namespace Lokad
{
	/// <summary>
	/// Enum helper class from xLim
	/// </summary>
	public static class EnumUtil
	{
		/// <summary>
		/// Parses the specified string into the <typeparamref name="TEnum"/>, ignoring the case
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="value">The value.</param>
		/// <returns>Parsed enum</returns>
		/// <exception cref="ArgumentNullException">If <paramref name="value"/> is null</exception>
		public static TEnum Parse<TEnum>(string value) where TEnum : struct
		{
			return (TEnum) Enum.Parse(typeof (TEnum), value, true);
		}

		/// <summary>
		/// Parses the specified string into the <typeparamref name="TEnum"/>, ignoring the case
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="value">The value.</param>
		/// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
		/// <returns>Parsed enum</returns>
		/// <exception cref="ArgumentNullException">If <paramref name="value"/> is null</exception>
		public static TEnum Parse<TEnum>(string value, bool ignoreCase) where TEnum : struct
		{
			return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
		}

		/// <summary>
		/// Unwraps the enum by creating a string usable for identifiers and resource lookups.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="enumItem">The enum item.</param>
		/// <returns>a string usable for identifiers and resource lookups</returns>
		public static string ToIdentifier<TEnum>(TEnum enumItem)
			where TEnum : struct
		{
			return EnumUtil<TEnum>.EnumPrefix + enumItem;
		}

		/// <summary>
		/// Gets the values associated with the specified enum.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>array instance of the enum values</returns>
		public static TEnum[] GetValues<TEnum>() where TEnum :struct
		{
			return EnumUtil<TEnum>.Values;
		}

		/// <summary>
		/// Gets the values associated with the specified enum, 
		/// with the exception of the default value
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>array instance of the enum values</returns>
		public static TEnum[] GetValuesWithoutDefault<TEnum>() where TEnum : struct
		{
			return EnumUtil<TEnum>.ValuesWithoutDefault;
		}
	}

	/// <summary>
	/// Strongly-typed enumeration util
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	public static class EnumUtil<TEnum> where TEnum : struct
	{
		/// <summary>
		/// Values of the <typeparamref name="TEnum"/>
		/// </summary>
		public static readonly TEnum[] Values;
		/// <summary>
		/// Values of the <typeparamref name="TEnum"/> without the default value.
		/// </summary>
		public static readonly TEnum[] ValuesWithoutDefault;
		internal static readonly string EnumPrefix = typeof (TEnum).Name + "_";

		static EnumUtil()
		{
			Values = GetValues();
			var def = default(TEnum);
			ValuesWithoutDefault = Values.Where(x => !def.Equals(x)).ToArray();
		}

		static TEnum[] GetValues()
		{
			Type enumType = typeof (TEnum);

			if (!enumType.IsEnum)
			{
				throw new ArgumentException("Type is not an enum: '" + enumType.Name);
			}

#if !SILVERLIGHT2

			return Enum
				.GetValues(enumType)
				.Cast<TEnum>()
				.ToArray();
#else
			return enumType
				.GetFields()
				.Where(field => field.IsLiteral)
				.ToArray(f => (TEnum) f.GetValue(enumType));
#endif
		}
	}
}
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

		static EnumUtil()
		{
			Values = GetValues();
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
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Testing
{
	/// <summary>
	/// Extends <see cref="IEquatable{T}"/> for the purposes of testing
	/// </summary>
	public static class ExtendIEquatable
	{
		/// <summary>
		/// Ensures that the specified equals to a value in tests.
		/// </summary>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="value">The value.</param>
		/// <param name="compare">The compare.</param>
		/// <returns>same instance for inlining</returns>
		public static IEquatable<TValue> ShouldBeEqualTo<TValue>(this IEquatable<TValue> value, TValue compare)
		{
			Assert.IsTrue(value.EqualsTo(compare), "Value should be equal to '{0}'", compare);
			return value;
		}
	}
}
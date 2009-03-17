#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad
{
	/// <summary>
	/// Utility class to manipulate arrays of arrays
	/// </summary>
	public static class ArrayUtil
	{
		/// <summary>
		/// Returns <em>True</em> if the provided array is null or empty
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static bool IsNullOrEmpty(Array array)
		{
			return array == null || array.Length == 0;
		}
	}
}
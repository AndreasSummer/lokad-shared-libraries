#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;

namespace Lokad.Testing
{
	/// <summary>
	/// Helper extensions for the <see cref="IEquatable{T}"/> used in testing of models.
	/// </summary>
	public static class IEquatableExtensions
	{
		/// <summary>
		/// Checks if the specified object instance is equal to another instance
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <param name="self">The object to check.</param>
		/// <param name="other">The other object to compare with.</param>
		/// <returns><c>True</c> if the object instances are equal</returns>
		public static bool EqualsTo<TObject>(this IEquatable<TObject> self, TObject other)
		{
			return self.Equals(other);
		}

		///// <summary>
		///// Checks if the specified array of object instances is equal to another similar array
		///// </summary>
		///// <typeparam name="TObject">The type of the object.</typeparam>
		///// <param name="self">The array to cehck.</param>
		///// <param name="other">The other array.</param>
		///// <returns><c>True</c> if all object instances are equal</returns>
		//public static bool EqualsTo<TObject>(this IEquatable<TObject>[] self, TObject[] other)
		//{
		//    if (self.Length != other.Length) return false;

		//    for (int i = 0; i < self.Length; i++)
		//    {
		//        if (!self[i].Equals(other[i]))
		//            return false;
		//    }
		//    return true;
		//}


		///// <summary>
		///// Checks if the specified array of object instances is equal to another similar array
		///// </summary>
		///// <typeparam name="TObject">The type of the object.</typeparam>
		///// <param name="self">The array to check.</param>
		///// <param name="other">The other array.</param>
		///// <returns><c>True</c> if all object instances are equal</returns>
		//public static bool EqualsTo<TObject>(this TObject[] self, TObject[] other)
		//    where TObject : IEquatable<TObject>
		//{
		//    if (self.Length != other.Length) return false;

		//    for (int i = 0; i < self.Length; i++)
		//    {
		//        if (!self[i].Equals(other[i]))
		//            return false;
		//    }
		//    return true;
		//}


		/// <summary>
		/// Checks if the specified collection of object instances is equal to another similar collection
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <param name="self">The collection to check.</param>
		/// <param name="other">The other collection.</param>
		/// <returns><c>True</c> if all object instances are equal</returns>
		public static bool EqualsTo<TObject>(this ICollection<TObject> self, ICollection<TObject> other)
			where TObject : IEquatable<TObject>
		{
			if (self.Count != other.Count) return false;

			
			var e2 = other.GetEnumerator();

			foreach (var item in self)
			{
				e2.MoveNext();
				if (!item.Equals(e2.Current))
					return false;
			}

			return true;
		}
	}
}
#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System
{
	/// <summary>
	/// Helper class that simplifies tuple inline generation
	/// </summary>
	///  <example>
	/// Tuple.From("Mike",1,true)
	/// </example>
	[NoCodeCoverage]
	public static class Tuple
	{
		/// <summary>
		/// Creates <see cref="Pair{T1,T2}"/> out of two arguments
		/// </summary>
		/// <typeparam name="T1">The type of the first item.</typeparam>
		/// <typeparam name="T2">The type of the second item.</typeparam>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <returns>New tuple instance</returns>
		public static Pair<T1, T2> From<T1, T2>(T1 first, T2 second)
		{
			return new Pair<T1, T2>(first, second);
		}

		/// <summary>
		/// Creates <see cref="Triple{T1,T2,T3}"/> out of the three arguments
		/// </summary>
		/// <typeparam name="T1">The type of the first item.</typeparam>
		/// <typeparam name="T2">The type of the second item.</typeparam>
		/// <typeparam name="T3">The type of the third item.</typeparam>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <param name="third">The third item.</param>
		/// <returns>New tuple instance</returns>
		public static Triple<T1, T2, T3> From<T1, T2, T3>(T1 first, T2 second, T3 third)
		{
			return new Triple<T1, T2, T3>(first, second, third);
		}

		/// <summary>
		/// Creates <see cref="Tuple{T1,T2,T3,T4}"/> out of four arguments
		/// </summary>
		/// <typeparam name="T1">The type of the first item.</typeparam>
		/// <typeparam name="T2">The type of the second item.</typeparam>
		/// <typeparam name="T3">The type of the third item.</typeparam>
		/// <typeparam name="T4">The type of the fourth.</typeparam>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <param name="third">The third item.</param>
		/// <param name="fourth">The fourth item.</param>
		/// <returns>New instance</returns>
		public static Quad<T1, T2, T3, T4> From<T1, T2, T3, T4>(T1 first, T2 second, T3 third, T4 fourth)
		{
			return new Quad<T1, T2, T3, T4>(first, second, third, fourth);
		}

		/// <summary>
		/// Creates <see cref="Tuple{T1,T2,T3,T4}"/> out of four arguments
		/// </summary>
		/// <typeparam name="T1">The type of the first item.</typeparam>
		/// <typeparam name="T2">The type of the second item.</typeparam>
		/// <typeparam name="T3">The type of the third item.</typeparam>
		/// <typeparam name="T4">The type of the fourth item.</typeparam>
		/// <typeparam name="T5">The type of the fifth item.</typeparam>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <param name="third">The third item.</param>
		/// <param name="fourth">The fourth item.</param>
		/// <param name="fifth">The fifth item.</param>
		/// <returns>New instance</returns>
		public static Tuple<T1, T2, T3, T4, T5> From<T1, T2, T3, T4, T5>(T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
		{
			return new Tuple<T1, T2, T3, T4, T5>(first, second, third, fourth, fifth);
		}
	}

	#region Tuple-2 - "Pair"

	/// <summary>
	/// Tuple class with 2 items
	/// </summary>
	/// <typeparam name="T1">The type of the first item.</typeparam>
	/// <typeparam name="T2">The type of the second item.</typeparam>
	[Serializable]
	[Immutable]
	[DebuggerDisplay("({Item1},{Item2})")]
	public class Tuple<T1, T2> : IEquatable<Tuple<T1, T2>>
	{
		readonly T1 _item1;

		/// <summary>
		/// Gets Item1.
		/// </summary>
		/// <value>The item1.</value>
		public T1 Item1
		{
			get { return _item1; }
		}

		readonly T2 _item2;

		/// <summary>
		/// Gets Item2.
		/// </summary>
		/// <value>The item2.</value>
		public T2 Item2
		{
			get { return _item2; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pair{T1,T2}"/> class.
		/// </summary>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		public Tuple(T1 first, T2 second)
		{
			_item1 = first;
			_item2 = second;
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="Pair{T1,T2}"/>.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="Pair{T1,T2}"/>.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="Pair{T1,T2}"/>; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj"/> parameter is null.
		/// </exception>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				throw new NullReferenceException("obj is null");
			if (ReferenceEquals(this, obj)) return true;
			if (!(obj is Pair<T1, T2>)) return false;
			return Equals((Tuple<T1, T2>)obj);
		}


		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="Pair{T1,T2}"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="Pair{T1,T2}"/>.
		/// </returns>
		public override string ToString()
		{
			return string.Format("({0},{1})", Item1, Item2);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="obj" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="obj">
		/// An object to compare with this object.
		/// </param>
		public bool Equals(Tuple<T1, T2> obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj.Item1, Item1) && Equals(obj.Item2, Item2);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return SystemUtil.GetHashCode(Item1, Item2);
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator ==(Tuple<T1, T2> left, Tuple<T1, T2> right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator !=(Tuple<T1, T2> left, Tuple<T1, T2> right)
		{
			return !Equals(left, right);
		}
	}


	#endregion

	#region Tuple-3 - "Triple"

	/// <summary>
	/// Tuple class with 3 items
	/// </summary>
	/// <typeparam name="T1">The type of the first item.</typeparam>
	/// <typeparam name="T2">The type of the second item.</typeparam>
	/// <typeparam name="T3">The type of the third item.</typeparam>
	[Serializable]
	[DebuggerDisplay("({Item1},{Item2},{Item3})")]
	public class Tuple<T1, T2, T3> : IEquatable<Tuple<T1, T2, T3>>
	{
		readonly T1 _item1;

		/// <summary>
		/// Gets Item1.
		/// </summary>
		/// <value>The item1.</value>
		public T1 Item1
		{
			get { return _item1; }
		}

		readonly T2 _item2;

		/// <summary>
		/// Gets Item2.
		/// </summary>
		/// <value>The item2.</value>
		public T2 Item2
		{
			get { return _item2; }
		}

		readonly T3 _item3;

		/// <summary>
		/// Gets Item3.
		/// </summary>
		/// <value>The item3.</value>
		public T3 Item3
		{
			get { return _item3; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Triple{T1,T2,T3}"/> class.
		/// </summary>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <param name="third">The third item.</param>
		public Tuple(T1 first, T2 second, T3 third)
		{
			_item1 = first;
			_item2 = second;
			_item3 = third;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="Triple{T1,T2,T3}"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="Triple{T1,T2,T3}"/>.
		/// </returns>
		public override string ToString()
		{
			return string.Format("({0},{1},{2})", Item1, Item2, Item3);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="Triple{T1,T2,T3}"/>.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="Triple{T1,T2,T3}"/>.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="Triple{T1,T2,T3}"/>; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj"/> parameter is null.
		/// </exception>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				throw new NullReferenceException("obj is null");
			if (ReferenceEquals(this, obj)) return true;
			if (!(obj is Tuple<T1, T2, T3>)) return false;
			return Equals((Tuple<T1, T2, T3>)obj);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="obj" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="obj">
		/// An object to compare with this object.
		/// </param>
		public bool Equals(Tuple<T1, T2, T3> obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj.Item1, Item1) && Equals(obj.Item2, Item2) && Equals(obj.Item3, Item3);
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="Triple{T1,T2,T3}"/>.
		/// </returns>
		public override int GetHashCode()
		{
			return SystemUtil.GetHashCode(Item1, Item2, Item3);
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator ==(Tuple<T1, T2, T3> left, Tuple<T1, T2, T3> right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator !=(Tuple<T1, T2, T3> left, Tuple<T1, T2, T3> right)
		{
			return !Equals(left, right);
		}
	}


	#endregion

	#region Tuple-4 - "Quad"

	/// <summary>
	/// Tuple class with 4 items (also called <em>Quadruple</em>)
	/// </summary>
	/// <typeparam name="T1">The type of the first item.</typeparam>
	/// <typeparam name="T2">The type of the second item.</typeparam>
	/// <typeparam name="T3">The type of the third item.</typeparam>
	/// <typeparam name="T4">The type of the fourth item.</typeparam>
	[Serializable]
	[DebuggerDisplay("({Item1},{Item2},{Item3},{Item4})")]
	[Immutable]
	public class Tuple<T1, T2, T3, T4> : IEquatable<Tuple<T1, T2, T3, T4>>
	{
		readonly T1 _item1;

		/// <summary>
		/// Gets or sets the item1.
		/// </summary>
		/// <value>The item1.</value>
		public T1 Item1
		{
			get { return _item1; }
		}

		readonly T2 _item2;

		/// <summary>
		/// Gets or sets the item2.
		/// </summary>
		/// <value>The item2.</value>
		public T2 Item2
		{
			get { return _item2; }
		}

		readonly T3 _item3;

		/// <summary>
		/// Gets or sets the item3.
		/// </summary>
		/// <value>The item3.</value>
		public T3 Item3
		{
			get { return _item3; }
		}

		readonly T4 _item4;

		/// <summary>
		/// Gets or sets the item4.
		/// </summary>
		/// <value>The item4.</value>
		public T4 Item4
		{
			get { return _item4; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tuple{T1,T2,T3,T4}"/> class.
		/// </summary>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <param name="third">The third item.</param>
		/// <param name="fourth">The fourth item.</param>
		public Tuple(T1 first, T2 second, T3 third, T4 fourth)
		{
			_item1 = first;
			_item2 = second;
			_item3 = third;
			_item4 = fourth;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="Tuple{T1,T2,T3,T4}"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="Tuple{T1,T2,T3,T4}"/>.
		/// </returns>
		public override string ToString()
		{
			return string.Format("({0},{1},{2},{3})", Item1, Item2, Item3, Item4);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="Tuple{T1,T2,T3,T4}"/>.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="Tuple{T1,T2,T3,T4}"/>.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="Tuple{T1,T2,T3,T4}"/>; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj"/> parameter is null.
		/// </exception>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				throw new NullReferenceException("obj is null");
			if (ReferenceEquals(this, obj)) return true;
			if (!(obj is Tuple<T1, T2, T3, T4>)) return false;
			return Equals((Tuple<T1, T2, T3, T4>)obj);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="obj" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="obj">
		/// An object to compare with this object.
		/// </param>
		public bool Equals(Tuple<T1, T2, T3, T4> obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj.Item1, Item1)
				&& Equals(obj.Item2, Item2)
					&& Equals(obj.Item3, Item3)
						&& Equals(obj.Item4, Item4);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="Tuple{T1,T2,T3,T4}" />.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return SystemUtil.GetHashCode(Item1, Item2, Item3, Item4);
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator ==(Tuple<T1, T2, T3, T4> left, Tuple<T1, T2, T3, T4> right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator !=(Tuple<T1, T2, T3, T4> left, Tuple<T1, T2, T3, T4> right)
		{
			return !Equals(left, right);
		}
	}

	#endregion

	#region Tuple-5

	/// <summary>
	/// Tuple class with 5 items (also called <em>Quintuple</em>)
	/// </summary>
	/// <typeparam name="T1">The type of the first item.</typeparam>
	/// <typeparam name="T2">The type of the second item.</typeparam>
	/// <typeparam name="T3">The type of the third item.</typeparam>
	/// <typeparam name="T4">The type of the fourth item.</typeparam>
	/// <typeparam name="T5">The type of the fifthi item.</typeparam>
	[Serializable]
	[DebuggerDisplay("({Item1},{Item2},{Item3},{Item4},{Item5})")]
	[Immutable]
	public sealed class Tuple<T1, T2, T3, T4, T5> : IEquatable<Tuple<T1, T2, T3, T4, T5>>
	{
		readonly T1 _item1;
		readonly T2 _item2;
		readonly T3 _item3;
		readonly T4 _item4;
		readonly T5 _item5;

		/// <summary>
		/// Gets or sets the item1.
		/// </summary>
		/// <value>The item1.</value>
		public T1 Item1
		{
			get { return _item1; }
		}

		/// <summary>
		/// Gets or sets the item2.
		/// </summary>
		/// <value>The item2.</value>
		public T2 Item2
		{
			get { return _item2; }
		}
		
		/// <summary>
		/// Gets or sets the item3.
		/// </summary>
		/// <value>The item3.</value>
		public T3 Item3
		{
			get { return _item3; }
		}

		/// <summary>
		/// Gets or sets the item4.
		/// </summary>
		/// <value>The item4.</value>
		public T4 Item4
		{
			get { return _item4; }
		}

		/// <summary>
		/// Gets or sets the item5.
		/// </summary>
		/// <value>The item5.</value>
		public T5 Item5
		{
			get { return _item5; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Tuple{T1,T2,T3,T4}"/> class.
		/// </summary>
		/// <param name="first">The first item.</param>
		/// <param name="second">The second item.</param>
		/// <param name="third">The third item.</param>
		/// <param name="fourth">The fourth item.</param>
		/// <param name="fifth">The fifth item.</param>
		public Tuple(T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
		{
			_item1 = first;
			_item2 = second;
			_item3 = third;
			_item4 = fourth;
			_item5 = fifth;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="Tuple{T1,T2,T3,T4,T5}"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="Tuple{T1,T2,T3,T4,T5}"/>.
		/// </returns>
		public override string ToString()
		{
			return string.Format("({0},{1},{2},{3},{4})", Item1, Item2, Item3, Item4, Item5);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="Tuple{T1,T2,T3,T4,T5}"/>.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="Tuple{T1,T2,T3,T4,T5}"/>.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="Tuple{T1,T2,T3,T4,T5}"/>; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj"/> parameter is null.
		/// </exception>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				throw new NullReferenceException("obj is null");
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Tuple<T1, T2, T3, T4, T5>)) return false;
			return Equals((Tuple<T1, T2, T3, T4, T5>)obj);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="obj" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="obj">
		/// An object to compare with this object.
		/// </param>
		public bool Equals(Tuple<T1, T2, T3, T4, T5> obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj.Item1, Item1)
				&& Equals(obj.Item2, Item2)
					&& Equals(obj.Item3, Item3)
						&& Equals(obj.Item4, Item4)
							&& Equals(obj.Item5, Item5);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="Tuple{T1,T2,T3,T4,T5}" />.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return SystemUtil.GetHashCode(Item1, Item2, Item3, Item4, Item5);
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator ==(Tuple<T1, T2, T3, T4, T5> left, Tuple<T1, T2, T3, T4, T5> right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator !=(Tuple<T1, T2, T3, T4, T5> left, Tuple<T1, T2, T3, T4, T5> right)
		{
			return !Equals(left, right);
		}
	}

	#endregion
}
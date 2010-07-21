#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Cqrs
{
	/// <summary>
	/// Represents View object with contextual information
	/// </summary>
	public sealed class ViewEntity : IEquatable<ViewEntity>
	{
		/// <summary>
		/// Identity of the view
		/// </summary>
		public readonly string Identity;

		/// <summary>
		/// Partition of the view
		/// </summary>
		public readonly string Partition;

		/// <summary>
		/// Actual view object
		/// </summary>
		public readonly object Value;

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewEntity"/> class.
		/// </summary>
		/// <param name="partition">The partition.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="value">The value.</param>
		public ViewEntity(string partition, string identity, object value)
		{
			Partition = partition;
			Identity = identity;
			Value = value;
		}

		#region IEquatable<ViewEntity> Members

		/// <summary>
		/// Determines whether the specified <see cref="ViewEntity"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns></returns>
		public bool Equals(ViewEntity other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.Partition, Partition) && Equals(other.Identity, Identity) && Equals(other.Value, Value);
		}

		#endregion

		/// <summary>
		/// Casts this view container to the specified type.
		/// </summary>
		/// <typeparam name="TView">The type of the view.</typeparam>
		/// <returns></returns>
		public ViewEntity<TView> Cast<TView>()
		{
			return new ViewEntity<TView>(Partition, Identity, (TView) Value);
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns>
		/// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj"/> parameter is null.
		/// </exception>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (ViewEntity)) return false;
			return Equals((ViewEntity) obj);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			unchecked
			{
				int result = (Partition != null ? Partition.GetHashCode() : 0);
				result = (result*397) ^ (Identity != null ? Identity.GetHashCode() : 0);
				result = (result*397) ^ (Value != null ? Value.GetHashCode() : 0);
				return result;
			}
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return string.Format("[{0}/{1}]: {2}", Partition, Identity, Value);
		}
	}
}
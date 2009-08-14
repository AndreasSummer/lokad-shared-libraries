using System;
using System.ComponentModel;
using Lokad.Quality;

namespace Lokad
{
	/// <summary>
	/// Helper class for creating fluent APIs
	/// </summary>
	/// <typeparam name="T">underlying type</typeparam>
	[Immutable]
	[Serializable]
	[NoCodeCoverage]
	public sealed class Syntax<T> : Syntax
	{
		readonly T _inner;

		/// <summary>
		/// Initializes a new instance of the <see cref="Syntax{T}"/> class.
		/// </summary>
		/// <param name="inner">The underlying instance.</param>
		public Syntax(T inner)
		{
			_inner = inner;
		}

		/// <summary>
		/// Gets the underlying object.
		/// </summary>
		/// <value>The underlying object.</value>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public T Target
		{
			get { return _inner; }
		}

		internal static Syntax<T> For(T item)
		{
			return new Syntax<T>(item);
		}
	}
}
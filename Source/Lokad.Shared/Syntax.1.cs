using System;
using System.ComponentModel;
using Lokad.Quality;

namespace Lokad
{
	/// <summary>
	/// Helper class for creating fluent APIs
	/// </summary>
	/// <typeparam name="TSyntaxTarget">underlying type</typeparam>
	[Immutable]
	[Serializable]
	[NoCodeCoverage]
	public sealed class Syntax<TSyntaxTarget> : Syntax, ISyntax<TSyntaxTarget>
	{
		readonly TSyntaxTarget _inner;

		/// <summary>
		/// Initializes a new instance of the <see cref="Syntax{T}"/> class.
		/// </summary>
		/// <param name="inner">The underlying instance.</param>
		public Syntax(TSyntaxTarget inner)
		{
			_inner = inner;
		}

		/// <summary>
		/// Gets the underlying object.
		/// </summary>
		/// <value>The underlying object.</value>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public TSyntaxTarget Target
		{
			get { return _inner; }
		}

		internal static Syntax<TSyntaxTarget> For(TSyntaxTarget item)
		{
			return new Syntax<TSyntaxTarget>(item);
		}
	}
}
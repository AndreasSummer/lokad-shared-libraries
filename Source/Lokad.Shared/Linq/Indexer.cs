namespace System.Linq
{
	/// <summary>
	/// Indexing wrapper that contains value and its integral position.
	/// </summary>
	/// <typeparam name="TSource">type of the underlying item</typeparam>
	public struct Indexer<TSource>
	{
		readonly int _index;
		readonly TSource _value;
		readonly bool _isFirst;

		/// <summary>
		/// Gets the integral position of the item.
		/// </summary>
		/// <value>The integral position of the item.</value>
		public int Index
		{
			get { return _index; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is first.
		/// </summary>
		/// <value><c>true</c> if this instance is first; otherwise, <c>false</c>.</value>
		public bool IsFirst
		{
			get { return _isFirst; }
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public TSource Value
		{
			get { return _value; }
		}

		internal Indexer(int index, TSource value)
		{
			_index = index;
			_isFirst = index == 0;
			_value = value;
		}
	}
}
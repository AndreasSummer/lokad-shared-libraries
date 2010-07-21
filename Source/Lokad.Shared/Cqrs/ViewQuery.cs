namespace Lokad.Cqrs
{
	/// <summary>
	/// Implementation of the <see cref="IViewQuery"/>
	/// </summary>
	public sealed class ViewQuery : IViewQuery
	{
		Maybe<IdentityConstraint> _constraint = Maybe<IdentityConstraint>.Empty;
		Maybe<int> _recordLimit = Maybe<int>.Empty;

		/// <summary>
		/// Empty query instance
		/// </summary>
		public static readonly ViewQuery Empty = new ViewQuery();

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewQuery"/> class.
		/// </summary>
		public ViewQuery()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewQuery"/> class.
		/// </summary>
		/// <param name="recordLimit">The record limit.</param>
		/// <param name="constraint">The constraint.</param>
		public ViewQuery(Maybe<int> recordLimit, Maybe<IdentityConstraint> constraint)
		{
			_recordLimit = recordLimit;
			_constraint = constraint;
		}

		/// <summary>
		/// Sets the maximum number records to retrieve.
		/// </summary>
		/// <param name="limit">The limit.</param>
		/// <returns>new query instance</returns>
		public ViewQuery SetMaxRecords(int limit)
		{
			_recordLimit = limit;
			return this;
		}

		/// <summary>
		/// Sets index constraint
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public ViewQuery SetIdentityConstraint(ConstraintOperand operand, string value)
		{
			_constraint = new IdentityConstraint(operand, value);
			return this;
		}

		/// <summary>
		/// Gets the record limit (optional).
		/// </summary>
		/// <value>The record limit.</value>
		public Maybe<int> RecordLimit
		{
			get { return _recordLimit; }
		}

		/// <summary>
		/// Gets the identity constraint (optional).
		/// </summary>
		/// <value>The constraint.</value>
		public Maybe<IdentityConstraint> Constraint
		{
			get { return _constraint; }
		}
	}
}
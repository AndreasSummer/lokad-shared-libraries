namespace Lokad.Cqrs
{
	/// <summary>
	/// Represents query for the <see cref="IQueryViews"/>
	/// </summary>
	public interface IViewQuery
	{
		/// <summary>
		/// Gets the record limit (optional).
		/// </summary>
		/// <value>The record limit.</value>
		Maybe<int> RecordLimit { get; }
		/// <summary>
		/// Gets the identity constraint (optional).
		/// </summary>
		/// <value>The constraint.</value>
		Maybe<IdentityConstraint> Constraint { get; }
	}
}
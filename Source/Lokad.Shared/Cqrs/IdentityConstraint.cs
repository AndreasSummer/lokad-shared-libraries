#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Cqrs
{
	/// <summary>
	/// Identity constraint for view query operations
	/// </summary>
	public sealed class IdentityConstraint
	{
		/// <summary>
		/// Constraint operand
		/// </summary>
		public readonly ConstraintOperand Operand;

		/// <summary>
		/// Constraint value
		/// </summary>
		public readonly string Value;

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityConstraint"/> class.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <param name="value">The value.</param>
		public IdentityConstraint(ConstraintOperand operand, string value)
		{
			Operand = operand;
			Value = value;
		}
	}
}
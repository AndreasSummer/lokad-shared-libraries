#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Cqrs
{
	/// <summary>
	/// Constraint operands for the <see cref="IViewQuery"/>
	/// </summary>
	public enum ConstraintOperand
	{
#pragma warning disable 1591
		Equal,
		GreaterThan,
		GreaterThanOrEqual,
		LessThan,
		LessThanOrEqual,
		NotEqual
	}
}
#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace System.Rules
{
	/// <summary>
	/// Rules for the <see cref="double"/>
	/// </summary>
	public static class DoubleIs
	{
		/// <summary>
		/// Checks if the specified double is valid.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="scope">The scope.</param>
		public static void Valid(double value, IScope scope)
		{
			if (Double.IsNaN(value) || Double.IsInfinity(value))
				scope.Error(RuleResources.Double_must_represent_valid_value);
		}
	}
}
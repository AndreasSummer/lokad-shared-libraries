#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace System.Rules
{
	/// <summary> Rules for the <see cref="DateTime"/> </summary>
	public static class DateIs
	{
		static readonly DateTime _sqlMinDateTime = new DateTime(1753, 1, 1);

		/// <summary>
		/// Verifies that it is ok to send this date directly into the MS SQL DB
		/// </summary>
		/// <param name="dateTime">The dateTime to validate.</param>
		/// <param name="scope">validation scope</param>
		public static void SqlCompatible(DateTime dateTime, IScope scope)
		{
			if (dateTime < _sqlMinDateTime)
				scope.Error(RuleResources.Date_must_be_greater_than_X, _sqlMinDateTime);
		}
	}
}
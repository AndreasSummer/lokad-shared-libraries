#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Diagnostics;

namespace Lokad.Api.Legacy
{
	/// <summary>Helper methods to manipulate <see cref="Period"/>s.</summary>
	public class PeriodOperations
	{
		/// <summary>Reference default period start.</summary>
		public static DateTime DefaultPeriodStart
		{
			get { return new DateTime(2001, 1, 1, 0, 0, 0, 0); }
		}

		/// <summary>
		/// Gets an <em>average</em>
		/// 	<see cref="TimeSpan"/> corresponding
		/// to the specified <see cref="Period"/>. Warning: there is not
		/// deterministic physical duration associated to each <see cref="Period"/>
		/// instances.
		/// </summary>
		/// <param name="period">The period.</param>
		/// <returns>timespan that represents the period</returns>
		public static TimeSpan ToTimeSpan(Period period)
		{
			switch (period)
			{
				case Period.QuarterHour:
					return new TimeSpan(0, 15, 0);

				case Period.HalfHour:
					return new TimeSpan(0, 30, 0);

				case Period.Hour:
					return new TimeSpan(1, 0, 0);

				case Period.Day:
					return new TimeSpan(1, 0, 0, 0);

				case Period.Week:
					return new TimeSpan(7, 0, 0, 0);

				case Period.Month:
					return new TimeSpan(ToTimeSpan(Period.Year).Ticks/12);

				case Period.Quarter:
					return new TimeSpan(ToTimeSpan(Period.Year).Ticks/4);

				case Period.Semester:
					return new TimeSpan(ToTimeSpan(Period.Year).Ticks/2);

				case Period.Year:
					return new TimeSpan(365, 6, 9, 9); // sidereal year value
			}

			throw new InvalidOperationException("Unsupported Period.");
		}

		/// <summary>Lower round the datetime to the closest period boundary.</summary>
		/// <param name="datetime">the datetime to round</param>
		/// <param name="period">the period type of the bounds</param>
		/// <param name="periodStart">the date of beginning of the bounds</param>
		/// <returns>the rounded date</returns>
		public static DateTime Floor(DateTime datetime, Period period, DateTime? periodStart)
		{
			if (periodStart == null)
			{
				periodStart = DefaultPeriodStart;
			}

			var nPeriod = (double) ((datetime.Ticks - ((DateTime) periodStart).Ticks)/(ToTimeSpan(period).Ticks));

			DateTime near = Add((DateTime) periodStart, period, nPeriod);
			while (near.CompareTo(datetime) < 0)
			{
				near = Add(near, period, 1);
			}

			while (near.CompareTo(datetime) > 0)
			{
				near = Add(near, period, -1);
			}

			return near;
		}

		/// <summary>
		/// Add a real number of <see cref="Period"/>s to the specified <see cref="DateTime"/>.
		/// </summary>
		/// <remarks>If <c>periodCount</c> is an integral number, then this method is
		/// identical to the <see cref="Add(DateTime,Period,int)"/> method.</remarks>
		static DateTime Add(DateTime datetime, Period period, double periodCount)
		{
			double integralPart = Math.Floor(periodCount);
			double floatingPart = periodCount - integralPart;
			Debug.Assert(floatingPart >= 0);

			DateTime time0 = Add(datetime, period, (int) integralPart);
			DateTime time1 = Add(datetime, period, (int) integralPart + 1);

			return new DateTime(time0.Ticks + (long) ((time1.Ticks - time0.Ticks)*floatingPart));
		}

		/// <summary>
		/// Add a number of <see cref="Period"/>s to the specified <see cref="DateTime"/>.
		/// </summary>
		public static DateTime Add(DateTime datetime, Period period, int periodCount)
		{
			switch (period)
			{
				case Period.QuarterHour:
				case Period.HalfHour:
				case Period.Hour:
				case Period.Day:
				case Period.Week:
					return new DateTime(datetime.Ticks + ToTimeSpan(period).Ticks*periodCount);
				case Period.Month:
					return datetime.AddMonths(periodCount);
				case Period.Quarter:
					return datetime.AddMonths(periodCount*3);
				case Period.Semester:
					return datetime.AddMonths(periodCount*6);
				case Period.Year:
					return datetime.AddYears(periodCount);
			}
			throw new InvalidOperationException("Unsupported Period.");
		}
	}
}
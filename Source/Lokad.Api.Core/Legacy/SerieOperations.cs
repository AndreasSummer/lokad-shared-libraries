using System;
using System.Collections.Generic;

namespace Lokad.Api.Legacy
{
	/// <summary>
	/// The static class <see cref="SerieOperations"/> regroups common operations
	/// performed over <see cref="TimeValue"/> arrays (referred as <em>time-series</em>).
	/// </summary>
	public static class SerieOperations
	{
		/// <summary>Aggregates the time-serie into a specified period.</summary>
		/// <param name="timeSerie">Should be not empty and sorted by increasing <c>Time</c> values.</param>
		/// <param name="period">Base unit of the aggregation duration.</param>
		/// <param name="periodStart">start of the period bounds</param>
		/// <returns>The aggregated time-serie sorted by increasing <c>Time</c> values.</returns>
		public static TimeValue[] Transform(TimeValue[] timeSerie, Period period, DateTime? periodStart)
		{
			if (timeSerie == null) throw new ArgumentNullException("timeSerie");
			if (timeSerie.Length == 0) return new TimeValue[0];


			if (periodStart == null)
			{
				periodStart = PeriodOperations.DefaultPeriodStart;
			}

			DateTime current = PeriodOperations.Floor(timeSerie[0].Time, period, (DateTime)periodStart);
			DateTime next = PeriodOperations.Add(current, period, 1);

			List<TimeValue> aggregatedSerie = new List<TimeValue>();

			double sum = 0.0;
			for (int i = 0; i < timeSerie.Length; i++)
			{
				if (timeSerie[i].Time.CompareTo(next) < 0)
				{
					sum += timeSerie[i].Value;
				}
				else
				{
					aggregatedSerie.Add(new TimeValue { Time = current, Value = sum });
					sum = 0.0;

					i--;
					current = next;
					next = PeriodOperations.Add(current, period, 1);
				}
			}
			aggregatedSerie.Add(new TimeValue { Time = current, Value = sum });

			return aggregatedSerie.ToArray();
		}

		/// <summary>Splits list of  the elements to smaller parts for work with time-series.</summary>
		/// <param name="splitSize">Maximum count of elements in list.</param>
		/// <param name="list">Initial list.</param>
		/// <returns>List of spited sublists.</returns>
		public static List<List<T>> Split<T>(int splitSize, List<T> list)
		{
			List<List<T>> lists = new List<List<T>>();

			List<T> subList = new List<T>();
			for (int i = 0; i < list.Count; i++)
			{
				subList.Add(list[i]);

				if (subList.Count >= splitSize)
				{
					lists.Add(subList);
					subList = new List<T>();
				}
			}

			if (subList.Count > 0)
			{
				lists.Add(subList);
			}

			return lists;
		}

		/// <summary>Merge the increment into the specified time-serie. The increment 
		/// overwrites the time-serie on the overlapping time segment.</summary>
		public static TimeValue[] Merge(TimeValue[] timeSerie, TimeValue[] increment)
		{
			if (null == timeSerie)
			{
				return increment;
			}

			Enforce.Argument(() => increment);

			if (0 == increment.Length)
			{
				return timeSerie;
			}

			var timeValues = new List<TimeValue>(timeSerie.Length + increment.Length);
			for (int i = 0; i < timeSerie.Length && timeSerie[i].Time < increment[0].Time; i++)
			{
				timeValues.Add(timeSerie[i]);
			}

			timeValues.AddRange(increment);

			return timeValues.ToArray();
		}

		/// <summary>Return a serie that does not overlap with <c>minusSerie</c>, i.e.
		/// the starting date of the returned serie is after the final date of the <c>minusSerie</c>.
		/// </summary>
		public static TimeValue[] PruneOverlap(TimeValue[] minusSerie, TimeValue[] overlapSerie)
		{
			Enforce.Argument(() => minusSerie);

			if (null == overlapSerie)
			{
				return null;
			}

			if (0 == minusSerie.Length || 0 == overlapSerie.Length)
			{
				return overlapSerie;
			}

			DateTime endBase = minusSerie[minusSerie.Length - 1].Time;

			int index = 0;
			while (index < overlapSerie.Length &&
				overlapSerie[index].Time <= endBase)
			{
				index++;
			}

			int nonOverlapLength = overlapSerie.Length - index;
			TimeValue[] nonOverlapSerie = new TimeValue[nonOverlapLength];

			if (overlapSerie.Length == nonOverlapLength)
			{
				return overlapSerie;
			}
			else
			{
				Array.Copy(overlapSerie, index, nonOverlapSerie, 0, nonOverlapLength);
				return nonOverlapSerie;
			}
		}

		/// <summary>Gets all the time-values that are prior (exclusive) to 
		/// the specified boundary.</summary>
		public static TimeValue[] GetStart(TimeValue[] timeSerie, DateTime exclusiveEndBoundary)
		{
			if (timeSerie == null)
				return null;

			var timeValues = new List<TimeValue>();
			for (int i = 0; i < timeSerie.Length; i++)
			{
				if (timeSerie[i].Time < exclusiveEndBoundary)
				{
					timeValues.Add(timeSerie[i]);
				}
			}

			return timeValues.ToArray();
		}

		/// <summary>Gets all the time-values that are posterior (inclusive) to 
		/// the specified boundary.</summary>
		public static TimeValue[] GetEnd(TimeValue[] timeSerie, DateTime inclusiveStartBoundary)
		{
			var timeValues = new List<TimeValue>();
			for (int i = 0; i < timeSerie.Length; i++)
			{
				if (timeSerie[i].Time >= inclusiveStartBoundary)
				{
					timeValues.Add(timeSerie[i]);
				}
			}

			return timeValues.ToArray();
		}

		/// <summary>Gets the first value associated to the specified date or <c>null</c>
		/// if the date cannot be found.</summary>
		public static double? GetValue(TimeValue[] timeSerie, DateTime dateTime)
		{
			if (ArrayUtil.IsNullOrEmpty(timeSerie))
				return null;

			int start = 0, end = timeSerie.Length - 1;

			while (end - start > 1)
			{
				int mean = (end + start) / 2;

				if (timeSerie[mean].Time > dateTime)
				{
					end = mean;
				}
				else
				{
					start = mean;
				}
			}

			if (timeSerie[start].Time == dateTime) return timeSerie[start].Value;
			else if (timeSerie[end].Time == dateTime) return timeSerie[end].Value;
			else return null;
		}

	}
}
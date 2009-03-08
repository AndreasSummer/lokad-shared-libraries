#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Rules;
using System.Linq;

namespace Lokad.Api
{
	/// <summary> Rules for the Lokad APIv2. See http://lokad.svn.sourceforge.net/viewvc/lokad/Platform/Trunk/Shared/Source/Lokad.Api.Core/ApiRules.cs?view=markup</summary>
	public static class ApiRules
	{
		/// <summary>
		/// Characters that are invalid in the names of tags, events, series etc: <em>;[]&lt;&gt;\/</em>
		/// </summary>
		public static readonly char[] IllegalCharacters = ";[]<>\"/".ToCharArray();


		internal static void ValidName(string name, IScope scope)
		{
			scope.ValidateInScope(name, 
				StringIs.Without(IllegalCharacters), 
				StringIs.ValidForXmlSerialization);
		}

		/// <summary>
		/// Validates the tag value.
		/// </summary>
		public static void ValidTag(string tag, IScope scope)
		{
			scope.ValidateInScope(tag, StringIs.Limited(1, 32), ValidName);
		}

		internal static Rule<string>[] UserName = new[]
			{
				StringIs.Limited(6, 256),
				StringIs.ValidEmail
			};

		static readonly Rule<string> Password = StringIs.Limited(1, 256);

		/// <summary> Validates <paramref name="report"/> </summary>
		public static void Report(Report report, IScope scope)
		{
			scope.Validate(report.Subject, "Subject", StringIs.Limited(5,256));
			scope.Validate(report.Message, "Message", StringIs.Limited(2, 4000));
			scope.Validate(report.Information, "Information");
		}

		/// <summary> Validates <paramref name="identity"/> </summary>
		/// <param name="identity">The identity.</param>
		/// <param name="scope">The scope.</param>
		public static void Identity(Identity identity, IScope scope)
		{
			scope.Validate(identity.Username, "Username", UserName);
			scope.Validate(identity.Password, "Password", Password);
		}

		static void TaskBody(TaskInfo info, IScope scope)
		{
			scope.Validate(info.Period, "Period", Is.NotDefault);
			scope.Validate(info.SerieID, "SerieID", Is.NotDefault);

			if (info.PeriodStart != DateTime.MinValue)
			{
				scope.Validate(info.PeriodStart, "PeriodStart", DateIs.SqlCompatible);
				if (info.PeriodStart.Day > 28)
					scope.Error("PeriodStart must have day smaller than or equal to 28");
			}
			
			scope.Validate(info.FuturePeriods, "FuturePeriods", ValidateFuturePeriods(info.Period));
		}


		/// <summary> Composes a rule to validate future periods, given the <paramref name="period"/> </summary>
		/// <param name="period">The period.</param>
		/// <returns>new rule instance</returns>
		public static Rule<int> ValidateFuturePeriods(Period period)
		{
			var max = GetMaxFuturePeriods(period);
			return (i, scope) => scope.ValidateInScope(i, Is.Within(1, max));
		}

		
		static int GetMaxFuturePeriods(Period period)
		{
			switch (period)
			{
				case Period.QuarterHour:
					return 6*7*24;
				case Period.HalfHour:
					return 6*7*24*2;
				case Period.Hour:
					return 6*7*24;
				default:
					return 64;
			}
		}

		internal static void NewTask(TaskInfo task, IScope scope)
		{
			scope.Validate(task.TaskID, "TaskID", Is.Default);
			scope.ValidateInScope(task, TaskBody);
		}

		internal static void Task(TaskInfo task, IScope scope)
		{
			scope.Validate(task.TaskID, "TaskID", Is.NotDefault);
			scope.ValidateInScope(task, TaskBody);
		}

		internal static readonly Rule<string>[] SerieName = new[]
			{
				StringIs.Limited(1, 64), ValidName
			};

		internal static void NewSerie(SerieInfo serieInfo, IScope scope)
		{
			scope.Validate(serieInfo.SerieID, "SerieID", Is.Default);
			scope.Validate(serieInfo.Name, "Name", SerieName);
		}

		internal static void Segments(SegmentForSerie[] segments, IScope scope)
		{
			var sLim = LokadApiRequestLimits.UpdateSerieSegments_Segments;
			if (segments.Length > sLim)
			{
				scope.Error("Only {0} segments are allowed per request", sLim);
			}

			var total = segments.Sum(s => s.Values.Length);
			var vLim = LokadApiRequestLimits.UpdateSerieSegments_Values;
			if (total > vLim)
			{
				scope.Error("Only {0} values are allowed per request.", vLim);
			}
			else
			{
				scope.ValidateInScope(segments, Segment);
			}
		}

		static void Segment(SegmentForSerie segment, IScope scope)
		{
			scope.Validate(segment.SerieID, "SerieID", Is.NotDefault);
			scope.ValidateMany(segment.Values, "Values", Value);
		}

		static void Value(TimeValue timeValue, IScope scope)
		{
			scope.Validate(timeValue.Time, "Time", DateIs.SqlCompatible);
			scope.Validate(timeValue.Value, "Value", DoubleIs.Valid);
		}

		internal static void SetTags(TagsForSerie[] tags, IScope scope)
		{
			if (tags.Length > LokadApiRequestLimits.SetTags_Series)
			{
				scope.Error("Only {0} items are allowed per request", LokadApiRequestLimits.SetTags_Series);
			}

			var total = tags.Sum(t => t.Tags.Length);
			if (total > LokadApiRequestLimits.SetTags_TagsPerRequest)
			{
				scope.Error("Only {0} tags are allowed per request", LokadApiRequestLimits.SetTags_TagsPerRequest);
			}
			else
			{
				scope.ValidateInScope(tags, Tags);
			}
		}

		

		static void Tags(TagsForSerie tags, IScope scope)
		{
			scope.Validate(tags.SerieID, "SerieID", Is.NotDefault);
			scope.ValidateMany(tags.Tags, "Tags", ValidTag);
		}

		internal static void SetEvents(EventsForSerie[] events, IScope scope)
		{
			if (events.Length > LokadApiRequestLimits.SetEvents_Series)
			{
				scope.Error("Only {0} items are allowed per request", LokadApiRequestLimits.SetEvents_Series);
			}
			var total = events.Sum(s => s.Events.Length);
			if (total > LokadApiRequestLimits.SetEvents_EventsPerRequest)
			{
				scope.Error("Only {0} events are allowed per request", LokadApiRequestLimits.SetEvents_EventsPerRequest);
			}
			else
			{
				scope.ValidateInScope(events, Events);
			}
		}

		static void Events(EventsForSerie serieEvent, IScope scope)
		{
			scope.Validate(serieEvent.SerieID, "SerieID", Is.NotDefault);
			scope.ValidateMany(serieEvent.Events, "Events", Event);
		}

		/// <summary> Validates <see cref="SerieEvent"/> </summary>
		/// <param name="e">The event to validate.</param>
		/// <param name="scope">The scope to capture results.</param>
		public static void Event(SerieEvent e, IScope scope)
		{
			scope.Validate(e.Name, "Name", StringIs.Limited(1, 32), ValidName);
			scope.Validate(e.Time, "Time", DateIs.SqlCompatible);
			scope.Validate(e.DurationDays, "DurationDays", Is.Within(0d, TimeSpan.MaxValue.TotalDays), DoubleIs.Valid);

			if (e.KnownSince != DateTime.MinValue)
				scope.Validate(e.KnownSince, "KnownSince", DateIs.SqlCompatible);
		}


		/// <summary>
		/// Validates connection to Lokad services
		/// </summary>
		/// <param name="connection">The connection.</param>
		/// <param name="scope">The scope.</param>
		public static void ValidConnection(ServiceConnection connection, IScope scope)
		{
			scope.Validate(connection.Username, "Username", UserName);
			scope.Validate(connection.Password, "Password", Password);
			scope.Validate(connection.Endpoint, "Endpoint", Endpoint);
		}

		static void Endpoint(Uri obj, IScope scope)
		{
			var local = obj.LocalPath.ToLowerInvariant();
			if (local == "/timeseries.asmx")
			{
				scope.Error("Please, use TimeSeries2.asmx");
			}
			else if (local != "/timeseries2.asmx")
			{
				scope.Error("Unsupported local address '{0}'", local);
			}

			if (!obj.IsLoopback)
			{
				var host = obj.Host.ToLowerInvariant();
				if ((host != "ws.lokad.com") && (host != "sandbox-ws.lokad.com"))
					scope.Error("Unknown host '{0}'", host);
			}
		}
	}
}
#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Rules;

namespace Lokad.Api
{
	/// <summary>
	/// <para> Settings that define recommended maximum settings
	/// for processing large requests. It is recommended to break
	/// the data into smaller batches if your item collections
	/// exceed provided values. </para>
	/// <para>Lokad API will decline requests that exceed these values.</para>
	/// <para>.NET developers can ignore these values, since 
	/// <see cref="LokadService"/> class performs all the necessary operations internally.</para> 
	/// <para>See http://lokad.svn.sourceforge.net/viewvc/lokad/Platform/Trunk/Shared/Source/Lokad.Api.Core/LokadApiRequestLimits.cs?view=markup</para>
	/// </summary> 
	public sealed class LokadApiRequestLimits
	{
		//
		// *Read the info on LAPIv2 documentation before making any changes"


		/// <summary>Series per request in <see cref="ITimeSerieApi.AddSeries"/> </summary>
		public const int AddSeries = 1000;

		/// <summary>Series per request in <see cref="ITimeSerieApi.DeleteSeries"/></summary>
		public const int DeleteSeries = 1000;

		/// <summary>Allowed page size in <see cref="ITimeSerieApi.GetSeries"/></summary>
		public static readonly Rule<int> GetSeries_Page = Is.Within(100, 1000);

		internal const int GetSeries_RecommendedPage = 750;

		/// <summary>Limit for the page size in <see cref="ILegacyApi.GetSeriesByPrefix"/></summary>
		public static readonly Rule<int> GetSeriesByPrefix_Page = Is.Within(100, 750);

		internal const int GetSeriesByPrefix_RecommendedPage = 500;


		/// <summary>Values per request in <see cref="ITimeSerieApi.UpdateSerieSegments"/></summary>
		public const int UpdateSerieSegments_Values = 25000;

		/// <summary>Segments per request in <see cref="ITimeSerieApi.UpdateSerieSegments"/></summary>
		public const int UpdateSerieSegments_Segments = 1000;

		/// <summary>Allowed page size in <see cref="ITimeSerieApi.GetSerieSegments"/> </summary>
		public static readonly Rule<int> GetSerieSegments_Page = Is.Within(250, 25000);

		internal const int GetSerieSegments_RecommendedPage = 20000;

		/// <summary> Segments per request in <see cref="ITimeSerieApi.GetSerieSegments"/> </summary>
		public const int GetSegments_Series = 1000;

		/// <summary> Series per request in <see cref="ITimeSerieApi.GetTags"/> </summary>
		public const int GetTags_Series = 1000;

		/// <summary> Series per request in <see cref="ITimeSerieApi.SetTags"/> </summary>
		public const int SetTags_Series = 1000;

		/// <summary> Tags per request in <see cref="ITimeSerieApi.SetTags"/> </summary>
		public const int SetTags_TagsPerRequest = 7000;

		/// <summary> Series per request in <see cref="ITimeSerieApi.GetEvents"/> </summary>
		public const int GetEvents_Series = 1000;

		/// <summary> Series per request in <see cref="ITimeSerieApi.SetEvents"/> </summary>
		public const int SetEvents_Series = 1000;

		/// <summary> Events per request in <see cref="ITimeSerieApi.SetEvents"/> </summary>
		public const int SetEvents_EventsPerRequest = 2500;

		/// <summary> Tasks per request in <see cref="IForecastApi.AddTasks"/> </summary>
		public const int AddTasks = 5000;

		/// <summary> Tasks per request in <see cref="IForecastApi.GetTasksBySerieIDs"/> </summary>
		public const int GetTasks_Series = 1000;

		/// <summary> Allowed page size in <see cref="IForecastApi.GetTasks"/> </summary>
		public static readonly Rule<int> GetTasks_Page = Is.Within(100, 1000);

		internal static int GetTasks_RecommendedPage = 750;

		/// <summary> Tasks per request in <see cref="IForecastApi.UpdateTasks"/> </summary>
		public const int UpdateTasks = 2000;

		/// <summary> Tasks per request in <see cref="IForecastApi.DeleteTasks"/> </summary>
		public const int DeleteTasks = 2000;

		/// <summary> Forecasts per request in <see cref="IForecastApi.GetForecasts"/> </summary>
		public const int GetForecasts = 50;
	}
}
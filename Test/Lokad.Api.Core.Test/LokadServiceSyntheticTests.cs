#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Diagnostics;
using System.Linq;
using System.Rules;
using NUnit.Framework;
using Rhino.Mocks;

namespace Lokad.Api.Core
{
	using Limits = LokadApiRequestLimits;

	[TestFixture]
	public sealed class LokadServiceSyntheticTests
	{
		ITimeSerieApi _series;
		ILokadService _service;
		MockRepository _mocks;
		Identity _identity;


		public void SetUp()
		{
			_mocks = new MockRepository();

			var ex = ActionPolicy.Null;
			var scopes = new NamedProvider<IScope>(s => Scope.ForEnforce(s, Scope.WhenError));
			var log = NullLog.Instance;

			_series = _mocks.CreateMock<ITimeSerieApi>();
			var accounts = _mocks.CreateMock<IAccountApi>();
			var tasks = _mocks.CreateMock<IForecastApi>();
			var legacy = _mocks.CreateMock<ILegacyApi>();
			var system = _mocks.CreateMock<ISystemApi>();
			_identity = GlobalSetup.Identity;

			_service = ServiceFactory.GetConnectorForTesting(_identity,
				new TimeSerieApiDecorator(ex, _series, scopes, log), tasks, accounts, legacy, system);
		}

		// resharper does not support rowtests yet
		void Run_UpdateSerieSegments(int serieCount, int valueCount)
		{
			SetUp();

			var series = TestHelper.CreateFakeSeries(serieCount);
			var values = TestHelper.CreateValues(series, valueCount);

			var capture = new Capture<SegmentForSerie[]>();

			using (_mocks.Record())
			{
				Expect.Call(() => _series.UpdateSerieSegments(null, null))
					.Constraints(Rhino.Mocks.Constraints.Is.Same(_identity), capture)
					.Repeat.AtLeastOnce();
			}

			using (_mocks.Playback())
			{
				_service.UpdateSerieSegments(values);
			}

			var actual = capture.Items
				.SelectMany(v => v.SelectMany(s => TestHelper.ToTuples(s)))
				.ToArray();

			var expected = values
				.Select(v => ILokadApiExtensions.CompressSegment(v))
				.SelectMany(v => TestHelper.ToTuples(v))
				.ToArray();

			CollectionAssert.AreEqual(expected, actual);
		}

		[Test]
		public void UpdateSerieSegments()
		{
			const int seriesUpper = (int) (LokadApiRequestLimits.UpdateSerieSegments_Segments*1.2);
			const int valuesUpper = (int) (LokadApiRequestLimits.UpdateSerieSegments_Values*1.2);

			Run_UpdateSerieSegments(3, 25);
			Run_UpdateSerieSegments(seriesUpper, 11);
			Run_UpdateSerieSegments(3, valuesUpper);

			//ExecutionCounters.Default.ToList().ForEach(es => Console.WriteLine(new TimeSpan(es.RunningTime)));
		}
	}
}
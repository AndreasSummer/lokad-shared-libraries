using System;
using System.Collections;
using System.Rules;
using NUnit.Framework;

namespace Lokad.Api.Core
{
	[TestFixture]
	public sealed class ILokadApiExtensionsTests
	{
		[Test]
		public void Compression_Works()
		{
			VerifyCompression(new[] {0}, new[] {0});
			VerifyCompression(new[] {0, 0}, new[] {0, 0});
			VerifyCompression(new[] { 0, 0, 0 }, new[] { 0, 0 });

			VerifyCompression(new[] { 1 }, new[] { 1 });
			VerifyCompression(new[] { 0, 1 }, new[] { 0, 1 });

			VerifyCompression(new[] { 0, 1, 0 }, new[] { 0, 1, 0 });
			VerifyCompression(new[] { 0, 2, 0, 3, 0 }, new[] { 0, 2, 3, 0 });
		}

		private static void VerifyCompression(int[] source, IEnumerable expected)
		{
			var segment = new SegmentForSerie
				{
					Values = source.Convert(t => new TimeValue {Value = t})
				};
			var compressed = ILokadApiExtensions.CompressSegment(segment);
			CollectionAssert.AreEqual(expected, compressed.Values.Convert(v => (int)v.Value));
		}

		#region

		sealed class HardToMockWithRhino : ITimeSerieApi
		{
			public Guid[] AddSeries(Identity identity, SerieInfo[] series)
			{
				return series.Convert(c => Guid.NewGuid());
			}

			public void DeleteSeries(Identity identity, Guid[] serieIDs)
			{
				throw new System.NotImplementedException();
			}

			public SerieInfoPage GetSeries(Identity identity, Guid cursor, int pageSize)
			{
				throw new System.NotImplementedException();
			}

			public void UpdateSerieSegments(Identity identity, SegmentForSerie[] segments)
			{
				throw new System.NotImplementedException();
			}

			public SerieSegmentPage GetSerieSegments(Identity identity, Guid[] serieIDs, SegmentCursor cursor, int pageSize)
			{
				throw new System.NotImplementedException();
			}

			public void SetTags(Identity identity, TagsForSerie[] tagsForSerie)
			{
				throw new System.NotImplementedException();
			}

			public void SetEvents(Identity identity, EventsForSerie[] eventsForSerie)
			{
				throw new System.NotImplementedException();
			}

			public TagsForSerie[] GetTags(Identity identity, Guid[] serieIDs)
			{
				throw new System.NotImplementedException();
			}

			public EventsForSerie[] GetEvents(Identity identity, Guid[] serieIDs)
			{
				throw new System.NotImplementedException();
			}


		}
		#endregion

		[Test]
		public void AddSeriesInBatchWithPrefix()
		{

			var series = TestHelper.CreateSeries(100);
			var api = new HardToMockWithRhino();
			api.AddSeriesInBatchWithPrefix(GlobalSetup.Identity, series, "test");
			
			series.ForEach(s => Enforce.That(s.SerieID, Is.NotDefault));
		}
	}
}
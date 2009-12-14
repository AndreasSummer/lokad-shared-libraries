#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class CurrencyAmountTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void Use_cases()
		{
			var eur10 = 10m.In(CurrencyType.Eur);
			var eur1 = 1m.In(CurrencyType.Eur);

			Assert.AreEqual(11m.In(CurrencyType.Eur), eur1 + eur10);
			Assert.AreEqual(9m.In(CurrencyType.Eur), eur10 - eur1);
			Assert.IsTrue(eur10 > eur1);
			Assert.IsTrue(eur10 >= eur1);
		}


		[Test, ExpectCurrencyMismatch]
		public void Subtract_mismatched()
		{
			var result = 10m.In(CurrencyType.Aud) - 1m.In(CurrencyType.Cad);
		}

		[Test, ExpectCurrencyMismatch]
		public void Add_mismatched()
		{
			var result = 10m.In(CurrencyType.Aud) - 1m.In(CurrencyType.Cad);
		}

		[Test, ExpectCurrencyMismatch]
		public void Less_mismatched()
		{
			var result = 10m.In(CurrencyType.Aud) < 1m.In(CurrencyType.Cad);
		}

		[Test, ExpectCurrencyMismatch]
		public void Greater_mismatched()
		{
			var result = 10m.In(CurrencyType.Aud) > 1m.In(CurrencyType.Cad);
		}


		sealed class ExpectCurrencyMismatchAttribute : ExpectedExceptionAttribute
		{
			public ExpectCurrencyMismatchAttribute()
				: base(typeof (CurrencyMismatchException))
			{
			}
		}
	}
}
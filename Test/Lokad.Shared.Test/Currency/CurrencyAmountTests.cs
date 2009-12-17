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
		// ReSharper disable EqualExpressionComparison

		[Test]
		public void Use_cases()
		{
			var eur10 = 10m.In(CurrencyType.Eur);
			var eur1 = 1m.In(CurrencyType.Eur);

			Assert.AreEqual(11m.In(CurrencyType.Eur), eur1 + eur10, "X+Y");
			Assert.AreEqual(9m.In(CurrencyType.Eur), eur10 - eur1, "X-Y");

			Assert.IsTrue(eur10 > eur1, ">");
			Assert.IsTrue(eur10 >= eur1, ">=");

			Assert.IsTrue(eur1 < eur10, "<");
			Assert.IsTrue(eur1 <= eur10, "<=");

			Assert.IsTrue(eur1 != eur10, "!=");
			Assert.IsFalse(eur1 == eur10, "==");
			Assert.AreEqual(eur1, eur1, "Eq");
			Assert.AreNotEqual(eur1, eur10, "!Eq");

			Assert.AreEqual(-1m, -eur1.Value, "-");
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

		[Test]
		public void Format_zero()
		{
			Assert.AreEqual("0.00", CurrencyAmount.Zero.ToString());
		}

		[Test]
		public void Format_negative()
		{
			Assert.AreEqual("-1.12", (-1.123m.In(CurrencyType.None)).ToString());
		}

		[Test]
		public void Format_zero_aud()
		{
			Assert.AreEqual("0.00 AUD", 0m.In(CurrencyType.Aud).ToString());
		}

		[Test]
		public void Format_posizitive_aud()
		{
			Assert.AreEqual("10.00 AUD", 10m.In(CurrencyType.Aud).ToString());
		}

		[Test]
		public void Format_positive_with_rounding()
		{
			Assert.AreEqual("12.35 USD", 12.347m.In(CurrencyType.Usd).ToString());
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
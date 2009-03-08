#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;
using System.Linq;

namespace System.Rules
{
	[TestFixture]
	public sealed class IsTests
	{
		[Test]
		public void Within_X_Y()
		{
			RuleAssert.For(Is.Within(0, 20))
				.ExpectNone(0, 12, 20)
				.ExpectError(-1, 21, int.MaxValue);

			RuleAssert.For(Is.Within(0D, 20D))
				.ExpectNone(0, 12, 20)
				.ExpectError(-1, 21, int.MaxValue);
		}

		[Test]
		public void Between_X_Y()
		{
			RuleAssert.For(Is.Between(0, 20))
				.ExpectNone(1, 15, 19)
				.ExpectError(-1, 0, 20, int.MaxValue);

			RuleAssert.For(Is.Between(0D, 20D))
				.ExpectNone(1, 15, 19)
				.ExpectError(-1, 0, 20, int.MaxValue);
		}

		[Test]
		public void Default()
		{
			RuleAssert.For<Guid>(Is.Default)
				.ExpectNone(Guid.Empty)
				.ExpectError(Guid.NewGuid());

			RuleAssert.For<int>(Is.Default)
				.ExpectNone(0)
				.ExpectError(1, 2);
		}

		[Test]
		public void NotDefault()
		{
			RuleAssert.For<Guid>(Is.NotDefault)
				.ExpectNone(Guid.NewGuid())
				.ExpectError(Guid.Empty);

			RuleAssert.For<double>(Is.NotDefault)
				.ExpectNone(1, 2)
				.ExpectError(0, 0D);
		}

		[Test]
		public void NotEqual_X()
		{
			RuleAssert.For(Is.NotEqual(Guid.Empty))
				.ExpectNone(Guid.NewGuid())
				.ExpectError(Guid.Empty);

			RuleAssert.For(Is.NotEqual(3D))
				.ExpectNone(1, 2, Math.PI)
				.ExpectError(3, 3D);
		}

		[Test]
		public void GreaterThen_X()
		{
			RuleAssert.For(Is.GreaterThan(5D))
				.ExpectNone(6, 6D, 5.1D)
				.ExpectError(5, 5D, 2);
		}

		[Test]
		public void LessThan_X()
		{
			RuleAssert.For(Is.LessThan(5D))
				.ExpectNone(4.9, 4D, 3)
				.ExpectError(5, 5D, 6);
		}

		[Test]
		public void AtMost_X()
		{
			RuleAssert.For(Is.AtMost(5D))
				.ExpectNone(-1, 4D, 5D, 5)
				.ExpectError(5.000001, 6D);
		}

		[Test]
		public void AtLeast_X()
		{
			RuleAssert.For(Is.AtLeast(5D))
				.ExpectNone(10, 6D, 5D, 5)
				.ExpectError(4.999999, 4D);
		}

		[Test]
		public void Equal_X()
		{
			RuleAssert.For(Is.Equal(Tuple.From(1, 2)))
				.ExpectNone(Tuple.From(1, 2))
				.ExpectError(Tuple.From(2, 1), Tuple.From(3, 4));
		}

		[Test]
		public void Value_X()
		{
			RuleAssert.For(Is.Value(RuleLevel.Error))
				.ExpectNone(RuleLevel.Error)
				.ExpectError(RuleLevel.None, RuleLevel.Warn);
		}

		[Test]
		public void True_X()
		{
			RuleAssert.For(Is.True<int>(i => (i > 2) && (i%2 == 0)))
				.ExpectNone(4, 6)
				.ExpectError(0, 1, 3, 5);
		}
	}
}
#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad.Testing.Test
{
	[TestFixture]
	public sealed class ModelAssertRegressions
	{
		// ReSharper disable InconsistentNaming

		[DesignOfClass.ImmutableFieldsModel]
		sealed class WithMaybe
		{
			public readonly Maybe<DateTime> Date;

			public WithMaybe(Maybe<DateTime> date)
			{
				Date = date;
			}
		}

		[Test]
		public void WithMaybe_non_empty_equal_valid()
		{
			var dateTime = SystemUtil.UtcNow;
			ModelAssert.AreEqual(new WithMaybe(dateTime), new WithMaybe(dateTime));
		}

		[Test]
		public void WithMaybe_empty_equal_valid()
		{
			ModelAssert.AreEqual(new WithMaybe(Maybe<DateTime>.Empty), new WithMaybe(Maybe<DateTime>.Empty));
		}

		[Test]
		public void WithMaybe_non_equal_invalid()
		{
			var m1 = new WithMaybe(Maybe<DateTime>.Empty);
			var m2 = new WithMaybe(SystemUtil.UtcNow);

			ModelAssert.AreNotEqual(m1, m2);
		}


		[DesignOfClass.ImmutableFieldsModel]
		sealed class WithStaticField
		{
			public static WithStaticField SingletonField = new WithStaticField();
			public readonly int Value = 10;
		}


		[DesignOfClass.ImmutablePropertiesModel]
		sealed class WithStaticProperty
		{
			public static WithStaticProperty SingletonProperty { get; set;}
			public int Value { get; set;}

			static WithStaticProperty()
			{
				SingletonProperty = new WithStaticProperty();
			}
		}

		[Test]
		public void Static_fields_are_ignored()
		{
			ModelAssert.AreEqual(new WithStaticField(), new WithStaticField());
		}

		[Test]
		public void Static_properties_are_ignored()
		{
			ModelAssert.AreEqual(new WithStaticField(), new WithStaticField());
		}
	}
}
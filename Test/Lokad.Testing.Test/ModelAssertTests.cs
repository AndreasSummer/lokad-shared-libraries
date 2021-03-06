#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using Lokad.Quality;
using NUnit.Framework;

namespace Lokad.Testing.Test
{
	[TestFixture]
	public sealed class ModelAssertTests
	{
		// ReSharper disable InconsistentNaming


		[DesignOfClass.ImmutableFieldsModel]
		public sealed class WithFields
		{
			public readonly string Field1;
			public readonly int Field2;

			public WithFields(string field1, int field2)
			{
				Field1 = field1;
				Field2 = field2;
			}
		}

		[Test]
		public void With_fields_valid()
		{
			var m1 = new WithFields("F1", 2);
			var m2 = new WithFields("F1", 2);

			ModelAssert.AreEqual(m1, m2);
		}


		[Test, ExpectAssert]
		public void With_fields_invalid()
		{
			var m1 = new WithFields("F1", 2);
			var m2 = new WithFields("F1", 3);

			ModelAssert.AreEqual(m1, m2);
		}

		[Test, ExpectAssert]
		public void With_fields_nullable_invalid()
		{
			var m1 = new WithFields("F1", 2);
			ModelAssert.AreEqual(m1, null);
		}

		[Test]
		public void With_properties_nullable_valid()
		{
			var m1 = new WithProperties("F1", 2);
			ModelAssert.AreNotEqual(m1, null);
		}

		[DesignOfClass.ImmutableFieldsModel]
		public sealed class WithNested
		{
			public readonly WithFields Model;
			public readonly Guid Unique;

			public WithNested(WithFields model, Guid unique)
			{
				Model = model;
				Unique = unique;
			}
		}

		[Test]
		public void Should_be_equal_valid_complex()
		{
			var guid = Guid.NewGuid();
			var m21 = new WithNested(new WithFields("F1", 2), guid);
			var m22 = new WithNested(new WithFields("F1", 2), guid);

			ModelAssert.AreEqual(m21, m22);
		}

		[DesignOfClass.ImmutablePropertiesModel]
		public sealed class WithArrayProperty
		{
			public WithFields[] Property { get; set; }

			public WithArrayProperty(params WithFields[] property)
			{
				Property = property;
			}
		}

		[Test]
		public void With_array_property_valid()
		{
			var m11 = new WithFields("F1", 2);
			var m12 = new WithFields("F1", 2);

			var m31 = new WithArrayProperty(m11, m11);
			var m32 = new WithArrayProperty(m12, m12);

			ModelAssert.AreEqual(m31, m32);
		}


		[Test]
		public void With_array_property_nullable_entry_valid()
		{
			var m11 = new WithFields("F1", 2);
			var m12 = new WithFields("F1", 2);

			var m31 = new WithArrayProperty(m11, m11,null);
			var m32 = new WithArrayProperty(m12, m12, null);

			ModelAssert.AreEqual(m31, m32);
		}

		[Test, ExpectAssert]
		public void With_array_property_null_array_invalid()
		{
			var m11 = new WithFields("F1", 2);
			var m12 = new WithFields("F1", 2);

			var m31 = new WithArrayProperty
				{
					Property = null
				};
			var m32 = new WithArrayProperty(m12, m12, null);

			ModelAssert.AreEqual(m31,m32);
		}



		[Test, ExpectAssert]
		public void With_array_property_invalid_length()
		{
			var m11 = new WithFields("F1", 2);
			var m12 = new WithFields("F1", 2);

			var m31 = new WithArrayProperty(m11, m11);
			var m32 = new WithArrayProperty(m12);

			ModelAssert.AreEqual(m31, m32);
		}

		[Test, ExpectAssert]
		public void With_array_property_mismatch()
		{
			var m11 = new WithFields("F1", 2);
			var m12 = new WithFields("F1", 2);
			var m13 = new WithFields("F2", 2);

			var m31 = new WithArrayProperty(m11, m11);
			var m32 = new WithArrayProperty(m12, m13);

			ModelAssert.AreEqual(m31, m32);
		}

		[DesignOfClass.ImmutablePropertiesModel]
		public sealed class WithProperties
		{
			public string Property1 { get; private set; }
			public int Property2 { get; private set; }

			public WithProperties(string property1, int property2)
			{
				Property1 = property1;
				Property2 = property2;
			}
		}


		[Test]
		public void With_properties_valid()
		{
			var m1 = new WithProperties("F1", 32);
			var m2 = new WithProperties("F1", 32);
			ModelAssert.AreEqual(m1, m2);
		}

		[Test, ExpectAssert]
		public void With_properties_valid_reversed()
		{
			var m1 = new WithProperties("F1", 32);
			var m2 = new WithProperties("F1", 32);
			ModelAssert.AreNotEqual(m1, m2);
		}

		[Test]
		public void With_properties_invalid_reversed()
		{
			var m1 = new WithProperties("F1", 33);
			var m2 = new WithProperties("F1", 32);
			ModelAssert.AreNotEqual(m1, m2);
		}

		[Test, ExpectedException(typeof (FailedAssertException))]
		public void With_properties_invalid()
		{
			var m1 = new WithProperties("F1", 32);
			var m2 = new WithProperties("F1", 33);
			ModelAssert.AreEqual(m1, m2);
		}

		[Test]
		public void Collection_of_models()
		{
			var m1 = new List<WithProperties>
				{
					new WithProperties("F1", 1), 
					new WithProperties("F2", 3)
				};
			var m2 = new List<WithProperties>
				{
					new WithProperties("F1", 1), 
					new WithProperties("F2", 3)
				};

			ModelAssert.AreEqualMany(m1, m2);
		}

		[Test]
		public void Array_of_models_valid()
		{
			var m1 = new []
				{
					new WithProperties("F1", 1), 
					new WithProperties("F2", 3)
				};
			var m2 = new []
				{
					new WithProperties("F1", 1), 
					new WithProperties("F2", 3)
				};

			ModelAssert.AreEqualMany(m1, m2);
		}

		[Test, ExpectAssert]
		public void Array_of_models_invalid()
		{
			var m1 = new[]
				{
					new WithProperties("F1", 1), 
					new WithProperties("F2", 4)
				};
			var m2 = new[]
				{
					new WithProperties("F1", 1), 
					new WithProperties("F2", 3)
				};

			ModelAssert.AreEqualMany(m1, m2);
		}

		public sealed class ExpectAssertAttribute : ExpectedExceptionAttribute
		{
			public ExpectAssertAttribute()
				: base(typeof (FailedAssertException))
			{
			}
		}
	}
}
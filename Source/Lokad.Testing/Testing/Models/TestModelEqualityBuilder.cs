#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lokad.Quality;
using Lokad.Rules;

#pragma warning disable 1591

namespace Lokad.Testing
{
	/// <summary>
	/// 	Builds equality testers for the models, using the provided design hints
	/// </summary>
	sealed class TestModelEqualityBuilder : ITestModelEqualityProvider
	{
		static readonly string FieldTag = DesignUtil.ConvertTagToString(DesignTag.ImmutableWithFields);
		static readonly string PropertyTag = DesignUtil.ConvertTagToString(DesignTag.ImmutableWithProperties);

		readonly ITestModelEqualityProvider _provider;

		public TestModelEqualityBuilder(ITestModelEqualityProvider provider)
		{
			_provider = provider;
		}

		public TestModelEqualityDelegate GetEqualityTester(Type type)
		{
			var tags = DesignUtil.GetClassDesignTags(type, false);

			var equatable = typeof(IEquatable<>).MakeGenericType(type);
			if (equatable.IsAssignableFrom(type))
			{
				// horray, friendly type here
				var methodInfo = equatable.GetMethod("Equals");
				return (scope, t, expected, actual) =>
				{
					Enforce.That(t == type);
					bool result;
					if (TryReferenceCheck(scope, expected, actual, out result))
					{
						return result;
					}
					return TestGenericEquality(scope, methodInfo, expected, actual);
				};
			}

			if (tags.Contains(FieldTag))
			{
				var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);

				return (scope, t, expected, actual) =>
					{
						Enforce.That(t == type);
						bool result;
						if (TryReferenceCheck(scope, expected, actual, out result))
						{
							return result;
						}
						return TestFieldEquality(scope, expected, actual, fields);
					};
			}

			if (tags.Contains(PropertyTag))
			{
				var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
				return (scope, t, expected, actual) =>
					{
						Enforce.That(t == type);
						bool result;
						if (TryReferenceCheck(scope, expected, actual, out result))
						{
							return result;
						}
						return TestPropertyEquality(scope, expected, actual, props);
					};
			}

			if (typeof (ICollection).IsAssignableFrom(type))
			{
				return (scope, t, expected, actual) =>
					{
						bool result;
						if (TryReferenceCheck(scope, expected, actual, out result))
						{
							return result;
						}
						var first = (ICollection) expected;
						var second = (ICollection) actual;
						return TestCollectionEquality(scope, first, second, first.Count, second.Count);
					};
			}
			if ((type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof (ICollection<>)))
			{
				//var elementType = type.GetGenericArguments()[0];
				var count = type.GetProperty("Count");

				return (scope, t, expected, actual) =>
					{
						bool result;
						if (TryReferenceCheck(scope, expected, actual, out result))
						{
							return result;
						}
						var firstCount = Convert.ToInt32(count.GetValue(expected, null));
						var secondCount = Convert.ToInt32(count.GetValue(actual, null));
						return TestCollectionEquality(scope, (IEnumerable) expected, (IEnumerable) actual, firstCount, secondCount);
					};
			}


			return (scope1, t, one1, two1) => TestSimpleEquality(scope1, one1, two1);
		}

		bool TestFieldEquality(IScope scope, object one, object another, IEnumerable<FieldInfo> fields)
		{
			bool equals = true;
			foreach (var fieldInfo in fields)
			{
				var type = fieldInfo.FieldType;

				var v1 = fieldInfo.GetValue(one);
				var v2 = fieldInfo.GetValue(another);

				using (var child = scope.Create(fieldInfo.Name))
				{
					var equality = _provider.GetEqualityTester(type);
					var localEquality = equality(child, type, v1, v2);
					if (!localEquality)
					{
						equals = false;
						//child.Error("Expected field '{0}' was '{1}'", v1, v2);
					}
				}
			}
			return equals;
		}


		bool TestPropertyEquality(IScope scope, object expected, object actual, PropertyInfo[] props)
		{

			bool equals = true;
			foreach (var fieldInfo in props)
			{
				var type = fieldInfo.PropertyType;

				var v1 = fieldInfo.GetValue(expected, null);
				var v2 = fieldInfo.GetValue(actual, null);

				using (var child = scope.Create(fieldInfo.Name))
				{
					var tester = _provider.GetEqualityTester(type);
					var localEquality = tester(child, type, v1, v2);
					if (!localEquality)
					{
						equals = false;
						//child.Error("Expected property '{0}' was '{1}'", v1, v2);
					}
				}
			}
			return equals;
		}

		static bool TryReferenceCheck(IScope scope, object expected, object actual, out bool result)
		{
			var expectedIsNull = ReferenceEquals(expected, null);
			var actualIsNull = ReferenceEquals(actual, null);
			if (expectedIsNull && actualIsNull)
			{
				result = true;
				return true;
			}

			if (expectedIsNull || actualIsNull)
			{
				var expectedString = expectedIsNull ? "<null>" : expected.GetType().Name;
				var actualString = actualIsNull ? "<null>" : actual.GetType().Name;

				result = false;
				scope.Error("Expected {0} was {1}", expectedString, actualString);
				return true;
			}
			result = false;
			return false;
		}

		bool TestCollectionEquality(IScope scope, IEnumerable expected, IEnumerable actual, int count1, int count2)
		{
			if (count1 != count2)
			{
				scope.Error("Expected ICollection count {0} was {1}", count1, count2);
				return false;
			}

			if (count1 == 0)
			{
				return true;
			}

			var e1 = expected.GetEnumerator();
			var e2 = actual.GetEnumerator();

			bool equals = true;

			for (int i = 0; i < count1; i++)
			{
				e1.MoveNext();
				e2.MoveNext();

				using (var child = scope.Create("[" + i + "]"))
				{
					bool result;
					if (TryReferenceCheck(child, e1.Current, e2.Current, out result))
					{
						equals |= result;
					}
					else
					{
						var t1 = e1.Current.GetType();
						var t2 = e2.Current.GetType();

						if (t1 != t2)
						{
							scope.Error("Expected '{0}' was '{1}'", t1, t2);
							equals = false;
						}
						else
						{
							var tester = _provider.GetEqualityTester(t1);

							if (!tester(child, t1, e1.Current, e2.Current))
							{
								equals = false;
							}
						}
					}
				}
			}
			return equals;
		}


		static bool TestGenericEquality(IScope scope, MethodInfo info, object expected, object actual)
		{
			var o = info.Invoke(expected, new[] {actual});
			var result = Convert.ToBoolean(o);
			if (!result)
			{
				scope.Error("Expected IEquatable<T> '{0}' was '{1}'.", expected, actual);
			}
			return result;
		}

		static bool TestSimpleEquality(IScope scope, object expected, object actual)
		{
			var result = expected.Equals(actual);
			if (!result)
			{
				scope.Error("Object equality failed. Expected '{0}' was '{1}'.", expected, actual);
			}
			return result;
		}
	}
}
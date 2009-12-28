#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Lokad.Quality;
using Lokad.Rules;

namespace Lokad.Testing
{
	/// <summary>
	/// 	Helper class responsible for building model equality testers
	/// </summary>
	public static class ModelEqualityTester
	{
		delegate bool Method<T>(IScope scope, T one, T two);

		static readonly string ModelTag = DesignOfClass.ConvertTagToString(ClassDesignTag.Model);
		static readonly string FieldTag = DesignOfClass.ConvertTagToString(ClassDesignTag.ImmutableWithFields);
		static readonly string PropertyTag = DesignOfClass.ConvertTagToString(ClassDesignTag.ImmutableWithProperties);

		/// <summary>
		/// 	Tests the equality of two models.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="scope">
		/// 	The scope in which the operation will be performed.
		/// </param>
		/// <param name="firstModel">The first model.</param>
		/// <param name="secondModel">The second model.</param>
		/// <returns>result</returns>
		public static bool TestEquality<T>(IScope scope, T firstModel, T secondModel)
		{
			if (!Cache<T>.IsThisAModel)
			{
				var error = string.Format("Type '{0}' should have '{1}' with '{2}' tag.",
					typeof (T),
					typeof (ClassDesignAttribute),
					ModelTag);
				throw new ArgumentException(error);
			}

			return Cache<T>.TestEquality(scope, firstModel, secondModel);
		}

		static bool TestEquality(IScope scope, Type type, object one, object two)
		{
			var genericType = typeof (Cache<>).MakeGenericType(type);


			var method = genericType.GetMethod("TestEquality", BindingFlags.Static | BindingFlags.Public);

			var invoke = method.Invoke(null, new[] {scope, one, two});
			var result = Convert.ToBoolean(invoke);
			return result;
		}

		static Method<T> BuildEqualizer<T>()
		{
			var type = typeof (T);

			var attributes = type
				.GetCustomAttributes(false)
				.Where(t => t is ClassDesignAttribute)
				.Cast<ClassDesignAttribute>()
				.ToArray();

			
			if (attributes.Any(a => a.ClassDesignTags.Contains(FieldTag)))
			{
				var fields = type.GetFields();
				return (scope, arg2, arg3) => TestFieldEquality(scope, arg2, arg3, fields);
			}
			
			if (attributes.Any(a => a.ClassDesignTags.Contains(PropertyTag)))
			{
				var props = type.GetProperties();
				return (scope, arg2, arg3) => TestPropertyEquality(scope, arg2, arg3, props);
			}

			var equatable = typeof (IEquatable<>).MakeGenericType(type);
			if (equatable.IsAssignableFrom(type))
			{
				// horray, friendly type here
				var methodInfo = equatable.GetMethod("Equals");
				return (scope, one, two) => TestGenericEquality(scope, methodInfo, one, two);
			}

			if (typeof(ICollection).IsAssignableFrom(type))
			{
				return (scope, one, two) => TestCollectionEquality(scope, (ICollection) one, (ICollection) two);
			}

			return (scope1, one1, two1) => TestSimpleEquality(scope1, one1, two1);
		}

		static bool TestFieldEquality<T>(IScope scope, T one, T another, FieldInfo[] fields)
		{
			bool equals = true;
			foreach (var fieldInfo in fields)
			{
				var type = fieldInfo.FieldType;

				var v1 = fieldInfo.GetValue(one);
				var v2 = fieldInfo.GetValue(another);

				using (var child = scope.Create(fieldInfo.Name))
				{
					var localEquality = TestEquality(child, type, v1, v2);
					if (!localEquality)
					{
						equals = false;
						//child.Error("Expected field '{0}' was '{1}'", v1, v2);
					}
				}
			}
			return equals;
		}

		static bool TestPropertyEquality<T>(IScope scope, T one, T another, PropertyInfo[] props)
		{
			bool equals = true;
			foreach (var fieldInfo in props)
			{
				var type = fieldInfo.PropertyType;

				var v1 = fieldInfo.GetValue(one, null);
				var v2 = fieldInfo.GetValue(another, null);

				using (var child = scope.Create(fieldInfo.Name))
				{
					var localEquality = TestEquality(child, type, v1, v2);
					if (!localEquality)
					{
						equals = false;
						//child.Error("Expected property '{0}' was '{1}'", v1, v2);
					}
				}
			}
			return equals;
		}

		static bool TestCollectionEquality(IScope scope, ICollection first, ICollection second)
		{
			if (first.Count != second.Count)
			{
				scope.Error("Expected ICollection count {0} was {1}", first.Count, second.Count);
				return false;
			}

			if (first.Count == 0)
			{
				return true;
			}

			var e1 = first.GetEnumerator();
			var e2 = second.GetEnumerator();
			
			bool equals = true;

			for (int i = 0; i < first.Count; i++)
			{
				e1.MoveNext();
				e2.MoveNext();

				using (var child = scope.Create("[" + i + "]"))
				{
					var t1 = e1.Current.GetType();
					var t2 = e2.Current.GetType();

					if (t1 != t2)
					{
						scope.Error("Expected '{0}' was '{1}'", t1, t2);
						equals = false;
					} else if (!TestEquality(child, t1, e1.Current, e2.Current))
					{
						equals = false;
					}
				}
			}
			return equals;
			
		}


		static bool TestGenericEquality(IScope scope, MethodInfo info, object v1, object v2)
		{
			var o = info.Invoke(v1, new[] {v2});
			var result = Convert.ToBoolean(o);
			if (!result)
			{
				scope.Error("Expected IEquatable<T> '{0}' was '{1}'.", v1, v2);
			}
			return result;
		}



		static bool TestSimpleEquality(IScope scope, object v1, object v2)
		{
			var result = v1.Equals(v2);
			if (!result)
			{
				scope.Error("Object equality failed. Expected '{0}' was '{1}'.", v1, v2);
			}
			return result;
		}

		static bool IsModel<T>()
		{
			var customAttributes = typeof (T).GetCustomAttributes(false);

			var attributes = customAttributes
				.Where(t => t is ClassDesignAttribute)
				.Cast<ClassDesignAttribute>();

			return attributes.Any(a => a.ClassDesignTags.Contains(ModelTag));
		}


		internal static class Cache<T>
		{
			static readonly Method<T> Cached = BuildEqualizer<T>();
			internal static readonly bool IsThisAModel = IsModel<T>();

			public static bool TestEquality(IScope scope, T first, T second)
			{
				return Cached(scope, first, second);
			}
		}
	}
}
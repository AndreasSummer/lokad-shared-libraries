using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace Lokad.Reflection
{
	static class TypeCache<TModel>
	{
		static TypeCache()
		{
			foreach (var propertyInfo in typeof(TModel).GetProperties())
			{
				CompilePropertyGetter(propertyInfo);
			}
			foreach (var fieldInfo in typeof(TModel).GetFields())
			{
				CompileFieldGetter(fieldInfo);
			}
		}

		public static TValue GetMemberValue<TValue>(TModel instance, Expression<Func<TModel,TValue>> expression)
		{
			var info = Express.MemberWithLambda(expression);
			return (TValue)Getters[info](instance);
		}

		static readonly IDictionary<MemberInfo, Func<TModel, object>> Getters =new Dictionary<MemberInfo, Func<TModel, object>>();

		static void CompilePropertyGetter(PropertyInfo propertyInfo)
		{
			if (!propertyInfo.CanRead) return;
			if (propertyInfo.GetGetMethod(true).IsStatic) return;

			// this
			var self = Expression.Parameter(typeof(TModel), "this");
			// this.Property
			var property = Expression.Property(self, propertyInfo);
			// (object)((this).Property)
			var propertyCast = Expression.Convert(property, typeof(object));
			
			var lambda = Expression.Lambda<Func<TModel, object>>(propertyCast, self);
			Getters.Add(propertyInfo, lambda.Compile());
		}

		static void CompileFieldGetter(FieldInfo fieldInfo)
		{
			if (fieldInfo.IsStatic) return;
			// this
			var self = Expression.Parameter(typeof(TModel), "this");
			// this.Property
			var accessor = Expression.Field(self, fieldInfo);
			// (object)((this).Property)
			var cast = Expression.Convert(accessor, typeof(object));
			var lambda = Expression.Lambda<Func<TModel, object>>(cast, self);
			Getters.Add(fieldInfo, lambda.Compile());
		}
	}
}
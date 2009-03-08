#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if !SILVERLIGHT2

using System.Rules;
using NUnit.Framework;

namespace System.Reflection
{
	[TestFixture]
	public sealed class ReflectTests
	{
		static void AssertVariable<T>(Func<T> func, string name)
		{
			var info = Reflect.Variable(func);
			Assert.AreSame(typeof (T), info.FieldType);
			Assert.AreEqual(name, info.Name);
		}

		static void AssertProperty<T>(Func<T> func, string name)
		{
			var info = Reflect.Property(func);
			Assert.AreSame(typeof (T), info.ReturnType);
			Assert.AreEqual("get_" + name, info.Name);
		}

		[Test]
		public void Variable_Works_With_Primitives()
		{
			var myVar = "Test";
			AssertVariable(() => myVar, "myVar");
		}

		[Test]
		public void Variable_Works_With_Arguments()
		{
			Check("argument", 3);
		}

		static void Check(string arg1, int arg2)
		{
			AssertVariable(() => arg1, "arg1");
			AssertVariable(() => arg2, "arg2");
		}

		[Test]
		public void Variable_Works_With_Multiple_Items_Per_Scope()
		{
			{
				var i1 = 1;
				var i2 = 2;

				AssertVariable(() => i1, "i1");
				AssertVariable(() => i2, "i2");
			}
			{
				var i1 = "A";
				var i2 = "B";

				AssertVariable(() => i1, "i1");
				AssertVariable(() => i2, "i2");
			}
		}

		[Test]
		public void Variable_Works_With_Anonymous()
		{
			var myVar = new
				{
					i = 1
				};

			AssertVariable(() => myVar, "myVar");
		}

		[Test, Expects.ArgumentException]
		public void Variable_Fails_On_Property()
		{
			var i = new
				{
					i = 1
				};
			Reflect.Variable(() => i.i);
		}

		[Test, Expects.ArgumentException]
		public void Variable_Fails_On_Constant()
		{
			Reflect.Variable(() => 1);
		}

		[Test]
		public void Variable()
		{
			var t = new Test<string>("Test");

			AssertVariable(() => t, "t");
		}


		public sealed class Test<T>
		{
			public readonly T Field;

			public Test(T property)
			{
				Field = property;
			}

			public T Property
			{
				get { return Field; }
			}
			
		}

		[Test]
		public void Property()
		{
			var i = new Test<string>("Value");
			var i2 = new
				{
					Property = "Value"
				};
			AssertProperty(() => i.Property, "Property");
			AssertProperty(() => i2.Property, "Property");
		}

		[Test]
		public void Variable_with_generic_method()
		{
			TestGenericMethod<Model>(null);
		}

		[Test]
		public void Variable_with_generic_class()
		{
			new GenericClass<Model>(null);
		}

		[Test]
		public void MemberName()
		{
			var i = new Test<string>("Value");
			var i2 = new
			{
				Property = "Value"
			};

			Assert.AreEqual(Reflect.MemberName(() => i.Property), "Property");
			Assert.AreEqual(Reflect.MemberName(() => i2.Property), "Property");
			Assert.AreEqual(Reflect.MemberName(() => i.Field), "Field");
		}


		static void TestGenericMethod<TModel>(IModel<TModel> model)
		{
			var name = Reflect.Variable(() => model).Name;
			Assert.AreEqual("model", name);
		}

		sealed class Model : IModel<Model>
		{
		}

		interface IModel<T>
		{
		}

		class GenericClass<TModel> where TModel : class, IModel<TModel>
		{
			public GenericClass(IModel<TModel> model)
			{
				Assert.AreEqual("model", Reflect.VariableName(() => model));
			}
		}

		[Test]
		public void Can_reflect_property_of_class_with_constraints()
		{
			var reflect = new ClassWithConstraints<ClassToReflect>();
			string propertyName = reflect.GetNameOfProperty();
			Assert.AreEqual("get_SomeProperty", propertyName);
		}

		class ClassWithConstraints<T> where T : ClassToReflect, new()
		{
			public T Target;
			public string GetNameOfProperty()
			{
				return Reflect.Property(() => Target.SomeProperty).Name;
			}
		}

		class ClassToReflect
		{
			public string SomeProperty { get; set; }
		}
	}
}

#endif
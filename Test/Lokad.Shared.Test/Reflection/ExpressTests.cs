#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Rules;
using NUnit.Framework;

namespace System.Reflection
{
	[TestFixture]
	public sealed class ExpressTests
	{
		[Test]
		public void Constructor()
		{
			var constructor = Express.Constructor(() => new ExpressTests());
			Enforce.That(constructor, Represents<ExpressTests>(".ctor"));
		}

		[Test]
		public void Static_Method()
		{
			var method = Express.Method(() => ReferenceEquals(null, null));
			Enforce.That(method, Represents<object>("ReferenceEquals"));
		}

		[Test]
		public void Method()
		{
			var method = Express<ExpressTests>.Method(i => i.Method());
			MemberInfo m2 = Express.Method(() => Method());
			Enforce.That(method, Represents<ExpressTests>("Method"), Is.SameAs(m2));
		}

		static Rule<MemberInfo> Represents<T>(IEquatable<string> name)
		{
			return (info, scope) =>
				{
					scope.Validate(info.DeclaringType, "DeclaringType", Is.SameAs(typeof (T)));
					scope.Validate(info.Name, "Name", Is.Equal(name));
				};
		}


		int IntProperty { get { return _intField; } }

		[Test]
		public void Property()
		{
			var property = Express.Property(() => IntProperty);
			MemberInfo p2 = Express<ExpressTests>.Property(i => i.IntProperty);

			Enforce.That(property, Is.SameAs(p2), Represents<ExpressTests>("IntProperty"));
		}

		// ReSharper disable ConvertToConstant
		readonly int _intField = 1;
		static readonly int _staticField = 4;
		// ReSharper restore ConvertToConstant

		static int StaticProperty { get { return _staticField; } }

		[Test]
		public void Static_Property()
		{
			var property = Express.Property(() => StaticProperty);
			Enforce.That(property, Represents<ExpressTests>("StaticProperty"));
		}

		

		[Test]
		public void Field()
		{
			var field = Express.Field(() => _intField);
			MemberInfo f2 = Express<ExpressTests>.Field(et => et._intField);

			Enforce.That(field, Is.SameAs(f2), Represents<ExpressTests>("_intField"));
		}

		[Test]
		public void Static_Field()
		{
			var field = Express.Field(() => _staticField);
			Enforce.That(field, Represents<ExpressTests>("_staticField"));
		}
	}
}
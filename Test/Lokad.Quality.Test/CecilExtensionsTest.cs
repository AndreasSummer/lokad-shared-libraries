#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Container;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Mono.Cecil;
using NUnit.Framework;

namespace Lokad.Quality.Test
{
	[TestFixture]
	public class CecilExtensionsTest
	{
// ReSharper disable RedundantExtendsListEntry
		[Serializable]
		[Component]
		abstract class B : IA, IB
// ReSharper restore RedundantExtendsListEntry
		{
			public abstract void Do();
			public abstract void Do(IA a);
		}

		interface IA : IB
		{
			void Do();
			void Do(IA a);
		}

		interface IB
		{
		}

		readonly Codebase _base = GlobalSetup.Codebase;

		[Test]
		public void Interface_Inheritance_Is_Detected()
		{
			var inheritance = _b.GetInheritance(_base).ToArray();

			// test that there are only unique names
			var dictionary = inheritance.ToDictionary(t => t.Name).Keys.ToSet();

			CollectionAssert.Contains(dictionary, typeof (IB).Name);
			CollectionAssert.Contains(dictionary, typeof (IA).Name);
			CollectionAssert.Contains(dictionary, typeof (B).Name);
		}

		[Test, Expects.InvalidOperationException]
		public void Has_Does_Not_Work_On_Serializable()
		{
			_b.Has<SerializableAttribute>();
		}

		readonly TypeDefinition _b = GlobalSetup.Codebase.Find<B>();


		[Test]
		public void Has()
		{
			Assert.IsTrue(_b.Has<ComponentAttribute>());
			Assert.IsFalse(_b.Has<ImmutableAttribute>());
		}
	}
}
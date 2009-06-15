#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using Mono.Cecil.Cil;
using NUnit.Framework;

namespace Lokad.Quality.Test
{
	public sealed class ElementAttribute : Attribute
	{
	}

	[Element]
	public static class Fire
	{
		public static void Extend(this object self)
		{
			throw new NotImplementedException();
		}
	}

	class ExceptionClass
	{
		public void Run()
		{
			InnerCall();
			throw new NotSupportedException();
		}

		void InnerCall()
		{
			throw new NotImplementedException();
		}
	}

	[TestFixture]
	public class RuleUseCases
	{
		// ReSharper disable InconsistentNaming
		[Test]
		public void Count_Methods_With_Exceptions()
		{
			var codebase = GlobalSetup.Codebase;

			var throwingMethods = codebase.Methods
				.Where(m => m
					.GetInstructions()
					.Exists(i => i.Creates<NotImplementedException>()))
				.ToArray();

			Assert.AreEqual(2, throwingMethods.Length);
		}

		[Test]
		public void Get_created_exceptions()
		{
			var type = GlobalSetup.Codebase.Find<ExceptionClass>();
			var method = type.GetMethods().First(md => md.Name == "Run");

			var exceptions = GetCreatedExceptions(method)
				.ToArray();

			Assert.AreEqual(1, exceptions.Length);
			Assert.AreEqual("NotSupportedException", exceptions[0].Name);

			var references = method
				.GetReferencedMethods()
				.ToArray();
			Console.WriteLine(references[0]);
		}

		static IEnumerable<TypeReference> GetCreatedExceptions(MethodDefinition method)
		{
			return method.GetInstructions()
				.Where(i => i.OpCode == OpCodes.Newobj)
				.Select(i => ((MemberReference) i.Operand).DeclaringType)
				.Where(tr => tr.Name.EndsWith("Exception"))
				.Distinct();
		}

		[Test]
		public void Count_Methods_That_Extend_Object()
		{
			var codebase = GlobalSetup.Codebase;

			var methods = codebase.Methods
				.Where(m => m.Has<ExtensionAttribute>())
				.Where(m => m.Parameters[0].Is<object>());

			Assert.AreEqual(1, methods.Count());

			//CollectionAssert.IsEmpty(methods.ToArray());
		}
	}
}
using System;
using System.Diagnostics;
using NUnit.Framework;
using System.Linq;

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class ExtendIScopeForMediumTrustTests
	{
		// ReSharper disable InconsistentNaming

		sealed class Model
		{
			public string Name { get; set; }
			public string Login;

			public string[] Names { get; set; }
			public string[] Logins;

			public Model()
			{
				Names = new string[0];
				Logins = new string[0];
			}
		}

		static void ValidateModelExpression(Model model, IScope scope)
		{
			scope.Validate(model, m => m.Login, StringIs.ValidEmail);
			scope.Validate(model, m => m.Name, StringIs.NotEmpty);
			scope.ValidateMany(model, m => m.Logins, StringIs.ValidEmail);
			scope.ValidateMany(model, m => m.Names, StringIs.NotEmpty);
		}

		static void ValidateModel(Model model, IScope scope)
		{
			scope.Validate(() => model.Login, StringIs.ValidEmail);
			scope.Validate(() => model.Name, StringIs.NotEmpty);
			
		}

		[Test, Explicit, Ignore]
		public void Test()
		{
			var model = new Model()
				{
					Login = "shared@lokad.com",
					Name = "Foo Bar"
				};

			MeasureExecution(() => Scope.IsValid(model, ValidateModelExpression));
			MeasureExecution(() => Scope.IsValid(model, ValidateModel));
		}

		[Test]
		public void Invalid_field()
		{
			var model = new Model()
			{
				Login = "shared@lokad.com",
				Name = ""
			};

			RuleAssert.IsError(model, ValidateModelExpression);
		}

		[Test]
		public void Invalid_property()
		{
			var model = new Model()
			{
				Login = "sharedlokad.com",
				Name = "valid"
			};
			RuleAssert.IsError(model, ValidateModelExpression);
		}

		[Test]
		public void Invalid_property_collection()
		{
			RuleAssert.IsError(new Model()
			{
				Login = "shared@lokad.com",
				Name = "valid",
				Names = new []{""}
			}, ValidateModelExpression);
		}

		[Test]
		public void Invalid_field_collection()
		{
			RuleAssert.IsError(new Model()
			{
				Login = "shared@lokad.com",
				Name = "valid",
				Logins = new[] { "non-email" }
			}, ValidateModelExpression);
		}

		[Test]
		public void Valid_model()
		{
			RuleAssert.IsNone(new Model()
			{
				Login = "shared@lokad.com",
				Name = "valid",
				Logins = new[] { "email@none.com" },
				Names = new [] { "Another name"}

			}, ValidateModelExpression);
		}

		static void MeasureExecution(Action action)
		{
			action(); // JIT

			var startNew = Stopwatch.StartNew();

			for (int i = 0; i < 100000; i++)
			{
				action();
			}
			startNew.Stop();
			
			Console.WriteLine("Execution time: {0} ", startNew.Elapsed);
		}
		
	}
}
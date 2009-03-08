using System.IO;
using System.Rules;
using NUnit.Framework;
using System;

namespace Lokad.Logging
{
	[TestFixture]
	public sealed class LoggingStackTests
	{
		static readonly string TestLog = Path.Combine(TestPath, "test.log");
		TextWriter _out;
		const string TestPath = "Logs";

		[Test]
		public void Test_RollingLog()
		{
			Assert.IsFalse(File.Exists(TestLog), "File should not exist before test");

			LoggingStack.UseRollingLog(TestLog, 10.Mb(), 10);
			LoggingStack.GetLog().Error(new Exception(), "Test");

			Assert.IsTrue(File.Exists(TestLog), "Log should be created");
		}

		[Test]
		public void Test_DailyLog()
		{
			Assert.IsFalse(Directory.Exists(TestPath));

			LoggingStack.UseDailyLog(TestLog);
			LoggingStack.GetLog().Error(new Exception(), "Some exception");

			Assert.IsTrue(File.Exists(TestLog), "Log should be created");
		}

		[TestFixtureSetUp]
		public void Init()
		{
			_out = Console.Out;
		}

		[Test]
		public void Test_ConsoleLog()
		{
			using (var writer = new StringWriter())
			{
				Console.SetOut(writer);
				LoggingStack.UseConsole();
				LoggingStack.GetLog().Error(new Exception(), "Some exception");
				Enforce.That(writer.ToString(), StringIs.NotEmpty);
			}
		}

		[TearDown]
		public void Dispose()
		{
			LoggingStack.Reset();
			if (Directory.Exists(TestPath))
				Directory.Delete(TestPath, true);
			
			Console.SetOut(_out);
		}
	}
}
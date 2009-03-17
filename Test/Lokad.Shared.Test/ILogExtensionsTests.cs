#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Diagnostics;
using Lokad.Rules;
using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class ILogExtensionsTests
	{
		[Test]
		public void UseCases()
		{
			var log = NullLog.Instance;
			var ex = new Exception();
			var id = 0;

			log.Debug("Message");
			log.Debug(ex, "Message with exception");
			log.DebugFormat("Message #{0}", id);
			log.DebugFormat(ex, "Message #{0}", id);
			RuleAssert.IsFalse(() => log.IsDebugEnabled());


			log.Warn("Message");
			log.Warn(ex, "Message with exception");
			log.WarnFormat("Message #{0}", id);
			log.WarnFormat(ex, "Message #{0}", id);
			RuleAssert.IsFalse(() => log.IsWarnEnabled());


			log.Info("Message");
			log.Info(ex, "Message with exception");
			log.InfoFormat("Message #{0}", id);
			log.InfoFormat(ex, "Message #{0}", id);
			RuleAssert.IsFalse(() => log.IsInfoEnabled());

			log.Error("Message");
			log.Error(ex, "Message with exception");
			log.ErrorFormat("Message #{0}", id);
			log.ErrorFormat(ex, "Message #{0}", id);
			RuleAssert.IsFalse(() => log.IsErrorEnabled());

			log.Fatal("Message");
			log.Fatal(ex, "Message with exception");
			log.FatalFormat("Message #{0}", id);
			log.FatalFormat(ex, "Message #{0}", id);
			RuleAssert.IsFalse(() => log.IsFatalEnabled());
		}
	}
}
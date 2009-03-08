#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace System.Rules
{
	[TestFixture]
	public sealed class MessageScopeTests
	{
		[Test]
		public void Test()
		{
			var messages = Scope.GetMessages(0, "item", ScopeTestHelper.RunNesting);
			Assert.AreEqual(6, messages.Count);

			var check = messages[3];
			Assert.AreEqual(RuleLevel.Error, check.Level);
			Assert.AreEqual("Error1", check.Message);
			Assert.AreEqual("item.Group1.Group2", check.Path);


			Assert.IsTrue(messages.IsError, "IsError");
			Assert.IsTrue(messages.IsWarn, "IsWarn");
			Assert.IsFalse(messages.IsSuccess, "IsSuccess");
		}

		[Test]
		public void Empty()
		{
			var messages = Scope.GetMessages(0, "item");
			Assert.IsNotNull(messages);
			CollectionAssert.IsEmpty(messages);

			Assert.IsFalse(messages.IsError, "IsError");
			Assert.IsFalse(messages.IsWarn, "IsWarn");
			Assert.IsTrue(messages.IsSuccess, "IsSuccess");
		}
	}
}
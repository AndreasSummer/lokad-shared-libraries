#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;
#if !SILVERLIGHT2
namespace System.Threading
{
	[TestFixture]
	public sealed class ReaderWriterLockSlimExtensionsTests
	{
		private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

		[Test]
		public void Test_GetReadLock()
		{
			using (_lock.GetReadLock())
			{
				Assert.IsTrue(_lock.IsReadLockHeld);
			}
			Assert.IsFalse(_lock.IsReadLockHeld);
		}

		[Test]
		public void Test_GetWriteLock()
		{
			using (_lock.GetWriteLock())
			{
				Assert.IsTrue(_lock.IsWriteLockHeld);
			}
			Assert.IsFalse(_lock.IsWriteLockHeld);
		}

		[Test]
		public void Test_GetUpgradeableReadLock()
		{
			using (_lock.GetUpgradeableReadLock())
			{
				Assert.IsTrue(_lock.IsUpgradeableReadLockHeld);
			}
			Assert.IsFalse(_lock.IsUpgradeableReadLockHeld);
		}

		[Test]
		public void Test_All()
		{
			using (_lock.GetUpgradeableReadLock())
			using (_lock.GetWriteLock())
			{
				Assert.IsTrue(_lock.IsWriteLockHeld);
				Assert.IsTrue(_lock.IsUpgradeableReadLockHeld);
			}
			Assert.IsFalse(_lock.IsUpgradeableReadLockHeld);
		}
	}
}
#endif
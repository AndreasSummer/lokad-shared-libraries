#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion
#if !SILVERLIGHT2

namespace System.Threading
{
	/// <summary>
	/// Helper class that simplifies <see cref="ReaderWriterLockSlim"/> usage
	/// </summary>
	public static class ReaderWriterLockSlimExtensions
	{
		/// <summary>
		/// Gets the read lock object, that is released when the object is disposed.
		/// </summary>
		/// <param name="slimLock">The slim lock object.</param>
		/// <returns></returns>
		public static IDisposable GetReadLock(this ReaderWriterLockSlim slimLock)
		{
			slimLock.EnterReadLock();
			return new DisposableAction(slimLock.ExitReadLock);
		}

		/// <summary>
		/// Gets the write lock, that is released when the object is disposed.
		/// </summary>
		/// <param name="slimLock">The slim lock.</param>
		/// <returns></returns>
		public static IDisposable GetWriteLock(this ReaderWriterLockSlim slimLock)
		{
			slimLock.EnterWriteLock();
			return new DisposableAction(slimLock.ExitWriteLock);
		}

		/// <summary>
		/// Gets the upgradeable read lock, that is released, when the object is disposed
		/// </summary>
		/// <param name="slimLock">The slim lock.</param>
		/// <returns></returns>
		public static IDisposable GetUpgradeableReadLock(this ReaderWriterLockSlim slimLock)
		{
			slimLock.EnterUpgradeableReadLock();
			return new DisposableAction(slimLock.ExitUpgradeableReadLock);
		}
	}
}
#endif
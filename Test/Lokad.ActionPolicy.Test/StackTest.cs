#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad
{
	static class StackTest<TException> where TException : Exception, new()
	{
		static void InternalStack()
		{
			throw new TException();
		}

		static void Fire()
		{
			InternalStack();
		}

		internal static void Check(ActionPolicy policy)
		{
			try
			{
				policy.Do(Fire);
			}
			catch (TException ex)
			{
				StringAssert.Contains("InternalStack", ex.StackTrace);
			}
		}
	}
}
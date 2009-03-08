#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Diagnostics.CodeAnalysis;

namespace System
{
	/// <summary>
	/// Helper class for creating lambda expressions that return
	/// anonymous types
	/// </summary>
	/// <typeparam name="T1">The type of the first argument.</typeparam>
	[NoCodeCoverage]
	public static class Lambda<T1>
	{
		/// <summary>
		/// Returns the provided function.
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="func">The func.</param>
		/// <returns>returns the same function instance</returns>
		public static Func<T1, TResult> Func<TResult>(Func<T1, TResult> func)
		{
			return func;
		}
	}

	/// <summary>
	/// Helper class for creating lambda expressions that return
	/// anonymous types
	/// </summary>
	[NoCodeCoverage]
	public static class Lambda
	{
		/// <summary>
		/// Returns the provided function
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="func">The function.</param>
		/// <returns>returns same function instance</returns>
		public static Func<TResult> Func<TResult>(Func<TResult> func)
		{
			return func;
		}

		/// <summary>
		/// Returns the provided action
		/// </summary>
		/// <param name="action">The action.</param>
		/// <returns>returns same action instance</returns>
		public static Action<T1> Action<T1>(Action<T1> action)
		{
			return action;
		}
	}
}
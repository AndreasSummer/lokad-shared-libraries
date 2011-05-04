#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Globalization;
using System.Reflection;
using Lokad.Quality;
using Lokad.Reflection;

namespace Lokad
{
	/// <summary>
	/// Helper class for generating exceptions
	/// </summary>
	[NoCodeCoverage]
	[UsedImplicitly]
	public class Errors
	{
	    [NotNull]
		internal static Exception ArgumentNull<T>([NotNull] Func<T> argumentReference)
		{
			var message = StringUtil.FormatInvariant("Parameter of type '{0}' can't be null", typeof (T));
			var paramName = Reflect.VariableName(argumentReference);
			return new ArgumentNullException(paramName, message);
		}

		[NotNull]
		internal static Exception Argument<T>([NotNull] Func<T> argumentReference, [NotNull] string message)
		{
			var paramName = Reflect.VariableName(argumentReference);
			return new ArgumentException(message, paramName);
		}


		static readonly MethodInfo InternalPreserveStackTraceMethod;

		static Errors()
		{
			InternalPreserveStackTraceMethod = typeof(Exception).GetMethod("InternalPreserveStackTrace",
				BindingFlags.Instance | BindingFlags.NonPublic);
		}

		/// <summary>
		/// Returns inner exception, while preserving the stack trace
		/// </summary>
		/// <param name="e">The target invocation exception to unwrap.</param>
		/// <returns>inner exception</returns>
		[NotNull, UsedImplicitly]
		public static Exception Inner([NotNull] TargetInvocationException e)
		{
			if (e == null) throw new ArgumentNullException("e");
			InternalPreserveStackTraceMethod.Invoke(e.InnerException, new object[0]);
			return e.InnerException;
		}
	}
}
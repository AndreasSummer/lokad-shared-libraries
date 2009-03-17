#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if !SILVERLIGHT2
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lokad.Quality;
using Lokad.Reflection;

namespace Lokad.Diagnostics
{
	/// <summary>
	/// Helper class to simplify counter creation syntax
	/// </summary>
	public abstract class ExecutionCounterGroup
	{
		readonly List<ExecutionCounter> _counters = new List<ExecutionCounter>();

		/// <summary>
		/// Registers the counters in the global cache
		/// </summary>
		protected void Register()
		{
			ExecutionCounters.Default.RegisterRange(_counters);
		}

		/// <summary>
		/// Creates the counter and adds it to the internal collection.
		/// </summary>
		/// <param name="name">The name for the new counter.</param>
		/// <param name="openCounterCount">The open counter count.</param>
		/// <param name="closeCounterCount">The close counter count.</param>
		/// <returns>instance of the created counter</returns>
		protected ExecutionCounter CreateCounter(string name, int openCounterCount, int closeCounterCount)
		{
			var counter = new ExecutionCounter(name, openCounterCount, closeCounterCount);
			_counters.Add(counter);
			return counter;
		}

		/// <summary> Creates the counter and adds it to the internal collection. </summary>
		/// <param name="expression">The expression to derive counter name from.</param>
		/// <param name="openCounterCount">The open counter count.</param>
		/// <param name="closeCounterCount">The close counter count.</param>
		/// <returns>instance of the new counter</returns>
		protected ExecutionCounter CreateCounter(Expression<Action> expression, int openCounterCount, int closeCounterCount)
		{
			var methodInfo = Express.Method(expression);
			string counterName = StringUtil.FormatInvariant("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name);
			return CreateCounter(counterName, openCounterCount, closeCounterCount);
		}


		/// <summary> Creates the counter and adds it to the internal collection. </summary>
		/// <param name="expression">The expression to derive counter name from.</param>
		/// <param name="openCounterCount">The open counter count.</param>
		/// <param name="closeCounterCount">The close counter count.</param>
		/// <returns>instance of the new counter</returns>
		protected ExecutionCounter CreateCounterForCtor<T>(Expression<Func<T>> expression, int openCounterCount,
			int closeCounterCount)
		{
			var methodInfo = Express.Constructor(expression);
			string counterName = StringUtil.FormatInvariant("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name);
			return CreateCounter(counterName, openCounterCount, closeCounterCount);
		}
	}

	/// <summary>
	/// Helper class to simplify counter creation syntax
	/// </summary>
	[NoCodeCoverage]
	public abstract class ExecutionCounterGroup<T> : ExecutionCounterGroup
	{
		/// <summary>
		/// Creates new counter and adds it to the internal collection
		/// </summary>
		/// <param name="call">The call to derive counter name from.</param>
		/// <param name="openCounterCount">The open counter count.</param>
		/// <param name="closeCounterCount">The close counter count.</param>
		/// <returns>instance of the created counter</returns>
		protected ExecutionCounter CreateCounter(Expression<Action<T>> call, int openCounterCount, int closeCounterCount)
		{
			var methodInfo = Express.MethodWithLambda(call);
			string counterName = StringUtil.FormatInvariant("{0}.{1}", typeof (T).Name, methodInfo.Name);
			return CreateCounter(counterName, openCounterCount, closeCounterCount);
		}
	}
}

#endif
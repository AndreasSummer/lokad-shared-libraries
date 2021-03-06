#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using Lokad.Exceptions;
using Lokad.Rules;

namespace Lokad
{
	/// <summary> Fluent API for defining <see cref="ActionPolicy"/> 
	/// that allows to handle exceptions. </summary>
	public static class ExceptionHandlerSyntax
	{
		/* Development notes
		 * =================
		 * If a stateful policy is returned by the syntax, 
		 * it must be wrapped with the sync lock
		 */

		static void DoNothing2(Exception ex, int count)
		{
		}

		static void DoNothing2(Exception ex, TimeSpan sleep)
		{
		}

		/// <summary>
		/// Builds <see cref="ActionPolicy"/> that will retry exception handling
		/// for a couple of times before giving up.
		/// </summary>
		/// <param name="syntax">The syntax.</param>
		/// <param name="retryCount">The retry count.</param>
		/// <returns>reusable instance of policy</returns>
		public static ActionPolicy Retry(this Syntax<ExceptionHandler> syntax, int retryCount)
		{
			Enforce.Argument(() => syntax);

			Func<IRetryState> state = () => new RetryStateWithCount(retryCount, DoNothing2);
			return new ActionPolicy(action => RetryPolicy.Implementation(action, syntax.Target, state));
		}

		/// <summary>
		/// Builds <see cref="ActionPolicy"/> that will retry exception handling
		/// for a couple of times before giving up.
		/// </summary>
		/// <param name="syntax">The syntax.</param>
		/// <param name="retryCount">The retry count.</param>
		/// <param name="onRetry">The action to perform on retry (i.e.: write to log).
		/// First parameter is the exception and second one is its number in sequence. </param>
		/// <returns>reusable policy instance </returns>
		public static ActionPolicy Retry(this Syntax<ExceptionHandler> syntax, int retryCount, Action<Exception, int> onRetry)
		{
			Enforce.Arguments(() => syntax, () => onRetry);
			Enforce.Argument(() => retryCount, Is.GreaterThan(0));

			Func<IRetryState> state = () => new RetryStateWithCount(retryCount, onRetry);
			return new ActionPolicy(action => RetryPolicy.Implementation(action, syntax.Target, state));
		}

		/// <summary> Builds <see cref="ActionPolicy"/> that will keep retrying forever </summary>
		/// <param name="syntax">The syntax to extend.</param>
		/// <param name="onRetry">The action to perform when the exception could be retried.</param>
		/// <returns> reusable instance of policy</returns>
		public static ActionPolicy RetryForever(this Syntax<ExceptionHandler> syntax, Action<Exception> onRetry)
		{
			Enforce.Arguments(() => syntax, () => onRetry);

			var state = new RetryState(onRetry);
			return new ActionPolicy(action => RetryPolicy.Implementation(action, syntax.Target, () => state));
		}

		/// <summary> <para>Builds the policy that will keep retrying as long as 
		/// the exception could be handled by the <paramref name="syntax"/> being 
		/// built and <paramref name="sleepDurations"/> is providing the sleep intervals.
		/// </para>
		/// <para>See <see cref="Range"/> for methods to create long intervals on-the-fly</para>
		/// </summary>
		/// <param name="syntax">The syntax.</param>
		/// <param name="sleepDurations">The sleep durations.</param>
		/// <param name="onRetry">The action to perform on retry (i.e.: write to log).
		/// First parameter is the exception and second one is the planned sleep duration. </param>
		/// <returns>new policy instance</returns>
		public static ActionPolicy WaitAndRetry(this Syntax<ExceptionHandler> syntax, IEnumerable<TimeSpan> sleepDurations,
			Action<Exception, TimeSpan> onRetry)
		{
			Enforce.Arguments(() => syntax, () => onRetry, () => sleepDurations);

			Func<IRetryState> state = () => new RetryStateWithSleep(sleepDurations, onRetry);
			return new ActionPolicy(action => RetryPolicy.Implementation(action, syntax.Target, state));
		}

		/// <summary> <para>Builds the policy that will keep retrying as long as 
		/// the exception could be handled by the <paramref name="syntax"/> being 
		/// built and <paramref name="sleepDurations"/> is providing the sleep intervals.
		/// </para>
		/// <para>See <see cref="Range"/> for methods to create long intervals on-the-fly</para>
		/// </summary>
		/// <param name="syntax">The syntax.</param>
		/// <param name="sleepDurations">The sleep durations.</param>
		/// <returns>new policy instance</returns>
		public static ActionPolicy WaitAndRetry(this Syntax<ExceptionHandler> syntax, IEnumerable<TimeSpan> sleepDurations)
		{
			Enforce.Arguments(() => syntax, () => sleepDurations);

			Func<IRetryState> state = () => new RetryStateWithSleep(sleepDurations, DoNothing2);
			return new ActionPolicy(action => RetryPolicy.Implementation(action, syntax.Target, state));
		}

#if !SILVERLIGHT2

		/// <summary>
		///  <para>Builds the policy that will "break the circuit" after <paramref name="countBeforeBreaking"/>
		/// exceptions that could be handled by the <paramref name="syntax"/> being built. The circuit 
		/// stays broken for the <paramref name="duration"/>. Any attempt to
		/// invoke method within the policy, while the circuit is broken, will immediately re-throw
		/// the last exception.  </para>
		/// <para>If the action fails within the policy after the block period, then the breaker 
		/// is blocked again for the next <paramref name="duration"/>.
		/// It will be reset, otherwise.</para> 
		/// </summary>
		/// <param name="syntax">The syntax.</param>
		/// <param name="duration">How much time the breaker will stay open before resetting</param>
		/// <param name="countBeforeBreaking">How many exceptions are needed to break the circuit</param>
		/// <returns>shared policy instance</returns>
		/// <remarks>(see "ReleaseIT!" for the details)</remarks>
		public static ActionPolicyWithState CircuitBreaker(this Syntax<ExceptionHandler> syntax, TimeSpan duration,
			int countBeforeBreaking)
		{
			Enforce.Argument(() => syntax);
			Enforce.Argument(() => countBeforeBreaking, Is.GreaterThan(0));
			Enforce.Argument(() => duration, Is.NotDefault);

			var state = new CircuitBreakerState(duration, countBeforeBreaking);
			var syncLock = new CircuitBreakerStateLock(state);
			return new ActionPolicyWithState(action => CircuitBreakerPolicy.Implementation(action, syntax.Target, syncLock));
		}
#endif
	}
}
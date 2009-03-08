#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Diagnostics.CodeAnalysis;

namespace System
{
	/// <summary>
	/// Helper class that allows to pass out method call results without using exceptions
	/// </summary>
	/// <typeparam name="T">type of the associated data</typeparam>
	[Immutable]
	public sealed class Result<T> : IEquatable<Result<T>>
	{
		readonly bool _isSuccess;
		readonly T _value;
		readonly string _error;

		Result(bool isSuccess, T value, string errorMessage)
		{
			_isSuccess = isSuccess;
			_error = errorMessage;
			_value = value;
		}

		/// <summary> Creates failure result </summary>
		/// <param name="errorMessage">error message</param>
		/// <returns>result that is a failure</returns>
		public static Result<T> Error(string errorMessage)
		{
			if (string.IsNullOrEmpty(errorMessage))
				throw new ArgumentException("String can't be null or empty", "errorMessage");

			return new Result<T>(false, default(T), errorMessage);
		}

		/// <summary>  Creates failure result </summary>
		/// <param name="errorFormatString">format string for the error message</param>
		/// <param name="args">The arguments.</param>
		/// <returns>result that is a failure</returns>
		public static Result<T> Error(string errorFormatString, params object[] args)
		{
			if (string.IsNullOrEmpty(errorFormatString))
				throw new ArgumentException("String can't be null or empty", "errorFormatString");

			return Error(string.Format(errorFormatString, args));
		}

		/// <summary>
		/// Creates success result
		/// </summary>
		/// <param name="value">item associated with the success result</param>
		/// <returns>result that is a success</returns>
		/// <exception cref="ArgumentNullException">if <paramref name="value"/> is a reference type that is null</exception>
		public static Result<T> Success(T value)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			if (null == value) throw new ArgumentNullException("value");
			// ReSharper restore CompareNonConstrainedGenericWithNull

			return new Result<T>(true, value, null);
		}

		/// <summary>
		/// Gets a value indicating whether this result is valid.
		/// </summary>
		/// <value><c>true</c> if this result is valid; otherwise, <c>false</c>.</value>
		public bool IsSuccess
		{
			get { return _isSuccess; }
		}

		/// <summary>
		/// item associated with this result
		/// </summary>
		public T Value
		{
			get
			{
				Enforce.That(_isSuccess, "You should not access result data if it has failed.");
				return _value;
			}
		}

		/// <summary>
		/// Error message associated with this failure
		/// </summary>
		public string ErrorMessage
		{
			get
			{
				Enforce.That(!_isSuccess, "You should not access error message if the result is valid.");
				return _error;
			}
		}

		/// <summary>
		/// Performs an implicit conversion from <typeparamref name="T"/> to <see cref="Result{T}"/>.
		/// </summary>
		/// <param name="value">The item.</param>
		/// <returns>The result of the conversion.</returns>
		/// <exception cref="ArgumentNullException">if <paramref name="value"/> is a reference type that is null</exception>
		public static implicit operator Result<T>(T value)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			if (null == value) throw new ArgumentNullException("value");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return new Result<T>(true, value, null);
		}

		/// <summary>
		/// Applies the specified <paramref name="action"/>
		/// to this <see cref="Result{T}"/>, if it has value.
		/// </summary>
		/// <param name="action">The action to apply.</param>
		/// <exception cref="ArgumentNullException">if <paramref name="action"/> is null</exception>
		public void Apply([NotNull] Action<T> action)
		{
			if (action == null) throw new ArgumentNullException("action");
			if (_isSuccess)
				action(_value);
		}



		/// <summary>
		/// Converts value of this instance
		/// using the provided <paramref name="converter"/>
		/// </summary>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="converter">The converter.</param>
		/// <returns>Converted result</returns>
		/// <exception cref="ArgumentNullException"> if <paramref name="converter"/> is null</exception>
		public Result<TTarget> Convert<TTarget>([NotNull] Func<T, TTarget> converter)
		{
			if (converter == null) throw new ArgumentNullException("converter");
			if (!_isSuccess)
				return Result<TTarget>.Error(_error);

			return converter(_value);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		public bool Equals(Result<T> other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other._isSuccess.Equals(_isSuccess) && Equals(other._value, _value) && Equals(other._error, _error);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj"/> parameter is null.
		/// </exception>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Result<T>)) return false;
			return Equals((Result<T>) obj);
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		public override int GetHashCode()
		{
			unchecked
			{
				int result = _isSuccess.GetHashCode();
				result = (result*397) ^ _value.GetHashCode();
				result = (result*397) ^ (_error != null ? _error.GetHashCode() : 0);
				return result;
			}
		}

		/// <summary>
		/// Combines this <see cref="Result{T}"/> with the result returned
		/// by <paramref name="converter"/>.
		/// </summary>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="converter">The converter.</param>
		/// <returns>Combined result.</returns>
		public Result<TTarget> Combine<TTarget>(Func<T, Result<TTarget>> converter)
		{
			if (!_isSuccess)
				return Result<TTarget>.Error(_error);

			return converter(_value);
		}

		/// <summary>
		/// Converts this <see cref="Result{T}"/> to <see cref="Maybe{T}"/>, 
		/// using the <paramref name="converter"/> to perform the value conversion.
		/// </summary>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="converter">The reflector.</param>
		/// <returns><see cref="Maybe{T}"/> that represents the original value behind the <see cref="Result{T}"/> after the conversion</returns>
		public Maybe<TTarget> ToMaybe<TTarget>(Func<T, TTarget> converter)
			
		{
			if (!_isSuccess)
				return Maybe<TTarget>.Empty;

			return converter(_value);
		}
	}

	/// <summary> Helper class for creating <see cref="Result{T}"/> instances </summary>
	[NoCodeCoverage]
	[Immutable]
	public static class Result
	{
		/// <summary> Creates success result </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="value">The item.</param>
		/// <returns>new result instance</returns>
		/// <seealso cref="Result{T}.Success"/>
		[NotNull]
		public static Result<TResult> Success<TResult>([NotNull]TResult value)
		{
			return Result<TResult>.Success(value);
		}
	}
}
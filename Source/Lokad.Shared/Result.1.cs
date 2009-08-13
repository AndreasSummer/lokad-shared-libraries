#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Quality;

namespace Lokad
{
	/// <summary>
	/// Helper class that allows to pass out method call results without using exceptions
	/// </summary>
	/// <typeparam name="T">type of the associated data</typeparam>
	[Immutable]
	public sealed class Result<T> : Result<T, string>, IEquatable<Result<T>>
	{
		Result(bool isSuccess, T value, string error)
			: base(isSuccess, value, error)
		{
		}

		/// <summary>
		/// Error message associated with this failure
		/// </summary>
		[Obsolete("Use Error instead"), UsedImplicitly]
		public string ErrorMessage
		{
			get { return _error; }
		}

		/// <summary>  Creates failure result </summary>
		/// <param name="errorFormatString">format string for the error message</param>
		/// <param name="args">The arguments.</param>
		/// <returns>result that is a failure</returns>
		/// <exception cref="ArgumentNullException">if format string is null</exception>
		public static Result<T> CreateError([NotNull] string errorFormatString, params object[] args)
		{
			if (errorFormatString == null) throw new ArgumentNullException("errorFormatString");

			return CreateError(string.Format(errorFormatString, args));
		}

		/// <summary>
		/// Creates the success result.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>result encapsulating the success value</returns>
		/// <exception cref="ArgumentNullException">if value is a null reference type</exception>
		public new static Result<T> CreateSuccess(T value)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			if (null == value) throw new ArgumentNullException("value");
			// ReSharper restore CompareNonConstrainedGenericWithNull

			return new Result<T>(true, value, default(string));
		}

		/// <summary>
		/// Converts value of this instance
		/// using the provided <paramref name="converter"/>
		/// </summary>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="converter">The converter.</param>
		/// <returns>Converted result</returns>
		/// <exception cref="ArgumentNullException"> if <paramref name="converter"/> is null</exception>
		public new Result<TTarget> Convert<TTarget>([NotNull] Func<T, TTarget> converter)
		{
			if (converter == null) throw new ArgumentNullException("converter");
			if (!_isSuccess)
				return Result<TTarget>.CreateError(_error);

			return converter(_value);
		}

		/// <summary>
		/// Creates the error result.
		/// </summary>
		/// <param name="error">The error.</param>
		/// <returns>result encapsulating the error value</returns>
		/// <exception cref="ArgumentNullException">if error is null</exception>
		public new static Result<T> CreateError(string error)
		{
			if (null == error) throw new ArgumentNullException("error");

			return new Result<T>(false, default(T), error);
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
		/// Combines this <see cref="Result{T}"/> with the result returned
		/// by <paramref name="converter"/>.
		/// </summary>
		/// <typeparam name="TTarget">The type of the target.</typeparam>
		/// <param name="converter">The converter.</param>
		/// <returns>Combined result.</returns>
		public Result<TTarget> Combine<TTarget>(Func<T, Result<TTarget>> converter)
		{
			if (!_isSuccess)
				return Result<TTarget>.CreateError(_error);

			return converter(_value);
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
	}
}
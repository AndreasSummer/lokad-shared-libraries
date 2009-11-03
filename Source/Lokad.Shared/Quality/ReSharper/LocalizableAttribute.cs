using System;

namespace Lokad.Quality
{
	/// <summary>
	/// Indicates that marked elements is localizable or not.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, Inherited = true)]
	[NoCodeCoverage]
	public sealed class LocalizableAttribute : Attribute
	{
		private readonly bool _isLocalizable;

		/// <summary>
		/// Initializes a new instance of the <see cref="LocalizableAttribute"/> class.
		/// </summary>
		/// <param name="isLocalizable"><c>true</c> if a element should be localized; otherwise, <c>false</c>.</param>
		public LocalizableAttribute(bool isLocalizable)
		{
			_isLocalizable = isLocalizable;
		}

		/// <summary>
		/// Gets a value indicating whether a element should be localized.
		/// <value><c>true</c> if a element should be localized; otherwise, <c>false</c>.</value>
		/// </summary>
		public bool IsLocalizable
		{
			get { return _isLocalizable; }
		}

		/// <summary>
		/// Returns whether the value of the given object is equal to the current <see cref="LocalizableAttribute"/>.
		/// </summary>
		/// <param name="obj">The object to test the value equality of. </param>
		/// <returns>
		/// <c>true</c> if the value of the given object is equal to that of the current; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			var attribute = obj as LocalizableAttribute;
			return attribute != null && attribute.IsLocalizable == IsLocalizable;
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>A hash code for the current <see cref="LocalizableAttribute"/>.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
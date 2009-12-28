#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 


// Company: http://www.lokad.com


// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Quality
{
	/// <summary>
	/// 	Class design markers for the Lokad namespace
	/// </summary>
	public static class DesignOfClass
	{
		/// <summary>
		/// Converts the tag to string.
		/// </summary>
		/// <param name="tag">The tag.</param>
		/// <returns></returns>
		public static string ConvertTagToString(ClassDesignTag tag)
		{
			return "Lokad.Class." + tag;
		}

		/// <summary>
		/// 	Indicates that a class is an immutable model with fields
		/// </summary>
		[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
		public sealed class ImmutableFieldsModel : ClassDesignAttribute
		{
			/// <summary>
			/// 	Initializes a new instance of the
			/// 	<see cref="ImmutableFieldsModel" />
			/// 	class.
			/// </summary>
			public ImmutableFieldsModel()
				: base(ClassDesignTag.Model, ClassDesignTag.ImmutableWithFields)
			{
			}
		}

		/// <summary>
		/// 	Indicates that a class is an immutable model with properties
		/// </summary>
		[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
		public sealed class ImmutablePropertiesModel : ClassDesignAttribute
		{
			/// <summary>
			/// 	Initializes a new instance of the
			/// 	<see cref="ImmutablePropertiesModel" />
			/// 	class.
			/// </summary>
			public ImmutablePropertiesModel() : base(ClassDesignTag.Model, ClassDesignTag.ImmutableWithProperties)
			{
			}
		}
	}
}
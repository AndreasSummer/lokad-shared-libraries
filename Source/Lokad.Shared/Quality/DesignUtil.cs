#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Linq;

namespace Lokad.Quality
{
	/// <summary>
	/// 	Helper class for managing designs
	/// </summary>
	public static class DesignUtil
	{
		/// <summary>
		/// 	Converts the tag to string.
		/// </summary>
		/// <param name="tag">The tag.</param>
		/// <returns>string representation for the tag</returns>
		public static string ConvertTagToString(DesignTag tag)
		{
			return "Lokad.Class." + tag;
		}

		/// <summary>
		/// Converts multiple tags to strings.
		/// </summary>
		/// <param name="tags">The tags to convert.</param>
		/// <returns>array of string representations for the design tags</returns>
		public static string[] ConvertTagsToStrings(params DesignTag[] tags)
		{
			return tags.Convert(ConvertTagToString);
		}

		/// <summary>
		/// 	Gets the class design tags.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="inherit">
		/// 	if set to
		/// 	<c>true</c>
		/// 	[inherit].
		/// </param>
		/// <returns>
		/// 	array of class design tags
		/// </returns>
		/// <remarks>
		/// 	it is advised to cache the results, if performance is important
		/// </remarks>
		public static string[] GetClassDesignTags(Type type, bool inherit)
		{
			// we are using unbound search, since property implementations might change
			var returnType = typeof (string[]);
			var attributes = type
				.GetCustomAttributes(inherit)
				.Cast<Attribute>();

			return attributes
				.Select(a => new
					{
						Attribute = a,
						Property = a.GetType().GetProperty("ClassDesignTags", returnType)
					}).Where(x => null != x.Property)
				.SelectMany(x => (string[]) x.Property.GetValue(x.Attribute, null))
				.Distinct()
				.ToArray();
		}

		/// <summary>
		/// Simple type cache
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static class ClassCache<T>
		{
			/// <summary>
			/// Tags for the specified class
			/// </summary>
			public readonly static string[] Tags;
			/// <summary>
			/// If this class contains a model design tag
			/// </summary>
			public static readonly bool IsModel;

			static ClassCache()
			{
				Tags = GetClassDesignTags(typeof (T), false);
				IsModel = Tags.Contains(ConvertTagToString(DesignTag.Model));
			}
		}
	}


}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Linq;

namespace Lokad.Client
{
	/// <summary>
	/// Helper extensions for <see cref="IForecastModel"/>
	/// </summary>
	public static class IForecastModelExtensions
	{
		/// <summary>
		/// Sets the tags as string with <paramref name="separator"/>.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="tags">The tags.</param>
		/// <param name="separator">The separator.</param>
		public static void SetTagsAsString(this IForecastModel model, string tags, char separator)
		{
			Enforce.Argument(() => tags);

			model.Tags = tags.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
		}

		/// <summary>
		/// Gets the tags as string with <paramref name="separator"/>.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="separator">The separator.</param>
		/// <returns></returns>
		public static string GetTagsAsString(this IForecastModel model, char separator)
		{
			return model.Tags.Join(new string(separator, 1));
		}
	}
}
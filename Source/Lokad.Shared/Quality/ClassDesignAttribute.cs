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
	/// Base attribute for marking classes with some design constraints
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public class ClassDesignAttribute : Attribute
	{
		readonly string[] _classDesignTags;

		/// <summary>
		/// Gets the class design tags.
		/// </summary>
		/// <value>The class design tags.</value>
		[UsedImplicitly]
		public string[] ClassDesignTags
		{
			get { return _classDesignTags; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClassDesignAttribute"/> class.
		/// </summary>
		/// <param name="designTags">The design tags.</param>
		public ClassDesignAttribute(params string[] designTags)
		{
			_classDesignTags = designTags;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClassDesignAttribute"/> class.
		/// </summary>
		/// <param name="classDesignTag">The class design tag.</param>
		public ClassDesignAttribute(string classDesignTag)
		{
			Enforce.ArgumentNotEmpty(() => classDesignTag);

			_classDesignTags = new [] { classDesignTag };
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="ClassDesignAttribute"/> class.
		/// </summary>
		/// <param name="designTag">The design tag.</param>
		public ClassDesignAttribute(DesignTag designTag)
		{
			_classDesignTags = new[] {DesignUtil.ConvertTagToString(designTag)};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClassDesignAttribute"/> class.
		/// </summary>
		/// <param name="designTags">The design tags.</param>
		public ClassDesignAttribute(params DesignTag[] designTags)
		{
			_classDesignTags = designTags
				.ToArray(DesignUtil.ConvertTagToString);
		}
	}
}
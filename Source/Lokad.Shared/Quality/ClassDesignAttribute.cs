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
		public string[] ClassDesignTags
		{
			get { return _classDesignTags; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClassDesignAttribute"/> class.
		/// </summary>
		/// <param name="firstDesignTag">The first design tag.</param>
		/// <param name="otherDesignTags">The other design tags.</param>
		public ClassDesignAttribute(string firstDesignTag, params string[] otherDesignTags)
		{
			_classDesignTags = otherDesignTags.Prepend(firstDesignTag).ToArray();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClassDesignAttribute"/> class.
		/// </summary>
		/// <param name="classDesignTag">The class design tag.</param>
		public ClassDesignAttribute(string classDesignTag)
		{
			_classDesignTags = new string[] { classDesignTag};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClassDesignAttribute"/> class.
		/// </summary>
		/// <param name="firstDesignTag">The first design tag.</param>
		/// <param name="otherDesignTags">The other design tags.</param>
		public ClassDesignAttribute(ClassDesignTag firstDesignTag, params ClassDesignTag[] otherDesignTags)
		{
			_classDesignTags = otherDesignTags
				.Prepend(firstDesignTag)
				.ToArray(p => DesignOfClass.ConvertTagToString(p));
		}
	}
}
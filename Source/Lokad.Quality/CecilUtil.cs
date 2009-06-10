#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Quality
{
	static class CecilUtil<T>
	{
		internal static readonly string MonoName = CecilUtil.GetMonoName(typeof(T));
	}

	static class CecilUtil
	{
		internal static string GetMonoName(Type type)
		{
			return (type.FullName).Replace('+', '/');
		}
	}
}
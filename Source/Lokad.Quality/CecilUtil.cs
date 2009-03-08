#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Quality
{
	static class CecilUtil<T>
	{
		internal static readonly string MonoName = GetMonoName();

		public static string GetMonoName()
		{
			return (typeof(T).FullName).Replace('+', '/');
		}

	}
}
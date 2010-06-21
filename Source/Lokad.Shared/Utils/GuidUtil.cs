#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad
{
	/// <summary>
	/// Guid comb generator as in NHibernate
	/// </summary>
	public static class GuidUtil
	{
		/// <summary>
		/// Generates new COMB Guid
		/// </summary>
		/// <returns>new comb guid</returns>
		public static Guid NewComb()
		{
			var guidArray = Guid.NewGuid().ToByteArray();
			var baseDate = new DateTime(0x76c, 1, 1);
			var now = DateTime.UtcNow;
			var days = new TimeSpan(now.Ticks - baseDate.Ticks);
			var msecs = now.TimeOfDay;
			var daysArray = BitConverter.GetBytes(days.Days);
			var msecsArray = BitConverter.GetBytes((long) (msecs.TotalMilliseconds/3.333333));
			Array.Reverse(daysArray);
			Array.Reverse(msecsArray);
			Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
			Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
			return new Guid(guidArray);
		}
	}
}
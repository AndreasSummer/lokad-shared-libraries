#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Rules
{
	sealed class Program
	{
		public string Name { get; set; }
		public bool Active { get; set; }


		public static Program CreateValid()
		{
			return new Program
				{
					Active = true,
					Name = Guid.NewGuid().ToString().Replace('-', ' ')
				};
		}
	}
}
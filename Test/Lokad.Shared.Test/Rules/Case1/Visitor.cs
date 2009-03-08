#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace System.Rules
{
	sealed class Visitor
	{
		public string Name { get; set; }
		public Program[] Programs { get; set; }

		public static Visitor CreateValid()
		{
			return new Visitor
				{
					Name = Guid.NewGuid().ToString().Replace('-', ' '),
					Programs = new[] {Program.CreateValid(), Program.CreateValid(), Program.CreateValid()}
				};
		}
	}
}
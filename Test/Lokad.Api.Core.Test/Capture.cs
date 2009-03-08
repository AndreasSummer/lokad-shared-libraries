#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using Rhino.Mocks.Constraints;

namespace Lokad.Api.Core
{
	sealed class Capture<T> : AbstractConstraint
	{
		readonly List<T> _items = new List<T>();

		public T[] Items
		{
			get { return _items.ToArray(); }
		}

		public override string Message
		{
			get { return "Capture"; }
		}

		public override bool Eval(object obj)
		{
			_items.Add((T) obj);
			return true;
		}
	}
}
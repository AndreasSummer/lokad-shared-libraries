#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Rules;

namespace Lokad.Testing
{
	/// <summary>
	/// Delegate that represents model equality tester
	/// </summary>
	delegate bool TestModelEqualityDelegate(IScope scope, Type type, object o1, object o2);
}
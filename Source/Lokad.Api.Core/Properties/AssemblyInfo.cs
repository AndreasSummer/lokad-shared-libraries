#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly : AssemblyTitle("Lokad.API.Core")]
[assembly : AssemblyDescription("Communication library for the Lokad.API")]
[assembly : AssemblyCopyright("Copyright (c) Lokad 2008")]
[assembly : AssemblyTrademark("This code is released under the terms of the new BSD licence")]
[assembly : CLSCompliant(true)]

[assembly: InternalsVisibleTo("Lokad.Api.Core.Test, PublicKey = " + GlobalAssemblyInfo.PublicKey)]
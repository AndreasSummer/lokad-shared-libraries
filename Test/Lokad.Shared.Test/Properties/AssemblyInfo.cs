#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Reflection;
using System.Runtime.InteropServices;
using Lokad.Properties;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly : AssemblyTitle("Lokad.Shared.Test")]
[assembly : AssemblyDescription("Test Assembly")]
[assembly : AssemblyConfiguration(AssemblyLocals.Configuration)]
[assembly : AssemblyCompany("Lokad")]
[assembly : AssemblyProduct("Lokad.Shared.Test")]
[assembly : AssemblyCopyright("Copyright © Lokad 2008")]
[assembly : AssemblyTrademark("")]
[assembly : AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly : ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly : Guid("f154d53c-9ee0-4834-a25e-e206ddc0389d")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

[assembly : AssemblyVersion("1.0.0.0")]
[assembly : AssemblyFileVersion("1.0.0.0")]

namespace Lokad.Properties
{
	internal static class AssemblyLocals
	{
		public const string Configuration = "TestConfig";
	}
}
#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Reflection;
using Lokad.Diagnostics.Persist;
using Lokad.Properties;
using NUnit.Framework;

namespace Lokad.Diagnostics
{
	[TestFixture]
	public sealed class SystemDescriptorTests
	{
		// ReSharper disable InconsistentNaming

		[Test]
		public void Test_default_descriptor()
		{
			SystemDescriptor.InitializeDefault();
			var descriptor = SystemDescriptor.Default;

			var assembly = Assembly.GetExecutingAssembly();
			var name = assembly.GetName();


			Assert.AreEqual(name.Name, descriptor.Name, "Name");
			Assert.AreEqual(name.Version, descriptor.Version, "Version");
			Assert.That(!string.IsNullOrEmpty(descriptor.Instance), "Instance");
			Assert.AreEqual(AssemblyLocals.Configuration, descriptor.Configuration);
		}

		[Test]
		public void Custom_descriptor()
		{
			var d = new SystemDescriptor(typeof (Enforce).Assembly);
			SystemDescriptor.InitializeDefault(d);
			Assert.AreSame(d, SystemDescriptor.Default);
		}
#if !SILVERLIGHT2

		[Test]
		public void Conversion()
		{
			var data = SystemDescriptor.Default.ToPersistence();
			XmlUtil.TestXmlSerialization(data);
		}
#endif
	}
}
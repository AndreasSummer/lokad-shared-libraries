#region Copyright (c) 2009-2010 LOKAD SAS. All rights reserved.

// Copyright (c) 2009-2010 LOKAD SAS. All rights reserved.
// You must not remove this notice, or any other, from this software.
// This document is the property of LOKAD SAS and must not be disclosed.

#endregion

using NUnit.Framework;

namespace Lokad.Cqrs.Test
{
	[TestFixture]
	public sealed class SyntaxTests
	{
		// ReSharper disable InconsistentNaming


		public class Domain
		{
			public Domain()
			{
			}

			public Domain(string id)
			{
			}

			public string Name { get; private set; }

			public void Rename(string newName)
			{
				Name = newName;
			}

			public static Domain Factory(string id)
			{
				return new Domain(id);
			}

			public void AddTask(string id, string name)
			{
			}
		}

		[Test, Ignore]
		public void Test()
		{
			IEntityWriter writer = null;


			writer.Add<Domain>("a1", x => x.Rename("New name"));

			var domain = new Domain("some");
			domain.AddTask("t1", "T2");

			writer.Add("a1", domain);


			writer.Update<Domain>("a1", x => x.Rename("New name"));

			// add or update
			writer.UpdateOrAdd<Domain>("a1", x => x.Rename("New name"));
			writer.UpdateOrAdd(() => new Domain(), "a1", x => x.Rename("New name"));
			writer.UpdateOrAdd(s => new Domain(), "a1", x => x.Rename("New name"));
			writer.UpdateOrAdd(Domain.Factory, "a1", x => x.Rename("New name"));


			writer.Remove<Domain>("a1");
		}
	}
}
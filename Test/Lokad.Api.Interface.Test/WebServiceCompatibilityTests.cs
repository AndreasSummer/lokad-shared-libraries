using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Lokad.Quality;
using NUnit.Framework;

namespace Lokad.Api
{
	[TestFixture]
	public sealed class WebServiceCompatibilityTests
	{
		internal static readonly Codebase Codebase = new Codebase("Lokad.Api.Interface.dll");

		[Test]
		public void All_Objects_Are_Serializable()
		{
			var types = typeof(ILokadApi).Assembly
				.GetExportedTypes()
				.Where(t => !t.IsAbstract);

			foreach (Type type in types)
			{
				XmlUtil.TestXmlSerialization(type);
				//using (var stream = new MemoryStream())
				//{
				//    new BinaryFormatter().Serialize(stream, Activator.CreateInstance(type));
				//}
				
			}
		}

		[Test]
		public void No_Methods_With_Same_Name()
		{
			var api = Codebase.Find<ILokadApi>();

			var methods = api
				.GetAllMethods(Codebase)
				.ToLookup(m => m.Name)
				.Where(m => m.Count() > 1)
				.Select(m => string.Format("{0} is declared at {1}", m.Key, m.Select(md => md.DeclaringType.Name).Join(",")));

			CollectionAssert.IsEmpty(methods.ToArray());
		}
	}

}
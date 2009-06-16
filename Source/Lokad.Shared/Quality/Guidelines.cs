using System;
#pragma warning disable 1591
namespace Lokad.Quality
{
	/// <summary>
	/// Attribute markers for Lokad design guidelines
	/// </summary>
	[UsedImplicitly]
	public class Guidelines
	{
		[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
		[NoCodeCoverage, UsedImplicitly]
		public sealed class ModelAttribute : Attribute { }
		[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
		[NoCodeCoverage, UsedImplicitly]
		public sealed class RuleSetAttribute : Attribute { }
		[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
		[NoCodeCoverage, UsedImplicitly]
		public sealed class ControllerAttribute : Attribute { }
		[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
		[NoCodeCoverage, UsedImplicitly]
		public sealed class XmlDataAttribute : Attribute { }
		[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
		[NoCodeCoverage, UsedImplicitly]
		public sealed class ViewAttribute : Attribute { }
	}
}
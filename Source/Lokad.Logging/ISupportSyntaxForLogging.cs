namespace Lokad
{
	/// <summary>
	/// Syntax extensions for Logging configurations
	/// </summary>
	public interface ISupportSyntaxForLogging
	{
		/// <summary>
		/// Registers the specified log provider instance as singleton.
		/// </summary>
		/// <param name="provider">The provider.</param>
		void RegisterLogProvider(ILogProvider provider);
	}
}
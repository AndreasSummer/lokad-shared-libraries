namespace Lokad.Cqrs
{
	/// <summary>
	/// Scalable state storage abstraction
	/// </summary>
	public interface IStateStore : IReadState, IWriteState
	{
		
		
	}
}
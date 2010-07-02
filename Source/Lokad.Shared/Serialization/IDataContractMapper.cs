using System;

namespace Lokad.Serialization
{
	/// <summary>
	/// Class responsible 
	/// </summary>
	public interface IDataContractMapper
	{
		/// <summary>
		/// Gets the contract name by the type
		/// </summary>
		/// <param name="messageType">Type of the message.</param>
		/// <returns></returns>
		Maybe<string> GetContractNameByType(Type messageType);
		/// <summary>
		/// Gets the type by contract name.
		/// </summary>
		/// <param name="contractName">Name of the contract.</param>
		/// <returns></returns>
		Maybe<Type> GetTypeByContractName(string contractName);
	}
}
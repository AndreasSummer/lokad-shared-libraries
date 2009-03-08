#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Lokad.Quality
{
	/// <summary>
	/// Extension methods for <see cref="Instruction"/>
	/// </summary>
	public static class InstructionExtensions
	{
		/// <summary>
		/// Checks if the instruction creates the specified type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instruction">The instr.</param>
		/// <returns></returns>
		public static bool Creates<T>(this Instruction instruction)
		{
			if (instruction.OpCode != OpCodes.Newobj)
				return false;

			var reference = (MemberReference) instruction.Operand;

			return reference.DeclaringType.FullName == CecilUtil<T>.MonoName;
		}
	}
}
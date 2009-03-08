using System;
using System.Rules;

namespace RulesUI
{
	public interface IEditorView<T> where T : class
	{
		Result<T> GetData(params Rule<T>[] rules);
		void BindData(T customer);
		void SetTitle(string text);
	}
}
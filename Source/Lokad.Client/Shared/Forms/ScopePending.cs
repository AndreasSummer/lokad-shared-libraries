using System;
using System.Linq.Expressions;
using System.Rules;

namespace Lokad.Client.Forms
{
	/// <summary>
	/// Simple helper class that allows to make rule paths strongly-typed
	/// instead of resorting to string literals
	/// </summary>
	internal static class ScopePending
	{
		public static string GetPath<TTarget, TProperty>(Expression<Func<TTarget, TProperty>> prop)
		{
			Enforce.Argument(() => prop);

			var lambda = prop as LambdaExpression;

			Enforce.That(lambda != null, "Must be a lambda expression");
			Enforce.That(lambda.Body.NodeType == ExpressionType.MemberAccess, "Must be member access");

			var memberExpr = lambda.Body as MemberExpression;

			if (null == memberExpr)
				throw new InvalidOperationException("memberExpr");

			string path = memberExpr.Member.Name;

			while (null != (memberExpr.Expression as MemberExpression))
			{
				memberExpr = (MemberExpression)memberExpr.Expression;
				path = Scope.ComposePath(memberExpr.Member.Name, path);
			}
			return path;
		}
		
	}
}
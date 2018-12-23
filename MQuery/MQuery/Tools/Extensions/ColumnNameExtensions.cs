using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MQuery.Tools.Extensions
{
    public static class ColumnNameExtensions
    {
        public static List<string> GetMemberName<T>(this Expression<Func<T, object>>[] expressions)
        {
            List<string> members = new List<string>();
            foreach (var item in expressions)
            {
                members.Add(item.GetMemberName());
            }

            return members;
        }

        public static string GetMemberName<T>(this Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(this Expression<Action<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        private static string GetMemberName(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("The expression cannot be null.");
            }

            if (expression is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression methodCallExpression)
            {
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression unaryExpression)
            {
                if (unaryExpression.Operand is MethodCallExpression methodCallExp)
                {
                    return methodCallExp.Method.Name;
                }

                return ((MemberExpression)unaryExpression.Operand).Member.Name;
            }

            throw new ArgumentException("Invalid expression");
        }
    }
}

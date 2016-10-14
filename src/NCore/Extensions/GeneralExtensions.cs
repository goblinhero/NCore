using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace NCore.Extensions
{
    public static class GeneralExtensions
    {
        public static bool Success(this object obj, out IEnumerable<string> errors)
        {
            errors = new string[0];
            return true;
        }

        public static bool Error(this Exception ex, out IEnumerable<string> errors)
        {
            return Error(ex, out errors, ex.Message, ex.StackTrace);
        }

        public static bool NotFound<T>(this T entity, object id, out IEnumerable<string> errors)
        {
            return Error(entity,out errors,$"{typeof(T).Name} not found (id:{id}).");
        }
        public static bool Error(this object obj, out IEnumerable<string> errors, params string[] error)
        {
            errors = error;
            return false;
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            MemberExpression operand;
            var lambdaExpression = (LambdaExpression)expression;
            var unaryExpression = lambdaExpression.Body as UnaryExpression;
            if (unaryExpression != null)
            {
                var body = unaryExpression;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }
            return operand.Member;
        }
    }
}
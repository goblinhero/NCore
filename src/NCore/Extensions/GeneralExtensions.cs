using System;
using System.Collections.Generic;

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
    }
}
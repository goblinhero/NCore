using System;
using System.Collections.Generic;
using System.Linq;

namespace NCore.Web.Extensions
{
    public static class FreeTextSearchExtensions
    {
        public static IEnumerable<string> SplitForFreeText(this string queryString)
        {
            if (string.IsNullOrWhiteSpace(queryString))
                return new string[0];
            return queryString.Trim()
                .Replace("+", "")
                .Replace("-", "")
                .Replace("&", "")
                .Replace("|", "")
                .Replace("!", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("{", "")
                .Replace("}", "")
                .Replace("\\", "")
                .Replace("/", "")
                .Replace("^", "")
                .Replace("*", "")
                .Replace("?", "")
                .Replace("\"", "")
                .Replace(":", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("~", "").Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Where(s => s != "-").ToList();
        }
    }
}
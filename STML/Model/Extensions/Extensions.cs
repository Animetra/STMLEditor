using System.Collections.Generic;

namespace STML.Model
{
    public static class Extensions
    {
        internal static int[]? FindAll(this string text, string toFind)
        {
            int result = 0;
            int index = 0;
            List<int> results = new List<int>();

            while(result != -1)
            {
                result = text.IndexOf(toFind, index);
                if (result != -1)
                {
                    results.Add(result);
                }

                index = result + toFind.Length;
            }

            return results.Count <= 0 ? null : results.ToArray();
        }

        internal static string? GetAttribute(this string text, int startIndex)
        {
            if (startIndex >= 0 && startIndex < text.Length)
            {
                int valueStart = text.IndexOf("\"", startIndex) + 1;
                int valueEnd = text.IndexOf("\"", valueStart);
                return valueStart != 0 && valueEnd != 0 && valueEnd > valueStart? text.Substring(valueStart, valueEnd - valueStart) : null;
            }
            else
            {
                return null;
            }
        }

        public static string NestInTags(this string text, string tag)
        {
            return $"<{tag}>{text}</{tag}>";
        }

        public static string NestInTags(this string text, string tag, string value)
        {
            return $"<{tag}=\"{value}\">{text}</{tag}>";
        }

        public static string NestInSTMLTags(this string text, string tag, string value)
        {
            if (text != "")
            {
                return $"<{tag} value=\"{value}\">{text}</{tag}>";
            }
            else
            {
                return $"<{tag} value=\"{value}\"/>";
            }
        }

        internal static string ReplaceAt(this string text, int startIndex, int count, string replaceWith)
        {
            return text
                    .Remove(startIndex, count)
                    .Insert(startIndex, replaceWith);
        }
    }
}

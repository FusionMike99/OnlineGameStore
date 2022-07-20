using System.Text.RegularExpressions;

namespace OnlineGameStore.BLL.Utils
{
    public static class StringExtensions
    {
        public static string ToKebabCase(this string value)
        {
            value = Regex.Replace(value, @"[^0-9a-zA-Z']", "-");

            value = Regex.Replace(value, @"[-]{2,}", "-");

            value = Regex.Replace(value, @"-+$", string.Empty);

            if (value.StartsWith("-")) value = value[1..];

            return value.ToLower();
        }
    }
}
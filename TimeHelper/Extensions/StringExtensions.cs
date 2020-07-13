using System.Globalization;
using System.Linq;
using TimeHelper.Extensions;

namespace TimeHelper.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveFormatCharacters(this string text) {
            return new string(text.Where(x => char.GetUnicodeCategory(x) != UnicodeCategory.Format).ToArray());
        }
    }
}

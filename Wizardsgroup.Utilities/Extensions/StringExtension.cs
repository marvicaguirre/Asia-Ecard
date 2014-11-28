using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;

namespace Wizardsgroup.Utilities.Extensions
{
    public static class StringExtension
    {
        public static string SplitCamelCase(this string toString)
        {
            if (toString != null)
                return Regex.Replace(toString, "(\\B[A-Z])", " $1");
            //return Regex.Replace(toString, "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", " $1");            
            return string.Empty;
        }

        public static string StripOffPunctuationsAndDeCapitalize(this string filterText)
        {
            if (filterText == null) return null;
            var nakedText = filterText.ToLower();
            char[] invalidCharacters = " ,.-*'|!?@#$^*+".ToCharArray();
            for (int i = 0; i < invalidCharacters.Length; i++)
            {
                if (filterText.Contains(invalidCharacters[i].ToString()))
                    nakedText = nakedText.Replace(invalidCharacters[i].ToString(), string.Empty);
            }
            return nakedText;

            //var result = invalidCharacters.AsParallel().Where(t => filterText.Contains(t.ToString()));
            //return result.AsParallel().AsOrdered().Aggregate(nakedText, (current, t) => current.Replace(t.ToString(), string.Empty));
        }
    }
}

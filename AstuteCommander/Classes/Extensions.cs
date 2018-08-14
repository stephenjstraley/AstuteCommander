using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstuteCommander
{
    public static class Extensions
    {
        public static string RemoveBetween(this string content, string chars)
        {
            string left = chars.Substring(0, 1);
            string right = chars.Substring(1, 1);

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex($"\\{left}.*?\\{right}");
            return regex.Replace(content, string.Empty);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YueShanp
{
    public static class StringExtension
    {
        public static int ToInt(this string s, int defaultInt = 0)
        {
            int r;
            int.TryParse(s, out r);

            return r == 0 ? defaultInt : r;
        }

        public static bool EqualsIgnoreCase(this string source, string target)
        {
            return string.Equals(source, target, StringComparison.OrdinalIgnoreCase);
        }
    }
}
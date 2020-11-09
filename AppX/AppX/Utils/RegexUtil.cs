using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AppX.Utils
{
    public static class RegexUtill
    {
        public static Regex MinLength(int length)
        {
            return new Regex(@"(\s*(\S)\s*){" + length + @",}");
        }

        public static Regex PhoneNumber()
        {
            return new Regex(@"^[0-9]\d{8}");
        }
    }
}

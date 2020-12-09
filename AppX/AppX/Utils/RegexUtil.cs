using Android.Graphics;
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
        public static Regex Email()
        {
            return new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
        public static Regex MinLengthNumbers(int length)
        {
            return new Regex(@"[0-9]{" + length + @",}");
        }

        public static (Xamarin.Forms.Color,bool) Check(Regex rule, string value)
        {
            Xamarin.Forms.Color color;
            bool correct;

            if (rule.IsMatch(value))        //If validation is correct
            {
                color = Xamarin.Forms.Color.Black;
                correct = true;
            }
            else
            {
                color = Xamarin.Forms.Color.Red;
                correct = false;
            }

            return (color, correct);
        }
    }
}

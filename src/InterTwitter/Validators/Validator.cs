using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace InterTwitter.Validators
{
    public static class Validator
    {
        public const string RegexEmail = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public const string RegexPasswordContainsNumber = @"[0-9]+";
        public const string RegexPasswordContainsUpper = @"[A-Z]+";
        public const string RegexPasswordContainsLower = @"[a-z]+";
        public const string RegexPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,30}$";

        public static bool IsMatch(string value, string regex)
        {
            bool isMatch = !string.IsNullOrEmpty(regex) && !string.IsNullOrEmpty(value) && Regex.IsMatch(value, regex);

            return isMatch;
        }
    }
}

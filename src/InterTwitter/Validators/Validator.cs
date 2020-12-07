using System.Text.RegularExpressions;

namespace InterTwitter.Validators
{
    public static class Validator
    {
        public const string RegexName = @"(?=^\S)(?=.+)";
        public const string RegexEmail = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public const string RegexPassword = @"(?=.+)(?=^\S*$).{8,}$";

        public static bool IsMatch(string value, string regex, RegexOptions regexOptions = RegexOptions.None)
        {
            return !string.IsNullOrEmpty(regex)
                && !string.IsNullOrEmpty(value)
                && Regex.IsMatch(value, regex, regexOptions);
        }
    }
}

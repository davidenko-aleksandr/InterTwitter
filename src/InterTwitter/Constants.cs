using System;
namespace InterTwitter
{
    public class Constants
    {
#if RELEASE
        public const string BASE_URL = "";
#elif STAGE
        public const string BASE_URL = "";
#elif DEV
        public const string BASE_URL = "";
#else
        public const string BASE_URL = "";
#endif
        public const string OpenMenuMessage = "OpenMenu";

        public static class Navigation
        {
            public const string Name = "Name";
            public const string Email = "Email";
        }
    }
}

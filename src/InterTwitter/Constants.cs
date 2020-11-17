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
    }
}

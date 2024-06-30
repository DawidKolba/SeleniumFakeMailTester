

namespace SeleniumFakeMailTester.Configuration
{
    public static class UrlAddresses
    {
        private static readonly string BaseUrl = "https://niepodam.pl/users/";

        public static string GetUserMailboxUrl(string userName)
        {
            return $"{BaseUrl}{userName}";
        }
    }
}

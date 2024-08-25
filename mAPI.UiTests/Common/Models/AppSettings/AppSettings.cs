#nullable disable

namespace mAPI.UiTests.Common.Models.AppSettings
{
    public class AppSettings
    {
        public static AppSettings Instance { get; private set; }

        public BrowserSettings BrowserSettings { get; set; }
        public DatabaseSettings DatabaseSettings { get; set; }
        public UserCredentials UserCredentials { get; set; }

        public static void Configure(AppSettings appSettings) => Instance = appSettings;
    }
}
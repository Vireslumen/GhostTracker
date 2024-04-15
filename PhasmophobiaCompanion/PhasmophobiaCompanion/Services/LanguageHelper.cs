using Xamarin.Essentials;

namespace PhasmophobiaCompanion.Services
{
    public static class LanguageHelper
    {
        private const string UserLanguageKey = "UserLanguage";

        public static string GetUserLanguage()
        {
            return Preferences.Get(UserLanguageKey, null);
        }

        public static void SaveUserLanguage(string languageCode)
        {
            Preferences.Set(UserLanguageKey, languageCode);
        }
    }
}
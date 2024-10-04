using Xamarin.Essentials;

namespace GhostTracker.Services
{
    /// <summary>
    ///     Статический класс <c>LanguageHelper</c> предоставляет методы для получения и сохранения кода языка пользователя
    ///     с использованием механизма сохранения предпочтений в приложении.
    /// </summary>
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
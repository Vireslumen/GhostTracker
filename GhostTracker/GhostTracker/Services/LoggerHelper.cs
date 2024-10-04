using Xamarin.Essentials;

namespace GhostTracker.Services
{
    /// <summary>
    ///     Статический класс <c>LoggerHelper</c> предоставляет методы для управления состоянием серверного логирования
    ///     с использованием сохранения предпочтений приложения.
    /// </summary>
    public static class LoggerHelper
    {
        private const string ServerLoggerActive = "ServerLogger";

        public static bool GetServerLogActive()
        {
            return Preferences.Get(ServerLoggerActive, false);
        }

        public static void SaveServerLogActive(bool serverActive)
        {
            Preferences.Set(ServerLoggerActive, serverActive);
        }
    }
}
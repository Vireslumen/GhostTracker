using Xamarin.Essentials;

namespace GhostTracker.Services
{
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
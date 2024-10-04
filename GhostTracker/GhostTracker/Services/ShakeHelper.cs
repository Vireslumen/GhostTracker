using Xamarin.Essentials;

namespace GhostTracker.Services
{
    /// <summary>
    ///     Статический класс <c>ShakeHelper</c> предоставляет методы для управления настройками активности обнаружения
    ///     встряхивания устройства.
    /// </summary>
    public static class ShakeHelper
    {
        private const string ShakeActive = "ShakeActive";

        public static bool GetShakeActive()
        {
            return Preferences.Get(ShakeActive, true);
        }

        public static void SaveShakeActive(bool shakeActive)
        {
            Preferences.Set(ShakeActive, shakeActive);
        }
    }
}
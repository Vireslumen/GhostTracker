using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace PhasmophobiaCompanion.Services
{
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

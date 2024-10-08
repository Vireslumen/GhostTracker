﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using GhostTracker.Droid;
using GhostTracker.Interfaces;
using Serilog;
using Xamarin.Forms;
using Exception = Java.Lang.Exception;

[assembly: Dependency(typeof(HapticFeedbackAndroid))]

namespace GhostTracker.Droid
{
    /// <summary>
    ///     Реализация тактильной обратной связи для Android, использующая функции Haptic Feedback устройства.
    /// </summary>
    public class HapticFeedbackAndroid : IHapticFeedback
    {
        /// <summary>
        ///     Инициирует тактильную обратную связь через Android API.
        ///     Применяет короткую вибрацию, имитируя эффект нажатия клавиш на клавиатуре.
        /// </summary>
        [Obsolete("Obsolete")]
        public void ExecuteHapticFeedback()
        {
            try
            {
                if (Forms.Context is Activity activity)
                    activity.RunOnUiThread(() =>
                    {
                        var vibrator = (Vibrator) activity.GetSystemService(Context.VibratorService);
                        if (Build.VERSION.SdkInt < BuildVersionCodes.O) return;
                        var effect = VibrationEffect.CreateOneShot(15, VibrationEffect.DefaultAmplitude);
                        vibrator?.Vibrate(effect);
                    });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время выполнения тактильной обратной связи.");
            }
        }
    }
}
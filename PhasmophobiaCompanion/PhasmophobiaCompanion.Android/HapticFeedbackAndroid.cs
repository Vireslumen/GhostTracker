using Android.App;
using Android.Content;
using Android.OS;
using PhasmophobiaCompanion.Droid;
using PhasmophobiaCompanion.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(HapticFeedbackAndroid))]

namespace PhasmophobiaCompanion.Droid
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
        public void ExecuteHapticFeedback()
        {
            if (Forms.Context is Activity activity)
                activity.RunOnUiThread(() =>
                {
                    var vibrator = (Vibrator) activity.GetSystemService(Context.VibratorService);
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    {
                        var effect = VibrationEffect.CreateOneShot(15, VibrationEffect.DefaultAmplitude);
                        vibrator.Vibrate(effect);
                    }
                });
        }
    }
}
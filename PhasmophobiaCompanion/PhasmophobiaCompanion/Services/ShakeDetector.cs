using System;
using Serilog;
using Xamarin.Essentials;

namespace PhasmophobiaCompanion.Services
{
    /// <summary>
    ///     Сервис для определения встряхивания девайса.
    /// </summary>
    public class ShakeDetectorService
    {
        public ShakeDetectorService()
        {
            try
            {
                Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
                Accelerometer.Start(SensorSpeed.UI);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при инициализации ShakeDetectorService.");
            }
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            ShakeDetected?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Событие вызываемое при обнаружении тряски
        /// </summary>
        public event EventHandler ShakeDetected;

        /// <summary>
        ///     Остановка акселерометра и отписка от события
        /// </summary>
        public void Stop()
        {
            try
            {
                Accelerometer.Stop();
                Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при остановке ShakeDetectorService.");
            }
        }
    }
}
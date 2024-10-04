using System;
using Xamarin.Forms;

namespace GhostTracker.Views
{
    /// <summary>
    ///     Представляет изображение, которое можно масштабировать и перемещать.
    /// </summary>
    public class InteractiveImageView : Image
    {
        /// <summary>
        ///     Событие, вызываемое при запросе закрытия изображения.
        /// </summary>
        public event Action CloseRequested;

        /// <summary>
        ///     Вызывается, когда изображение должно быть закрыто.
        ///     Этот метод предназначен для внутреннего использования рендерером.
        /// </summary>
        public void OnCloseRequested()
        {
            CloseRequested?.Invoke();
        }

        /// <summary>
        ///     Вызывается при изменении прозрачности изображения.
        ///     Этот метод предназначен для внутреннего использования рендерером.
        /// </summary>
        /// <param name="opacity">Новое значение прозрачности.</param>
        public void OnTransparencyChanged(float opacity)
        {
            TransparencyChanged?.Invoke(opacity);
        }

        /// <summary>
        ///     Событие, вызываемое при изменении прозрачности изображения.
        /// </summary>
        /// <param name="opacity">Новое значение прозрачности.</param>
        public event Action<float> TransparencyChanged;
    }
}
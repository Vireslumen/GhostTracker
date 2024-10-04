using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using GhostTracker.Droid;
using GhostTracker.Views;
using Serilog;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(InteractiveImageView), typeof(InteractiveImageViewRenderer))]

namespace GhostTracker.Droid
{
    /// <summary>
    ///     Рендер для создания элемента управления InteractiveImageView на Android.
    /// </summary>
    public class InteractiveImageViewRenderer : ImageRenderer
    {
        private readonly float maxScale = 4f;
        private readonly float minScale = 1f;
        private readonly GestureDetector gestureDetector;
        private readonly Matrix matrix = new Matrix();
        private readonly ScaleGestureDetector scaleDetector;
        private float accumulatedTranslationX;
        private float accumulatedTranslationY;
        private float currentScale = 1f;
        private float opacity = 1f;
        private float panTranslationX;
        private float panTranslationY;

        public InteractiveImageViewRenderer(Context context) : base(context)
        {
            scaleDetector = new ScaleGestureDetector(context, new ScaleListener(this));
            gestureDetector = new GestureDetector(context, new GestureListener(this));
        }

        /// <summary>
        ///     Обрабатывает события касания экрана, включая масштабирование и скроллинг.
        /// </summary>
        /// <param name="sender">Отправитель события.</param>
        /// <param name="e">Аргументы события касания.</param>
        private void HandleTouch(object sender, TouchEventArgs e)
        {
            try
            {
                gestureDetector.OnTouchEvent(e.Event);
                scaleDetector.OnTouchEvent(e.Event);

                if ((e.Event.Action & MotionEventActions.Mask) == MotionEventActions.Up)
                {
                    // Вызов события при изменении прозрачности или закрытии
                    if (currentScale == minScale && opacity <= 0)
                    {
                        (Element as InteractiveImageView)?.OnCloseRequested();
                    }
                    else if (currentScale == minScale)
                    {
                        // Сброс параметров к начальным состояниям
                        opacity = 1f;
                        (Element as InteractiveImageView)?.OnTransparencyChanged(opacity);
                        accumulatedTranslationX = 0f;
                        accumulatedTranslationY = 0f;
                        panTranslationY = 0f;
                        panTranslationX = 0f;
                    }
                }

                Invalidate();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при обработке события касания экрана в методе HandleTouch");
            }
        }

        /// <summary>
        ///     Отрисовывает изображение с учетом текущего масштаба и позиционирования.
        /// </summary>
        /// <param name="canvas">Холст для рисования.</param>
        protected override void OnDraw(Canvas canvas)
        {
            try
            {
                // Применение трансформаций для масштабирования и панорамирования
                var centerX = Width / 2f;
                var centerY = Height / 2f;

                matrix.Reset();
                matrix.PostTranslate(panTranslationX + accumulatedTranslationX,
                    panTranslationY + accumulatedTranslationY);
                matrix.PostScale(currentScale, currentScale, centerX, centerY);

                canvas.Matrix = matrix;

                base.OnDraw(canvas);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при отрисовке изображения в методе OnDraw");
            }
        }

        /// <summary>
        ///     Обрабатывает изменение связанного элемента, устанавливая обработчики событий касания.
        /// </summary>
        /// <param name="e">Аргументы изменения элемента.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            try
            {
                base.OnElementChanged(e);
                if (e.OldElement == null)
                {
                    // Отслеживание касаний на изображении
                    SetWillNotDraw(false);
                    Touch += HandleTouch;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка в методе OnElementChanged");
            }
        }

        /// <summary>
        ///     Обновляет прозрачность страницы в зависимости от его положения.
        /// </summary>
        public void UpdateTransparency()
        {
            try
            {
                var absTranslationY = Math.Abs(panTranslationY);
                var threshold = Height * 0.05f;
                if (absTranslationY <= threshold)
                {
                    opacity = 1.0f;
                }
                else
                {
                    opacity = 1.0f - absTranslationY / Height * 3;
                    opacity = Math.Max(0, Math.Min(1, opacity));
                }

                (Element as InteractiveImageView)?.OnTransparencyChanged(opacity);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при обновлении прозрачности страницы в методе UpdateTransparency");
            }
        }

        /// <summary>
        ///     Слушатель для обработки жестов масштабирования.
        /// </summary>
        private class ScaleListener : ScaleGestureDetector.SimpleOnScaleGestureListener
        {
            private readonly InteractiveImageViewRenderer renderer;

            public ScaleListener(InteractiveImageViewRenderer renderer)
            {
                this.renderer = renderer;
            }

            /// <summary>
            ///     Вызывается при обнаружении жеста масштабирования.
            /// </summary>
            /// <param name="detector">Детектор жестов масштабирования.</param>
            /// <returns>true, если событие обработано; иначе false.</returns>
            public override bool OnScale(ScaleGestureDetector detector)
            {
                try
                {
                    if (renderer.currentScale == 1 && renderer.opacity < 1f) return true;
                    var scaleFactor = detector.ScaleFactor;
                    var newScale = renderer.currentScale * scaleFactor;

                    if (newScale > renderer.maxScale)
                        scaleFactor = renderer.maxScale / renderer.currentScale;
                    else if (newScale < renderer.minScale) scaleFactor = renderer.minScale / renderer.currentScale;

                    renderer.matrix.PostScale(scaleFactor, scaleFactor, detector.FocusX, detector.FocusY);
                    renderer.currentScale *= scaleFactor;

                    renderer.accumulatedTranslationX += (detector.FocusX - renderer.Width / 2f) * (1 - scaleFactor);
                    renderer.accumulatedTranslationY += (detector.FocusY - renderer.Height / 2f) * (1 - scaleFactor);

                    return true;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Ошибка при обработке жеста масштабирования в методе OnScale");
                    return false;
                }
            }
        }

        /// <summary>
        ///     Слушатель для обработки простых жестов, таких как скроллинг.
        /// </summary>
        private class GestureListener : GestureDetector.SimpleOnGestureListener
        {
            private readonly InteractiveImageViewRenderer renderer;

            public GestureListener(InteractiveImageViewRenderer renderer)
            {
                this.renderer = renderer;
            }

            /// <summary>
            ///     Вызывается при скроллинге.
            /// </summary>
            /// <param name="e1">Первое событие касания.</param>
            /// <param name="e2">Второе событие касания.</param>
            /// <param name="distanceX">Расстояние по оси X между событиями касания.</param>
            /// <param name="distanceY">Расстояние по оси Y между событиями касания.</param>
            /// <returns>true, если событие обработано; иначе false.</returns>
            public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
            {
                try
                {
                    if (renderer.currentScale > 1f)
                    {
                        renderer.panTranslationX -= distanceX / 2;
                        renderer.panTranslationY -= distanceY / 2;
                    }
                    else
                    {
                        renderer.panTranslationY -= distanceY;
                        renderer.UpdateTransparency();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Ошибка при обработке скроллинга изображения в методе OnScale");
                    return false;
                }
            }
        }
    }
}
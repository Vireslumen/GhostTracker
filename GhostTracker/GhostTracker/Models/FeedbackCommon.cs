namespace GhostTracker.Models
{
    /// <summary>
    ///     Класс FeedbackCommon содержит атрибуты для локализации текстовых элементов интерфейса, относящихся к странице
    ///     фидбэка.
    /// </summary>
    public class FeedbackCommon
    {
        /// <summary>
        ///     Текст кнопки отмены отправки фидбэка. Примеры: "Cancel", "Отмена".
        /// </summary>
        public string Cancel { get; set; }
        /// <summary>
        ///     Текст плэйсхолдера для поля ввода фидбэка.
        /// </summary>
        public string EditorTip { get; set; }
        /// <summary>
        ///     Текст кнопки отправки фидбэка. Примеры: "Submit", "Отправить".
        /// </summary>
        public string Submit { get; set; }
        /// <summary>
        ///     Название страницы отправки фидбэка.
        /// </summary>
        public string Title { get; set; }
    }
}
using GhostTracker.Interfaces;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Класс QuestCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к квестам - Quest.
    /// </summary>
    public class QuestCommon : ITitledItem
    {
        /// <summary>
        ///     Название поля ежедневных квестов. Примеры: "Daily Tasks", "Ежедневные задания".
        /// </summary>
        public string Daily { get; set; }
        /// <summary>
        ///     Текст описания квестов в целом.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///     Название самих квестов. Примеры: "Tasks", "Задания".
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///     Название поля еженедельных квестов. Примеры: "Weekly Tasks", "Еженедельные задания".
        /// </summary>
        public string Weekly { get; set; }
    }
}
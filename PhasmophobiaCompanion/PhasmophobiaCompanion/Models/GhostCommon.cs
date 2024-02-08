namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Класс GhostCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к призракам - Ghost.
    /// </summary>
    public class GhostCommon
    {
        /// <summary>
        ///     Текст кнопки применения выбранных фильтров или настроек. Примеры: "Apply", "Принять".
        /// </summary>
        public string ApplyTitle { get; set; }
        /// <summary>
        ///     Текст для фильтра по выбранным уликам Clue. Примеры: "Filter by Clue", "Фильтр по Уликам."
        /// </summary>
        public string FilterTitle { get; set; }
        /// <summary>
        ///     Название списка призраков в интерфейсе. Примеры: "Ghosts", "Призраки".
        /// </summary>
        public string GhostsTitle { get; set; }
        /// <summary>
        ///     Название понятия призрака в интерфейсе. Примеры: "Ghost", "Призрак".
        /// </summary>
        public string GhostTitle { get; set; }
        /// <summary>
        ///     Текст заголовка в таблице скоростей. Примеры: "LoS", "ЛН".
        /// </summary>
        public string LoS { get; set; }
        /// <summary>
        ///     Текст заголовка в таблице скоростей и охот. Примеры: "Max.", "Макс."
        /// </summary>
        public string Max { get; set; }
        /// <summary>
        ///     Текст для всплывающей подсказки о том, что такое Max. в контексте таблицы, описывающей значения рассудка для начала
        ///     охоты для каждого призрака.
        /// </summary>
        public string MaxSanityHunt { get; set; }
        /// <summary>
        ///     Текст для всплывающей подсказки о том, что такое Max. в контексте таблицы скоростей призрака.
        /// </summary>
        public string MaxSpeed { get; set; }
        /// <summary>
        ///     Текст для всплывающей подсказки о том, что такое LoS. в контексте таблицы скоростей призрака.
        /// </summary>
        public string MaxSpeedLoS { get; set; }
        /// <summary>
        ///     Текст заголовка в таблице скоростей и охот. Примеры: "Min.", "Мин."
        /// </summary>
        public string Min { get; set; }
        /// <summary>
        ///     Текст для всплывающей подсказки о том, что такое Min. в контексте таблицы, описывающей значения рассудка для начала
        ///     охоты для каждого призрака.
        /// </summary>
        public string MinSanityHunt { get; set; }
        /// <summary>
        ///     Текст для всплывающей подсказки о том, что такое Min. в контексте таблицы скоростей призрака.
        /// </summary>
        public string MinSpeed { get; set; }
        /// <summary>
        ///     Текст названия таблицы, описывающей значения рассудка для начала охоты для каждого призрака. Примеры: "Sanity
        ///     Hunt", "Рассудок для начала охоты".
        /// </summary>
        public string SanityHunt { get; set; }
        /// <summary>
        ///     Текст для поля поиска призраков. Примеры: "Search Ghosts", "Поиск призраков".
        /// </summary>
        public string Search { get; set; }
        /// <summary>
        ///     Текст названия таблицы, описывающей скорости призраков. Примеры: "Ghosts Speed", "Скорости Призраков".
        /// </summary>
        public string Speed { get; set; }
    }
}
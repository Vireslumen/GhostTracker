namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Класс MainPageCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к главной странице MainPage.
    /// </summary>
    public class MainPageCommon
    {
        /// <summary>
        ///     Название списка улик на главной странице. Примеры: "Clues", "Улики".
        /// </summary>
        public string Clue { get; set; }
        /// <summary>
        ///     Название списка ежедневных заданий на главной странице. Примеры: "Daily Quests", "Ежедневные задания".
        /// </summary>
        public string DailyQuest { get; set; }
        /// <summary>
        ///     Название списка некатегоризованных страниц на главной странице. Примеры: "Other Pages", "Другие страницы".
        /// </summary>
        public string OtherPages { get; set; }
        /// <summary>
        ///     Название списка последних патчей на главной странице. Примеры "Last Patches", "Последние Обновления".
        /// </summary>
        public string Patches { get; set; }
        /// <summary>
        ///     Текст для поля поиска всего на главной странице. Примеры "Search", "Поиск".
        /// </summary>
        public string Search { get; set; }
        /// <summary>
        ///     Название кнопки настроек на главной странице. Примеры "Settings", "Настройки".
        /// </summary>
        public string Settings { get; set; }
        /// <summary>
        ///     Название поля особого режима на главной странице. Примеры "Special Mode","Особый режим".
        /// </summary>
        public string SpecialMode { get; set; }
        /// <summary>
        ///     Название кнопки смены темы на главной странице. Примеры "Theme", "Тема".
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        ///     Название поля подсказки на главной странице. Примеры "Tip", "Подсказка".
        /// </summary>
        public string Tip { get; set; }
        /// <summary>
        ///     Название списка еженедельных заданий на главной странице. Примеры: "Weekly Quests", "Еженедельные задания".
        /// </summary>
        public string WeeklyQuest { get; set; }
    }
}
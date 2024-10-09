namespace GhostTracker.Models
{
    /// <summary>
    ///     Класс MainPageCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к главной странице MainPage.
    /// </summary>
    public class MainPageCommon
    {
        /// <summary>
        ///     Максимальная скорость игрока.
        /// </summary>
        public double PlayerMaxSpeed { get; set; }
        /// <summary>
        ///     Минимальная скорость игрока.
        /// </summary>
        public double PlayerMinSpeed { get; set; }
        /// <summary>
        ///     Название списка доказательств на главной странице. Примеры: "Evidence", "Доказательства".
        /// </summary>
        public string Clue { get; set; }
        /// <summary>
        ///     Название списка ежедневных заданий на главной странице. Примеры: "Daily Quests", "Ежедневные задания".
        /// </summary>
        public string DailyQuest { get; set; }
        /// <summary>
        ///     Название списка сложностей на главной странице. Примеры: "Difficulties", "Сложности".
        /// </summary>
        public string Difficulties { get; set; }
        /// <summary>
        ///     Название кнопки перехода на страницу определения призрака. Примеры: "Help to guess the ghost", "Помочь определить
        ///     призрака".
        /// </summary>
        public string GhostGuess { get; set; }
        /// <summary>
        ///     Текст кнопки принятия. Примеры: "OK", "ОК".
        /// </summary>
        public string Ok { get; set; }
        /// <summary>
        ///     Название списка некатегоризованных страниц на главной странице. Примеры: "Other Pages", "Другие страницы".
        /// </summary>
        public string OtherPages { get; set; }
        /// <summary>
        ///     Название списка последних патчей на главной странице. Примеры "Last Patches", "Последние Обновления".
        /// </summary>
        public string Patches { get; set; }
        /// <summary>
        ///     Текст оповещающий о выходе патча. Примеры: "There is a new update called, "Вышло новое обновление - ".
        /// </summary>
        public string PatchIsOut { get; set; }
        /// <summary>
        ///     Текст подсказки относительно максимальной скорости игрока. Пример: "Sprint for 3 seconds with 5 second cooldown."
        /// </summary>
        public string PlayerMaxSpeedTip { get; set; }
        /// <summary>
        ///     Текст подсказки относительно минимальной скорость игрока. Примеры: "Default walk.", "Обычный шаг."
        /// </summary>
        public string PlayerMinSpeedTip { get; set; }
        /// <summary>
        ///     Название обозначения игрока в интерфейсе. Примеры "Player", "Игрок".
        /// </summary>
        public string PlayerTitle { get; set; }
        /// <summary>
        ///     Текст кнопки прочтения подробно. Примеры: "Read more", "Прочитать подробнее".
        /// </summary>
        public string ReadMore { get; set; }
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
        ///     Текст уведомляющий о неудачной загрузки квестов.
        /// </summary>
        public string TasksError { get; set; }
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
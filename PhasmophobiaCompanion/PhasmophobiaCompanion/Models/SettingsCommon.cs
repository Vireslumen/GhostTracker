namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Класс SettingsCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к странице настроек - SettingsPage.
    /// </summary>
    public class SettingsCommon
    {
        /// <summary>
        ///     Текст информации о приложении.
        /// </summary>
        public string About { get; set; }
        /// <summary>
        ///     Название пункта для любого уровня подсказок. Примеры: "Any", "Любой".
        /// </summary>
        public string AnyLevel { get; set; }
        /// <summary>
        ///     Название списка языков приложения. Примеры: "App Language", "Язык приложения".
        /// </summary>
        public string AppLanguage { get; set; }
        /// <summary>
        ///     Название кнопки отправки репорта об ошибке. Примеры: "Send Error Report", "Отправить репорт об ошибках".
        /// </summary>
        public string ErrorReport { get; set; }
        /// <summary>
        ///     Текущий выбранный уровень подсказок. По умолчанию: "Any", "Любой".
        /// </summary>
        public string SelectedLevel { get; set; }
        /// <summary>
        ///     Название выбора языка на странице настроек. Примеры: "Select Language", "Выбор языка".
        /// </summary>
        public string SelectLanguage { get; set; }
        /// <summary>
        ///     Название выбора уровня подсказки на странице настроек. Примеры: "Select Level of Tip", "Выбор уровня подсказки".
        /// </summary>
        public string SelectLevel { get; set; }
        /// <summary>
        ///     Название страницы настроек. Примеры: "Settings", "Настройки".
        /// </summary>
        public string SettingsTitle { get; set; }
        /// <summary>
        ///     Текст для чекбокса активности открытия окна обратной связи при тряске девайса.
        /// </summary>
        public string ShakeActiveLabel { get; set; }
        /// <summary>
        ///     Название списка уровней подсказок. Примеры: "Level of Tip", "Уровень подсказки".
        /// </summary>
        public string TipLevel { get; set; }
    }
}
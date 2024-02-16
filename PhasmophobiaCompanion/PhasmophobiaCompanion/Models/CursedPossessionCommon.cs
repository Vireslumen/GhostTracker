namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Класс CursedPossessionCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к проклятым предметам - CursedPossession.
    /// </summary>
    public class CursedPossessionCommon
    {
        /// <summary>
        /// Названий списка проклятых предметов в интерфейсе. Пример: "Cursed Possessions", "Проклятые предметы".
        /// </summary>
        public string CursedsTitle { get; set; }
        /// <summary>
        ///     Текст для поля поиска проклятых предметов. Примеры: "Search Cursed Possessions", "Поиск Проклятых Предметов".
        /// </summary>
        public string Search { get; set; }
    }
}
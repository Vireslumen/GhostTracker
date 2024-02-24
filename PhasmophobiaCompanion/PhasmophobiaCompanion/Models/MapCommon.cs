namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Класс MapCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к картам - Map.
    /// </summary>
    public class MapCommon
    {
        /// <summary>
        ///     Текст кнопки применения выбранных фильтров или настроек. Примеры: "Apply", "Принять".
        /// </summary>
        public string Apply { get; set; }
        /// <summary>
        ///     Текст кнопки сброса фильтров. Примеры: "Clear", "Очистить".
        /// </summary>
        public string Clear { get; set; }
        /// <summary>
        ///     Текст для отображения количества выходов на карте. Примеры: "Exits", "Выходы".
        /// </summary>
        public string Exits { get; set; }
        /// <summary>
        ///     Текст для фильтра по количеству комнат на карте. Примеры: "Filter by Room Number", "Фильтр по количеству комнат".
        /// </summary>
        public string FilterRoom { get; set; }
        /// <summary>
        ///     Текст для фильтра по размеру карты. Примеры: "Filter by Size", "Фильтр по размеру".
        /// </summary>
        public string FilterSize { get; set; }
        /// <summary>
        ///     Текст для отображения количества этажей на карте. Примеры: "Floors", "Этажи".
        /// </summary>
        public string Floors { get; set; }
        /// <summary>
        ///     Текст для отображения количества укрытий на карте. Примеры: "HidenSpot Count", "Количество укрытий".
        /// </summary>
        public string HidenSpot { get; set; }
        /// <summary>
        ///     Текст для отображения размера карты. Примеры: "Size", "Размер".
        /// </summary>
        public string MapSize { get; set; }
        /// <summary>
        ///     Название списка карт в интерфейсе. Примеры: "Maps", "Карты".
        /// </summary>
        public string MapsTitle { get; set; }
        /// <summary>
        ///     Текст для отображения количества комнат на карте. Примеры "Room Number", "Количество комнат".
        /// </summary>
        public string RoomNumber { get; set; }
        /// <summary>
        ///     Текст для поля поиска карт. Примеры: "Search Maps", "Поиск Карт".
        /// </summary>
        public string Search { get; set; }
        /// <summary>
        ///     Текст для отображения уровня разблокировки карт. Примеры: "Unlock Level", "Уровень разблокировки".
        /// </summary>
        public string UnlockLvl { get; set; }
    }
}
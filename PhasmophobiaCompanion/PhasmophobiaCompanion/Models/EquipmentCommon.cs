namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Класс EquipmentCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к снаряжению - Equipment.
    /// </summary>
    public class EquipmentCommon
    {
        /// <summary>
        ///     Текст кнопки применения выбранных фильтров или настроек. Примеры: "Apply", "Принять".
        /// </summary>
        public string Apply { get; set; }
        /// <summary>
        ///     Название списка снаряжения в интерфейсе. Примеры: "Equipments", "Снаряжение".
        /// </summary>
        public string EquipmentsTitle { get; set; }
        /// <summary>
        ///     Текст для фильтра по тиру снаряжения. Примеры: "Filter by Tier", "Фильтр по Тиру".
        /// </summary>
        public string FilterTier { get; set; }
        /// <summary>
        ///     Текст для фильтра по уровню разблокировки снаряжения. Примеры: "Filter by Unlock Level", "Фильтр по уровню
        ///     разблокировки".
        /// </summary>
        public string FilterUnlock { get; set; }
        /// <summary>
        ///     Текст для указания максимального количества снаряжения. Примеры: "Max Limit", "Максимальное количество".
        /// </summary>
        public string MaxLimit { get; set; }
        /// <summary>
        ///     Текст для отображения цены снаряжения. Примеры: "Price", "Цена".
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        ///     Текст для отображения цены разблокировки снаряжения. Примеры: "Price to Unlock", "Цена разблокировки".
        /// </summary>
        public string PriceUnlock { get; set; }
        /// <summary>
        ///     Текст для поля поиска снаряжения. Примеры: "Search Equipments", "Поиск Снаряжения".
        /// </summary>
        public string Search { get; set; }
        /// <summary>
        ///     Название категории снаряжения. Примеры: "Tier", "Тир".
        /// </summary>
        public string Tier { get; set; }
        /// <summary>
        ///     Текст для отображения уровня разблокировки снаряжения. Примеры: "Unlock Level", "Уровень разблокировки".
        /// </summary>
        public string UnlockLevel { get; set; }
    }
}
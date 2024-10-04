namespace GhostTracker.Models
{
    /// <summary>
    ///     Класс DifficultyCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к сложностям - Difficulty.
    /// </summary>
    public class DifficultyCommon
    {
        /// <summary>
        ///     Текст для отображения работает ли монитор активности призрака. Примеры: "Is monitor activity work", "Работает ли
        ///     монитор активности".
        /// </summary>
        public string ActivityMonitorWork { get; set; }
        /// <summary>
        ///     Текст для отображения какой процент денег потраченных на снаряжение вернется игроку при смерти. Примеры: "Dead Cash
        ///     Back", "Кэшбэк при смерти".
        /// </summary>
        public string DeadCashBack { get; set; }
        /// <summary>
        ///     Название списка сложностей в интерфейсе. Примеры: "Difficulties", "Сложности".
        /// </summary>
        public string DifficultiesTitle { get; set; }
        /// <summary>
        ///     Текс для названия списка параметров сложности. Примеры: "Difficulty Parameters", "Параметры Сложности"
        /// </summary>
        public string DifficultyParams { get; set; }
        /// <summary>
        ///     Текст для отображения названия сложности в заголовке интерфейса. Примеры: "Difficulty", "Сложность".
        /// </summary>
        public string DifficultyTitle { get; set; }
        /// <summary>
        ///     Текст для отображения того, как много дверей открыто в начале игровой сессии. Примеры: "Door Opened Count",
        ///     "Сколько дверей открыто".
        /// </summary>
        public string DoorOpenedCount { get; set; }
        /// <summary>
        ///     Текст для отображения показывается ли электрический щиток на карте. Примеры: "Door Opened Count", "Сколько дверей
        ///     открыто".
        /// </summary>
        public string ElectricityBlockNotShowedOnMap { get; set; }
        /// <summary>
        ///     Текст для отображения будет ли включен эл. щиток в начале игровой сессии. Примеры: "ElectricityOn", "Электричество
        ///     включено".
        /// </summary>
        public string ElectricityOn { get; set; }
        /// <summary>
        ///     Текст для отображения количества доступных улик у призрака. Примеры: "Evidence Available", "Улик доступно".
        /// </summary>
        public string EvidenceAvailable { get; set; }
        /// <summary>
        ///     Текст для отображения информация об ультрафиолетовых отпечатках призрака. Примеры: "Finger Prints",
        ///     "Ультрафиолетовые отпечатки".
        /// </summary>
        public string FingerPrints { get; set; }
        /// <summary>
        ///     Текст для отображения насколько сильно активен призрак. Примеры: "Ghost Activity", "Активность призрака".
        /// </summary>
        public string GhostActivity { get; set; }
        /// <summary>
        ///     Текст для отображения длительности охоты призрака. Примеры: "Ghost Hunt Time", "Длительность охоты".
        /// </summary>
        public string GhostHuntTime { get; set; }
        /// <summary>
        ///     Текст для отображения насколько много мест для прятанья заблокировано. Примеры: "Hiding Spot Blocked", "Мест для
        ///     прятанья заблокировано".
        /// </summary>
        public string HidingSpotBlocked { get; set; }
        /// <summary>
        ///     Текст для отображения продления охоты из-за смерти игрока. Примеры: "Is Hunt Extend By Killing", "Охота продлится
        ///     после убийства".
        /// </summary>
        public string HuntExtendByKilling { get; set; }
        /// <summary>
        ///     Текст для отображения доступны ли проклятые предметы на этой сложности. Примеры "Is Cursed Possession available",
        ///     "Доступны ли проклятые предметы.".
        /// </summary>
        public string IsCursedAvailable { get; set; }
        /// <summary>
        ///     Текст для отображения показывает ли доска задач отвечает ли призрак всем или одному. Примеры:
        ///     "ObjectiveBoardPendingAloneAll", "Показывает ли доска требования ответа".
        /// </summary>
        public string ObjectiveBoardPendingAloneAll { get; set; }
        /// <summary>
        ///     Текст для отображения коэффициента награды за завершение игровой сессии. Примеры: "Reward Multiplier", "Множитель
        ///     награды".
        /// </summary>
        public string RewardMultiplier { get; set; }
        /// <summary>
        ///     Текст для отображения скорости потребления рассудка. Примеры: "Sanity Consumption", "Потребление рассудка".
        /// </summary>
        public string SanityConsumption { get; set; }
        /// <summary>
        ///     Текст для отображения работает ли монитор рассудка игроков. Примеры: "Sanity Monitor Work", "Монитор рассудка
        ///     работает".
        /// </summary>
        public string SanityMonitorWork { get; set; }
        /// <summary>
        ///     Текст для отображения насколько сильно восстанавливают рассудок лекарства. Примеры: "Sanity Restoration",
        ///     "Восстановление рассудка".
        /// </summary>
        public string SanityRestoration { get; set; }
        /// <summary>
        ///     Текст для отображения начального значения рассудка. Примеры: "Sanity Start At", "Начальный рассудок".
        /// </summary>
        public string SanityStartAt { get; set; }
        /// <summary>
        ///     Текст для отображения начального безопасного времени игровой сессии. Примеры: "Setup Time", "Безопасное время".
        /// </summary>
        public string SetupTime { get; set; }
        /// <summary>
        ///     Текст для отображения на каком уровне разблокируется эта сложность. Примеры: "Unlock Level", "Уровень
        ///     разблокировки".
        /// </summary>
        public string UnlockLevel { get; set; }
    }
}
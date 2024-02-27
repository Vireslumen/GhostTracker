namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Представляет собой уровень сложности конкретной игровой сессии.
    /// </summary>
    public class Difficulty
    {
        /// <summary>
        ///     Работает ли монитор активности призрака.
        /// </summary>
        public bool ActivityMonitorWork { get; set; }
        /// <summary>
        ///     Будет ли включен эл. щиток в начале игровой сессии.
        /// </summary>
        public bool ElectricityOn { get; set; }
        /// <summary>
        ///     Доступны ли проклятые предметы на этой сложности.
        /// </summary>
        public bool IsCursedAvailable { get; set; }
        /// <summary>
        ///     Работает ли монитор рассудка игроков.
        /// </summary>
        public bool SanityMonitorWork { get; set; }
        /// <summary>
        ///     Коэффициент награды за завершение игровой сессии.
        /// </summary>
        public float RewardMultiplier { get; set; }
        /// <summary>
        ///     Скорость потребления рассудка.
        /// </summary>
        public float SanityConsumption { get; set; }
        /// <summary>
        ///     Количество доступных улик у призрака.
        /// </summary>
        public int EvidenceAvailable { get; set; }
        public int ID { get; set; }
        /// <summary>
        ///     Насколько сильно восстанавливают рассудок лекарства.
        /// </summary>
        public int SanityRestoration { get; set; }
        /// <summary>
        ///     Начальное безопасное время игровой сессии.
        /// </summary>
        public int SetupTime { get; set; }
        /// <summary>
        ///     На каком уровне разблокируется эта сложность.
        /// </summary>
        public int UnlockLevel { get; set; }
        /// <summary>
        ///     Какой процент денег потраченных на снаряжение вернется игроку при смерти.
        /// </summary>
        public string DeadCashBack { get; set; }
        public string Description { get; set; }
        /// <summary>
        ///     Как много дверей открыто в начале игровой сессии на карте.
        /// </summary>
        public string DoorOpenedCount { get; set; }
        /// <summary>
        ///     Показывается ли электрический щиток на карте.
        /// </summary>
        public string ElectricityBlockNotShowedOnMap { get; set; }
        /// <summary>
        ///     Информация об ультрафиолетовых отпечатках призрака.
        /// </summary>
        public string FingerPrints { get; set; }
        /// <summary>
        ///     Насколько сильно активен призрак.
        /// </summary>
        public string GhostActivity { get; set; }
        /// <summary>
        ///     Длительность охоты призрака.
        /// </summary>
        public string GhostHuntTime { get; set; }
        /// <summary>
        ///     Насколько много мест для прятанья заблокировано.
        /// </summary>
        public string HidingSpotBlocked { get; set; }
        /// <summary>
        ///     Продление охоты из-за смерти игрока.
        /// </summary>
        public string HuntExtendByKilling { get; set; }
        /// <summary>
        ///     Показывает ли доска задач отвечает ли призрак всем или одному.
        /// </summary>
        public string ObjectiveBoardPendingAloneAll { get; set; }
        /// <summary>
        ///     Начальное значение рассудка.
        /// </summary>
        public string SanityStartAt { get; set; }
        public string Title { get; set; }
    }
}
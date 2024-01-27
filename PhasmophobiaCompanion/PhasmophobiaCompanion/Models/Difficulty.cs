using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    /// Представляет собой уровень сложности конкретной игровой сессии.
    /// </summary>
    public class Difficulty
    {
        // Работает ли монитор активности призрака.
        public bool ActivityMonitorWork { get; set; }

        // Какой процент денег потраченных на снаряжение вернется игроку при смерти.
        public string DeadCashBack { get; set; }

        // Описание данной сложности.
        public string Description { get; set; }

        // Как много дверей открыто в начале игровой сессии на карте.
        public string DoorOpenedCount { get; set; }

        // Показывается ли электрический щиток на карте.
        public string ElectricityBlockNotShowedOnMap { get; set; }

        // Будет ли включен эл. щиток в начале игровой сессии.
        public bool ElectricityOn { get; set; }

        // Количество доступных улик у призрака.
        public int EvidenceAvailable { get; set; }

        // Информация об ультрафиолетовых отпечатках призрака.
        public string FingerPrints { get; set; }

        // Насколько сильно активен призрак.
        public string GhostActivity { get; set; }

        // Длительность охоты призрака.
        public string GhostHuntTime { get; set; }

        // Насколько много мест для прятанья заблокировано.
        public string HidingSpotBlocked { get; set; }

        // Продление охоты из-за смерти игрока.
        public string HuntExtendByKilling { get; set; }

        // Показывает ли доска задач отвечает ли призрак всем или одному.
        public string ObjectiveBoardPendingAloneAll { get; set; }

        // Коэффициент награды за завершение игровой сессии.
        public float RewardMultiplier { get; set; }

        // Скорость потребления рассудка.
        public float SanityConsumption { get; set; }

        // Работает ли монитор рассудка игроков.
        public bool SanityMonitorWork { get; set; }

        // Насколько сильно восстанавливают рассудок лекарства.
        public string SanityRestoration { get; set; }

        // Начальное значение рассудка.
        public string SanityStartAt { get; set; }

        // Начальное безопасное время игровой сессии.
        public int SetupTime { get; set; }

        // Название уровня сложности.
        public string Title { get; set; }

        // На каком уровне разблокируется эта сложность.
        public int UnlockLevel { get; set; }
    }
}

using GhostTracker.Interfaces;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Класс ChallengeModeCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к особым режимам - ChallengeMode.
    /// </summary>
    public class ChallengeModeCommon : ITitledItem
    {
        /// <summary>
        ///     Текст описания особого режима в целом.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///     Название списка отличительных параметров особого режима. Примеры: "Distinctive mode parameters", "Отличительные
        ///     параметры режима".
        /// </summary>
        public string DistinctiveParams { get; set; }
        /// <summary>
        ///     Название списка предоставляемого в особом режиме снаряжения. Примеры: "Equipment provided", "Предоставляемое
        ///     снаряжение".
        /// </summary>
        public string EquipmentProvided { get; set; }
        /// <summary>
        ///     Название самих особых режимов в целом. Примеры: "Challenge Modes", "Особые режимы".
        /// </summary>
        public string Title { get; set; }
    }
}
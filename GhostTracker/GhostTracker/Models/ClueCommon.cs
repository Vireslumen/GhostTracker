namespace GhostTracker.Models
{
    /// <summary>
    ///     Класс ClueCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к доказательства - Clue.
    /// </summary>
    public class ClueCommon
    {
        /// <summary>
        ///     Название списка призраков, содержащих данную доказательствам. Примеры: "Ghosts with this evidence", "Призраки с данными доказательствами
        /// </summary>
        public string OtherGhosts { get; set; }
        /// <summary>
        ///     Название списка снаряжения, которое каким-либо образом связано с доказательством. Примеры: "Related Equipment", "Связанное
        ///     снаряжение".
        /// </summary>
        public string RelatedEquipment { get; set; }
    }
}
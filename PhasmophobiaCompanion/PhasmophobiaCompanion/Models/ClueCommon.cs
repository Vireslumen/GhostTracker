namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Класс ClueCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к уликам - Clue.
    /// </summary>
    public class ClueCommon
    {
        /// <summary>
        ///     Название списка призраков, содержащих данную улику. Примеры: "Ghosts with this evidence", "Призраки с данной
        ///     уликой".
        /// </summary>
        public string OtherGhosts { get; set; }
        /// <summary>
        ///     Название списка снаряжения, которое каким-либо образом связано с уликой. Примеры: "Related Equipment", "Связанное
        ///     снаряжение".
        /// </summary>
        public string RelatedEquipment { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы SpeedRangesBase.
    /// </summary>
    public class SpeedRangesBase
    {
        public GhostBase GhostBase { get; set; }
        public int GhostBaseID { get; set; }
        [Key] public int ID { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
    }
}
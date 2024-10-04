using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы SpeedRangesBase.
    /// </summary>
    public class SpeedRangesBase
    {
        public GhostBase GhostBase { get; set; }
        public int GhostBaseId { get; set; }
        [Key] public int Id { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
    }
}
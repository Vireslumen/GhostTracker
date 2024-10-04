using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы PatchBase.
    /// </summary>
    public class PatchBase
    {
        [Key] public int Id { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace PhasmophobiaCompanion.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы PatchBase.
    /// </summary>
    public class PatchBase
    {
        [Key] public int ID { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
    }
}
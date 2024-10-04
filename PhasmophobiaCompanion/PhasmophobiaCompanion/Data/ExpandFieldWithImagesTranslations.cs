using System.ComponentModel.DataAnnotations;

namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы ExpandFieldWithImagesTranslations, содержащей переводы на множество языков.
    /// </summary>
    public class ExpandFieldWithImagesTranslations
    {
        public ExpandFieldWithImagesBase ExpandFieldWithImages { get; set; }
        public int ExpandFieldWithImagesBaseID { get; set; }
        [Key] public int ID { get; set; }
        public string Body { get; set; }
        public string Header { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
    }
}
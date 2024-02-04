using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    /// Представляет собой информацию о патче, с сылкой на его страницу.
    /// </summary>
    public class Patch
    {
        public int ID { get; set; }
        // Ссылка на страницу описания патча.
        public string Source { get; set; }

        public string Title { get; set; }
    }
}

using PhasmophobiaCompanion.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    /// Предоставляет базовую реализацию для элементов, которые могут быть отображены в приложении.
    /// Может быть расширен для создания специфичных типов элементов.
    /// </summary>
    public abstract class BaseDisplayableItem : IDisplayableItem
    {
        /// <summary>
        /// Краткое описание элемента.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Путь к файлу изображения, связанного с элементом.
        /// </summary>
        public string ImageFilePath { get; set; }

        /// <summary>
        /// Название элемента.
        /// </summary>
        public string Title { get; set; }
    }
}

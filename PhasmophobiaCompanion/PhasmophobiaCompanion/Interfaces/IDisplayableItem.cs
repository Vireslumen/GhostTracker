using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Interfaces
{
    public interface IDisplayableItem
    {
        /// <summary>
        /// Краткое описание элемента.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Путь к файлу с изображением, связанным с элементом.
        /// </summary>
        string ImageFilePath { get; }

        /// <summary>
        /// Название элемента.
        /// </summary>
        string Title { get; }
    }
}

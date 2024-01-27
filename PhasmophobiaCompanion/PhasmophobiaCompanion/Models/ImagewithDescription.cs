using System;
using System.Collections.Generic;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class ImageWithDescription
    {
        // Описание изображения.
        public string Description { get; set; }

        // Путь к файлу изображения.
        public string ImageFilePath { get; set; }
    }
}

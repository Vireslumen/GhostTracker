using PhasmophobiaCompanion.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Models
{
    public class AchievementCommon : ITitledItem
    {
        public string Description { get; set; }
        public string Title { get; set; }
    }
}

using GhostTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GhostTracker.Models
{
    public class AchievementCommon : ITitledItem
    {
        public string Description { get; set; }
        public string Title { get; set; }
    }
}

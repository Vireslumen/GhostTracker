using GhostTracker.Interfaces;

namespace GhostTracker.Models
{
    public class AchievementCommon : ITitledItem
    {
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
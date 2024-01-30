using PhasmophobiaCompanion.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhasmophobiaCompanion.ViewModels
{
    public class MainPageViewModel
    {
        public ChallengeMode ChallengeMode { get; set; }
        public ObservableCollection<string> Tips { get; set; }
        public ObservableCollection<Ghost> Ghosts { get; set; }
        public ObservableCollection<Clue> Clues { get; set; }
        public ObservableCollection<Difficulty> Difficulties { get; set; }
        public ObservableCollection<Patch> Patches { get; set; }
        public ObservableCollection<string> DailyQuest { get; set; }
        public ObservableCollection<string> WeeklyQuest { get; set; }
        public ObservableCollection<OtherInfo> OtherInfos { get; set; }
        public MainPageViewModel()
        {

        }
    }
}

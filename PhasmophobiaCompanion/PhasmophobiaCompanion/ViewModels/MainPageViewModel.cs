using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    /// ViewModel для главной страницы.
    /// </summary>
    public class MainPageViewModel
    {
        public readonly DataService _dataService;
        public ChallengeMode ChallengeMode { get; set; }
        public ObservableCollection<string> Tips { get; set; }
        public ObservableCollection<Ghost> Ghosts { get; set; }
        public GhostCommon GhostCommon { get; set; }
        public ObservableCollection<Clue> Clues { get; set; }
        public ObservableCollection<Difficulty> Difficulties { get; set; }
        public ObservableCollection<Patch> Patches { get; set; }
        public ObservableCollection<Quest> Quests { get; set; }
        public ObservableCollection<OtherInfo> OtherInfos { get; set; }
        public string DisplayedTip { get; set; }
        public ObservableCollection<Quest> DailyQuest { get; set; }
        public ObservableCollection<Quest> WeeklyQuest { get; set; }
        public MainPageViewModel()
        {
            _dataService = DependencyService.Get<DataService>();
            //Загрузка всех данных для страницы
            ChallengeMode = _dataService.GetChallengeMode(1);
            Tips = _dataService.GetTips();
            Ghosts = _dataService.GetGhosts();
            GhostCommon = _dataService.GetGhostCommon();
            Clues = _dataService.GetClues();
            Difficulties = _dataService.GetDifficulties();
            Patches = _dataService.GetPatches();
            Quests = _dataService.GetQuests();
            OtherInfos = _dataService.GetOtherInfos();
            ChangeTip();
            DailyQuest = GetFourQuests(new int[] { 1, 2, 3, 4});
            WeeklyQuest = GetFourQuests(new int[] { 1, 2, 3, 4 });
        }

        /// <summary>
        /// Смена отображаемой подсказки на случайную из списка всех подсказок.
        /// </summary>
        public void ChangeTip()
        {
                Random random = new Random();
                DisplayedTip = Tips[random.Next(Tips.Count)];
        }
        
        /// <summary>
        /// Получение коллекции квестов по номерам.
        /// </summary>
        /// <param name="indices">массив номеров квестов.</param>
        /// <returns>Коллекция квестов.</returns>
        public ObservableCollection<Quest> GetFourQuests(int[] indices)
        {
            return new ObservableCollection<Quest>(Quests.Where((c, index) => indices.Contains(index)));
        }
    }
}

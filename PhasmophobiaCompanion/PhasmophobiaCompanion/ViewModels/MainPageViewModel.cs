using System;
using System.Collections.ObjectModel;
using System.Linq;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для главной страницы.
    /// </summary>
    public class MainPageViewModel : BaseViewModel
    {
        public readonly DataService _dataService;
        private Difficulty selectedDifficulty;
        private DifficultyCommon difficultyCommon;

        public MainPageViewModel()
        {
            try
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
                MainPageCommon = _dataService.GetMainPageCommon();
                ChangeTip();
                DailyQuest = GetFourQuests(new[] {1, 2, 3, 4});
                WeeklyQuest = GetFourQuests(new[] {1, 2, 3, 4});
                DifficultyCommon = _dataService.GetDifficultyCommon();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации MainPageViewModel.");
                throw;
            }
        }

        public ChallengeMode ChallengeMode { get; set; }
        public Difficulty SelectedDifficulty
        {
            get => selectedDifficulty;
            set
            {
                selectedDifficulty = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        ///     Общие текстовые данные для интерфейса относящегося к сложностям.
        /// </summary>
        public DifficultyCommon DifficultyCommon
        {
            get => difficultyCommon;
            set
            {
                difficultyCommon = value;
                OnPropertyChanged();
            }
        }
        public GhostCommon GhostCommon { get; set; }
        public MainPageCommon MainPageCommon { get; set; }
        public ObservableCollection<Clue> Clues { get; set; }
        public ObservableCollection<Difficulty> Difficulties { get; set; }
        public ObservableCollection<Ghost> Ghosts { get; set; }
        public ObservableCollection<OtherInfo> OtherInfos { get; set; }
        public ObservableCollection<Patch> Patches { get; set; }
        public ObservableCollection<Quest> DailyQuest { get; set; }
        public ObservableCollection<Quest> Quests { get; set; }
        public ObservableCollection<Quest> WeeklyQuest { get; set; }
        public ObservableCollection<string> Tips { get; set; }
        public string DisplayedTip { get; set; }

        /// <summary>
        ///     Смена отображаемой подсказки на случайную из списка всех подсказок.
        /// </summary>
        public void ChangeTip()
        {
            try
            {
                var random = new Random();
                DisplayedTip = Tips[random.Next(Tips.Count)];
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время смены отображаемой подсказки.");
                throw;
            }
        }

        /// <summary>
        ///     Получение коллекции квестов по номерам.
        /// </summary>
        /// <param name="indices">массив номеров квестов.</param>
        /// <returns>Коллекция квестов.</returns>
        public ObservableCollection<Quest> GetFourQuests(int[] indices)
        {
            try
            {
                return new ObservableCollection<Quest>(Quests.Where((c, index) => indices.Contains(index)));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время получения квестов по выбранным номерам.");
                throw;
            }
        }
    }
}
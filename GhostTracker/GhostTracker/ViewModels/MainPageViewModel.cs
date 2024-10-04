using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Input;
using GhostTracker.Interfaces;
using GhostTracker.Models;
using GhostTracker.Services;
using GhostTracker.Views;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Essentials;
using Xamarin.Forms;
using Map = GhostTracker.Models.Map;

namespace GhostTracker.ViewModels
{
    /// <summary>
    ///     ViewModel для главной страницы.
    /// </summary>
    public class MainPageViewModel : SearchableViewModel
    {
        public readonly DataService dataService;
        private bool isSearchResultVisible;
        private ObservableCollection<object> searchResults;
        private ObservableCollection<Quest> dailyQuest;
        private ObservableCollection<Quest> weekyQuest;
        private Tip displayedTip;

        public MainPageViewModel()
        {
            try
            {
                var random = new Random();
                dataService = DependencyService.Get<DataService>();
                //Загрузка всех данных для страницы
                ChallengeMode = dataService.GetCurrentChallengeMode();
                Tips = dataService.GetTips();
                Ghosts = dataService.GetGhosts();
                GhostCommon = dataService.GetGhostCommon();
                Clues = dataService.GetClues();
                Difficulties = dataService.GetDifficulties();
                Patches = dataService.GetPatches().TakeLast(5).ToList();
                Patches.Reverse();
                Quests = dataService.GetQuests();
                OtherInfos = new List<ITitledItem>();
                OtherInfos.Add(dataService.GetQuestCommon());
                OtherInfos.Add(dataService.GetChallengeModeCommon());
                OtherInfos.Add(dataService.GetAchievementCommon());
                OtherInfos.AddRange(dataService.GetOtherInfos());
                MainPageCommon = dataService.GetMainPageCommon();
                ChangeTip();
                SearchResults = new ObservableCollection<object>();
                ConvertSpeedToMeter();
                SetTasks();
                // Инициализация команд
                ChallengeModeTappedCommand = new Command(OnChallengeModeTapped);
                ClueTappedCommand = new Command<Clue>(OnClueTapped);
                DifficultyTappedCommand = new Command<Difficulty>(OnDifficultyTapped);
                GhostTappedCommand = new Command<Ghost>(OnGhostTapped);
                MaxSanityHeaderTappedCommand = new Command(OnMaxSanityHeaderTapped);
                SanityHuntTappedCommand = new Command<Ghost>(OnSanityHuntTapped);
                MaxSpeedHeaderTappedCommand = new Command(OnMaxSpeedHeaderTapped);
                MaxSpeedLoSHeaderTappedCommand = new Command(OnMaxSpeedLoSHeaderTapped);
                GhostSpeedTappedCommand = new Command<Ghost>(OnGhostSpeedTapped);
                MinSanityHeaderTappedCommand = new Command(OnMinSanityHeaderTapped);
                MinSpeedHeaderTappedCommand = new Command(OnMinSpeedHeaderTapped);
                OtherPageTappedCommand = new Command<ITitledItem>(OnOtherPageTapped);
                PatchTappedCommand = new Command<Patch>(OnPatchTapped);
                MaxPlayerSpeedTappedCommand = new Command(OnMaxPlayerSpeedTapped);
                MinPlayerSpeedTappedCommand = new Command(OnMinPlayerSpeedTapped);
                QuestTappedCommand = new Command<Quest>(OnQuestTapped);
                TipTappedCommand = new Command(ChangeTip);
                ReadMoreCommand = new Command(ToPatchPage);
                OkCommand = new Command(CloseAlert);
                SettingsTappedCommand = new Command(OpenSettings);
                SearchResultTappedCommand = new Command<object>(NavigateToDetailPage);
                GhostGuessTappedCommand = new Command(OnGhostGuessTapped);
                CheckPatchUpdate();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации MainPageViewModel.");
            }
        }

        public bool IsSearchResultVisible
        {
            get => isSearchResultVisible;
            set
            {
                isSearchResultVisible = value;
                OnPropertyChanged();
            }
        }
        public ChallengeMode ChallengeMode { get; set; }
        public GhostCommon GhostCommon { get; set; }
        public ICommand ChallengeModeTappedCommand { get; private set; }
        public ICommand ClueTappedCommand { get; private set; }
        public ICommand DifficultyTappedCommand { get; private set; }
        public ICommand GhostGuessTappedCommand { get; }
        public ICommand GhostSpeedTappedCommand { get; private set; }
        public ICommand GhostTappedCommand { get; private set; }
        public ICommand MaxPlayerSpeedTappedCommand { get; private set; }
        public ICommand MaxSanityHeaderTappedCommand { get; private set; }
        public ICommand MaxSpeedHeaderTappedCommand { get; private set; }
        public ICommand MaxSpeedLoSHeaderTappedCommand { get; private set; }
        public ICommand MinPlayerSpeedTappedCommand { get; private set; }
        public ICommand MinSanityHeaderTappedCommand { get; private set; }
        public ICommand MinSpeedHeaderTappedCommand { get; private set; }
        public ICommand OkCommand { get; private set; }
        public ICommand OtherPageTappedCommand { get; private set; }
        public ICommand PatchTappedCommand { get; private set; }
        public ICommand QuestTappedCommand { get; private set; }
        public ICommand ReadMoreCommand { get; private set; }
        public ICommand SanityHuntTappedCommand { get; private set; }
        public ICommand SearchResultTappedCommand { get; private set; }
        public ICommand SettingsTappedCommand { get; private set; }
        public ICommand TipTappedCommand { get; private set; }
        public List<Clue> Clues { get; set; }
        public List<Difficulty> Difficulties { get; set; }
        public List<Ghost> Ghosts { get; set; }
        public List<ITitledItem> OtherInfos { get; set; }
        public List<Patch> Patches { get; set; }
        public List<Quest> Quests { get; set; }
        public List<Tip> Tips { get; set; }
        public MainPageCommon MainPageCommon { get; set; }
        public ObservableCollection<object> SearchResults
        {
            get => searchResults;
            set
            {
                searchResults = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Quest> DailyQuest
        {
            get => dailyQuest;
            set
            {
                dailyQuest = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Quest> WeeklyQuest
        {
            get => weekyQuest;
            set
            {
                weekyQuest = value;
                OnPropertyChanged();
            }
        }
        public Patch LastPatch { get; set; }
        public Tip DisplayedTip
        {
            get => displayedTip;
            set
            {
                displayedTip = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Смена отображаемой подсказки на случайную из списка всех подсказок.
        /// </summary>
        public void ChangeTip()
        {
            try
            {
                var random = new Random();
                if (dataService.SelectedTipLevel == dataService.GetSettingsCommon().AnyLevel)
                {
                    if (DisplayedTip != null)
                    {
                        var tipsWithoutCurrent = Tips.Where(tip => tip.TipValue != DisplayedTip.TipValue).ToList();
                        DisplayedTip = tipsWithoutCurrent[random.Next(tipsWithoutCurrent.Count)];
                    }
                    else
                    {
                        DisplayedTip = Tips[random.Next(Tips.Count)];
                    }
                }
                else
                {
                    var filteredTips = Tips.Where(t => t.Level == dataService.SelectedTipLevel).ToList();
                    if (DisplayedTip != null)
                    {
                        var tipsWithoutCurrent =
                            filteredTips.Where(tip => tip.TipValue != DisplayedTip.TipValue).ToList();
                        DisplayedTip = tipsWithoutCurrent[random.Next(tipsWithoutCurrent.Count)];
                    }
                    else
                    {
                        DisplayedTip = filteredTips[random.Next(filteredTips.Count)];
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время смены отображаемой подсказки.");
            }
        }

        /// <summary>
        ///     Метод проверки выхода нового патча и открытия алерта.
        /// </summary>
        private async void CheckPatchUpdate()
        {
            try
            {
                if (dataService.NewPatch)
                {
                    LastPatch = Patches.First();
                    // Логика для открытия алерта о выходе нового патча
                    var alertPage = new PatchAlertPage(this);
                    await PopupNavigation.Instance.PushAsync(alertPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время открытия алерта о выходе нового патча PatchAlertPage.");
            }
        }

        /// <summary>
        ///     Закрытие алерта о выходе патча.
        /// </summary>
        private void CloseAlert()
        {
            PopupNavigation.Instance.PopAsync();
        }

        /// <summary>
        ///     Конвертация скорости призраков и игрока в метры в секунду.
        /// </summary>
        private void ConvertSpeedToMeter()
        {
            foreach (var ghost in Ghosts)
            {
                ghost.MaxGhostSpeed /= 100;
                ghost.MinGhostSpeed /= 100;
                ghost.MaxGhostSpeedLoS /= 100;
            }

            MainPageCommon.PlayerMaxSpeed /= 100;
            MainPageCommon.PlayerMinSpeed /= 100;
        }

        /// <summary>
        ///     Получение коллекции квестов по номерам.
        /// </summary>
        /// <param name="indices">Массив номеров квестов.</param>
        /// <returns>Коллекция квестов.</returns>
        public ObservableCollection<Quest> GetSomeQuests(int[] indices)
        {
            return new ObservableCollection<Quest>(Quests.Where((c, index) => indices.Contains(index)));
        }

        /// <summary>
        ///     Переход на выбранную страницу или сайт из поиска.
        /// </summary>
        /// <param name="selectedSearchItem">Выбранный элемент в поиске</param>
        private async void NavigateToDetailPage(object selectedSearchItem)
        {
            try
            {
                if (isNavigating || selectedSearchItem == null) return;

                Page detailPage = null;
                switch (selectedSearchItem)
                {
                    case CursedPossession cursedPossession:
                        detailPage = new CursedDetailPage(cursedPossession);
                        await NavigateWithLoadingAsync(detailPage);
                        break;
                    case Clue clue:
                        detailPage = new ClueDetailPage(clue);
                        await NavigateWithLoadingAsync(detailPage);
                        break;
                    case Difficulty difficulty:
                        detailPage = new DifficultyDetailPage(difficulty);
                        await NavigateWithLoadingAsync(detailPage);
                        break;
                    case Patch patch:
                        if (Uri.TryCreate(patch.Source, UriKind.Absolute, out var uri))
                            await Launcher.OpenAsync(uri);
                        break;
                    case OtherInfo otherInfo:
                        detailPage = new OtherInfoPage(otherInfo);
                        await NavigateWithLoadingAsync(detailPage);
                        break;
                    case Ghost ghost:
                        detailPage = new GhostDetailPage(ghost);
                        await NavigateWithLoadingAsync(detailPage);
                        break;
                    case Equipment equipment:
                        detailPage = new EquipmentDetailPage(equipment);
                        await NavigateWithLoadingAsync(detailPage);
                        break;
                    case Map map:
                        detailPage = new MapDetailPage(map);
                        await NavigateWithLoadingAsync(detailPage);
                        break;
                    case ChallengeMode challengeMode:
                        detailPage = new ChallengeModeDetailPage(challengeMode);
                        await NavigateWithLoadingAsync(detailPage);
                        break;
                    case Quest quest:
                        detailPage = new QuestsPage();
                        await NavigateWithLoadingAsync(detailPage);
                        break;
                    case Achievement achievement:
                        detailPage = new AchievementPage();
                        await NavigateWithLoadingAsync(detailPage);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу из поиска главной страницы.");
            }
        }

        /// <summary>
        ///     Переход на подробную страницу особого режима по нажатию на него.
        /// </summary>
        private async void OnChallengeModeTapped()
        {
            if (isNavigating) return;
            if (dataService.IsMapsDataLoaded && dataService.IsEquipmentsDataLoaded)
            {
                var page = new ChallengeModeDetailPage(ChallengeMode);
                await NavigateWithLoadingAsync(page);
            }
        }

        /// <summary>
        ///     Переход на подробную страницу улики по нажатию на неё.
        /// </summary>
        private async void OnClueTapped(Clue clueItem)
        {
            if (isNavigating) return;
            var page = new ClueDetailPage(clueItem);
            await NavigateWithLoadingAsync(page);
        }

        /// <summary>
        ///     Переход на подробную страницу сложности по нажатию на неё.
        /// </summary>
        private async void OnDifficultyTapped(Difficulty difficultyItem)
        {
            if (isNavigating) return;
            var page = new DifficultyDetailPage(difficultyItem);
            await NavigateWithLoadingAsync(page);
        }

        /// <summary>
        ///     Переход на страницу определения призрака по нажатию на неё.
        /// </summary>
        private async void OnGhostGuessTapped()
        {
            var detailPage = new GhostGuessPage();
            await NavigateWithLoadingAsync(detailPage);
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по скорости призрака на экране.
        /// </summary>
        private async void OnGhostSpeedTapped(Ghost ghostItem)
        {
            var ghostSpeedClause = GhostCommon.Min;
            if (ghostItem.MinGhostSpeedClause == ghostItem.MaxGhostSpeedClause)
                ghostSpeedClause += "-" + GhostCommon.Max + ": " + ghostItem.MinGhostSpeedClause;
            else
                ghostSpeedClause += ": " + ghostItem.MinGhostSpeedClause + "\n" + GhostCommon.Max + ": " +
                                    ghostItem.MaxGhostSpeedClause;
            ghostSpeedClause += "\n" + GhostCommon.Max + GhostCommon.LoS + ": " + ghostItem.MaxGhostSpeedLoSClause;
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(ghostSpeedClause));
        }

        /// <summary>
        ///     Переход на подробную страницу призрака по нажатию на него.
        /// </summary>
        private async void OnGhostTapped(Ghost ghostItem)
        {
            if (isNavigating) return;
            var page = new GhostDetailPage(ghostItem);
            await NavigateWithLoadingAsync(page);
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по минимальной скорости игрока на экране.
        /// </summary>
        private async void OnMaxPlayerSpeedTapped()
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(MainPageCommon.PlayerMaxSpeedTip));
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку максимума минимального порога рассудка для начала охоты.
        /// </summary>
        private async void OnMaxSanityHeaderTapped()
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(GhostCommon.MaxSanityHunt));
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку максимальной скорости призрака на экране.
        /// </summary>
        private async void OnMaxSpeedHeaderTapped()
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(GhostCommon.MaxSpeed));
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку максимальной скорости призрака с учётом LoS на экране.
        /// </summary>
        private async void OnMaxSpeedLoSHeaderTapped()
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(GhostCommon.MaxSpeedLoS));
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по минимальной скорости игрока на экране.
        /// </summary>
        private async void OnMinPlayerSpeedTapped()
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(MainPageCommon.PlayerMinSpeedTip));
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку минимума минимального порога рассудка для начала охоты.
        /// </summary>
        private async void OnMinSanityHeaderTapped()
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(GhostCommon.MinSanityHunt));
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку минимальной скорости призрака на экране.
        /// </summary>
        private async void OnMinSpeedHeaderTapped()
        {
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(GhostCommon.MinSpeed));
        }

        /// <summary>
        ///     Переход на подробную некатегоризованную страницу по нажатию на неё.
        /// </summary>
        private async void OnOtherPageTapped(ITitledItem otherInfoItem)
        {
            if (isNavigating) return;
            if (otherInfoItem is OtherInfo)
            {
                var page = new OtherInfoPage((OtherInfo) otherInfoItem);
                await NavigateWithLoadingAsync(page);
            }
            else if (otherInfoItem is QuestCommon)
            {
                var page = new QuestsPage();
                await NavigateWithLoadingAsync(page);
            }
            else if (otherInfoItem is ChallengeModeCommon)
            {
                var page = new ChallengeModesPage();
                await NavigateWithLoadingAsync(page);
            }
            else if (otherInfoItem is AchievementCommon)
            {
                var page = new AchievementPage();
                await NavigateWithLoadingAsync(page);
            }
        }

        /// <summary>
        ///     Переход на страницу в браузере патча.
        /// </summary>
        private async void OnPatchTapped(Patch patch)
        {
            try
            {
                if (Uri.TryCreate(patch.Source, UriKind.Absolute, out var uri))
                    await Launcher.OpenAsync(uri);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу(в браузере) патча Patch.");
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки о прохождении квеста на экране.
        /// </summary>
        private async void OnQuestTapped(Quest questItem)
        {
            var questTip = questItem.Tip;
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(questTip));
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по порогу рассудка для начала охоты на экране.
        /// </summary>
        private async void OnSanityHuntTapped(Ghost ghostItem)
        {
            var ghostSanityHuntClause = GhostCommon.Min;
            if (ghostItem.MinSanityHuntClause == ghostItem.MaxSanityHuntClause)
                ghostSanityHuntClause += "-" + GhostCommon.Max + ": " + ghostItem.MinSanityHuntClause;
            else
                ghostSanityHuntClause += ": " + ghostItem.MinSanityHuntClause + "\n" + GhostCommon.Max + ": " +
                                         ghostItem.MaxSanityHuntClause;
            await PopupNavigation.Instance.PushAsync(new TooltipPopup(ghostSanityHuntClause));
        }

        /// <summary>
        ///     Переход на страницу настроек по нажатию на неё.
        /// </summary>
        private async void OpenSettings()
        {
            if (isNavigating) return;
            var page = new SettingsPage();
            await NavigateWithLoadingAsync(page);
        }

        /// <summary>
        ///     Глобальный поиск среди на главной странице.
        /// </summary>
        protected override void PerformSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var results = dataService.Search(SearchText);
                SearchResults.Clear();
                foreach (var item in results) SearchResults.Add(item);
                IsSearchResultVisible = SearchResults.Any();
            }
            else
            {
                IsSearchResultVisible = false;
            }
        }

        /// <summary>
        ///     Получение текущих задач с сервера.
        /// </summary>
        private async void SetTasks()
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://a28577-767d.u.d-f.pw/Tasks");
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                var numbers = JsonSerializer.Deserialize<int[]>(jsonString);
                if (numbers.Length >= 8)
                {
                    DailyQuest = GetSomeQuests(new ArraySegment<int>(numbers, 0, 4).ToArray());
                    WeeklyQuest = GetSomeQuests(new ArraySegment<int>(numbers, 4, 4).ToArray());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при получении текущих задач с сервера.");
                DailyQuest = new ObservableCollection<Quest>();
                WeeklyQuest = new ObservableCollection<Quest>();
                DailyQuest.Add(new Quest {Title = MainPageCommon.TasksError});
                WeeklyQuest.Add(new Quest {Title = MainPageCommon.TasksError});
            }
        }

        /// <summary>
        ///     Переход на страницу последнего патча.
        /// </summary>
        private void ToPatchPage()
        {
            PopupNavigation.Instance.PopAsync();
            OnPatchTapped(LastPatch);
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Essentials;
using Xamarin.Forms;
using Map = PhasmophobiaCompanion.Models.Map;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для главной страницы.
    /// </summary>
    public class MainPageViewModel : SearchableViewModel
    {
        public readonly DataService dataService;
        private bool isSearchResultVisible;
        private object selectedItem;
        private ObservableCollection<object> searchResults;

        public MainPageViewModel()
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                //Загрузка всех данных для страницы
                ChallengeMode = dataService.GetChallengeMode(1);
                Tips = dataService.GetTips();
                Ghosts = dataService.GetGhosts();
                GhostCommon = dataService.GetGhostCommon();
                Clues = dataService.GetClues();
                Difficulties = dataService.GetDifficulties();
                Patches = dataService.GetPatches();
                Quests = dataService.GetQuests();
                OtherInfos = new ObservableCollection<ITitledItem>(dataService.GetOtherInfos());
                OtherInfos.Add(dataService.GetQuestCommon());
                OtherInfos.Add(dataService.GetChallengeModeCommon());
                MainPageCommon = dataService.GetMainPageCommon();
                ChangeTip();
                SearchResults = new ObservableCollection<object>();
                DailyQuest = GetSomeQuests(new[] {1, 2, 3, 4});
                WeeklyQuest = GetSomeQuests(new[] {1, 2, 3, 4});
                // Инициализация команд
                ChallengeModeTappedCommand = new Command(OnChallengeModeTapped);
                ClueTappedCommand = new Command<Clue>(OnClueTapped);
                DifficultyTappedCommand = new Command<Difficulty>(OnDifficultyTapped);
                GhostTappedCommand = new Command<Ghost>(OnGhostTapped);
                MaxGhostSpeedLoSTappedCommand = new Command<Ghost>(OnMaxGhostSpeedLoSTapped);
                MaxGhostSpeedTappedCommand = new Command<Ghost>(OnMaxGhostSpeedTapped);
                MaxSanityHeaderTappedCommand = new Command(OnMaxSanityHeaderTapped);
                MaxSanityHuntTappedCommand = new Command<Ghost>(OnMaxSanityHuntTapped);
                MaxSpeedHeaderTappedCommand = new Command(OnMaxSpeedHeaderTapped);
                MaxSpeedLoSHeaderTappedCommand = new Command(OnMaxSpeedLoSHeaderTapped);
                MinGhostSpeedTappedCommand = new Command<Ghost>(OnMinGhostSpeedTapped);
                MinSanityHeaderTappedCommand = new Command(OnMinSanityHeaderTapped);
                MinSanityHuntTappedCommand = new Command<Ghost>(OnMinSanityHuntTapped);
                MinSpeedHeaderTappedCommand = new Command(OnMinSpeedHeaderTapped);
                OtherPageTappedCommand = new Command<ITitledItem>(OnOtherPageTapped);
                PatchTappedCommand = new Command<Patch>(OnPatchTapped);
                MaxPlayerSpeedTappedCommand = new Command(OnMaxPlayerSpeedTapped);
                MinPlayerSpeedTappedCommand = new Command(OnMinPlayerSpeedTapped);
                QuestTappedCommand = new Command<Quest>(OnQuestTapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации MainPageViewModel.");
                throw;
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
        public ICommand GhostTappedCommand { get; private set; }
        public ICommand MaxGhostSpeedLoSTappedCommand { get; private set; }
        public ICommand MaxGhostSpeedTappedCommand { get; private set; }
        public ICommand MaxPlayerSpeedTappedCommand { get; private set; }
        public ICommand MaxSanityHeaderTappedCommand { get; private set; }
        public ICommand MaxSanityHuntTappedCommand { get; private set; }
        public ICommand MaxSpeedHeaderTappedCommand { get; private set; }
        public ICommand MaxSpeedLoSHeaderTappedCommand { get; private set; }
        public ICommand MinGhostSpeedTappedCommand { get; private set; }
        public ICommand MinPlayerSpeedTappedCommand { get; private set; }
        public ICommand MinSanityHeaderTappedCommand { get; private set; }
        public ICommand MinSanityHuntTappedCommand { get; private set; }
        public ICommand MinSpeedHeaderTappedCommand { get; private set; }
        public ICommand OtherPageTappedCommand { get; private set; }
        public ICommand PatchTappedCommand { get; private set; }
        public ICommand QuestTappedCommand { get; private set; }
        public MainPageCommon MainPageCommon { get; set; }
        public object SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
                NavigateToDetailPage(selectedItem);
            }
        }
        public ObservableCollection<Clue> Clues { get; set; }
        public ObservableCollection<Difficulty> Difficulties { get; set; }
        public ObservableCollection<Ghost> Ghosts { get; set; }
        public ObservableCollection<ITitledItem> OtherInfos { get; set; }
        public ObservableCollection<object> SearchResults
        {
            get => searchResults;
            set
            {
                searchResults = value;
                OnPropertyChanged();
            }
        }
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
        /// <param name="indices">Массив номеров квестов.</param>
        /// <returns>Коллекция квестов.</returns>
        public ObservableCollection<Quest> GetSomeQuests(int[] indices)
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

        /// <summary>
        ///     Переход на выбранную страницу или сайт из поиска.
        /// </summary>
        /// <param name="selectedSearchItem">Выбранный элемент в поиске</param>
        private async void NavigateToDetailPage(object selectedSearchItem)
        {
            try
            {
                if (selectedSearchItem == null) return;

                Page detailPage = null;
                switch (selectedSearchItem)
                {
                    case CursedPossession cursedPossession:
                        detailPage = new CursedDetailPage(cursedPossession);
                        await Shell.Current.Navigation.PushAsync(detailPage);
                        break;
                    case Clue clue:
                        detailPage = new ClueDetailPage(clue);
                        await Shell.Current.Navigation.PushAsync(detailPage);
                        break;
                    case Difficulty difficulty:
                        detailPage = new DifficultyDetailPage(difficulty);
                        await Shell.Current.Navigation.PushAsync(detailPage);
                        break;
                    case Patch patch:
                        if (Uri.TryCreate(patch.Source, UriKind.Absolute, out var uri))
                            await Launcher.OpenAsync(uri);
                        break;
                    case OtherInfo otherInfo:
                        detailPage = new OtherInfoPage(otherInfo);
                        await Shell.Current.Navigation.PushAsync(detailPage);
                        break;
                    case Ghost ghost:
                        detailPage = new GhostDetailPage(ghost);
                        await Shell.Current.Navigation.PushAsync(detailPage);
                        break;
                    case Equipment equipment:
                        detailPage = new EquipmentDetailPage(equipment);
                        await Shell.Current.Navigation.PushAsync(detailPage);
                        break;
                    case Map map:
                        detailPage = new MapDetailPage(map);
                        await Shell.Current.Navigation.PushAsync(detailPage);
                        break;
                    case ChallengeMode challengeMode:
                        detailPage = new ChallengeModeDetailPage(challengeMode);
                        await Shell.Current.Navigation.PushAsync(detailPage);
                        break;
                }

                SelectedItem = null;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу из поиска главной страницы.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу особого режима по нажатию на него.
        /// </summary>
        private void OnChallengeModeTapped()
        {
            try
            {
                if (dataService.IsMapsDataLoaded && dataService.IsEquipmentsDataLoaded)
                {
                    var page = new ChallengeModeDetailPage(ChallengeMode);
                    Application.Current.MainPage.Navigation.PushAsync(page);
                }
                // TODO: Сделать, чтобы если Карты не были загружены, какую-нибудь загрузки или что-нибудь в этом духе.
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу особого режима ChallengeModeDetailPage.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу улики по нажатию на неё.
        /// </summary>
        private void OnClueTapped(Clue clueItem)
        {
            try
            {
                var page = new ClueDetailPage(clueItem);
                Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу улики ClueDetailPage с главной страницы MainPage.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу сложности по нажатию на неё.
        /// </summary>
        private void OnDifficultyTapped(Difficulty difficultyItem)
        {
            try
            {
                var page = new DifficultyDetailPage(difficultyItem);
                Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу сложности DifficultyDetailPage.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу призрака по нажатию на него.
        /// </summary>
        private void OnGhostTapped(Ghost ghostItem)
        {
            try
            {
                var page = new GhostDetailPage(ghostItem);
                Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Ошибка во время перехода на подробную страницу призрака GhostDetailPage с главной страницы MainPage.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по максимальной скорости призрака с учётом LoS на экране.
        /// </summary>
        private async void OnMaxGhostSpeedLoSTapped(Ghost ghostItem)
        {
            try
            {
                var maxGhostSpeedLoSClause = ghostItem.MaxGhostSpeedLoSClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(maxGhostSpeedLoSClause));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки maxGhostSpeedLoSClause.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по максимальной скорости призрака на экране.
        /// </summary>
        private async void OnMaxGhostSpeedTapped(Ghost ghostItem)
        {
            try
            {
                var maxGhostSpeedClause = ghostItem.MaxGhostSpeedClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(maxGhostSpeedClause));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки maxGhostSpeedClause.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по минимальной скорости игрока на экране.
        /// </summary>
        private async void OnMaxPlayerSpeedTapped()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(MainPageCommon.PlayerMaxSpeedTip));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки PlayerMaxSpeedTip.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку максимума минимального порога рассудка для начала охоты.
        /// </summary>
        private async void OnMaxSanityHeaderTapped()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(GhostCommon.MaxSanityHunt));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки MaxSanityHunt.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по максимуму минимального порога рассудка для начала охоты на экране.
        /// </summary>
        private async void OnMaxSanityHuntTapped(Ghost ghostItem)
        {
            try
            {
                var maxSanityHuntClause = ghostItem.MaxSanityHuntClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(maxSanityHuntClause));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки maxSanityHuntClause.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку максимальной скорости призрака на экране.
        /// </summary>
        private async void OnMaxSpeedHeaderTapped()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(GhostCommon.MaxSpeed));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки MaxSpeed.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку максимальной скорости призрака с учётом LoS на экране.
        /// </summary>
        private async void OnMaxSpeedLoSHeaderTapped()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(GhostCommon.MaxSpeedLoS));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки MaxSpeedLoS.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по минимальной скорости призрака на экране.
        /// </summary>
        private async void OnMinGhostSpeedTapped(Ghost ghostItem)
        {
            try
            {
                var minGhostSpeedClause = ghostItem.MinGhostSpeedClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(minGhostSpeedClause));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки minGhostSpeedClause.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по минимальной скорости игрока на экране.
        /// </summary>
        private async void OnMinPlayerSpeedTapped()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(MainPageCommon.PlayerMinSpeedTip));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки PlayerMinSpeedTip.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку минимума минимального порога рассудка для начала охоты.
        /// </summary>
        private async void OnMinSanityHeaderTapped()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(GhostCommon.MinSanityHunt));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки MinSanityHunt.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по минимуму минимального порога рассудка для начала охоты на экране.
        /// </summary>
        private async void OnMinSanityHuntTapped(Ghost ghostItem)
        {
            try
            {
                var minSanityHuntClause = ghostItem.MinSanityHuntClause;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(minSanityHuntClause));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки minSanityHuntClause.");
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по заголовку минимальной скорости призрака на экране.
        /// </summary>
        private async void OnMinSpeedHeaderTapped()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(GhostCommon.MinSpeed));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки MinSpeed.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную некатегоризованную страницу по нажатию на неё.
        /// </summary>
        private void OnOtherPageTapped(ITitledItem otherInfoItem)
        {
            try
            {
                if (otherInfoItem is OtherInfo)
                {
                    var page = new OtherInfoPage((OtherInfo) otherInfoItem);
                    Application.Current.MainPage.Navigation.PushAsync(page);
                }
                else if (otherInfoItem is QuestCommon)
                {
                    var page = new QuestsPage();
                    Application.Current.MainPage.Navigation.PushAsync(page);
                }
                else if (otherInfoItem is ChallengeModeCommon)
                {
                    var page = new ChallengeModesPage();
                    Application.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на некатегоризированную страницу OtherInfo.");
                throw;
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
                throw;
            }
        }

        /// <summary>
        ///     Отображение всплывающей подсказки о прохождении квеста на экране.
        /// </summary>
        private async void OnQuestTapped(Quest questItem)
        {
            try
            {
                var questTip = questItem.Tip;
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(questTip));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки maxGhostSpeedClause.");
                throw;
            }
        }

        /// <summary>
        ///     Глобальный поиск среди на главной странице.
        /// </summary>
        protected override void PerformSearch()
        {
            try
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
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время выполнения глобального поиска на главной странице.");
                throw;
            }
        }
    }
}
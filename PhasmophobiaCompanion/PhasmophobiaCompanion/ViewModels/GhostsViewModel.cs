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
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для страницы списка призраков, поддерживает поиск и фильтрацию.
    /// </summary>
    public class GhostsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        private readonly DataService dataService;
        private readonly ObservableCollection<Ghost> ghosts;
        private GhostCommon ghostCommon;
        private ObservableCollection<Clue> allClues;
        private ObservableCollection<Ghost> filteredGhosts;
        private ObservableCollection<object> selectedClues;
        private ObservableCollection<object> selectedCluesSaved;
        private string searchQuery;

        public GhostsViewModel()
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                //Загрузка всех призраков и улик.
                ghosts = dataService.GetGhosts();
                allClues = dataService.GetClues();
                SelectedClues = new ObservableCollection<object>();
                selectedCluesSaved = new ObservableCollection<object>();
                Ghosts = new ObservableCollection<Ghost>(ghosts);
                AllClues = new ObservableCollection<Clue>(allClues);
                //Загрузка данных для интерфейса.
                GhostCommon = dataService.GetGhostCommon();
                // Инициализация команд
                SearchCommand = new Command<string>(OnSearchCompleted);
                GhostSelectedCommand = new Command<Ghost>(OnGhostSelected);
                FilterCommand = new Command(OnFilterTapped);
                FilterApplyCommand = new Command(OnFilterApplyTapped);
                FilterClearCommand = new Command(OnFilterClearTapped);
                BackgroundClickCommand = new Command(ExecuteBackgroundClick);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации GhostsViewModel.");
                throw;
            }
        }

        /// <summary>
        ///     Общие текстовые данные для интерфейса относящегося к призракам.
        /// </summary>
        public GhostCommon GhostCommon
        {
            get => ghostCommon;
            set
            {
                ghostCommon = value;
                OnPropertyChanged();
            }
        }
        public ICommand BackgroundClickCommand { get; private set; }
        public ICommand FilterApplyCommand { get; private set; }
        public ICommand FilterClearCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }
        public ICommand GhostSelectedCommand { get; private set; }
        public ObservableCollection<Clue> AllClues
        {
            get => allClues;
            set => SetProperty(ref allClues, value);
        }
        /// <summary>
        ///     Отображаемая коллекция призраков, которая может быть отфильтрована на основе заданных критериев.
        /// </summary>
        public ObservableCollection<Ghost> Ghosts
        {
            get => filteredGhosts;
            set => SetProperty(ref filteredGhosts, value);
        }
        public ObservableCollection<object> SelectedClues
        {
            get => selectedClues;
            set => SetProperty(ref selectedClues, value);
        }
        public string FilterColor => SelectedClues.Any() ? "Yellow" : "White";

        /// <summary>
        ///     Фильтрация списка призраков на основе выбранных улик.
        /// </summary>
        public void Filter()
        {
            try
            {
                UpdateFilteredGhosts();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время фильтрации призраков.");
                throw;
            }
        }

        public ICommand SearchCommand { get; }
        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                SetProperty(ref searchQuery, value);
                SearchGhosts();
            }
        }

        /// <summary>
        ///     Установка поискового запроса и активация поиска.
        /// </summary>
        /// <param name="query">Поисковый запрос.</param>
        public void OnSearchCompleted(string query)
        {
            try
            {
                SearchQuery = query;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время установки поискового запроса GhostsViewModel.");
                throw;
            }
        }

        /// <summary>
        ///     При нажатии на фон сбросить параметры фильтра до состояния на момента его открытия.
        /// </summary>
        private void ExecuteBackgroundClick()
        {
            try
            {
                SelectedClues = new ObservableCollection<object>(selectedCluesSaved);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при сбрасывании параметров фильтра до состояния на момент открытия.");
                throw;
            }
        }

        /// <summary>
        ///     Принятие фильтрации и закрытие окна фильтра.
        /// </summary>
        private void OnFilterApplyTapped()
        {
            try
            {
                selectedCluesSaved = new ObservableCollection<object>(SelectedClues);
                Filter();
                OnPropertyChanged(nameof(FilterColor));
                PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при принятии фильтрации.");
                throw;
            }
        }

        /// <summary>
        ///     Сброс параметров фильтрации и закрытие окна фильтрации.
        /// </summary>
        private void OnFilterClearTapped()
        {
            try
            {
                selectedCluesSaved = new ObservableCollection<object>();
                SelectedClues = new ObservableCollection<object>();
                Filter();
                OnPropertyChanged(nameof(FilterColor));
                PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при сбросе фильтрации.");
                throw;
            }
        }

        /// <summary>
        ///     Открытие страницы фильтра призраков.
        /// </summary>
        private async void OnFilterTapped()
        {
            try
            {
                // Логика для открытия страницы фильтра
                var filterPage = new FilterPage(this);
                await PopupNavigation.Instance.PushAsync(filterPage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время открытия фильтра на странице призраков GhostPage.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу выбранного призрака.
        /// </summary>
        /// <param name="selectedGhost">Выбранный призрак.</param>
        private async void OnGhostSelected(Ghost selectedGhost)
        {
            try
            {
                if (selectedGhost == null) return;
                // Логика для открытия страницы деталей призрака
                var detailPage = new GhostDetailPage(selectedGhost);
                await Application.Current.MainPage.Navigation.PushAsync(detailPage);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Ошибка во время перехода на подробную страницу призрак из страницы призраков GhostPage.");
                throw;
            }
        }

        /// <summary>
        ///     Фильтрация коллекции в соответствии с поисковым запросом.
        /// </summary>
        private void SearchGhosts()
        {
            try
            {
                UpdateFilteredGhosts();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время поиска призраков.");
                throw;
            }
        }

        /// <summary>
        ///     Фильтрация призраков по поисковому запросу и выбранным параметрам фильтра.
        /// </summary>
        private void UpdateFilteredGhosts()
        {
            try
            {
                var filtered = ghosts.Where(ghost => (string.IsNullOrWhiteSpace(SearchQuery) ||
                                                      ghost.Title.ToLowerInvariant()
                                                          .Contains(SearchQuery.ToLowerInvariant()))
                                                     &&
                                                     (!selectedCluesSaved.Any() ||
                                                      selectedCluesSaved.All(selectedClue =>
                                                          ghost.Clues.Any(clue =>
                                                              clue.Title == ((Clue) selectedClue).Title))))
                    .ToList();

                Ghosts = new ObservableCollection<Ghost>(filtered);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время обновления отфильтрованных призраков.");
                throw;
            }
        }
    }
}
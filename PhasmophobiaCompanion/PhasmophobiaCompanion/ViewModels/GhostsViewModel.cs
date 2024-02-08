using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для страницы списка призраков, поддерживает поиск и фильтрацию.
    /// </summary>
    public class GhostsViewModel : BaseViewModel, ISearchable, IFilterable
    {
        private readonly DataService _dataService;
        private readonly ObservableCollection<Ghost> ghosts;
        private GhostCommon ghostCommon;
        private ObservableCollection<Clue> allClues;
        private ObservableCollection<Clue> selectedClues;
        private ObservableCollection<Ghost> filteredGhosts;
        private string searchQuery;

        public GhostsViewModel()
        {
            try
            {
                _dataService = DependencyService.Get<DataService>();
                //Загрузка всех призраков и улик.
                ghosts = _dataService.GetGhosts();
                allClues = _dataService.GetClues();
                SelectedClues = new ObservableCollection<Clue>();
                Ghosts = new ObservableCollection<Ghost>(ghosts);
                AllClues = new ObservableCollection<Clue>(allClues);
                //Загрузка данных для интерфейса.
                GhostCommon = _dataService.GetGhostCommon();

                SearchCommand = new Command<string>(query => Search(query));
            }
            catch (Exception ex)
            {
                Console.ReadLine();
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
        public ObservableCollection<Clue> AllClues
        {
            get => allClues;
            set => SetProperty(ref allClues, value);
        }
        public ObservableCollection<Clue> SelectedClues
        {
            get => selectedClues;
            set => SetProperty(ref selectedClues, value);
        }
        /// <summary>
        ///     Отображаемая коллекция призраков, которая может быть отфильтрована на основе заданных критериев.
        /// </summary>
        public ObservableCollection<Ghost> Ghosts
        {
            get => filteredGhosts;
            set => SetProperty(ref filteredGhosts, value);
        }

        /// <summary>
        ///     Фильтрация списка призраков на основе выбранных улик.
        /// </summary>
        public void Filter()
        {
            var filtered = ghosts.Where(ghost => !SelectedClues.Any() ||
                                                 SelectedClues.All(selectedClue =>
                                                     ghost.Clues.Any(clue => clue.Title == selectedClue.Title)))
                .ToList();
            Ghosts = new ObservableCollection<Ghost>(filtered);
        }

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                SetProperty(ref searchQuery, value);
                SearchGhosts();
            }
        }
        public ICommand SearchCommand { get; set; }

        /// <summary>
        ///     Установка поискового запроса и активация поиска.
        /// </summary>
        /// <param name="query">Поисковый запрос.</param>
        public void Search(string query)
        {
            SearchQuery = query;
        }

        /// <summary>
        ///     Фильтрация коллекции в соответствии с поисковым запросом.
        /// </summary>
        private void SearchGhosts()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Ghosts = new ObservableCollection<Ghost>(ghosts);
            }
            else
            {
                // Поиск по названию призрака.
                var filtered = ghosts
                    .Where(ghost => ghost.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())).ToList();
                Ghosts = new ObservableCollection<Ghost>(filtered);
            }
        }
    }
}
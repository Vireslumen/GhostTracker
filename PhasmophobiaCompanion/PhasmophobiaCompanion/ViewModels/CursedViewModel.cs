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
    ///     ViewModel для страницы списка проклятых предметов, поддерживает поиск.
    /// </summary>
    internal class CursedViewModel : BaseViewModel, ISearchable
    {
        private readonly DataService _dataService;
        private readonly ObservableCollection<CursedPossession> curseds;
        private ObservableCollection<CursedPossession> filteredCurseds;
        private string searchQuery;

        public CursedViewModel()
        {
            _dataService = DependencyService.Get<DataService>();
            //Загрузка всех проклятых предметов.
            curseds = _dataService.GetCurseds();
            Curseds = new ObservableCollection<CursedPossession>(curseds);

            SearchCommand = new Command<string>(query => Search(query));
        }

        public ObservableCollection<CursedPossession> Curseds
        {
            get => filteredCurseds;
            set => SetProperty(ref filteredCurseds, value);
        }
        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                SetProperty(ref searchQuery, value);
                SearchCurseds();
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
        private void SearchCurseds()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Curseds = new ObservableCollection<CursedPossession>(curseds);
            }
            else
            {
                //Поиск по названию проклятого предмета.
                var filtered = curseds
                    .Where(cursed => cursed.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant())).ToList();
                Curseds = new ObservableCollection<CursedPossession>(filtered);
            }
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhasmophobiaCompanion.Interfaces;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using Serilog;
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
        private CursedPossessionCommon cursedPossessionCommon;

        public CursedViewModel()
        {
            try
            {
                _dataService = DependencyService.Get<DataService>();
                //Загрузка всех проклятых предметов.
                curseds = _dataService.GetCurseds();
                Curseds = new ObservableCollection<CursedPossession>(curseds);
                cursedPossessionCommon = _dataService.GetCursedCommon();
                SearchCommand = new Command<string>(query => Search(query));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации CursedViewModel.");
                throw;
            }
        }

        public CursedPossessionCommon CursedPossessionCommon
        {
            get => cursedPossessionCommon;
            set
            {
                cursedPossessionCommon = value;
                OnPropertyChanged();
            }
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
            try
            {
                SearchQuery = query;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время установки поискового запроса у CursedViewModel.");
                throw;
            }
        }

        /// <summary>
        ///     Фильтрация коллекции в соответствии с поисковым запросом.
        /// </summary>
        private void SearchCurseds()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchQuery))
                {
                    Curseds = new ObservableCollection<CursedPossession>(curseds);
                }
                else
                {
                    //Поиск по названию проклятого предмета.
                    var filtered = curseds
                        .Where(cursed => cursed.Title.ToLowerInvariant().Contains(SearchQuery.ToLowerInvariant()))
                        .ToList();
                    Curseds = new ObservableCollection<CursedPossession>(filtered);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время поиска проклятых предметов.");
                throw;
            }
        }
    }
}
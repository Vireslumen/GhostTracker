using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для страницы списка проклятых предметов, поддерживает поиск.
    /// </summary>
    internal class CursedViewModel : SearchableViewModel
    {
        private readonly DataService dataService;
        private readonly List<CursedPossession> curseds;
        private CursedPossessionCommon cursedPossessionCommon;
        private ObservableCollection<CursedPossession> filteredCurseds;

        public CursedViewModel()
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                //Загрузка всех проклятых предметов.
                curseds = dataService.GetCurseds().OrderBy(c => c.Title).ToList(); ;
                Curseds = new ObservableCollection<CursedPossession>(curseds);
                cursedPossessionCommon = dataService.GetCursedCommon();
                // Инициализация команд
                CursedSelectedCommand = new Command<CursedPossession>(OnCursedSelected);
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
        public ICommand CursedSelectedCommand { get; private set; }
        public ObservableCollection<CursedPossession> Curseds
        {
            get => filteredCurseds;
            set => SetProperty(ref filteredCurseds, value);
        }

        /// <summary>
        ///     Переход на подробную страницу выбранного проклятого предмета.
        /// </summary>
        /// <param name="selectedCursed">Выбранный проклятый предмет.</param>
        private async void OnCursedSelected(CursedPossession selectedCursed)
        {
            try
            {
                if (selectedCursed != null)
                {
                    // Логика для открытия страницы деталей проклятого предмета
                    var detailPage = new CursedDetailPage(selectedCursed);
                    await Application.Current.MainPage.Navigation.PushAsync(detailPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Ошибка во время перехода на подробную страницу проклятого предмета из страницы проклятых предметов CursedsPage.");
                throw;
            }
        }

        /// <summary>
        ///     Фильтрация коллекции в соответствии с поисковым запросом.
        /// </summary>
        protected override void PerformSearch()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    Curseds = new ObservableCollection<CursedPossession>(curseds);
                }
                else
                {
                    //Поиск по названию проклятого предмета.
                    var filtered = curseds
                        .Where(cursed => cursed.Title.ToLowerInvariant().Contains(SearchText.ToLowerInvariant()))
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
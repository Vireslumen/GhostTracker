using System;
using System.Windows.Input;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Views;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы призрака.
    /// </summary>
    public class GhostDetailViewModel : BaseViewModel
    {
        private Ghost ghost;

        public GhostDetailViewModel(Ghost ghost)
        {
            try
            {
                Ghost = ghost;
                ClueSelectedCommand = new Command<Clue>(OnClueSelected);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации GhostDetailViewModel.");
                throw;
            }
        }

        public Ghost Ghost
        {
            get => ghost;
            set
            {
                ghost = value;
                OnPropertyChanged();
            }
        }
        public ICommand ClueSelectedCommand { get; private set; }

        /// <summary>
        ///     Переход на подробную страницу улики.
        /// </summary>
        /// <param name="clue">Выбранная улика</param>
        private async void OnClueSelected(Clue clue)
        {
            try
            {
                if (clue == null) return;
                // Логика для открытия страницы деталей призрака
                var detailPage = new ClueDetailPage(clue);
                await Application.Current.MainPage.Navigation.PushAsync(detailPage);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Ошибка во время перехода на подробную страницу улики из подробной страницы призраков GhostDetailPage.");
                throw;
            }
        }
    }
}
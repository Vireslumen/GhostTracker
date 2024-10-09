using System;
using System.Windows.Input;
using GhostTracker.Models;
using GhostTracker.Views;
using Serilog;
using Xamarin.Forms;

namespace GhostTracker.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы призрака.
    /// </summary>
    public class GhostDetailViewModel : UnfoldingItemsViewModel
    {
        private Ghost ghost;

        public GhostDetailViewModel(Ghost ghost)
        {
            Ghost = ghost;
            foreach (var item in Ghost.UnfoldingItems) item.IsExpanded = true;
            ClueSelectedCommand = new Command<Clue>(OnClueSelected);
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

        public void Cleanup()
        {
            ToggleExpandCommand = null;
            ClueSelectedCommand = null;
        }

        /// <summary>
        ///     Переход на подробную страницу доказательства.
        /// </summary>
        /// <param name="clue">Выбранное доказательство</param>
        private async void OnClueSelected(Clue clue)
        {
            try
            {
                if (IsNavigating) return;
                if (clue == null) return;
                // Логика для открытия страницы деталей призрака
                var detailPage = new ClueDetailPage(clue);
                await NavigateWithLoadingAsync(detailPage);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Ошибка во время перехода на подробную страницу доказательства из подробной страницы призраков GhostDetailPage.");
            }
        }
    }
}
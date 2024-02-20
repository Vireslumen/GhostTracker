using System;
using PhasmophobiaCompanion.Models;
using Serilog;

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
    }
}
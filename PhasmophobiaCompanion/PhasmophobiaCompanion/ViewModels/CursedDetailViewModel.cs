using System;
using PhasmophobiaCompanion.Models;
using Serilog;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы проклятого предмета.
    /// </summary>
    public class CursedDetailViewModel : BaseViewModel
    {
        private CursedPossession cursedPossession;

        public CursedDetailViewModel(CursedPossession cursedPossession)
        {
            try
            {
                CursedPossession = cursedPossession;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации CursedDetailViewModel.");
                throw;
            }
        }

        public CursedPossession CursedPossession
        {
            get => cursedPossession;
            set
            {
                cursedPossession = value;
                OnPropertyChanged();
            }
        }
    }
}
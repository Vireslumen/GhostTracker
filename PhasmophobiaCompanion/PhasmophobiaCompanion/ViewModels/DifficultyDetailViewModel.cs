using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы сложности.
    /// </summary>
    public class DifficultyDetailViewModel : BaseViewModel
    {
        public readonly DataService dataService;
        private Difficulty difficulty;
        private DifficultyCommon difficultyCommon;

        public DifficultyDetailViewModel(Difficulty difficulty)
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                DifficultyCommon = dataService.GetDifficultyCommon();
                Difficulty = difficulty;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации CursedDetailViewModel.");
                throw;
            }
        }

        public Difficulty Difficulty
        {
            get => difficulty;
            set
            {
                difficulty = value;
                OnPropertyChanged();
            }
        }
        public DifficultyCommon DifficultyCommon
        {
            get => difficultyCommon;
            set
            {
                difficultyCommon = value;
                OnPropertyChanged();
            }
        }
    }
}
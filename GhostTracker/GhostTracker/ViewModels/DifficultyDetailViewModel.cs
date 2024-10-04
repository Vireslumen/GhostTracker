using GhostTracker.Models;
using GhostTracker.Services;
using Xamarin.Forms;

namespace GhostTracker.ViewModels
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
            dataService = DependencyService.Get<DataService>();
            DifficultyCommon = dataService.GetDifficultyCommon();
            Difficulty = difficulty;
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
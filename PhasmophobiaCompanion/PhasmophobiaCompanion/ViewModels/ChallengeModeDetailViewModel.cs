using System;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы Особого режима.
    /// </summary>
    public class ChallengeModeDetailViewModel : BaseViewModel
    {
        public readonly DataService dataService;
        private ChallengeMode challengeMode;
        private DifficultyCommon difficultyCommon;

        public ChallengeModeDetailViewModel(ChallengeMode challengeMode)
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                DifficultyCommon = dataService.GetDifficultyCommon();
                ChallengeMode = challengeMode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации ChallengeModeDetailViewModel.");
                throw;
            }
        }

        public ChallengeMode ChallengeMode
        {
            get => challengeMode;
            set
            {
                challengeMode = value;
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
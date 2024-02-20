using System;
using PhasmophobiaCompanion.Models;
using Serilog;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы Особого режима.
    /// </summary>
    public class ChallengeModeDetailViewModel : BaseViewModel
    {
        private ChallengeMode challengeMode;

        public ChallengeModeDetailViewModel(ChallengeMode challengeMode)
        {
            try
            {
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
    }
}
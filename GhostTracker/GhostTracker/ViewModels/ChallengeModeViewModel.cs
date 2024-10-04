using System;
using System.Collections.Generic;
using System.Windows.Input;
using GhostTracker.Models;
using GhostTracker.Services;
using GhostTracker.Views;
using Serilog;
using Xamarin.Forms;

namespace GhostTracker.ViewModels
{
    /// <summary>
    ///     ViewModel для страницы списка особых режимов.
    /// </summary>
    public class ChallengeModeViewModel : BaseViewModel
    {
        private readonly DataService dataService;
        private ChallengeModeCommon challengeModeCommon;
        private List<ChallengeMode> challengeModes;

        public ChallengeModeViewModel()
        {
            dataService = DependencyService.Get<DataService>();
            ChallengeModes = dataService.GetChallengeModes();
            ChallengeModeCommon = dataService.GetChallengeModeCommon();
            ChallengeModeTappedCommand = new Command<ChallengeMode>(OnChallengeModeTapped);
        }

        public ChallengeModeCommon ChallengeModeCommon
        {
            get => challengeModeCommon;
            set => SetProperty(ref challengeModeCommon, value);
        }
        public ICommand ChallengeModeTappedCommand { get; private set; }
        public List<ChallengeMode> ChallengeModes
        {
            get => challengeModes;
            set => SetProperty(ref challengeModes, value);
        }

        public void Cleanup()
        {
            ChallengeModeTappedCommand = null;
        }

        /// <summary>
        ///     Переход на подробную страницу особого режима по нажатию на него.
        /// </summary>
        private async void OnChallengeModeTapped(ChallengeMode challengeMode)
        {
            try
            {
                if (IsNavigating) return;
                if (dataService.IsMapsDataLoaded && dataService.IsEquipmentsDataLoaded)
                {
                    var page = new ChallengeModeDetailPage(challengeMode);
                    await NavigateWithLoadingAsync(page);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу особого режима ChallengeModeDetailPage.");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    public class ChallengeModeViewModel : BaseViewModel
    {
        private readonly DataService dataService;
        private ChallengeModeCommon challengeModeCommon;
        private List<ChallengeMode> challengeModes;

        public ChallengeModeViewModel()
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                ChallengeModes = dataService.GetChallengeModes();
                ChallengeModeCommon = dataService.GetChallengeModeCommon();
                ChallengeModeTappedCommand = new Command<ChallengeMode>(OnChallengeModeTapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации QuestsViewModel.");
                throw;
            }
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

        /// <summary>
        ///     Переход на подробную страницу особого режима по нажатию на него.
        /// </summary>
        private void OnChallengeModeTapped(ChallengeMode challengeMode)
        {
            try
            {
                if (dataService.IsMapsDataLoaded && dataService.IsEquipmentsDataLoaded)
                {
                    var page = new ChallengeModeDetailPage(challengeMode);
                    Application.Current.MainPage.Navigation.PushAsync(page);
                }
                // TODO: Сделать, чтобы если Карты не были загружены, какую-нибудь загрузки или что-нибудь в этом духе.
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу особого режима ChallengeModeDetailPage.");
                throw;
            }
        }
    }
}
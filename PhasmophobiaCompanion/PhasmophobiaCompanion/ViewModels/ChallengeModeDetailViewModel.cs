using System;
using System.Windows.Input;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
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
                EquipmentSelectedCommand = new Command<Equipment>(OnEquipmentSelected);
                MapSelectedCommand = new Command(OnMapSelected);
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
        public ICommand EquipmentSelectedCommand { get; private set; }
        public ICommand MapSelectedCommand { get; private set; }

        /// <summary>
        ///     Переход на подробную страницу выбранного снаряжения.
        /// </summary>
        /// <param name="selectedEquipment">Выбранное снаряжение.</param>
        private async void OnEquipmentSelected(Equipment selectedEquipment)
        {
            try
            {
                if (selectedEquipment != null)
                {
                    // Логика для открытия страницы деталей снаряжения
                    var detailPage = new EquipmentDetailPage(selectedEquipment);
                    await Application.Current.MainPage.Navigation.PushAsync(detailPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Ошибка во время перехода на подробную страницу снаряжения из страницы особого режима ChallengeModeDetailPage.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на подробную страницу выбранной карты.
        /// </summary>
        private async void OnMapSelected()
        {
            try
            {
                if (ChallengeMode.ChallengeMap != null)
                {
                    // Логика для открытия страницы деталей карты
                    var detailPage = new MapDetailPage(ChallengeMode.ChallengeMap);
                    await Application.Current.MainPage.Navigation.PushAsync(detailPage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "Ошибка во время перехода на подробную страницу карты из страницы особого режима ChallengeModeDetailPage.");
                throw;
            }
        }
    }
}
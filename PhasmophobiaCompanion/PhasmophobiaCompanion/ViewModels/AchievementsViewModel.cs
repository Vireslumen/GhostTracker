using System;
using System.Collections.Generic;
using System.Windows.Input;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    public class AchievementsViewModel : BaseViewModel
    {
        private readonly DataService dataService;
        private AchievementCommon achievementCommon;
        private List<Achievement> achievements;

        public AchievementsViewModel()
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                Achievements = dataService.GetAchievements();
                AchievementCommon = dataService.GetAchievementCommon();
                ChallengeModeTappedCommand = new Command<Achievement>(OnAchievementTapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации QuestsViewModel.");
                throw;
            }
        }

        public AchievementCommon AchievementCommon
        {
            get => achievementCommon;
            set => SetProperty(ref achievementCommon, value);
        }
        public ICommand ChallengeModeTappedCommand { get; private set; }
        public List<Achievement> Achievements
        {
            get => achievements;
            set => SetProperty(ref achievements, value);
        }

        /// <summary>
        ///     Отображение всплывающей подсказки по получению достижения.
        /// </summary>
        private async void OnAchievementTapped(Achievement achievement)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new TooltipPopup(achievement.Tip));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время отображение всплывающей подсказки о получении достижения.");
                throw;
            }
        }
    }
}
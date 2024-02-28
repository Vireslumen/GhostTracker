using System;
using System.Collections.ObjectModel;
using System.Linq;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    public class QuestsViewModel : BaseViewModel
    {
        private readonly DataService dataService;
        private ObservableCollection<Quest> dailyQuests;
        private ObservableCollection<Quest> quests;
        private ObservableCollection<Quest> weeklyQuests;
        private QuestCommon questCommon;

        public QuestsViewModel()
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                Quests = dataService.GetQuests();
                QuestCommon = dataService.GetQuestCommon();
                SetDailyQuests();
                SetWeeklyQuests();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации QuestsViewModel.");
                throw;
            }
        }

        public ObservableCollection<Quest> DailyQuests
        {
            get => dailyQuests;
            set => SetProperty(ref dailyQuests, value);
        }
        public ObservableCollection<Quest> Quests
        {
            get => quests;
            set => SetProperty(ref quests, value);
        }
        public ObservableCollection<Quest> WeeklyQuests
        {
            get => weeklyQuests;
            set => SetProperty(ref weeklyQuests, value);
        }
        public QuestCommon QuestCommon
        {
            get => questCommon;
            set => SetProperty(ref questCommon, value);
        }

        private void SetDailyQuests()
        {
            try
            {
                var daily = Quests.Where(q => q.Type == QuestCommon.Daily).ToList();
                DailyQuests = new ObservableCollection<Quest>(daily);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время установки DailyQuests.");
                throw;
            }
        }

        private void SetWeeklyQuests()
        {
            try
            {
                var weekly = Quests.Where(q => q.Type == QuestCommon.Weekly).ToList();
                WeeklyQuests = new ObservableCollection<Quest>(weekly);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время установки WeeklyQuests.");
                throw;
            }
        }
    }
}
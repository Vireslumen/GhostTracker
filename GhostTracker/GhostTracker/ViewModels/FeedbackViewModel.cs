using System;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using GhostTracker.Models;
using GhostTracker.Services;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Forms;

namespace GhostTracker.ViewModels
{
    /// <summary>
    ///     ViewModel для страницы Фидбэка.
    /// </summary>
    public class FeedbackViewModel : BindableObject
    {
        private FeedbackCommon feedbackCommon;
        private string feedbackText;

        public FeedbackViewModel()
        {
            var dataService = DependencyService.Get<DataService>();
            FeedbackCommon = dataService.GetFeedbackCommon();
            SubmitCommand = new Command(ExecuteSubmit);
            CancelCommand = new Command(Exit);
        }

        public FeedbackCommon FeedbackCommon
        {
            get => feedbackCommon;
            set
            {
                feedbackCommon = value;
                OnPropertyChanged();
            }
        }
        public ICommand CancelCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public string FeedbackText
        {
            get => feedbackText;
            set
            {
                feedbackText = value;
                OnPropertyChanged();
            }
        }

        public void Cleanup()
        {
            SubmitCommand = null;
            CancelCommand = null;
        }

        private async void ExecuteSubmit()
        {
            try
            {
                var json = JsonConvert.SerializeObject(FeedbackText);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var client = new HttpClient();
                // Укажите URL вашего API
                var response = await client.PostAsync("https://a28577-767d.u.d-f.pw/Feedback", content);

                if (response.IsSuccessStatusCode)
                {
                    await PopupNavigation.Instance.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при отправке фидбэка.");
            }
        }

        private static async void Exit()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
using System.Diagnostics;
using System.Windows.Input;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для страницы Фидбэка.
    /// </summary>
    public class FeedbackViewModel : BindableObject
    {
        private readonly DataService dataService;
        private FeedbackCommon feedbackCommon;
        private string feedbackText;
        private string sourcePage;

        public FeedbackViewModel()
        {
            dataService = DependencyService.Get<DataService>();
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
        public string SourcePage
        {
            get => sourcePage;
            set
            {
                sourcePage = value;
                OnPropertyChanged();
            }
        }

        public void Cleanup()
        {
            SubmitCommand = null;
            CancelCommand = null;
        }

        private void ExecuteSubmit()
        {
            //TODO: Сделать отправку на сервер.
            Debug.WriteLine($"Feedback from {SourcePage}: {FeedbackText}");
            PopupNavigation.Instance.PopAsync();
        }

        private void Exit()
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
using System;
using System.Windows.Input;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Views;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     ViewModel для подробной страницы улики.
    /// </summary>
    public class ClueDetailViewModel : BaseViewModel
    {
        public ICommand ImageTappedCommand;
        private Clue clue;

        public ClueDetailViewModel(Clue clue)
        {
            try
            {
                Clue = clue;
                ClueSelectedCommand = new Command<Clue>(OnClueSelected);
                GhostSelectedCommand = new Command<Ghost>(OnGhostSelected);
                ImageTappedCommand = new Command<ImageWithDescription>(OpenImagePage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации ClueDetailViewModel.");
                throw;
            }
        }

        public Clue Clue
        {
            get => clue;
            set
            {
                clue = value;
                OnPropertyChanged();
            }
        }
        public ICommand ClueSelectedCommand { get; }
        public ICommand GhostSelectedCommand { get; }

        /// <summary>
        ///     Переход на страницу улики при нажатии на неё.
        /// </summary>
        private void OnClueSelected(Clue clueItem)
        {
            try
            {
                var page = new ClueDetailPage(clueItem);
                Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу улики с подробной страницы улики.");
                throw;
            }
        }

        /// <summary>
        ///     Переход на страницу призрака при нажатии на него.
        /// </summary>
        private void OnGhostSelected(Ghost ghostItem)
        {
            try
            {
                var page = new GhostDetailPage(ghostItem);
                Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу призрака с подробной страницы улики.");
                throw;
            }
        }

        private async void OpenImagePage(ImageWithDescription image)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ImagePage(image));
        }
    }
}
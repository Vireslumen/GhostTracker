using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
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
        public readonly DataService dataService;
        public ICommand ImageTappedCommand;
        private Clue clue;
        private ClueCommon clueCommon;

        public ClueDetailViewModel(Clue clue)
        {
            try
            {
                dataService = DependencyService.Get<DataService>();
                Clue = clue;
                ClueCommon = dataService.GetClueCommon();
                Clue.ClueRelatedEquipments = new List<Equipment>
                (dataService.GetEquipments().Where(e => Clue.EquipmentsID.Contains(e.ID))
                    .ToList());
                ClueSelectedCommand = new Command<Clue>(OnClueSelected);
                GhostSelectedCommand = new Command<Ghost>(OnGhostSelected);
                ImageTappedCommand = new Command<ImageWithDescription>(OpenImagePage);
                EquipmentSelectedCommand = new Command<Equipment>(OpenEquipPage);
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
        public ClueCommon ClueCommon
        {
            get => clueCommon;
            set
            {
                clueCommon = value;
                OnPropertyChanged();
            }
        }
        public ICommand ClueSelectedCommand { get; }
        public ICommand EquipmentSelectedCommand { get; }
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

        /// <summary>
        ///     Переход на страницу связанного с уликой снаряжения при нажатии на него.
        /// </summary>
        private void OpenEquipPage(Equipment eqipItem)
        {
            try
            {
                var page = new EquipmentDetailPage(eqipItem);
                Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на страницу снаряжения с подробной страницы улики.");
                throw;
            }
        }

        private async void OpenImagePage(ImageWithDescription image)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ImagePage(image));
        }
    }
}
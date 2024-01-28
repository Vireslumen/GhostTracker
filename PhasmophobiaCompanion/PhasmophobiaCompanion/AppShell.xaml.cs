using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.ViewModels;
using PhasmophobiaCompanion.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhasmophobiaCompanion
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private DataService _dataService;

        private bool isShowingLoadingScreen = false;
        public AppShell()
        {
            InitializeComponent();
            this.CurrentItem = mainTab;
            Shell.SetNavBarIsVisible(this, false);
            _dataService = DependencyService.Get<DataService>();
            this.Navigating += OnShellNavigation;
        }

        private async void OnShellNavigation(object sender, ShellNavigatingEventArgs e)
        {
            if (e.Target.Location.OriginalString.Contains("ghostsTab") && !_dataService.IsGhostsDataLoaded)
            {
                // Subscribe to an event or notification that data loading is complete
                _dataService.GhostsDataLoaded += OnGhostsDataLoaded;

                // Cancel the navigation if data is not loaded
                e.Cancel();

                // Display a loading screen here
                ShowLoadingScreen();
            }
            else if (e.Target.Location.OriginalString.Contains("equipmentsTab") && !_dataService.IsEquipmentsDataLoaded)
            {
                // Subscribe to an event or notification that data loading is complete
                _dataService.EquipmentsDataLoaded += OnEquipmentsDataLoaded;

                // Cancel the navigation if data is not loaded
                e.Cancel();

                // Display a loading screen here
                ShowLoadingScreen();
            }
            else if (e.Target.Location.OriginalString.Contains("mapsTab") && !_dataService.IsMapsDataLoaded)
            {                // Subscribe to an event or notification that data loading is complete
                _dataService.MapsDataLoaded += OnMapsDataLoaded;

                // Cancel the navigation if data is not loaded
                e.Cancel();

                // Display a loading screen here
                ShowLoadingScreen();
            }
            else if (e.Target.Location.OriginalString.Contains("cursedsTab") && !_dataService.IsCursedsDataLoaded)
            {                // Subscribe to an event or notification that data loading is complete
                _dataService.CursedsDataLoaded += OnCursedsDataLoaded;

                // Cancel the navigation if data is not loaded
                e.Cancel();

                // Display a loading screen here
                ShowLoadingScreen();
            }
        }

        private void OnCursedsDataLoaded()
        {
            // Hide the loading screen
            HideLoadingScreen();

            // Unsubscribe from the event
            _dataService.CursedsDataLoaded -= OnCursedsDataLoaded;

            // Navigate to ghostPage now that data is loaded
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("cursedsTab");
            });
        }

        private void OnMapsDataLoaded()
        {
            // Hide the loading screen
            HideLoadingScreen();

            // Unsubscribe from the event
            _dataService.MapsDataLoaded -= OnMapsDataLoaded;

            // Navigate to ghostPage now that data is loaded
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("mapsTab");
            });
        }

        private void OnEquipmentsDataLoaded()
        {
            // Hide the loading screen
            HideLoadingScreen();

            // Unsubscribe from the event
            _dataService.EquipmentsDataLoaded -= OnEquipmentsDataLoaded;

            // Navigate to ghostPage now that data is loaded
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("equipmentsTab");
            });
        }

        private void OnGhostsDataLoaded()
        {
            // Hide the loading screen
            HideLoadingScreen();

            // Unsubscribe from the event
            _dataService.GhostsDataLoaded -= OnGhostsDataLoaded;

            // Navigate to ghostPage now that data is loaded
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync("ghostsTab");
            });
        }

        private async void ShowLoadingScreen()
        {
            if (!isShowingLoadingScreen)
            {
                isShowingLoadingScreen = true;
                await Shell.Current.Navigation.PushModalAsync(new LoadingScreenPage(), true);
            }
        }

        private async void HideLoadingScreen()
        {
            if (isShowingLoadingScreen)
            {
                await Shell.Current.Navigation.PopModalAsync(true);
                isShowingLoadingScreen = false;
            }
        }

    }
}

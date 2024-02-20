using System;
using PhasmophobiaCompanion.Services;
using PhasmophobiaCompanion.Views;
using Serilog;
using Xamarin.Forms;

namespace PhasmophobiaCompanion
{
    public partial class AppShell : Shell
    {
        private readonly DataService dataService;
        private bool isShowingLoadingScreen;

        public AppShell()
        {
            try
            {
                InitializeComponent();
                CurrentItem = mainTab;
                SetNavBarIsVisible(this, false);
                dataService = DependencyService.Get<DataService>();
                Navigating += OnShellNavigation;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации оболочки вкладок.");
                throw;
            }
        }

        private async void HideLoadingScreen()
        {
            try
            {
                if (isShowingLoadingScreen)
                {
                    await Current.Navigation.PopModalAsync(true);
                    isShowingLoadingScreen = false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время скрытия загрузочного экрана.");
                throw;
            }
        }

        /// <summary>
        ///     Метод перехода на вкладку проклятых предметов - CursedsPage.
        /// </summary>
        private void OnCursedsDataLoaded()
        {
            try
            {
                HideLoadingScreen();
                dataService.CursedsDataLoaded -= OnCursedsDataLoaded;
                Device.BeginInvokeOnMainThread(async () => { await Current.GoToAsync("cursedsTab"); });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки вкладки проклятых предметов.");
                throw;
            }
        }

        /// <summary>
        ///     Метод перехода на вкладку снаряжения - EquipmentPage.
        /// </summary>
        private void OnEquipmentsDataLoaded()
        {
            try
            {
                HideLoadingScreen();
                dataService.EquipmentsDataLoaded -= OnEquipmentsDataLoaded;
                Device.BeginInvokeOnMainThread(async () => { await Current.GoToAsync("equipmentsTab"); });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки вкладки снаряжения.");
                throw;
            }
        }

        /// <summary>
        ///     Метод перехода на вкладку призраков - GhostPage.
        /// </summary>
        private void OnGhostsDataLoaded()
        {
            try
            {
                HideLoadingScreen();
                dataService.GhostsDataLoaded -= OnGhostsDataLoaded;
                Device.BeginInvokeOnMainThread(async () => { await Current.GoToAsync("ghostsTab"); });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки вкладки призраков.");
                throw;
            }
        }

        /// <summary>
        ///     Метод перехода на вкладку карт - MapsPage.
        /// </summary>
        private void OnMapsDataLoaded()
        {
            try
            {
                HideLoadingScreen();
                dataService.MapsDataLoaded -= OnMapsDataLoaded;
                Device.BeginInvokeOnMainThread(async () => { await Current.GoToAsync("mapsTab"); });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время загрузки вкладки карт.");
                throw;
            }
        }

        private async void OnShellNavigation(object sender, ShellNavigatingEventArgs e)
        {
            try
            {
                if (e.Target.Location.OriginalString.Contains("ghostsTab") && !dataService.IsGhostsDataLoaded)
                {
                    dataService.GhostsDataLoaded += OnGhostsDataLoaded;
                    e.Cancel();
                    ShowLoadingScreen();
                }
                else if (e.Target.Location.OriginalString.Contains("equipmentsTab") &&
                         !dataService.IsEquipmentsDataLoaded)
                {
                    dataService.EquipmentsDataLoaded += OnEquipmentsDataLoaded;
                    e.Cancel();
                    ShowLoadingScreen();
                }
                else if (e.Target.Location.OriginalString.Contains("mapsTab") && !dataService.IsMapsDataLoaded)
                {
                    dataService.MapsDataLoaded += OnMapsDataLoaded;
                    e.Cancel();
                    ShowLoadingScreen();
                }
                else if (e.Target.Location.OriginalString.Contains("cursedsTab") && !dataService.IsCursedsDataLoaded)
                {
                    dataService.CursedsDataLoaded += OnCursedsDataLoaded;
                    e.Cancel();
                    ShowLoadingScreen();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время перехода на другую вкладку.");
                throw;
            }
        }

        private async void ShowLoadingScreen()
        {
            try
            {
                if (!isShowingLoadingScreen)
                {
                    isShowingLoadingScreen = true;
                    await Current.Navigation.PushModalAsync(new LoadingScreenPage(), true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время показа загрузочного экрана.");
                throw;
            }
        }
    }
}
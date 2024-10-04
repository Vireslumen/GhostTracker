using System;
using GhostTracker.Services;
using GhostTracker.Views;
using Rg.Plugins.Popup.Services;
using Serilog;
using Xamarin.Forms;

namespace GhostTracker
{
    /// <summary>
    ///     Основной класс оболочки приложения, отвечающий за навигацию и обработку событий приложения.
    /// </summary>
    public partial class AppShell
    {
        private readonly DataService dataService;
        private readonly ShakeDetectorService shakeDetector;
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
                BindingContext = dataService.GetAppShellCommon();

                // Инициализация обработки тряски
                shakeDetector = new ShakeDetectorService();
                shakeDetector.ShakeDetected += OnShakeDetected;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время инициализации оболочки вкладок.");
            }
        }

        public bool IsFeedbackPopupOpen { get; set; }

        public void StopShakeDetector()
        {
            shakeDetector.Stop();
        }

        private async void HideLoadingScreen()
        {
            if (!isShowingLoadingScreen) return;
            await Current.Navigation.PopModalAsync(true);
            isShowingLoadingScreen = false;
        }

        /// <summary>
        ///     Выполнение навигации на указанную страницу.
        /// </summary>
        /// <param name="page">Имя страницы для перехода.</param>
        private static async void NavigateToPageAsync(string page)
        {
            try
            {
                await Current.GoToAsync(page);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Ошибка во время навигации на страницу {page}.");
            }
        }

        /// <summary>
        ///     Метод перехода на вкладку проклятых предметов - CursedsPage.
        /// </summary>
        private void OnCursedsDataLoaded()
        {
            HideLoadingScreen();
            dataService.CursedsDataLoaded -= OnCursedsDataLoaded;
            Device.BeginInvokeOnMainThread(() => NavigateToPageAsync("cursedsTab"));
        }

        /// <summary>
        ///     Метод перехода на вкладку снаряжения - EquipmentPage.
        /// </summary>
        private void OnEquipmentsDataLoaded()
        {
            HideLoadingScreen();
            dataService.EquipmentsDataLoaded -= OnEquipmentsDataLoaded;
            Device.BeginInvokeOnMainThread(() => NavigateToPageAsync("equipmentsTab"));
        }

        /// <summary>
        ///     Метод перехода на вкладку призраков - GhostPage.
        /// </summary>
        private void OnGhostsDataLoaded()
        {
            HideLoadingScreen();
            dataService.GhostsDataLoaded -= OnGhostsDataLoaded;
            Device.BeginInvokeOnMainThread(() => NavigateToPageAsync("ghostsTab"));
        }

        /// <summary>
        ///     Метод перехода на вкладку карт - MapsPage.
        /// </summary>
        private void OnMapsDataLoaded()
        {
            HideLoadingScreen();
            dataService.MapsDataLoaded -= OnMapsDataLoaded;
            Device.BeginInvokeOnMainThread(() => NavigateToPageAsync("mapsTab"));
        }

        /// <summary>
        ///     Переход на страницу отправки фидбэка.
        /// </summary>
        private async void OnShakeDetected(object sender, EventArgs e)
        {
            try
            {
                if (IsFeedbackPopupOpen || !dataService.GetShakeActive()) return;
                IsFeedbackPopupOpen = true;
                await PopupNavigation.Instance.PushAsync(new FeedbackPopupPage());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка во время открытия страницы обратной связи.");
            }
        }

        /// <summary>
        ///     Обработчик события навигации внутри оболочки.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события навигации.</param>
        private void OnShellNavigation(object sender, ShellNavigatingEventArgs e)
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
            }
        }

        /// <summary>
        ///     Показ экрана загрузки.
        /// </summary>
        private async void ShowLoadingScreen()
        {
            if (isShowingLoadingScreen) return;
            isShowingLoadingScreen = true;
            await Current.Navigation.PushModalAsync(new LoadingScreenPage(), true);
        }
    }
}